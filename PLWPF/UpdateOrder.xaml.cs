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
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        HostingUnit unit = new HostingUnit();
        string ID;
        public UpdateOrder()
        {
            InitializeComponent();
        }
        public UpdateOrder(string id)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            ID = id;

        }
        
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to leave?\n Your changes will not be saved!", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
                new hostprop(ID).ShowDialog();
            }
            else
            {
                // Do not close the window  
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to leave?\n Your changes will not be saved!", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
                new hostprop(ID).ShowDialog();
            }
            else
            {
                // Do not close the window  
            }
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

        private void select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = sender as ComboBox;
            string name = result.SelectedItem as string;
            if (name.Equals("No propertys found"))
                return;

            foreach (HostingUnit hosting in MainWindow.ibl.GetAllHostingUnits())
            {
                if (hosting.HostingUnitName == name)
                {
                    unit = hosting;
                    break;
                }
            }
            List<Order> ord = new List<Order>();
            foreach (Order order in MainWindow.ibl.GetAllOrders())
            {
                if (order.HostingUnitKey == unit.HostingUnitKey)
                    ord.Add(order);
            }
            if (ord.Count == 0)
                MessageBox.Show("Oops:( You do not have any open orders");
            @try.ItemsSource = ord;
            @try.Visibility = Visibility.Visible;
        }

        private void select_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> hu = MainWindow.ibl.GetHubyHost(ID);
            var combo = sender as ComboBox;
            if (hu.Count == 0)
                hu.Add("No propertys found");
            combo.ItemsSource = hu;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Order order = button.DataContext as Order;
            new UpdateOrderSpec(order.OrderKey,ID).ShowDialog();
            List<Order> ord1 = new List<Order>();
            foreach (Order order1 in MainWindow.ibl.GetAllOrders())
            {
                if (order1.HostingUnitKey == unit.HostingUnitKey)
                    ord1.Add(order1);
            }
            @try.ItemsSource = null;
            @try.ItemsSource = ord1;
        }
    }
}