using BlApi;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    /// 
    public partial class OrderListWindow : Window
    {
        private static IBL bl = BlApi.Factory.Get();
        public OrderListWindow()
        {
            InitializeComponent();
            try
            {
                OrderListview.ItemsSource = bl.Order.GetOrders();
            }
            catch (Exception)
            {

                throw;
            }
        // DateSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        }

        private void DateSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OrderListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderForList order = (BO.OrderForList)OrderListview.SelectedItem;
            new OrderWindow(order.IdOrder,false).ShowDialog();
            try
            {
                OrderListview.ItemsSource = bl.Order.GetOrders();
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}



