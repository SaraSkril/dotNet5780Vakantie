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
using System.ComponentModel;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Mail.xaml
    /// </summary>
    public partial class Mail : Window
    {
        BackgroundWorker worker = new BackgroundWorker();
        Guest gu;
        HostingUnit hu;
        string t;
        string pic = "";
        Order ord;

        public Mail()
        {
            InitializeComponent();
            starttextbox();
        }
        public Mail(Order order)
        {
            InitializeComponent();
            
            ord = order;
            foreach(HostingUnit u in MainWindow.ibl.GetAllHostingUnits())
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                if (u.HostingUnitKey == ord.HostingUnitKey)
                {
                    hu = u;
                    break;
                }
            }
            starttextbox();
        }


        private void starttextbox()
        {
            
            foreach(Guest g in MainWindow.ibl.GetAllGuests())
            {
                if (g.GuestRequestKey == ord.GuestRequestKey)
                    gu = g;
            }

             t = "Hello " + gu.FirstName + "," + "\n";
            t += "Thank you for visiting Vakantie!\n";
            t += "My Hosting Unit - " + hu.HostingUnitName + "  fills your requirements and I'd be more than glad to host you." + "\n";
            t += "Please contact me at: " + hu.Owner.EmailAddress + " to follow up with your order." + "\n";
            t += "All the best, " + "\n";
            t+=hu.Owner.FirstName + " " + hu.Owner.LastName;
            main.Text = t;
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                pic = op.FileName;
            }
            imageloaded.Visibility = Visibility.Visible;
            imageloaded.Content = op.FileName;
            imageloaded.Background = Brushes.Beige;
        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
           /* t = main.Text;
            worker.DoWork += new DoWorkEventHandler(bw_DoWork);
            worker.WorkerReportsProgress = false;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            worker.RunWorkerAsync();*/



           try
            {
                MainWindow.ibl.UpdateOrder(ord, t, pic);
                Close();
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
                Close();
            }

        }
        public static void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        public void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => MainWindow.ibl.UpdateOrder(ord, t, pic)));
                MainWindow.ibl.UpdateOrder(ord, t, pic);
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
