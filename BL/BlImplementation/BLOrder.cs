using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using DO;
using IOrder = BlApi.IOrder;

namespace BlImplementation
{
    internal class BLOrder : IOrder
    {
        private static DalApi.IDal dal = DalApi.Factory.Get()?? throw new Null();
        public IEnumerable<BO.OrderForList> GetOrders()
        {
            //IEnumerable<DO.Order> orders = new List<DO.Order>();
            IEnumerable<DO.Order> orders =dal.order.ReadAll();
            List<BO.OrderForList> orderForLists = new List<BO.OrderForList>();
            foreach (DO.Order order in orders)
            {
                IEnumerable<DO.OrderItem> ordersItems = new List<DO.OrderItem>();
                try
                {
                    ordersItems = dal.orderItem.ReadAll(element => element.orderId == order.orderId);
                }
                catch (Exception ex)
                {
                    throw new BO.DalException(ex);
                }

                BO.OrderForList tempOrderForList = new BO.OrderForList();
                foreach (var item in ordersItems)
                {
                    tempOrderForList.AmountProducts += item.amount;
                    tempOrderForList.TotalPrice += item.amount * item.priceForUnit;
                }

                tempOrderForList.IdOrder = order.orderId;
                tempOrderForList.CustomersName = order.clientName;
                if (order.dateDelivered == DateTime.MaxValue && order.dateSent == DateTime.MaxValue)
                    tempOrderForList.OrderCondition = BO.eCondition.OrderConfirmed;
                else if (order.dateDelivered == DateTime.MaxValue)
                    tempOrderForList.OrderCondition = BO.eCondition.OrderSent;
                else
                    tempOrderForList.OrderCondition = BO.eCondition.OrderSupllied;
                try
                {
                    orderForLists.Add(tempOrderForList);
                }
                catch (Exception ex)
                {
                    throw new BO.DalException(ex);
                }
            }
            //orders.ForEach(order =>
            //{
            //    IEnumerable<DO.OrderItem> ordersItems = new List<DO.OrderItem>();
            //    try
            //    {
            //        ordersItems = dal.orderItem.ReadAll(element => element.orderId == order.orderId);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new BO.DalException(ex);
            //    }

            //    BO.OrderForList tempOrderForList = new BO.OrderForList();
            //    foreach (var item in ordersItems)
            //    {
            //        tempOrderForList.AmountProducts += item.amount;
            //        tempOrderForList.TotalPrice += item.amount * item.priceForUnit;
            //    }

            //    tempOrderForList.IdOrder = order.orderId;
            //    tempOrderForList.CustomersName = order.clientName;
            //    if (order.dateDelivered == DateTime.MaxValue && order.dateSent == DateTime.MaxValue)
            //        tempOrderForList.OrderCondition = BO.eCondition.OrderConfirmed;
            //    else if (order.dateDelivered == DateTime.MaxValue)
            //        tempOrderForList.OrderCondition = BO.eCondition.OrderSent;
            //    else
            //        tempOrderForList.OrderCondition = BO.eCondition.OrderSupllied;
            //    try
            //    {
            //        orderForLists.Add(tempOrderForList);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new BO.DalException(ex);
            //    }
            //});
            return orderForLists;
        }

