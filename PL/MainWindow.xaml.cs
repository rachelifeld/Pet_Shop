using BlApi;
using BlImplementation;
using BO;
using PL.Order;
using PL.Product;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static IBL bl = new Bl();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new adminWindow().ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow("newOrder").ShowDialog();
            //new Order.newOrder().ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
                try
                {
                    BO.OrderTracking orderTracking = bl.Order.GetOrderTracking(Convert.ToInt32(trackingNumber.Text));
                    new OrderTrackingWindow(orderTracking).ShowDialog();
                }
                catch (Exception)
                {
                MessageBox.Show(" Invalid id");
            }
        }
            
         
        

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new OrderListWindow().ShowDialog();
        }

        private void Simulator_btn_Click(object sender, RoutedEventArgs e)
        {
            new SimulatorWindow().Show();
        }
    }
}
