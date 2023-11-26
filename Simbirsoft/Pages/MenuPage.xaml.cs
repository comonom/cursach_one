using Simbirsoft.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private Button ActiveButton;
        private readonly Brush ActiveBrush = new SolidColorBrush(Color.FromRgb(241, 146, 54));
        private readonly Brush UnActiveBrush = new SolidColorBrush(Color.FromRgb(247, 247, 249));

        public MenuPage()
        {
            InitializeComponent();
        }

        private void ChangeActiveButton(Button btn)
        {
            if (ActiveButton != null)
            {
                ActiveButton.Foreground = UnActiveBrush;
            }

            ActiveButton = btn;
            ActiveButton.Foreground = ActiveBrush;
        }

        private void Button_ClickBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void Button_Project_Click(object sender, RoutedEventArgs e)
        {
            frameNav.Navigate(new ProjectPage());

            ChangeActiveButton((Button)sender);
        }
        private void Button_Employee_Click(object sender, RoutedEventArgs e)
        {
            frameNav.Navigate(new EmployeePage());

            ChangeActiveButton((Button)sender);
        }

        private void Button_Customers_Click(object sender, RoutedEventArgs e)
        {
            frameNav.Navigate(new CustomersPage());
            ChangeActiveButton((Button)sender);
        }

        private void Button_Services_Click(object sender, RoutedEventArgs e)
        {
            frameNav.Navigate(new ServicesPage());
            ChangeActiveButton((Button)sender);
        }

        private void Button_TypeServices_Click(object sender, RoutedEventArgs e)
        {
            frameNav.Navigate(new TypeServicesPage());
            ChangeActiveButton((Button)sender);
        }
    }
}
