using PL.Product;
using PL.Order;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for adminWindow.xaml
    /// </summary>
    public partial class adminWindow : Window
    {
        public adminWindow()
        {
            InitializeComponent();
        }

        private void GoToProductsList_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow("Add New Product").ShowDialog();
        }

        private void GoToOrdersList_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow().ShowDialog();
        }
    }
}
