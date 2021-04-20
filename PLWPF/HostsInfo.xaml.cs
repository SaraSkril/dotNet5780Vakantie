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
    /// Interaction logic for HostsInfo.xaml
    /// </summary>
    public partial class HostsInfo : Window
    {
        public HostsInfo()
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
            List<Host> hosts = new List<Host>();
            List<Host> ans = new List<Host>();
            hosts = MainWindow.ibl.GetAllHosts();
            if (FirstName.Text != "" && LastName.Text != "" )
            {
               
                string fn = (char.ToUpper(FirstName.Text[0]) + FirstName.Text.Substring(1));
                
                 string ln = (char.ToUpper(LastName.Text[0]) + LastName.Text.Substring(1));
                foreach (Host h in hosts)
                {
                    if (h.FirstName.Contains(FirstName.Text) || h.LastName.Contains(LastName.Text) || h.FirstName.Contains(fn)||h.LastName.Contains(ln))
                        ans.Add(h);
                }

            }
            else
               if (FirstName.Text != "" && LastName.Text == "" )
            {
                string fn = (char.ToUpper(FirstName.Text[0]) + FirstName.Text.Substring(1));
                foreach (Host h in hosts)
                {
                    if (h.FirstName.Contains(FirstName.Text)||h.FirstName.Contains(fn))
                        ans.Add(h);
                }
            }
            else
                  if (FirstName.Text == "" && LastName.Text != "" )
            {
                string ln = (char.ToUpper(LastName.Text[0]) + LastName.Text.Substring(1));
                foreach (Host h in hosts)
                {
                    if (h.LastName.Contains(LastName.Text)||h.LastName.Contains(ln))
                        ans.Add(h);
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
            opt.Add("Number of Hosting Units");
            opt.Add("Automatic billing");
            var combo = sender as ComboBox;
            combo.ItemsSource = opt;
        }

        private void Opt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var result = sender as ComboBox;
            string name = result.SelectedItem as string;
            FirstName.Text = "";
            LastName.Text = "";
            if (name.Equals("Number of Hosting Units"))
            {
                ByNum.Text = "Please Enter a number";
                COl.Visibility = Visibility.Collapsed;
                ByNum.Visibility = Visibility.Visible;
                b1.Visibility = Visibility.Visible;
            }
            if (name.Equals("Automatic billing"))
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
            IEnumerable<Host> hosts = new List<Host>();
            IEnumerable<IGrouping<int, Host>> g = MainWindow.ibl.GetHostsGroupsByHostingUnits();
            foreach (var g1 in g)
            {
                if (g1.Key == num1)
                {
                    hosts = g1;
                    break;
                }
            }
            if(hosts.Count()==0)
                MessageBox.Show("Oops:( \n No results found , Please try again");
            HostList.ItemsSource = null;
            HostList.ItemsSource = hosts;
        }

        private void COl_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> opt = new List<string>();
            opt.Add("Yes");
            opt.Add("No");
            var combo = sender as ComboBox;
            combo.ItemsSource = opt;
        }

        private void COl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = sender as ComboBox;
            string name = result.SelectedItem as string;
            if (name.Equals("Yes"))
            {
                IEnumerable<Host> hosts = new List<Host>();
                IEnumerable<IGrouping<CollectionClearance, Host>> g = MainWindow.ibl.GetHostsGroupsByClearance();
                foreach (var g1 in g)
                {
                    if (g1.Key == CollectionClearance.Yes)
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
            if (name.Equals("No"))
            {
                IEnumerable<Host> hosts = new List<Host>();
                IEnumerable<IGrouping<CollectionClearance, Host>> g = MainWindow.ibl.GetHostsGroupsByClearance();
                foreach (var g1 in g)
                {
                    if (g1.Key == CollectionClearance.No)
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
        }

        private void ByNum_MouseEnter(object sender, MouseEventArgs e)
        {
            ByNum.Text = "";
        }
    }
}
