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
    /// Логика взаимодействия для AddChangeTypeServiceWindow.xaml
    /// </summary>
    public partial class EditTypeServiceWindow : Window
    {
        private TypeService _typeService;
        public EditTypeServiceWindow(TypeService type)
        {
            InitializeComponent();
            _typeService = type;

            Title = type.Id == 0 ? "Добавление" : "Редактирование";

            this.DataContext = type;
        }

        private async void Button_ClickSave(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_typeService.Name) || string.IsNullOrEmpty(_typeService.Description))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (_typeService.Id == 0)
            {
                ModelContext.Instance.TypeServices.Add(_typeService);
            }
            await ModelContext.Instance.SaveChangesAsync();

            Close();
        }
    }
}
