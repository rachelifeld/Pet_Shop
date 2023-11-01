using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal class DalOrderItem : IOrderItem
    {
       public int Add(OrderItem item)
        {
            if (item.orderItemId == 0)//status of add
            {
                XElement? Config = XDocument.Load("../xml/Config.xml").Root;
                XElement? orderItemId = Config?.Element("OrderItem");
                int newId = Convert.ToInt32(orderItemId?.Value);
                item.orderItemId = newId++;
                orderItemId.Value = newId.ToString();
                Config?.Save("../xml/config.xml");
            }

            XElement ? OrdersItems = XDocument.Load("../xml/OrderItem.xml").Root;
            //var list = OrdersItems?.Elements().ToList().Where(orderItem => 
            //Convert.ToInt32(orderItem?.Element("ID")?.Value) == item.itemId);
            //if (list?.Count() > 0)
            //    throw new Exception();
            XElement? orderItem = new XElement("OrderItem");
            XElement? Id = new XElement("ID", item.orderItemId);
            orderItem.Add(Id);
            XElement? ProductId = new XElement("ProductID", item.itemId);
            orderItem.Add(ProductId);
            XElement? OrderID = new XElement("OrderID", item.orderId);
            orderItem.Add(OrderID);
            XElement? Price = new XElement("Price", item.priceForUnit);
            orderItem.Add(Price);
            XElement? Amount = new XElement("Amount", item.amount);
            orderItem.Add(Amount);
            OrdersItems?.Add(orderItem);
            OrdersItems?.Save("../xml/OrderItem.xml");
            return item.itemId;
        }

        public void Delete(int id)
        {
            XElement? OrdersItems = XDocument.Load("../xml/OrederItem.xml").Root;
            OrdersItems?.Elements().ToList().Find(orderItem => 
            Convert.ToInt32(orderItem?.Element("ID")?.Value) == id)?.Remove();
            OrdersItems?.Save("../xml/OrederItem.xml");
        }

        public OrderItem Get(int id)
        {
            XElement? OrdersItems = XDocument.Load("../xml/OrderItem.xml").Root;
            var found = OrdersItems?.Elements().ToList().Find(OrderItem => Convert.ToInt32(OrderItem?.Element("Id")?.Value) == id);
            if (found == null)
                throw new Exception();
            return new DO.OrderItem { itemId = Convert.ToInt32(found?.Element("ID")?.Value),
                orderItemId = Convert.ToInt32(found?.Element("ProductID")?.Value),
                orderId = Convert.ToInt32(found?.Element("OrderID")?.Value), 
                priceForUnit = Convert.ToInt32(found?.Element("Price")?.Value),
                amount = Convert.ToInt32(found?.Element("Amount")?.Value) };
        }

        public OrderItem Get(Predicate<OrderItem> func)
        {
            IEnumerable<DO.OrderItem> lst = ReadAll();
            return lst.ToList().Find(func);
        }

        public OrderItem ProductItemByOrderIDProductID(int orderId, int productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> ReadAll(Func<OrderItem, bool>? func=null)
        {
            XElement? OrdersItems = XDocument.Load("../xml/OrderItem.xml").Root;
            List<DO.OrderItem> lst = new List<DO.OrderItem> { };
            OrdersItems?.Elements().ToList().ForEach(o =>
            {
                lst.Add(new DO.OrderItem { itemId = Convert.ToInt32(o?.Element("ID")?.Value),
                    orderItemId = Convert.ToInt32(o?.Element("ProductID")?.Value),
                    orderId = Convert.ToInt32(o?.Element("OrderID")?.Value),
                    priceForUnit = Convert.ToInt32(o?.Element("Price")?.Value),
                    amount = Convert.ToInt32(o?.Element("Amount")?.Value) });
            });
           if(lst.Count > 0) { }
            return func == null ? lst : lst.Where(func);
        }

        public List<OrderItem> ReadAllProductInOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderItem item)
        {
            throw new NotImplementedException();
        }
    }
}
