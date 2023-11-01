using BO;
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
using System.Windows.Shapes;

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartView.xaml
    /// 
    /// </summary>
    /// 
    public partial class CartView : Window
    {

        public CartView()
        {
            InitializeComponent();
            CartsListview.ItemsSource = ProductWindow.cart.ListOfOrderDetails;
            if (ProductWindow.cart?.ListOfOrderDetails?.Count==0)
            {
                checkOutBtn.IsEnabled = false;
            }
        }

        private void CartsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderItem orderItem = (BO.OrderItem)CartsListview.SelectedItem;
            new ProductWindow(orderItem.itemId,orderItem.amount).ShowDialog();
            Close();
            new CartView().ShowDialog();
        }

 

        private void checkOutBtn_Click(object sender, RoutedEventArgs e)
        {
            new checkOutWindow().ShowDialog();
            Close();
        }
    }
}
