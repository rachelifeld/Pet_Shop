using PL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for checkOutWindow.xaml
    /// </summary>
    public partial class checkOutWindow : Window
    {
        BlApi.IBL bl = BlApi.Factory.Get();
        public checkOutWindow()
        {
            InitializeComponent();          
        }

        private void OrderIDtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
     
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e )
        {
            try
            {
                bl.Cart.CheckOut(ProductWindow.cart, ClientNametxt.Text, ClientEmailtxt.Text, AddressForDeliverytxt.Text);
                ProductWindow.cart = new();
                MessageBox.Show("thank you for shopping at D&F pet shop!");
            }
            catch (Exception)
            {

                throw;
            }
            Close();
        }
    }
}
