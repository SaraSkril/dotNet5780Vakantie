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
    /// Interaction logic for UpdateOrderSpec.xaml
    /// </summary>
    public partial class UpdateOrderSpec : Window
    {
        public UpdateOrderSpec()
        {
            InitializeComponent();
        }
        Order order = new Order();
        string ID;
        public UpdateOrderSpec(int key,string id)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            ID = id;
            foreach (Order o in MainWindow.ibl.GetAllOrders())
            {
                if (o.OrderKey == key)
                {
                    order = o;
                    break;
                }

            }
            OKey.Content = order.OrderKey;
            HUKey.Content = order.HostingUnitKey;
            OGKey.Content = order.GuestRequestKey;
            CDate.Content = order.CreateDate;
            ODate.Content = order.OrderDate;
            stat.Text = order.Status.ToString();
            this.stat.ItemsSource = Enum.GetValues(typeof(BE.Status));

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            if (stat.SelectedItem != null) 
               order.Status = (Status)stat.SelectedItem;
            if ((Status)stat.SelectedItem == Status.Mail_Sent)
            {
                Close();
                new Mail(order).ShowDialog();
                
                return;
            }
            Host h = MainWindow.ibl.FindHost(ID);
           
            try
            {
                MainWindow.ibl.UpdateOrder(order,"","");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }
    }
}
