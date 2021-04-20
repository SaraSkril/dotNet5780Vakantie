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
    /// Interaction logic for GuestsInfo.xaml
    /// </summary>
    public partial class GuestsInfo : Window
    {
        public GuestsInfo()
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
            List<Guest> Guests = new List<Guest>();
            List<Guest> ans = new List<Guest>();
            Guests = MainWindow.ibl.GetAllGuests();
            if (FirstName.Text != "" && LastName.Text != "")
            {
                string fn = (char.ToUpper(FirstName.Text[0]) + FirstName.Text.Substring(1));

                string ln = (char.ToUpper(LastName.Text[0]) + LastName.Text.Substring(1));
                foreach (Guest G in Guests)
                {
                    if (G.FirstName.Contains(FirstName.Text) || G.LastName.Contains(LastName.Text)|| G.FirstName.Contains(fn)||G.LastName.Contains(ln))
                        ans.Add(G);
                }

            }
            else
               if (FirstName.Text != "" && LastName.Text == "")
            {
                string fn = (char.ToUpper(FirstName.Text[0]) + FirstName.Text.Substring(1));
                foreach (Guest G in Guests)
                {
                    if (G.FirstName.Contains(FirstName.Text)||G.FirstName.Contains(fn))
                        ans.Add(G);
                }
            }
            else
                  if (FirstName.Text == "" && LastName.Text != "")
            {
                string ln = (char.ToUpper(LastName.Text[0]) + LastName.Text.Substring(1));
                foreach (Guest G in Guests)
                {
                    if (G.LastName.Contains(LastName.Text)||G.LastName.Contains(ln))
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
            opt.Add("Number Of Vacationers");
            var combo = sender as ComboBox;
            combo.ItemsSource = opt;
        }

        private void Opt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var result = sender as ComboBox;
            string name = result.SelectedItem as string;
            FirstName.Text = "";
            LastName.Text = "";
            if (name.Equals("Number Of Vacationers"))
            {
                ByNum.Text = "Please Enter a number";
                COl.Visibility = Visibility.Collapsed;
                ByNum.Visibility = Visibility.Visible;
                b1.Visibility = Visibility.Visible;
            }
            if (name.Equals("Area"))
            {
                ByNum.Visibility = Visibility.Collapsed;
                b1.Visibility = Visibility.Collapsed;
                COl.Visibility = Visibility.Visible;
            }
        }



        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string num = ByNum.Text;
            if (!int.TryParse(num, out int n))
            {
                MessageBox.Show("Please Enter a valid number");
                return;
            }
            int num1 = int.Parse(num);
            if (num1 < 0)
                MessageBox.Show("Please Enter a valid number");
            IEnumerable<Guest> guests = new List<Guest>();
            IEnumerable<IGrouping<int, Guest>> g = MainWindow.ibl.GetGuestsGroupsByVacationers();
            foreach (var g1 in g)
            {
                if (g1.Key == num1)
                {
                    guests = g1;
                    break;
                }
            }
            if (guests.Count() == 0)
                MessageBox.Show("Oops:( \n No results found , Please try again");
            HostList.ItemsSource = null;
            HostList.ItemsSource = guests;
        }

        private void COl_Loaded(object sender, RoutedEventArgs e)
        {

            this.COl.ItemsSource = Enum.GetValues(typeof(BE.Area));
        }

        private void COl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
            if ((Area)COl.SelectedItem!=Area.All)
            {
                IEnumerable<Guest> hosts = new List<Guest>();
                IEnumerable<IGrouping<Area, Guest>> g = MainWindow.ibl.GetGuestsGroupsByArea();
                foreach (var g1 in g)
                {
                    if (g1.Key == (Area)COl.SelectedItem)
                    {
                        hosts = g1;
                        break;
                    }
                }
                if (hosts.Count() == 0)
                    MessageBox.Show("Oops:( \n No results found , Please try again");
                HostList.ItemsSource = null;
                HostList.ItemsSource = hosts;
            }
            else
            
            {
                IEnumerable<Guest> guests = MainWindow.ibl.GetAllGuests();
             
                if (guests.Count() == 0)
                    MessageBox.Show("Oops:( \n No results found , Please try again");
                HostList.ItemsSource = null;
                HostList.ItemsSource = guests;
            }
        }

        private void ByNum_MouseEnter(object sender, MouseEventArgs e)
        {
            ByNum.Text = "";
        }
    }
}
