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
    /// Interaction logic for OrederInfo.xaml
    /// </summary>
    public partial class OrederInfo : Window
    {
        public OrederInfo()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            byStatus.Visibility = Visibility.Collapsed;
           // Opt.Text = "";
           List<Order> hu = new List<Order>();
            List<Order> ans = new List<Order>();
            hu = MainWindow.ibl.GetAllOrders();
            if (FirstName.Text != "")
            {
                foreach (Order G in hu)
                {
                    if (G.OrderKey==int.Parse(FirstName.Text))
                        ans.Add(G);
                }

            }

            if (ans.Count == 0)
                MessageBox.Show("Oops:( \n No results found , Please try again");
            
            HostList.ItemsSource = null;
            HostList.ItemsSource = ans;

           
        }

        private void Opt_Loaded(object sender, RoutedEventArgs e)
        {

            List<string> opt = new List<string>();
            opt.Add("Status");
            opt.Add("Show All");
            var combo = sender as ComboBox;
            combo.ItemsSource = opt;
        }

        private void Opt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            var result = sender as ComboBox;
            string name = result.SelectedItem as string;
            FirstName.Text = "";

            if (name.Equals("Status"))
            {
                byStatus.Visibility = Visibility.Visible;
            }
            if (name.Equals("Show All"))
            {
                byStatus.Visibility = Visibility.Collapsed;
                List<Order> orders = MainWindow.ibl.GetAllOrders();
                HostList.ItemsSource = null;
                HostList.ItemsSource = orders;

            }
        }



        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


       

        private void byUnit_Loaded(object sender, RoutedEventArgs e)
        {
            this.byStatus.ItemsSource = Enum.GetValues(typeof(BE.Status));
        }

        private void byUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<Order> hu = new List<Order>();
            IEnumerable<IGrouping<Status, Order>> g = MainWindow.ibl.GetOrderByStatus();
            foreach (var g1 in g)
            {
                if (g1.Key == (Status)byStatus.SelectedItem)
                {
                    hu = g1;
                    break;
                }
            }
            if (hu.Count() == 0)
                MessageBox.Show("Oops:( \n No results found , Please try again");
            HostList.ItemsSource = null;
            HostList.ItemsSource = hu;
        }
    }
}

