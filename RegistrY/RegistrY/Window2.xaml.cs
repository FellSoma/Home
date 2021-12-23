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
using System.IO;
using Microsoft.Win32;

namespace RegistrY
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }
        string fileToOpen;
        private void Review(object sender, RoutedEventArgs e)
        {
            var FD = new OpenFileDialog();

            FD.ShowDialog();
                fileToOpen = FD.FileName;
                
            if(FD.FileName!="")
            {
                System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);
                System.IO.StreamReader reader = new System.IO.StreamReader(fileToOpen);
            }

            //OR

            //etc
            switch (choice.SelectedIndex)
            {
                case -1:
                    MessageBox.Show("Выберите один из вариантов");
                    break;
                case 0:
                    SetAutorunValue(true);
                    MessageBox.Show("Добавленно в автозагрузку");
                    break;
                case 1:
                    SetAutorunValue(false);
                    MessageBox.Show("Убранно из автозагрузки");
                    break;

                default:
                    break;
            }

           
        }

        private void CreateDir(object sender, RoutedEventArgs e)
        {

        }
        public bool SetAutorunValue(bool autorun)
        {


            string ExePath = fileToOpen;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    reg.SetValue("Test", ExePath);
                else
                    reg.DeleteValue("Test");

                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
    
        } 
    }
}
