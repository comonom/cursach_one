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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simbirsoft.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTb.Text == "" || PassTb.Password == "")
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            var emp = ModelContext.Instance.Employees.FirstOrDefault(e => e.Login == LoginTb.Text && e.Password == PassTb.Password);
            if (emp == null)
            {
                MessageBox.Show("Такого пользователя нет!");
                return;
            }

            NavigationService.Navigate(new MenuPage());
        }
    }
}
