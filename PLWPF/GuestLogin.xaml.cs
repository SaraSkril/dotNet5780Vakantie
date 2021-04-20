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

namespace PLWPF
{
    /// <summary>
   
    /// Interaction logic for GuestLogin.xaml
    /// </summary>
    public partial class GuestLogin : Window
    {

        public GuestLogin()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
        private void button1_Click_Login_Guest(object sender, RoutedEventArgs e)
        {/*
            if (Idtextbox.Text.Length != 9)
            {
                MessageBox.Show("Invalid ID!");
                return;
            }
            int number = 0;

            if (int.TryParse(Idtextbox.Text, out number) == false)
            {
                MessageBox.Show("Invalid ID!");
                return;
            }
            else
                if (MainWindow.ibl.checkifGuests(Idtextbox.Text) == false)
            {
                MessageBox.Show("This Id does not exist!");
                return;
            }
            Close();
            new updateguest(Idtextbox.Text).ShowDialog();
            
            */

        }
        private void button2_Click_addnewGuest(object sender, RoutedEventArgs e)
        {
            Close();
            new AddGuests().ShowDialog();
            
        }
    }
}