using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        private List<Doctors> doctors = new List<Doctors>();
        private List<Patients> patients = new List<Patients>();
        private List<Reception> receptions = new List<Reception>();
        private List<DoctorsShedule> shedules = new List<DoctorsShedule>();
        public Main()
        {
            InitializeComponent();
            NewLists();
            Bindings();
        }

        private void SearchDoctors()
        {
            var TextUser = SearchTextBox.Text.ToLower();
            var FilteredList = doctors.Where(p => p.FullName.ToLower().Contains(TextUser)).ToList();

            if (SearchTextBox.Text == "")
            {
                DoctorsGrid.ItemsSource = doctors;
            }

            if (this.DoctorsGrid == null)
            {
                return;
            }

            DoctorsGrid.ItemsSource = FilteredList;
        }

        private void SearchPatients()
        {
            var TextUser = SearchTextBox.Text.ToLower();
            var FilteredList = patients.Where(p => p.FullName.ToLower().Contains(TextUser)).ToList();

            if (SearchTextBox.Text == "")
            {
                PatientsGrid.ItemsSource = patients;
            }

            if (this.PatientsGrid == null)
            {
                return;
            }

            PatientsGrid.ItemsSource = FilteredList;
        }

        private void SearchReception()
        {
            var TextUser = SearchTextBox.Text.ToLower();
            var FilteredList = receptions.Where(p => p.Patients.FullName.ToLower().Contains(TextUser)).ToList();

            if (SearchTextBox.Text == "")
            {
                ReceptionGrid.ItemsSource = receptions;
            }

            if (this.ReceptionGrid == null)
            {
                return;
            }

            ReceptionGrid.ItemsSource = FilteredList;
        }

        private void SearchShedules()
        {
            var TextUser = SearchTextBox.Text.ToLower();
            var FilteredList = shedules.Where(p => p.Doctors.FullName.ToLower().Contains(TextUser)).ToList();

            if (SearchTextBox.Text == "")
            {
                ShedulesGrid.ItemsSource = shedules;
            }

            if (this.ShedulesGrid == null)
            {
                return;
            }

            ShedulesGrid.ItemsSource = FilteredList;
        }

        private void NewLists()
        {
            doctors = App.db.Doctors.ToList();
            patients = App.db.Patients.ToList();
            receptions = App.db.Reception.ToList();
            shedules = App.db.DoctorsShedule.ToList();
        }

        private void Bindings()
        {
            DoctorsGrid.ItemsSource = App.db.Doctors.ToList();
            PatientsGrid.ItemsSource = App.db.Patients.ToList();
            ReceptionGrid.ItemsSource = App.db.Reception.ToList();
            ShedulesGrid.ItemsSource = App.db.DoctorsShedule.ToList();
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            NavigationService.Navigate(new Pages.Add(new Reception(), flag));
        }

        private void BTEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!TIReception.IsSelected)
            {
                return;
            }
           
            if (ReceptionGrid.SelectedItem is Reception reception)
            {
                bool flag = true;
                NavigationService.Navigate(new Add(reception, flag));
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования!");
            }

        }

        private void BTDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!TIReception.IsSelected)
            {
                return;
            }

            if (ReceptionGrid.SelectedItem is Reception reception)
            {
                bool flag = true;
                NavigationService.Navigate(new Add(reception, flag));
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления!");
            }
        }


        private void BTClear_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
            CBFilter.SelectedItem = null;
            if (SearchTextBox.Text == "")
            {
                DoctorsGrid.ItemsSource = doctors;
            }
            if (SearchTextBox.Text == "")
            {
                PatientsGrid.ItemsSource = patients;
            }
            if (SearchTextBox.Text == "")
            {
                ReceptionGrid.ItemsSource = receptions;
            }
            if (SearchTextBox.Text == "")
            {
                ShedulesGrid.ItemsSource = shedules;
            }
        }


        private void BTClear_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                DoctorsGrid.ItemsSource = doctors;
            }
            if (SearchTextBox.Text == "")
            {
                PatientsGrid.ItemsSource = patients;
            }
            if (SearchTextBox.Text == "")
            {
                ReceptionGrid.ItemsSource = receptions;
            }
            if (SearchTextBox.Text == "")
            {
                ShedulesGrid.ItemsSource = shedules;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TIDoctors.IsSelected)
            {
                SearchDoctors();

            }
            if (TIPatients.IsSelected)
            {
                SearchPatients();

            }
            if (TIReception.IsSelected)
            {
                SearchReception();

            }
            if (TISchedules.IsSelected)
            {
                SearchShedules();

            }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Bindings();
            NewLists();

            if (TIDoctors.IsSelected)
            {
                CBFilter.ItemsSource = doctors.Select(p => p.FullName);
            }
            if (TIPatients.IsSelected)
            {
                CBFilter.ItemsSource = patients.Select(p => p.FullName);
            }
            if (TIReception.IsSelected)
            {
                CBFilter.ItemsSource = receptions.Select(p => p.Patients.FullName);
            }
            if (TISchedules.IsSelected)
            {
                CBFilter.ItemsSource = shedules.Select(p => p.Doctors.FullName);
            }

        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TIDoctors.IsSelected)
            {
                CBFilter.ItemsSource = doctors.Select(p => p.FullName);
                SearchTextBox.Text = "";
                if (SearchTextBox.Text == "")
                {
                    DoctorsGrid.ItemsSource = doctors;
                }
            }
            if (TIPatients.IsSelected)
            {
                CBFilter.ItemsSource = patients.Select(p => p.FullName);
                SearchTextBox.Text = "";
                if (SearchTextBox.Text == "")
                {
                    PatientsGrid.ItemsSource = patients;
                }
            }
            if (TIReception.IsSelected)
            {
                CBFilter.ItemsSource = receptions.Select(p => p.Patients.FullName);
                SearchTextBox.Text = "";
                if (SearchTextBox.Text == "")
                {
                    ReceptionGrid.ItemsSource = receptions;
                }
            }
            if (TISchedules.IsSelected)
            {
                CBFilter.ItemsSource = shedules.Select(p => p.Doctors.FullName);
                SearchTextBox.Text = "";
                if (SearchTextBox.Text == "")
                {
                    ShedulesGrid.ItemsSource = shedules;
                }
            }
        }

        private void CBFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBFilter.SelectedItem == null)
            {
                return;
            }
            else
            {
                SearchTextBox.Text = CBFilter.SelectedItem.ToString();
            }

        }

        private void DoctorsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}

