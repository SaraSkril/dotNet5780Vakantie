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

using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostLogin.xaml
    /// </summary>
    public partial class HostLogin : Window
    {
        public HostLogin()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
         
        }

        private void button1_Click_Login_Host(object sender, RoutedEventArgs e)
        {
           
            if (Idtextbox.Password.Length != 9)
            {
                MessageBox.Show("Invalid ID!");
                return;
            }
            int number = 0;

            if (int.TryParse(Idtextbox.Password, out number) == false)
            {
                MessageBox.Show("Invalid ID!");
                return;
            }
            else
                if (MainWindow.ibl.checkifHost(Idtextbox.Password) == false)
            {
                MessageBox.Show("This Id does not exist!");
                return;
            }
            Close();
            new hostprop(Idtextbox.Password).ShowDialog();


        }

        private void button2_Click_addnewHost(object sender, RoutedEventArgs e)
        {
            Close();
            new NewHost().ShowDialog();
            //add new window
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width *= 1.1;
            ((Button)sender).Height *= 1.1;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width /= 1.1;
            ((Button)sender).Height /= 1.1;
        }

        private void Button_Click_MinimizeWindow(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void Button_Click_MaximizeWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                SystemCommands.RestoreWindow(this);
            else
                SystemCommands.MaximizeWindow(this);
        }

        private void Button_Click_CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_MouseEnter_RED(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width *= 1.1;
            ((Button)sender).Height *= 1.1;
        }
    }
    }

