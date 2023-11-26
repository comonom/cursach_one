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
    /// Логика взаимодействия для CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        public CustomersPage()
        {
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                CustomersGrid.ItemsSource =
                    await ModelContext.Instance.Customers
                    .Where(c =>
                        c.NameCompany.Contains(tbSearch.Text))
                    .ToListAsync();
            });
        }

        private void Button_Click_AddCustomer(object sender, RoutedEventArgs e)
        {
            EditCustomersWindow editCustomers = new EditCustomersWindow(new Customer());
            editCustomers.ShowDialog();
            Refresh();
        }

        private void Button_Click_ChangeCustomer(object sender, RoutedEventArgs e)
        {
            var customer = (Customer)CustomersGrid.SelectedItem;
            if (customer == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }
            
            EditCustomersWindow editCustomers = new EditCustomersWindow(customer);
            editCustomers.ShowDialog();
            Refresh();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private async void Button_Click_DeleteCustomer(object sender, RoutedEventArgs e)
        {
            var customer = (Customer)CustomersGrid.SelectedItem;
            if (customer == null)
            {
                MessageBox.Show("Вы ничего не выбрали!");
                return;
            }

            var result = MessageBox.Show("Действительно ли вы хотите удалить?", "Удаление", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ModelContext.Instance.Customers.Remove(customer);
                await ModelContext.Instance.SaveChangesAsync();
                Refresh();
            }
        }
    }
}
