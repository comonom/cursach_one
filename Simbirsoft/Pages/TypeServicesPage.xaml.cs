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
    /// Логика взаимодействия для TypeServicesPage.xaml
    /// </summary>
    public partial class TypeServicesPage : Page
    {
        public TypeServicesPage()
        {
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                TypeServicesGrid.ItemsSource =
                    await ModelContext.Instance.TypeServices
                    .Where(t =>
                        t.Name.Contains(tbSearch.Text))
                    .ToListAsync();
          });
        }

        private void Button_Click_AddTypeServices(object sender, RoutedEventArgs e)
        {
            EditTypeServiceWindow editTypeService = new EditTypeServiceWindow(new TypeService());
            editTypeService.ShowDialog();
            Refresh();
        }

        private void Button_Click_ChangeTypeServices(object sender, RoutedEventArgs e)
        {
            var typeservices = (TypeService)TypeServicesGrid.SelectedItem;
            if (typeservices == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }

            EditTypeServiceWindow editTypeService = new EditTypeServiceWindow(typeservices);
            editTypeService.ShowDialog();
            Refresh();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private async void Button_Click_DeleteTypeService(object sender, RoutedEventArgs e)
        {
            var typeservices = (TypeService)TypeServicesGrid.SelectedItem;
            if (typeservices == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }

            var result = MessageBox.Show("Действительно ли вы хотите удалить?", "Удаление", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ModelContext.Instance.TypeServices.Remove(typeservices);
                await ModelContext.Instance.SaveChangesAsync();
                Refresh();
            }
        }
    }
}
