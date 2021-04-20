using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
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
    /// Interaction logic for AddHostingUnit.xaml
    /// </summary>
    public partial class AddHostingUnit : Window
    {
        Host h;
        HostingUnit hu = new HostingUnit();
        public AddHostingUnit()
        {
            InitializeComponent();
        }
        public AddHostingUnit(Host host)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            h = host;
            this.Resort.ItemsSource = Enum.GetValues(typeof(BE.TypeUnit));
            this.Area.ItemsSource = Enum.GetValues(typeof(BE.Area));
            
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width /= 1.1;
            ((Button)sender).Height /= 1.1;

        }

        private void Button_MouseEnter_RED(object sender, MouseEventArgs e)
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
        private void Button_Click_CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_MaximizeWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                SystemCommands.RestoreWindow(this);
            else
                SystemCommands.MaximizeWindow(this);

        }

        private void Button_Click_MinimizeWindow(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (Name.Text == "")
            {
                Name.BorderBrush = Brushes.Red;
                flag=false;
            }
            hu.HostingUnitName = Name.Text;
            hu.Owner = h;
            if (MainWindow.ibl.HostingUnitExist(hu))
                {
                   MessageBox.Show("Oops!/n  " + hu.HostingUnitName + " already exists");
                return;
              }

            if (Area.SelectedItem != null && Resort.SelectedItem != null)
            {
                if ((Yes.IsChecked == true || No.IsChecked == true) && (Yes2.IsChecked == true || No2.IsChecked == true)&& (Yes3.IsChecked == true || No3.IsChecked == true)&& (Yes4.IsChecked == true || No4.IsChecked == true)&& (Yes5.IsChecked == true || No5.IsChecked == true))
                {
                   
                                    hu.Owner = h;
                                    hu.area = (Area)Area.SelectedItem;
                                    hu.TypeUnit = (TypeUnit)Resort.SelectedItem;
                                    hu.pool = (bool)Yes.IsChecked;
                                    hu.Jacuzzi = (bool)Yes2.IsChecked;
                                    hu.Garden = (bool)Yes3.IsChecked;
                                    hu.ChildrensAttractions = (bool)Yes4.IsChecked;
                                    hu.Wifi = (bool)Yes5.IsChecked;
                       
                }
                else
                {
                    MessageBox.Show("Please select all fields!");
                    return;
                }

            }
            else
            {
                MessageBox.Show("Please select all fields!");
                return;
            }
            if (flag)
            {
                try
                {
                    MainWindow.ibl.AddHostingUnit(hu);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                MessageBox.Show("Hosting Unit " + Name.Text + " was added succesfully!");
                Name.BorderBrush = Brushes.Gray;
                Name.Text = "";
                Resort.Text = "Choose Resort";
                Area.Text = "Choose Area";
                Yes.IsChecked = false;
                No.IsChecked = false;
                Yes2.IsChecked = false;
                No2.IsChecked = false;
                Yes3.IsChecked = false;
                No3.IsChecked = false;
                Yes4.IsChecked = false;
                No4.IsChecked = false;
                Yes5.IsChecked = false;
                No5.IsChecked = false;
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to leave?\n Your changes will not be saved!", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
                new hostprop(h.ID).ShowDialog();
            }
            else
            {
                // Do not close the window  
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Name.BorderBrush = Brushes.Gray;
        }

       
    }
}

