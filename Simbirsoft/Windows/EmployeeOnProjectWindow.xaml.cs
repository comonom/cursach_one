using Microsoft.EntityFrameworkCore;
using Simbirsoft.entities;
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
using System.Windows.Shapes;

namespace Simbirsoft.Windows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeOnProjectWindow : Window
    {
        private Project _project;
        public EmployeeOnProjectWindow(Project project)
        {
            InitializeComponent();
            _project = project;
            Refresh();
        }

        public void Refresh()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                EmpGrid.ItemsSource =
                    await ModelContext.Instance.EmployeeOnProjects
                    .Include(i => i.Employee)
                    .Where(e => e.ProjectId == _project.Id)
                    .ToListAsync();
            });
        }

        private void Button_Click_AddEmployee(object sender, RoutedEventArgs e)
        {
            EditEmpOnProjectWindow1 editEmpOn
                = new EditEmpOnProjectWindow1(
                    new EmployeeOnProject
                    {
                        ProjectId = _project.Id,
                    });
            editEmpOn.ShowDialog();
            Refresh();
            
        }

        private void Button_Click_ChangeEmployee(object sender, RoutedEventArgs e)
        {
            var employeeOnProject = (EmployeeOnProject)EmpGrid.SelectedItem;
            if (employeeOnProject == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }
            EditEmpOnProjectWindow1 editEmpOn = new EditEmpOnProjectWindow1(employeeOnProject);
            editEmpOn.ShowDialog();
            Refresh();
        }

        private async void Button_Click_DeleteEmployee(object sender, RoutedEventArgs e)
        {
            var employee = (EmployeeOnProject)EmpGrid.SelectedItem;
            if (employee == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }

            var result = MessageBox.Show("Действительно ли вы хотите удалить?", "Удаление", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ModelContext.Instance.EmployeeOnProjects.Remove(employee);
                await ModelContext.Instance.SaveChangesAsync();
                Refresh();
            }
        }
    }
}
