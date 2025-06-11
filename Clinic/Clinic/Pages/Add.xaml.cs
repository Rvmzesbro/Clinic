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

        public Add(Reception reception, bool flag)
        {
            InitializeComponent();
            LoadData();
            if(flag == true)
            {
                SelectedReception(reception, flag);
            }
            Reception = reception;
            Bindings();
        }
        private void FillFormWithReceptionData()
        {
            if (Reception == null) return;

            try
            {
                // Загружаем полные данные записи из БД
                var fullReception = App.db.Reception
                    .Include(r => r.Doctors)
                    .Include(r => r.Doctors.Specialitys)
                    .Include(r => r.Patients)
                    .FirstOrDefault(r => r.Id == Reception.Id);

                if (fullReception == null) return;

                // Заполняем данные пациента
                PatientSurnameTextBox.Text = fullReception.Patients.Surname;
                PatientNameTextBox.Text = fullReception.Patients.Name;
                PatientPatronymicTextBox.Text = fullReception.Patients.Patronymic;
                PatientPhoneTextBox.Text = fullReception.Patients.Phone;
                PatientBirthDatePicker.SelectedDate = fullReception.Patients.DateBirthday;
                GenderComboBox.SelectedValue = fullReception.Patients.IdGender;

                // Устанавливаем специальность и врача
                SpecialitiesComboBox.SelectedValue = fullReception.Doctors.IdSpeciality;
                DoctorsComboBox.SelectedValue = fullReception.Doctors.Id;

                // Устанавливаем дату и время
                AppointmentDatePicker.SelectedDate = fullReception.TimeNote.Date;
                UpdateAvailableTimeSlots();

                // Устанавливаем выбранное время
                var timeStr = fullReception.TimeNote.ToString("HH:mm");
                TimeSlotsListBox.SelectedValue = timeStr;

                // Меняем текст кнопки
                BookAppointmetnClick.Content = "Сохранить изменения";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void SelectedReception(Reception reception, bool flag)
        {
            try
            {
                // Загружаем полные данные из БД
                var fullReception = App.db.Reception
                    .Include(r => r.Doctors)
                    .Include(r => r.Doctors.Specialitys)
                    .Include(r => r.Patients)
                    .Include(r => r.Patients.Genders)
                    .FirstOrDefault(r => r.Id == reception.Id);

                if (fullReception == null) return;

                // Устанавливаем специальность
                SpecialitiesComboBox.SelectedValue = fullReception.Doctors.IdSpeciality;

                // Устанавливаем врача (дожидаемся загрузки врачей)
                DoctorsComboBox.ItemsSource = App.db.Doctors
                    .Where(d => d.IdSpeciality == fullReception.Doctors.IdSpeciality)
                    .ToList();
                DoctorsComboBox.SelectedValue = fullReception.IdDoctor;

                // Дата приема
                AppointmentDatePicker.SelectedDate = fullReception.TimeNote.Date;

                // Обновляем доступное время
                UpdateAvailableTimeSlots();

                // Устанавливаем выбранное время
                var timeStr = fullReception.TimeNote.ToString("HH:mm");
                TimeSlotsListBox.SelectedValue = timeStr;

                // Данные пациента
                PatientSurnameTextBox.Text = fullReception.Patients.Surname;
                PatientNameTextBox.Text = fullReception.Patients.Name;
                PatientPatronymicTextBox.Text = fullReception.Patients.Patronymic;
                PatientBirthDatePicker.SelectedDate = fullReception.Patients.DateBirthday;
                PatientPhoneTextBox.Text = fullReception.Patients.Phone;

                // Устанавливаем пол пациента
                GenderComboBox.SelectedValue = fullReception.Patients.IdGender;

                // Меняем текст кнопки
                BookAppointmetnClick.Content = "Сохранить изменения";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void Bindings()
        {
            AppointmentsDataGrid.ItemsSource = App.db.Reception.ToList();
        }

        private void LoadData()
        {
            // Загрузка специальностей
            SpecialitiesComboBox.ItemsSource = App.db.Specialitys.ToList();
            SpecialitiesComboBox.DisplayMemberPath = "Name";
            SpecialitiesComboBox.SelectedValuePath = "Id";

            // Загрузка полов
            GenderComboBox.ItemsSource = App.db.Genders.ToList();
            GenderComboBox.DisplayMemberPath = "Name";
            GenderComboBox.SelectedValuePath = "Id";

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

                DoctorsComboBox.DisplayMemberPath = "FullName";
                DoctorsComboBox.SelectedValuePath = "Id";
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
            try
            {
                if (DoctorsComboBox.SelectedItem == null || !AppointmentDatePicker.SelectedDate.HasValue)
                    return;

                dynamic selectedDoctor = DoctorsComboBox.SelectedItem;
                int doctorId = selectedDoctor.Id;
                DateTime selectedDate = AppointmentDatePicker.SelectedDate.Value;

                
                


                // Получаем расписание врача
                var schedules = App.db.DoctorsShedule
                    .Where(s => s.IdDoctor == doctorId)
                    .AsNoTracking()
                    .ToList();

                // Получаем занятые слоты (только часы и минуты)
                var bookedSlots = App.db.Reception
                    .Where(r => r.IdDoctor == doctorId &&
                               EntityFunctions.TruncateTime(r.TimeNote) == selectedDate.Date)
                    .Select(r => new { r.TimeNote.Hour, r.TimeNote.Minute })
                    .AsNoTracking()
                    .ToList()
                    .Select(x => new TimeSpan(x.Hour, x.Minute, 0))
                    .ToList();

                // Генерируем доступные слоты с шагом 30 минут
                var availableSlots = new List<TimeSpan>();

                foreach (var schedule in schedules)
                {
                    TimeSpan start = schedule.BeginingDate;
                    TimeSpan end = schedule.EndDate;

                    for (TimeSpan slot = start; slot < end; slot = slot.Add(TimeSpan.FromMinutes(30)))
                    {
                        if (!bookedSlots.Any(bs => bs.Hours == slot.Hours && bs.Minutes == slot.Minutes))
                        {
                            availableSlots.Add(slot);
                        }
                    }
                }

                TimeSlotsListBox.ItemsSource = availableSlots
                    .OrderBy(t => t)
                    .Select(t => $"{t.Hours:00}:{t.Minutes:00}");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении слотов: {ex.Message}");
            }

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

                // Парсим выбранное время
                string[] timeParts = TimeSlotsListBox.SelectedItem.ToString().Split(':');
                if (timeParts.Length != 2 || !int.TryParse(timeParts[0], out int hours) ||
                    !int.TryParse(timeParts[1], out int minutes))
                {
                    MessageBox.Show("Неверный формат времени!");
                    return;
                }

                DateTime appointmentDateTime = selectedDate.Date.Add(new TimeSpan(hours, minutes, 0));

                // Проверяем, редактируем ли мы существующую запись
                if (BookAppointmetnClick.Content.ToString() == "Сохранить изменения" && Reception != null)
                {
                    // Редактирование существующей записи

                    // Получаем полные данные записи из БД
                    var existingReception = App.db.Reception
                        .Include(r => r.Patients)
                        .FirstOrDefault(r => r.Id == Reception.Id);

                    if (existingReception != null)
                    {
                        // Обновляем данные пациента
                        existingReception.Patients.Surname = PatientSurnameTextBox.Text;
                        existingReception.Patients.Name = PatientNameTextBox.Text;
                        existingReception.Patients.Patronymic = PatientPatronymicTextBox.Text;
                        existingReception.Patients.Phone = PatientPhoneTextBox.Text;
                        existingReception.Patients.DateBirthday = PatientBirthDatePicker.SelectedDate.Value;
                        existingReception.Patients.IdGender = ((Genders)GenderComboBox.SelectedItem).Id;

                        // Обновляем данные приема
                        existingReception.IdDoctor = doctorId;
                        existingReception.TimeNote = appointmentDateTime;

                        App.db.SaveChanges();
                        MessageBox.Show("Изменения успешно сохранены!");
                    }
                }
                else
                {
                    // Создание новой записи

                    // Проверка доступности времени
                    bool isSlotAvailable = !App.db.Reception
                        .AsNoTracking()
                        .Any(r => r.IdDoctor == doctorId &&
                                 EntityFunctions.TruncateTime(r.TimeNote) == selectedDate.Date &&
                                 r.TimeNote.Hour == hours &&
                                 r.TimeNote.Minute == minutes);

                    if (!isSlotAvailable)
                    {
                        MessageBox.Show("Выбранное время уже занято. Пожалуйста, выберите другое время.");
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
                }

                LoadAppointments();
                ClearForm();
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
            AppointmentDatePicker.SelectedDate = null;
            DoctorsComboBox.SelectedIndex = -1;
            SpecialitiesComboBox.SelectedIndex = -1;
        }
    

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
