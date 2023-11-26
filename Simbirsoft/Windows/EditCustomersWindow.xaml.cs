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
    /// Логика взаимодействия для AddChangeCustomersWindow.xaml
    /// </summary>
    public partial class EditCustomersWindow : Window
    {
        private Customer _customer;
        public EditCustomersWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer;

            Title = customer.Id == 0 ? "Добавление" : "Редактирование";

            this.DataContext = customer;
        }

        private async void Button_ClickSave(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_customer.NameCompany) || string.IsNullOrEmpty(_customer.Address))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (_customer.Id == 0)
            {
                ModelContext.Instance.Customers.Add(_customer);
            }
            await ModelContext.Instance.SaveChangesAsync();

            Close();
        }
    }
}
