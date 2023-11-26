using Microsoft.EntityFrameworkCore;
using Simbirsoft.entities;
using Simbirsoft.Windows;
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

namespace Simbirsoft.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                EmployeeGrid.ItemsSource =
                    await ModelContext.Instance.Employees
                    .Where(e =>
                        e.Surname.Contains(tbSearch.Text)
                        || e.Name.Contains(tbSearch.Text)
                        || e.Patronymic.Contains(tbSearch.Text)
                        || e.Login.Contains(tbSearch.Text))
                    .ToListAsync();
            });
        }

        private void Button_Click_AddEmployee(object sender, RoutedEventArgs e)
        {
            EditEmployeeWindow editEmployee = new EditEmployeeWindow(new Employee());
            editEmployee.ShowDialog();
            Refresh();
        }

        private void Button_Click_ChangeEmployee(object sender, RoutedEventArgs e)
        {
            var employee = (Employee)EmployeeGrid.SelectedItem;
            if (employee == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }

            EditEmployeeWindow editEmployee = new EditEmployeeWindow(employee);
            editEmployee.ShowDialog();
            Refresh();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private async void Button_Click_DeleteEmployee(object sender, RoutedEventArgs e)
        {
            var employee = (Employee)EmployeeGrid.SelectedItem;
            if (employee == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }

            var result = MessageBox.Show("Действительно ли вы хотите удалить?", "Удаление", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ModelContext.Instance.Employees.Remove(employee);
                await ModelContext.Instance.SaveChangesAsync();
                Refresh();
            }
            
        }
    }
}
