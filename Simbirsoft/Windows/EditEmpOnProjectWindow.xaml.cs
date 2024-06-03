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
    /// Логика взаимодействия для EditEmpOnProjectWindow1.xaml
    /// </summary>
    public partial class EditEmpOnProjectWindow1 : Window
    {
        private EmployeeOnProject _empOnProject;
        public EditEmpOnProjectWindow1(EmployeeOnProject empOnProject)
        {
            InitializeComponent();
            _empOnProject = empOnProject;

            Title = empOnProject.EmpolyeeId == 0 ? "Добавление" : "Редактирование";
            if (empOnProject.EmpolyeeId == 0)
            {
                empOnProject.DateStart = DateTime.Now;
            }
            else
            {
                cbEmployee.IsEnabled = false;
            }

            this.DataContext = empOnProject;

            this.cbEmployee.ItemsSource = ModelContext.Instance.Employees.ToList();
        }

        private async void Button_ClickSave(object sender, RoutedEventArgs e)
        {
            if (_empOnProject.SalaryForCompany <= 0)
            {
                MessageBox.Show("Цена должна быть больше 0!");
                return;
            }

            if (_empOnProject.Employee == null)
            {
                MessageBox.Show("Должны быть заполнены все поля!");
                return;
            }

            if(_empOnProject.DateEnd < _empOnProject.DateStart)
            {
                MessageBox.Show("Дата конца должна быть позже даты начала!");
                return;
            }

            if(ModelContext.Instance.EmployeeOnProjects.Any(f => f.ProjectId == _empOnProject.ProjectId && f.EmpolyeeId == _empOnProject.Employee.Id))
            {
                MessageBox.Show("Такой сотрудник уже добавлен на проект");
                return;
            }

            if (_empOnProject.EmpolyeeId == 0)
            {
                ModelContext.Instance.EmployeeOnProjects.Add(_empOnProject);
            }

            await ModelContext.Instance.SaveChangesAsync();

            Close();
        }
    }
}
