using Simbirsoft.entities;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace Simbirsoft.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddChangeEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        private Employee _employee;
        private string _lastLogin;
        public EditEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            _employee = employee;

            Title = employee.Id == 0 ? "Добавление" : "Редактирование";
            if(employee.Id > 0)
            {
                Password.Visibility = Visibility.Collapsed;
                textBlockPass.Visibility = Visibility.Collapsed;
                _lastLogin = employee.Login;
            }

            this.DataContext = employee;
        }

        private async void Button_ClickSave(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_employee.Surname)
                || string.IsNullOrEmpty(_employee.Name)
                || string.IsNullOrEmpty(_employee.Login)
                || string.IsNullOrEmpty(_employee.Password))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (_employee.Login != _lastLogin &&
                ModelContext.Instance.Employees.Any(e => e.Login == _employee.Login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return;
            }

            if (_employee.Id == 0)
            {
                ModelContext.Instance.Employees.Add(_employee);
            }

            await ModelContext.Instance.SaveChangesAsync();

            Close();
        }
    }
}
