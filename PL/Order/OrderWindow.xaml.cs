using BlApi;
using BlImplementation;
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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private static IBL bl = BlApi.Factory.Get();
        BO.Order order = new();
        public OrderWindow(int OrderId, bool isTracking)
        {
            InitializeComponent();
            try
            {
                order = bl.Order.GetOrdersDetails(OrderId);
                ID_order.Text = order.orderId.ToString();
                ID_order.IsEnabled = false;
                CustomerNameTextBox.Text = order.clientName?.ToString();
                CustomerNameTextBox.IsEnabled = false;
                orderCondition.Text = order.orderCondition.ToString();
                orderCondition.IsEnabled = false;
                TotalPriceTextBox.Text = order.totalPrice.ToString();
                TotalPriceTextBox.IsEnabled = false;
                OrderItemListView.ItemsSource = order.items;
                txtCustomerEmail.Text = order.clientEmail;
                txtCustomerEmail.IsEnabled = false;
                txtCustomerAddress.Text = order.addressForDelivery;
                txtCustomerAddress.IsEnabled = false;
                txtDeliveryDate.Text = order.dateDelivered.ToShortDateString();
                txtDeliveryDate.IsEnabled = false;
                txtOrderDate.Text = order.dateOrdered.ToShortDateString();
                txtOrderDate.IsEnabled = false;
                txtsentDate.Text = order.dateSent.ToShortDateString().ToString();
                txtsentDate.IsEnabled = false;
                update_delivered_btn.IsEnabled = false;
                update_sent_btn.IsEnabled = false;
                totalPrice.Text=order.totalPrice.ToString();
                totalPrice.IsEnabled = false;   
                if (order.dateDelivered == DateTime.MaxValue)
                {
                    txtDeliveryDate.Text = "";
                    update_delivered_btn.IsEnabled = true;
                }
                if (order.dateSent == DateTime.MaxValue)
                {
                    txtsentDate.Text = "";
                    update_sent_btn.IsEnabled = true;
                }
                if (isTracking)
                {
                    update_delivered_btn.Visibility = Visibility.Hidden;
                    update_sent_btn.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void update_delivered_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.UpdateDelivered(order.orderId);
                txtDeliveryDate.Text = DateTime.Now.ToShortDateString();
                txtDeliveryDate.IsEnabled = false;
                orderCondition.Text = BO.eCondition.OrderSupllied.ToString();
                update_delivered_btn.IsEnabled = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void update_sent_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                update_delivered_btn.IsEnabled = true;
                bl.Order.UpdateSent(order.orderId);
                txtsentDate.Text = DateTime.Now.ToShortDateString();
                txtsentDate.IsEnabled = false;
                orderCondition.Text = BO.eCondition.OrderSent.ToString();
                update_sent_btn.IsEnabled = false;
            }
            catch (Exception)
            {

                throw;
            }
      
        }

        private void OrderItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
