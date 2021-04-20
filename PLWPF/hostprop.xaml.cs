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
    /// Interaction logic for hostprop.xaml
    /// </summary>
    public partial class hostprop : Window
    {
        Host h;
        public hostprop()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
        public hostprop(string id)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            h = MainWindow.ibl.FindHost(id);
            welcome.Content = "Welcome " + h.FirstName + " " + h.LastName + "!";
           
         
        }
        
        private void Addhu_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new AddHostingUnit(h).ShowDialog();
        }

      

        private void Delete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = sender as ComboBox;
            string name = result.SelectedItem as string;
            HostingUnit unit = new HostingUnit();
            unit.HostingUnitName = name;
            if (name.Equals("No propertys found"))
                return;
            unit.HostingUnitKey = MainWindow.ibl.FindHostingUnit(name, h.ID).HostingUnitKey;
            try
            {
                MainWindow.ibl.DelHostingUnit(unit);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Oops!/n" + ex.Message);
                return;
            }
            MessageBox.Show(name + "  was removed succesfully!");
            Close();
        }

        private void Update_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> hu = MainWindow.ibl.GetHubyHost(h.ID);
            var combo = sender as ComboBox;
            if (hu.Count == 0)
                hu.Add("No propertys found");
            combo.ItemsSource = hu;
           
        }

        private void Update_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = sender as ComboBox;
            string name = result.SelectedItem as string;
            if (name.Equals("No propertys found"))
                return;
            Close();
            new UpdateHU(name).ShowDialog();
        }

        private void myorders_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> opt = new List<string>();
            opt.Add("Add Order");
            opt.Add("Update Order");
            var combo = sender as ComboBox;
            combo.ItemsSource = opt;
        }

        private void myorders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = sender as ComboBox;
            string opt1 = result.SelectedItem as string;
            if (opt1 == "Add Order")
            {
                Close();
                new addorder(h.ID).ShowDialog();
            }
            else
            {
                Close();
                new UpdateOrder(h.ID).ShowDialog();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new UpdateHost(h.ID).ShowDialog();
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

    }
}
