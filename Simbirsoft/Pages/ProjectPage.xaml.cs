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

namespace Simbirsoft
{
    /// <summary>
    /// Логика взаимодействия для Project.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        public ProjectPage()
        {
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                ProjectGrid.ItemsSource =
                    await ModelContext.Instance.Projects
                    .Include(p=>p.Customer)
                    .Where(p =>
                        p.Name.Contains(tbSearch.Text))
                    .ToListAsync();
            });
        }
        private void Button_Click_AddProject(object sender, RoutedEventArgs e)
        {
            EditProjectWindow editProject = new EditProjectWindow(new Project());
            editProject.ShowDialog();
            Refresh();
        }

        private void Button_Click_ChangeProject(object sender, RoutedEventArgs e)
        {
            var project = (Project)ProjectGrid.SelectedItem;
            if (project == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }

            EditProjectWindow editProject = new EditProjectWindow(project);
            editProject.ShowDialog();
            Refresh();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private async void Button_Click_DeleteProject(object sender, RoutedEventArgs e)
        {
            var project = (Project)ProjectGrid.SelectedItem;
            if (project == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }

            var result = MessageBox.Show("Действительно ли вы хотите удалить?", "Удаление", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ModelContext.Instance.Projects.Remove(project);
                await ModelContext.Instance.SaveChangesAsync();
                Refresh();
            }
        }

        private void Button_Click_Employees(object sender, RoutedEventArgs e)
        {
            var project = (Project)ProjectGrid.SelectedItem;
            if (project == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }

            EmployeeOnProjectWindow employeeWindow = new EmployeeOnProjectWindow(project);
            employeeWindow.ShowDialog();
        }
    }
}
