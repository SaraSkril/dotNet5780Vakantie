using System;
using System.Threading;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public static IBL ibl = FactoryBl.GetBL();
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.FlowDirection = FlowDirection.RightToLeft;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            SystemCommands.MaximizeWindow(this);
            callThreadToUpdateOrders();
            
        }

        public void callThreadToUpdateOrders()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (MainWindow.ibl.GetLastUpdated() < DateTime.Today)//only updates them if it didn't update today already
                    {

                        foreach (Order order in MainWindow.ibl.GetAllOrders())
                        {
                            if (order.OrderDate != default(DateTime) && MainWindow.ibl.DaysBetween(order.OrderDate) > 31 && order.Status == Status.Mail_Sent)
                            {
                                order.Status = Status.Closed_NoReply;
                                MainWindow.ibl.UpdateOrder(order, "", "");

                            }

                        }
                        MainWindow.ibl.UpdateLastUpdated();//updates the date 
                    }
                    Thread.Sleep(86400000);//sleeps for 24 hours
                }
            }).Start();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void Button_MouseEnter_RED(object sender, MouseEventArgs e)//change to when pressed red
        {
            ((Button)sender).Background = (Brush)Brushes.Red;
            ((Button)sender).Width *= 1.1;
            ((Button)sender).Height *=1.1;
            
            
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

        private void Add_Guest(object sender, RoutedEventArgs e)
        {
            new AddGuests().ShowDialog();
        }

        private void Add_Host(object sender, RoutedEventArgs e)
        {
            new HostLogin().ShowDialog();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            new AdminLogin().ShowDialog();
        }
    }
}
