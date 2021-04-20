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
using BL;
using BE;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UpdateHU.xaml
    /// </summary>
    public partial class UpdateHU : Window
    {
        HostingUnit hu = new HostingUnit();
        public UpdateHU()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
        public UpdateHU(string name)
        { foreach (HostingUnit temp in MainWindow.ibl.GetAllHostingUnits())
            {
                if (temp.HostingUnitName == name)
                {
                    hu = temp;
                    break;
                }
            }
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.Resort.ItemsSource = Enum.GetValues(typeof(BE.TypeUnit));
            this.Area.ItemsSource = Enum.GetValues(typeof(BE.Area));
            Name.Text= hu.HostingUnitName;
            Area.Text = hu.area.ToString();
            Resort.Text = hu.TypeUnit.ToString();
                if (hu.pool)
                    Yes.IsChecked = true;
                else
                    No.IsChecked = true;
                if (hu.Jacuzzi)
                    Yes2.IsChecked = true;
                else
                    No2.IsChecked = true;
                if (hu.Garden)
                    Yes3.IsChecked = true;
                else
                    No3.IsChecked = true;
                if (hu.ChildrensAttractions)
                    Yes4.IsChecked = true;
                else
                    No4.IsChecked = true;
                if (hu.Wifi)
                    Yes5.IsChecked = true;
                else
                    No5.IsChecked = true;



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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "")
            {
                Name.BorderBrush = Brushes.Red;
                return;
            }
           
            if (Area.SelectedItem != null && Resort.SelectedItem != null)
            {
                if ((Yes.IsChecked == true || No.IsChecked == true) && (Yes2.IsChecked == true || No2.IsChecked == true) && (Yes3.IsChecked == true || No3.IsChecked == true) && (Yes4.IsChecked == true || No4.IsChecked == true) && (Yes5.IsChecked == true || No5.IsChecked == true))
                {
                    hu.HostingUnitName = Name.Text;
                   
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
            try
            {
                MainWindow.ibl.UpdateHostUnit(hu);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Hosting Unit " + Name.Text + " was updated succesfully!");
            Close();
            new hostprop(hu.Owner.ID).ShowDialog();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to leave?\n Your changes will not be saved!", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
                new hostprop(hu.Owner.ID).ShowDialog();
            }
            else
            {
                // Do not close the window  
            }
        }
    }
    
    
}