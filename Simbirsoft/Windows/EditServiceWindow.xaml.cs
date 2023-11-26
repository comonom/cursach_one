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
    /// Логика взаимодействия для AddChangeServiceWindow.xaml
    /// </summary>
    public partial class EditServiceWindow : Window
    {
        private Service _service;
        public EditServiceWindow(Service service)
        {
            InitializeComponent();
            _service = service;

            Title = service.Id == 0 ? "Добавление" : "Редактирование";
            if(service.Id == 0)
            {
                service.DateStart = DateTime.Now;
            }

            this.DataContext = service;

            this.cbProject.ItemsSource = ModelContext.Instance.Projects.ToList();
            this.cbEmployee.ItemsSource = ModelContext.Instance.Employees.ToList();
            this.cbTypeService.ItemsSource = ModelContext.Instance.TypeServices.ToList();
        }

        private async void Button_ClickSave(object sender, RoutedEventArgs e)
        {
            if(_service.Price <= 0)
            {
                MessageBox.Show("Цена должна быть больше 0!");
                return;
            }

            if (_service.Employee == null || _service.Project == null || _service.TypeService == null )
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (_service.Id == 0)
            {
                ModelContext.Instance.Services.Add(_service);
            }
            await ModelContext.Instance.SaveChangesAsync();

            Close();
        }
    }
}
