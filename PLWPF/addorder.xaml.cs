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
    /// Interaction logic for addorder.xaml
    /// </summary>
    public partial class addorder : Window
    {
        HostingUnit unit = new HostingUnit();
        string ID;
        public addorder()
        {
            InitializeComponent();
        }
        public addorder(string id)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ID = id;
            InitializeComponent();
        }

        private void select_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> hu = MainWindow.ibl.GetHubyHost(ID);
            var combo = sender as ComboBox;
            if (hu.Count == 0)
                hu.Add("No propertys found");
                   
            combo.ItemsSource = hu;
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
            Pool p;
            Garden ga;
            Jacuzzi j;
            ChildrensAttractions c;
            Wifi w;
            if (unit.pool)
                p = Pool.Interested;
            else
                p = Pool.NotIntersted;
            if (unit.Garden)
                ga = Garden.Interested;
            else
                ga = Garden.NotIntersted;
            if (unit.Jacuzzi)
                j = Jacuzzi.Interested;
            else
                j = Jacuzzi.NotIntersted;
            if (unit.ChildrensAttractions)
                c = ChildrensAttractions.Interested;
            else
                c = ChildrensAttractions.NotIntersted;
            if (unit.Wifi)
                w = Wifi.Interested;
            else
                w = Wifi.NotIntersted;


            var list = MainWindow.ibl.GetAllGuests(g => (g.Area == unit.area || g.Area == Area.All) && (g.TypeUnit == unit.TypeUnit) && (g.Pool == Pool.Maybe || g.Pool == p) &&
              (g.Garden == Garden.Maybe || g.Garden == ga) && (g.Jacuzzi == Jacuzzi.Maybe || g.Jacuzzi == j) && (g.ChildrensAttractions == ChildrensAttractions.Maybe || g.ChildrensAttractions == c)
              && (g.Wifi == Wifi.Maybe || g.Wifi == w) && (MainWindow.ibl.IsAvailible(unit, g.EntryDate, g.ReleaseDate)) && (g.GuestStatus == Status.Active) && (!MainWindow.ibl.checkifOrderExist(unit.HostingUnitKey, g.GuestRequestKey)));

            if (list == null)
                MessageBox.Show("Oops:( \n We couldnt find new guests to fit this unit");
            @try.ItemsSource = list;
            @try.Visibility = Visibility.Visible;
        }

        private void Guests_Loaded(object sender, RoutedEventArgs e)
        {
            List<Guest> l = MainWindow.ibl.GetAllGuests();
            var grid = sender as DataGrid;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            Order order = new Order();
          foreach(Guest guest in _selectedGuest)
            {
                flag = true;
                try
                {
                    order.GuestRequestKey = guest.GuestRequestKey;
                    order.HostingUnitKey = unit.HostingUnitKey;
                    MainWindow.ibl.AddOrder(order);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            if (flag)
            {
                MessageBox.Show("Your orders were created successfully!\n you will be able to update them in the update window");
                Close();
                new hostprop(ID).ShowDialog();
            }
            else
                MessageBox.Show("No orders were selected!");
            
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

        private  List<Guest> _selectedGuest = new List<Guest>();
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox ckhBox = sender as CheckBox;
            Guest checkedGuest= ckhBox.DataContext as Guest;
            if (!_selectedGuest.Contains(checkedGuest))
                _selectedGuest.Add(checkedGuest);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox ckhBox = sender as CheckBox;
            Guest checkedGuest = ckhBox.DataContext as Guest;
            if (_selectedGuest.Contains(checkedGuest))
                _selectedGuest.Remove(checkedGuest);
        }
    }
}
