using Microsoft.EntityFrameworkCore;
using Simbirsoft.DocumentGenerators;
using Simbirsoft.entities;
using Simbirsoft.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        public ServicesPage()
        {
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                ServicesGrid.ItemsSource =
                    await ModelContext.Instance.Services
                    .Include(s=>s.Project)
                    .Include(s=>s.TypeService)
                    .Include(s=>s.Employee)
                    .Where(s =>
                        s.Project.Name.Contains(tbSearch.Text)
                        || s.Employee.Surname.Contains(tbSearch.Text)
                        || s.Employee.Name.Contains(tbSearch.Text)
                        || s.Employee.Patronymic.Contains(tbSearch.Text))
                    .ToListAsync();
            });
        }
        private void Button_Click_AddServices(object sender, RoutedEventArgs e)
        {
            EditServiceWindow editService = new EditServiceWindow(new Service());
            editService.ShowDialog();
            Refresh();
        }

        private void Button_Click_ChangeServices(object sender, RoutedEventArgs e)
        {
            var service = (Service)ServicesGrid.SelectedItem;
            if (service == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }

            EditServiceWindow editService = new EditServiceWindow(service);
            editService.ShowDialog();
            Refresh();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private async void Button_Click_DeleteService(object sender, RoutedEventArgs e)
        {
            var service = (Service)ServicesGrid.SelectedItem;
            if (service == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }

            var result = MessageBox.Show("Действительно ли вы хотите удалить?", "Удаление", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ModelContext.Instance.Services.Remove(service);
                await ModelContext.Instance.SaveChangesAsync();
                Refresh();
            }
        }

        private async void Button_Click_Otchet(object sender, RoutedEventArgs e)
        {
            new DocumentSettings().ShowDialog();
        }
    }
}
