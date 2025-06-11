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

        public Add()
        {
            InitializeComponent();
            LoadData();
            //SelectedReception(reception);

            Bindings();
            
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
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            AppointmentsDataGrid.ItemsSource = App.db.Reception
                .Include(r => r.Doctors)
                .Include(r => r.Doctors.Specialitys)
                .Include(r => r.Patients)
                .ToList();
        }

        private void SpecialitiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpecialitiesComboBox.SelectedItem is Specialitys selectedSpeciality)
            {
                // Загрузка врачей выбранной специальности
                DoctorsComboBox.ItemsSource = App.db.Doctors
                    .Where(d => d.IdSpeciality == selectedSpeciality.Id)
                    .ToList()
                    .Select(d => new
                    {
                        d.Id,
                        FullName = $"{d.Surname} {d.Name} {d.Patronymic}",
                        d.Specialitys
                    });

                DoctorsComboBox.SelectedIndex = -1;
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
            var availableSlots = new List<TimeSpan>();

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
                        availableSlots.Add(slot);
                    }
                }
            }

            // Отображаем доступные слоты
            TimeSlotsListBox.ItemsSource = availableSlots
                .OrderBy(t => t)
                .Select(t => $"{t.Hours:00}:{t.Minutes:00}");
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
                TimeSpan selectedTime = TimeSpan.Parse(TimeSlotsListBox.SelectedItem.ToString());

                // Создаем дату и время приема
                DateTime appointmentDateTime = selectedDate.Date.Add(selectedTime);

                // Проверяем, не занято ли уже это время
                bool isSlotAvailable = !App.db.Reception
                    .Any(r => r.IdDoctor == doctorId && r.TimeNote == appointmentDateTime);

                if (!isSlotAvailable)
                {
                    MessageBox.Show("Выбранное время уже занято. Пожалуйста, выберите другое время.");
                    return;
                }

                // Создаем или находим пациента
                var patient = new Patients
                {
                    Surname = PatientSurnameTextBox.Text,
                    Name = PatientNameTextBox.Text,
                    Patronymic = PatientPatronymicTextBox.Text,
                    IdGender = ((Genders)GenderComboBox.SelectedItem).Id,
                    Phone = PatientPhoneTextBox.Text,
                    DateBirthday = PatientBirthDatePicker.SelectedDate.Value,
                    IdPlots = 1 // По умолчанию участок 1, можно добавить выбор участка
                };

                App.db.Patients.Add(patient);
                App.db.SaveChanges();

                // Создаем запись на прием
                var reception = new Reception
                {
                    IdDoctor = doctorId,
                    IdPatient = patient.Id,
                    TimeNote = appointmentDateTime
                };

                App.db.Reception.Add(reception);
                App.db.SaveChanges();

                MessageBox.Show("Запись на прием успешно создана!");

                // Обновляем список записей
                LoadAppointments();

                // Очищаем форму
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании записи: {ex.Message}");
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