        public BO.Order GetOrdersDetails(int OrderId)
        {
            try
            {
                DO.Order orderData = dal.order.Get(OrderId);
                try
                {
                    IEnumerable<DO.OrderItem> orderItemsData = dal.orderItem.ReadAll(element => element.orderId == OrderId);
                    BO.Order order = new();

                    order.clientName = orderData.clientName;
                    order.clientEmail = orderData.clientEmail;
                    order.orderId = OrderId;
                    order.addressForDelivery = orderData.addressForDelivery;
                    order.dateOrdered = orderData.dateOrdered;
                    order.dateSent = orderData.dateSent;
                    order.dateDelivered = orderData.dateDelivered;
                    if (order.dateDelivered == DateTime.MaxValue && order.dateSent == DateTime.MaxValue)
                        order.orderCondition = BO.eCondition.OrderConfirmed;
                    else if (order.dateDelivered == DateTime.MaxValue)
                        order.orderCondition = BO.eCondition.OrderSent;
                    else
                        order.orderCondition = BO.eCondition.OrderSupllied;
                    List<BO.OrderItem> myOrderItems = new();
                    orderItemsData.ToList().ForEach(item => {
                        BO.OrderItem tmpOrderItems = new();
                        tmpOrderItems.amount = item.amount;
                        tmpOrderItems.itemName = dal.product.Get(item.orderItemId).productName;
                        tmpOrderItems.orderItemId = item.orderItemId;
                        tmpOrderItems.itemId = item.itemId;
                        tmpOrderItems.priceForUnit = dal.product.Get(item.orderItemId).productPrice;
                        tmpOrderItems.sumPrice = tmpOrderItems.amount * tmpOrderItems.priceForUnit;
                        order.totalPrice += tmpOrderItems.sumPrice;
                        myOrderItems.Add(tmpOrderItems);
                    });
                  
                    order.items = myOrderItems;
                    return order;
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }
        }

        public BO.Order UpdateSent(int OrderId)
        {
            try
            {
                IEnumerable<DO.OrderItem> orderItem = new List<DO.OrderItem>();
                DO.Order order = new();
                BO.Order BoOrder = new();
                order = dal?.order.Get(OrderId)??throw new Null();
                if (order.dateSent!=DateTime.MaxValue)
                {
                    throw new BO.OrderAlreadySent();
                }
                order.dateSent = DateTime.Now;
                orderItem = dal.orderItem.ReadAll(element => element.orderId == OrderId);
                BoOrder.clientName = order.clientName;
                BoOrder.clientEmail = order.clientEmail;
                BoOrder.orderId = OrderId;
                BoOrder.addressForDelivery = order.addressForDelivery;
                BoOrder.dateOrdered = order.dateOrdered;
                BoOrder.dateSent = order.dateSent;
                BoOrder.dateDelivered = order.dateDelivered;
                List<BO.OrderItem> tmpOrderItem = new();
                orderItem.ToList().ForEach(item => {
                    BO.OrderItem orderItemTemp = new();
                    orderItemTemp.amount = item.amount;
                    orderItemTemp.itemName = dal.product.Get(item.itemId).productName;
                    orderItemTemp.orderItemId = item.orderItemId;
                    orderItemTemp.itemId = item.itemId;
                    orderItemTemp.priceForUnit = dal.product.Get(item.itemId).productPrice;
                    orderItemTemp.sumPrice = orderItemTemp.amount * orderItemTemp.priceForUnit;
                    tmpOrderItem.Add(orderItemTemp);
                });
                BoOrder.items = tmpOrderItem;
                dal.order.Update(order);
                return BoOrder;
            }
            catch (Exception ex)
            {
                throw new BO.DalException(ex);
            }
        }

        public BO.Order UpdateDelivered(int OrderId)
        {
            try
            {
                IEnumerable<DO.OrderItem> orderItem = new List<DO.OrderItem>();
                DO.Order order = new();
                BO.Order BoOrder = new();

                order = dal?.order.Get(OrderId)??throw new Null();
                if (order.dateDelivered != DateTime.MaxValue)
                {
                    throw new BO.OrderAlreadyDelivered();
                }
                order.dateDelivered = DateTime.Now;
                orderItem = dal.orderItem.ReadAll(element => element.orderId == OrderId);
                BoOrder.clientName = order.clientName;
                BoOrder.clientEmail = order.clientEmail;
                BoOrder.orderId = OrderId;
                BoOrder.addressForDelivery = order.addressForDelivery;
                BoOrder.dateOrdered = order.dateOrdered;
                BoOrder.dateSent = order.dateSent;
                BoOrder.dateDelivered = order.dateDelivered;
                List<BO.OrderItem> tmpOrderItem = new();
                orderItem.ToList().ForEach(item => {
                    BO.OrderItem orderItemTemp = new();
                    orderItemTemp.amount = item.amount;
                    orderItemTemp.itemName = dal.product.Get(item.itemId).productName;
                    orderItemTemp.orderItemId = item.orderItemId;
                    orderItemTemp.itemId = item.itemId;
                    orderItemTemp.priceForUnit = dal.product.Get(item.itemId).productPrice;
                    orderItemTemp.sumPrice = orderItemTemp.amount * orderItemTemp.priceForUnit;
                });
                BoOrder.items = tmpOrderItem;
                dal.order.Update(order);
                return BoOrder;
            }
            catch (Exception ex)
            {
                throw new BO.DalException(ex);
            }
        }
        public OrderTracking GetOrderTracking(int OrderId)
        {
            DO.Order oDO;
            try
            {
                oDO = dal?.order.Get(OrderId)?? throw new Null();
            }
            catch (Exception ex)
            {
                throw new BO.DalException(ex);
            }
            BO.Order oBO = new BLOrder().GetOrdersDetails(OrderId);
            Dictionary<DateTime, eCondition> statusDictionary = new();
            if (oBO.orderCondition >= BO.eCondition.OrderConfirmed) statusDictionary.Add(oBO.dateOrdered, eCondition.OrderConfirmed);
            if (oBO.orderCondition >= BO.eCondition.OrderSupllied) statusDictionary.Add(oBO.dateDelivered, eCondition.OrderSupllied);
            if (oBO.orderCondition >= BO.eCondition.OrderSent) statusDictionary.Add(oBO.dateSent, eCondition.OrderSent);

            return new BO.OrderTracking()
            {
                OrderId = oBO.orderId,
                OrderCondition = (eCondition?)oBO.orderCondition ?? throw new Null(),
                OrderConditionWithDate = statusDictionary
            };

        }

        public int LongestNotProcessed()
        {
          
                lock (dal ?? throw new Exception())
                {
                    var result = (from order in GetOrders()
                                  where order.OrderCondition != eCondition.OrderSupllied
                                  select new
                                  {
                                      orderId = order.IdOrder,
                                      name = order.CustomersName,
                                      orderLastTreat = GetOrderTracking(order.IdOrder).OrderConditionWithDate?.Last().Key
                                  })
                             .OrderBy(x => x.orderLastTreat)
                             .FirstOrDefault();
                    return result == null ? 0 : result.orderId;
                }
            
        }
    }
}

