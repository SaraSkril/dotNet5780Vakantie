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
    /// Interaction logic for NewHost.xaml
    /// </summary>
    public partial class NewHost : Window
    {
        
        List<int> bankNumberList;
        IEnumerable<IGrouping<int, BankAccount>> ba;
        IEnumerable<IGrouping<int, BankAccount>> branches;
        IEnumerable<BankAccount> br;
        public NewHost()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            bankNumberList = new List<int>();
            ba = MainWindow.ibl.GetBanksbyBankNumbers();
            foreach(var b in ba)
            {
                bankNumberList.Add(b.Key);
            }
            Bnumber.ItemsSource = bankNumberList;
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



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to leave?\n Your changes will not be saved!", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
            }
            else
            {
                // Do not close the window  
            }
        }

        private void Add_Host_Click(object sender, RoutedEventArgs e)
        {
            Host h = new Host();
            bool flag = true;

            if (ID.Text.Length != 9)
            {
                MessageBox.Show("Invalid ID!");
                return;
            }

           int number1 = 0;

            if (int.TryParse(ID.Text, out number1) == false)
            {

                MessageBox.Show("Invalid ID!");
                return;
            }
            if (First_Name.Text == "")
            {
                First_Name.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (Last_Name.Text == "")
            {
                Last_Name.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (Email.Text == "")
            {
                Email.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (MainWindow.ibl.checkEmail(Email.Text) == false)
            {
                MessageBox.Show("Invalid Email");
                return;
            }
          
                 if (int.TryParse(number.Text, out number1) == false)
            {
                number.BorderBrush = Brushes.Red;
                MessageBox.Show("Invalid Number!");
                return;
            }
                 //check if 9/10 digit
            if(Yes.IsChecked==false&&No.IsChecked==false)
            {

                MessageBox.Show("Please select Automatic billing");
                return;
            }
            if (Bnumber.SelectedItem == null || BRnumber.SelectedItem == null || ActNum.Text == "" || int.TryParse(ActNum.Text, out number1) == false)
                flag = false;
            if (!flag)
                return;
            h.ID = ID.Text;
            h.FirstName = First_Name.Text;
            h.LastName = Last_Name.Text;
            h.PhoneNumber = int.Parse(number.Text);
            if (Yes.IsChecked == true)
                h.CollectionClearance = CollectionClearance.Yes;
            else
                h.CollectionClearance = CollectionClearance.No;
            h.EmailAddress = Email.Text;
            {
                h.BankAccountNumber = int.Parse(ActNum.Text);
                BankAccount b = new BankAccount();
                b.BankName = Bname.Content.ToString();
                b.BankNumber = int.Parse(Bnumber.SelectedItem.ToString());
                b.BranchAddress = BRadrress.Content.ToString();
                b.BranchCity = BRcity.Content.ToString();
                b.BranchNumber = int.Parse(BRnumber.Text);
                h.BankDetails = b;
            }

            //bank
            try
            {
                MainWindow.ibl.addHost(h);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Host " + First_Name.Text + " was added succefully!");
            ID.Text = "";
            First_Name.Text = "";
            Last_Name.Text = "";
            Email.Text = "";
            Yes.IsChecked = false;
            No.IsChecked = false;
            number.Text = "";
            Close();
        }

        private void Bnumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bname.Visibility = Visibility.Visible;
            BRnumber.Visibility = Visibility.Visible;
            int BankNumber = int.Parse(Bnumber.SelectedItem.ToString());//gets bank number that was chosen

            br = null;
            foreach(var b in ba)//goes over banks
            {
                if (b.Key == BankNumber)
                {
                    br = b;//gets the bank accounts that belong to the bank number that was chosen
                    break;
                }
                    
            }
            Bname.Content = br.First().BankName;//displays bank name according to chosen
            branches = MainWindow.ibl.GetBanksbyBranchesNumbers(br);//groups accounts by branches of chosen bank
            List<int> branchnumer = new List<int>();
            #region getsBranch
            foreach (var branch in branches)
            {
                branchnumer.Add(branch.Key);
            }
            #endregion
            BRnumber.ItemsSource = branchnumer;
            BRcity.Content = "";
            BRadrress.Content = "";





            }

        private void BRnumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BRcity.Visibility = Visibility.Visible;
            BRadrress.Visibility = Visibility.Visible;
            if (BRnumber.SelectedItem == null)
                return;
            int BankNumber = int.Parse(BRnumber.SelectedItem.ToString());//gets bank number that was chosen
            foreach (var temp in branches)
            {
                if(temp.Key==BankNumber)
                {
                    BRcity.Content = temp.First().BranchCity;
                    BRadrress.Content = temp.First().BranchAddress;
                    break;
                }
            }
        }
    }
    }
