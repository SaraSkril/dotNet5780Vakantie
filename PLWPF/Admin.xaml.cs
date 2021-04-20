using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            int sum = 0;
            List<Host> h = MainWindow.ibl.GetAllHosts();
            List<Guest> g = MainWindow.ibl.GetAllGuests();
            List<HostingUnit> hu = MainWindow.ibl.GetAllHostingUnits();
            foreach(Host host in h)
            {
                sum += host.commission;
            }
            string s = "Current Commissiom: " + Configuration.commission + "\n";
            s += "Total Profit to date: " + sum + "\n";
            s += "Number of Hosts: " + h.Count + "\n";
            s+= "Number of Guests: " + g.Count + "\n";
            s += "Number of Hosting Units: " + hu.Count + "\n";
            Info.Content = s;
            
        }

        private void Hosts_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new HostsInfo().ShowDialog();
        }

        private void Button_MouseEnter_RED(object sender, MouseEventArgs e)//change to when pressed red
        {
            ((Button)sender).Background = (Brush)Brushes.Red;
            ((Button)sender).Width *= 1.1;
            ((Button)sender).Height *= 1.1;


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
        private void Button_Click_CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new GuestsInfo().ShowDialog();
        }

        private void HostingUnit_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new HUinfo().ShowDialog();
        }

        private void order_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new OrederInfo().ShowDialog();
        }

    }
}
