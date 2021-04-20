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
    /// Interaction logic for UpdateHost.xaml
    /// </summary>
    public partial class UpdateHost : Window
    {
        Host h = new Host();
        List<int> bankNumberList;
        IEnumerable<IGrouping<int, BankAccount>> ba;
        IEnumerable<IGrouping<int, BankAccount>> branches;
        IEnumerable<BankAccount> br;
        public UpdateHost()
        {
            InitializeComponent();
        }
        public UpdateHost(string id)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            h = MainWindow.ibl.FindHost(id);
            bankNumberList = new List<int>();
            ba = MainWindow.ibl.GetBanksbyBankNumbers();
            foreach (var b in ba)
            {
                bankNumberList.Add(b.Key);
            }
            Bnumber.ItemsSource = bankNumberList;
            SetAll();
            

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

        public void SetAll()
        {
            ID.Content = h.ID;
            First_Name.Text = h.FirstName;
            Last_Name.Text = h.LastName;
            Email.Text = h.EmailAddress;
            number.Text = h.PhoneNumber.ToString();
            if (h.CollectionClearance == CollectionClearance.Yes)
                Yes.IsChecked = true;
            else
                No.IsChecked = true;
            {
                Bnumber.Text = h.BankDetails.BankNumber.ToString();
                Bname.Content = h.BankDetails.BankName.ToString();
                BRadrress.Content = h.BankDetails.BranchAddress.ToString();
                BRcity.Content = h.BankDetails.BranchCity.ToString();
                ActNum.Text = h.BankAccountNumber.ToString();
                BRnumber.Text = h.BankDetails.BranchNumber.ToString();
            }

        }

        private void Update_Host_Click(object sender, RoutedEventArgs e)
        {
            int number1;
            if (First_Name.Text == "")
            {
                First_Name.BorderBrush = Brushes.Red;
                return;
            }
            if (Last_Name.Text == "")
            {
                Last_Name.BorderBrush = Brushes.Red;
                return;
            }
            if (Email.Text == "")
            {
                Email.BorderBrush = Brushes.Red;
                return;
            }
            if (MainWindow.ibl.checkEmail(Email.Text) == false)
            {
                MessageBox.Show("Invalid Email");
                return;
            }

            if (int.TryParse(number.Text, out number1) == false)
            {

                MessageBox.Show("Invalid Number!");
                return;
            }
            //check if 9/10 digit
            if (Yes.IsChecked == false && No.IsChecked == false)
            {

                MessageBox.Show("Please select Automatic billing");
                return;
            }
            h.FirstName = First_Name.Text;
            h.LastName = Last_Name.Text;
            h.PhoneNumber = int.Parse(number.Text);
            if (Yes.IsChecked == true)
                h.CollectionClearance = CollectionClearance.Yes;
            else
                h.CollectionClearance = CollectionClearance.No;

            h.EmailAddress = Email.Text;
            {
                if (Bnumber.SelectedItem == null || BRnumber.SelectedItem == null || ActNum.Text == "" || int.TryParse(ActNum.Text, out number1) == false)
                    return;
            }
            h.BankAccountNumber = int.Parse(ActNum.Text);
            h.BankDetails.BankName = Bname.Content.ToString();
            h.BankDetails.BankNumber = System.Convert.ToInt32(Bnumber.SelectedItem.ToString());
            h.BankDetails.BranchAddress =BRadrress.Content.ToString();
            h.BankDetails.BranchCity =BRcity.Content.ToString();
            h.BankDetails.BranchNumber =int.Parse(BRnumber.SelectedItem.ToString());
            

            //bank
            try
            {
                MainWindow.ibl.UpdateHost(h);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Host " + First_Name.Text + " Was Updated Succefully!");
            Close();
            new hostprop(h.ID).ShowDialog();
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

        private void BRnumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BRcity.Visibility = Visibility.Visible;
            BRadrress.Visibility = Visibility.Visible;
            if (BRnumber.SelectedItem == null)
                return;
            int BankNumber = int.Parse(BRnumber.SelectedItem.ToString());//gets bank number that was chosen
            h.BankDetails.BranchNumber = BankNumber;
            foreach (var temp in branches)
            {
                if (temp.Key == BankNumber)
                {
                    BRcity.Content = temp.First().BranchCity;
                    BRadrress.Content = temp.First().BranchAddress;
                    h.BankDetails.BranchCity= temp.First().BranchCity;
                    h.BankDetails.BranchAddress= temp.First().BranchAddress;
                    break;
                }
            }
        }

        private void Bnumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bname.Visibility = Visibility.Visible;
            BRnumber.Visibility = Visibility.Visible;
            int BankNumber = int.Parse(Bnumber.SelectedItem.ToString());//gets bank number that was chosen

            br = null;
            foreach (var b in ba)//goes over banks
            {
                if (b.Key == BankNumber)
                {
                    br = b;//gets the bank accounts that belong to the bank number that was chosen
                    break;
                }

            }
            Bname.Content = br.First().BankName;//displays bank name according to chosen
            BRnumber.Text = "";
           
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
    }
}
