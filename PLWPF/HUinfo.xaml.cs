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
    /// Interaction logic for HUinfo.xaml
    /// </summary>
    public partial class HUinfo : Window
    {
        public HUinfo()
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
            List<HostingUnit> hu = new List<HostingUnit>();
            List<HostingUnit> ans = new List<HostingUnit>();
            hu = MainWindow.ibl.GetAllHostingUnits();
            if (FirstName.Text != "" )
            {
                foreach (HostingUnit G in hu)
                {
                    if (G.HostingUnitName.Contains(FirstName.Text) )
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
            opt.Add("Area");
            opt.Add("Resort Type");
            var combo = sender as ComboBox;
            combo.ItemsSource = opt;
        }

        private void Opt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var result = sender as ComboBox;
            string name = result.SelectedItem as string;
            FirstName.Text = "";
        
            if (name.Equals("Resort Type"))
            {
                COl.Visibility = Visibility.Collapsed;
                byUnit.Visibility = Visibility.Visible;
            }
            if (name.Equals("Area"))
            {
                byUnit.Visibility = Visibility.Collapsed;
                COl.Visibility = Visibility.Visible;
            }
        }



        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        
        private void COl_Loaded(object sender, RoutedEventArgs e)
        {

            this.COl.ItemsSource = Enum.GetValues(typeof(BE.Area));
        }

        private void COl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if ((Area)COl.SelectedItem != Area.All)
            {
                IEnumerable<HostingUnit> hu = new List<HostingUnit>();
                IEnumerable<IGrouping<Area, HostingUnit>> g = MainWindow.ibl.GetHUGroupsByArea();
                foreach (var g1 in g)
                {
                    if (g1.Key == (Area)COl.SelectedItem)
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
            else

            {
                IEnumerable<HostingUnit> hu = MainWindow.ibl.GetAllHostingUnits();

                if (hu.Count() == 0)
                    MessageBox.Show("Oops:( \n No results found , Please try again");
                HostList.ItemsSource = null;
                HostList.ItemsSource = hu;
            }
        }

        private void byUnit_Loaded(object sender, RoutedEventArgs e)
        {
            this.byUnit.ItemsSource= Enum.GetValues(typeof(BE.TypeUnit));
        }

        private void byUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<HostingUnit> hu = new List<HostingUnit>();
            IEnumerable<IGrouping<TypeUnit, HostingUnit>> g = MainWindow.ibl.GetHUGroupsByType();
            foreach (var g1 in g)
            {
                if (g1.Key == (TypeUnit)byUnit.SelectedItem)
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
