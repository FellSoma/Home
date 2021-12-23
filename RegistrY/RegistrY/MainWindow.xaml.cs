using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegistrY
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

            bool click = false;
        private void Info(object sender, RoutedEventArgs e)
        {
            if (click==false)
            {
                Application.Current.MainWindow.Height = 500;
                Application.Current.MainWindow.Width  = 650;
                click = true;
            }
            else if (click==true)
            {
                Application.Current.MainWindow.Height = 500;
                Application.Current.MainWindow.Width = 390;
                click = false;
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            Window1 g = new Window1();
            g.Show();
        }

        private void Next2(object sender, RoutedEventArgs e)
        {
            Window2 g = new Window2();
            g.Show();
        }

        private void ReadMe(object sender, RoutedEventArgs e)
        {

          
            System.Diagnostics.Process.Start("ReadMe.txt");


        }
    }
}
