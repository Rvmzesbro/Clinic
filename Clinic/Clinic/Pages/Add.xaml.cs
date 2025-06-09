using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clinic.Pages
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Reception Reception { get; set; }
        public Add(Reception reception)
        {
            InitializeComponent();
            LoadData();
            SelectedReception(reception);
            Bindings();
            DataContext = this;
            Reception = reception;
        }

        private void SelectedReception(Reception reception)
        {

        }

        private void Bindings()
        {
            AppointmentsDataGrid.ItemsSource = App.db.Reception.ToList();
        }

        private void LoadData()
        {
            // Загрузка специальностей
            SpecialitiesComboBox.ItemsSource = App.db.Specialitys.ToList();

            // Загрузка полов
            GenderComboBox.ItemsSource = App.db.Genders.ToList();

            // Загрузка существующих записей
        }


        private void SpecialitiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpecialitiesComboBox.SelectedItem is Specialitys selectedSpeciality)
            {
                // Загрузка врачей выбранной специальности
                DoctorsComboBox.ItemsSource = App.db.Doctors
                    .Where(d => d.IdSpeciality == selectedSpeciality.Id)
                    .ToList();
                DoctorsComboBox.SelectedItem = Reception.Doctors;
                TimeSlotsListBox.ItemsSource = null;
            }
        }

        private void DoctorsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoctorsComboBox.SelectedItem != null && AppointmentDatePicker.SelectedDate.HasValue)
            {
                UpdateAvailableTimeSlots();
            }
        }

        private void AppointmentDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoctorsComboBox.SelectedItem != null && AppointmentDatePicker.SelectedDate.HasValue)
            {
                UpdateAvailableTimeSlots();
            }
        }

        private void UpdateAvailableTimeSlots()
        {
            if (DoctorsComboBox.SelectedItem == null || !AppointmentDatePicker.SelectedDate.HasValue)
                return;

            dynamic selectedDoctor = DoctorsComboBox.SelectedItem;
            int doctorId = selectedDoctor.Id;
            DateTime selectedDate = AppointmentDatePicker.SelectedDate.Value;

            // Получаем расписание врача
            var schedules = App.db.DoctorsShedule
                .Where(s => s.IdDoctor == doctorId)
                .ToList();

            // Получаем уже занятые временные слоты (используем DbFunctions для извлечения времени)
            var bookedSlots = App.db.Reception
                .Where(r => r.IdDoctor == doctorId &&
                           DbFunctions.TruncateTime(r.TimeNote) == selectedDate.Date)
                .ToList()
                .Select(r => r.TimeNote.TimeOfDay) // Теперь преобразуем в TimeOfDay после получения данных
                .ToList();

            // Генерируем доступные временные слоты
            var availableSlots = new List<DateTime>();

            foreach (var schedule in schedules)
            {
                TimeSpan start = schedule.BeginingDate;
                TimeSpan end = schedule.EndDate;

                // Добавляем слоты каждые 30 минут
                for (TimeSpan slot = start; slot < end; slot = slot.Add(TimeSpan.FromMinutes(30)))
                {
                    // Проверяем, не занят ли слот
                    if (!bookedSlots.Contains(slot))
                    {
                        availableSlots.Add(new DateTime(Reception.TimeNote.Year, Reception.TimeNote.Month, Reception.TimeNote.Day, slot.Hours, slot.Minutes, slot.Seconds));
                    }
                }
            }

            // Отображаем доступные слоты
            TimeSlotsListBox.ItemsSource = availableSlots
                .OrderBy(t => t)
                ;
        }

        private void BookAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка выбора врача и времени
                if (DoctorsComboBox.SelectedItem == null || TimeSlotsListBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите врача и время приема!");
                    return;
                }

                // Проверка данных пациента
                if (string.IsNullOrWhiteSpace(PatientSurnameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(PatientNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(PatientPatronymicTextBox.Text) ||
                    string.IsNullOrWhiteSpace(PatientPhoneTextBox.Text) ||
                    !PatientBirthDatePicker.SelectedDate.HasValue ||
                    GenderComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Заполните все данные пациента!");
                    return;
                }

                dynamic selectedDoctor = DoctorsComboBox.SelectedItem;
                int doctorId = selectedDoctor.Id;
                DateTime selectedDate = AppointmentDatePicker.SelectedDate.Value;

                // Парсим выбранное время с точностью до минуты
                string[] timeParts = TimeSlotsListBox.SelectedItem.ToString().Split(':');
                if (timeParts.Length != 2 || !int.TryParse(timeParts[0], out int hours) ||
                    !int.TryParse(timeParts[1], out int minutes))
                {
                    MessageBox.Show("Неверный формат времени!");
                    return;
                }

                DateTime appointmentDateTime = selectedDate.Date.Add(new TimeSpan(hours, minutes, 0));

                // Точная проверка доступности времени
                bool isSlotAvailable = !App.db.Reception
                    .AsNoTracking()
                    .Any(r => r.IdDoctor == doctorId &&
                             EntityFunctions.TruncateTime(r.TimeNote) == selectedDate.Date &&
                             r.TimeNote.Hour == hours &&
                             r.TimeNote.Minute == minutes);

                if (!isSlotAvailable)
                {
                    MessageBox.Show("Выбранное время уже занято. Пожалуйста, выберите другое время.");
                    // Обновляем список доступных слотов
                    UpdateAvailableTimeSlots();
                    return;
                }

                // Создаем пациента
                var patient = new Patients
                {
                    Surname = PatientSurnameTextBox.Text,
                    Name = PatientNameTextBox.Text,
                    Patronymic = PatientPatronymicTextBox.Text,
                    IdGender = ((Genders)GenderComboBox.SelectedItem).Id,
                    Phone = PatientPhoneTextBox.Text,
                    DateBirthday = PatientBirthDatePicker.SelectedDate.Value,
                    IdPlots = 1
                };

                App.db.Patients.Add(patient);
                App.db.SaveChanges();

                // Создаем запись
                var reception = new Reception
                {
                    IdDoctor = doctorId,
                    IdPatient = patient.Id,
                    TimeNote = appointmentDateTime
                };

                App.db.Reception.Add(reception);
                App.db.SaveChanges();

                MessageBox.Show($"Успешная запись!\nВрач: {selectedDoctor.FullName}\nДата: {appointmentDateTime:dd.MM.yyyy HH:mm}");

                
                ClearForm();
                Bindings();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}\n\nПодробности: {ex.InnerException?.Message}");
            }
        }

        private void ClearForm()
        {
            PatientSurnameTextBox.Text = "";
            PatientNameTextBox.Text = "";
            PatientPatronymicTextBox.Text = "";
            PatientPhoneTextBox.Text = "";
            PatientBirthDatePicker.SelectedDate = null;
            GenderComboBox.SelectedIndex = -1;
            TimeSlotsListBox.ItemsSource = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
