using BO;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        OrderTracking orderTracking1 = new();
        public OrderTrackingWindow(OrderTracking orderTracking)
        {
            InitializeComponent();
            orderTrackingListBox.ItemsSource = orderTracking.OrderConditionWithDate;
            orderTracking1=orderTracking;
        }

        private void orderDetails_Click(object sender, RoutedEventArgs e)
        {
          new OrderWindow(orderTracking1.OrderId,true).ShowDialog();
        }
    }
}
