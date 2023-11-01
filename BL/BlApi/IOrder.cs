using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<OrderForList> GetOrders();
        public Order GetOrdersDetails(int OrderId);
        public Order UpdateSent(int OrderId);
        public Order UpdateDelivered(int OrderId);
        public OrderTracking GetOrderTracking(int OrderId);
        public int LongestNotProcessed();
    }
}
