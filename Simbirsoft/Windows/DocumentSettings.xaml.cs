using Microsoft.Win32;
using Simbirsoft.DocumentGenerators;
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
using System.Windows.Shapes;

namespace Simbirsoft.Windows
{
    /// <summary>
    /// Логика взаимодействия для DocumentSettings.xaml
    /// </summary>
    public partial class DocumentSettings : Window
    {
        public DocumentSettings()
        {
            InitializeComponent();
        }

        private async void onclick(object sender, RoutedEventArgs e)
        {
            if(fromDatePicker.SelectedDate == null || toDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PDF files (*.pdf)|*.pdf";
            if (dlg.ShowDialog() == true)
            {
                var pdf = await new SimpleGenerator(fromDatePicker.SelectedDate.Value, toDatePicker.SelectedDate.Value).CreatePdfDocument();
                using (var filestream = File.Create(dlg.FileName))
                {
                    filestream.Write(pdf, 0, pdf.Count());
                }

                Process.Start(new ProcessStartInfo { FileName = dlg.FileName, UseShellExecute = true });
            }
        }
    }
}
