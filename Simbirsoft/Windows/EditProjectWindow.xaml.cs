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
    /// Логика взаимодействия для AddProjectWindow.xaml
    /// </summary>
    public partial class EditProjectWindow : Window
    {
        private Project _project;
        public EditProjectWindow(Project project)
        {
            InitializeComponent();
            _project = project;

            Title = project.Id == 0 ? "Добавление" : "Редактирование";
            if (project.Id == 0)
            {
                project.DateStart = DateTime.Now;
            }

            this.DataContext = project;

            this.cbCustomer.ItemsSource = ModelContext.Instance.Customers.ToList();
        }

        private async void Button_ClickSave(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_project.Name) || _project.Customer == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (_project.Id == 0)
            {
                ModelContext.Instance.Projects.Add(_project);
            }

            await ModelContext.Instance.SaveChangesAsync();

            Close();
        }
    }
}
