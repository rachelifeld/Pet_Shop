using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal
{
    internal class DalOrder : IOrder
    {
        public int Add(Order item)
        {
            var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>("Orders");
            if(item.orderId==0)
            {
               item.orderId = LoadFromConfigXML("OrderID");
            }
            listOrders.Add(item);
            XMLTools.SaveListToXMLSerializer(listOrders, "Orders");
            return item.orderId;
        }

        public void Delete(int id)
        {
            var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>("Orders");
            if (listOrders.RemoveAll(p => p.orderId == id) == 0)
                throw new Exception();
            XMLTools.SaveListToXMLSerializer(listOrders, "Orders");
        }

        public Order Get(int id)
        {
            DO.Order order = XMLTools.LoadListFromXMLSerializer<DO.Order>("Orders")
    .FirstOrDefault(p => p.orderId == id);
            if (order.orderId != 0)
            {
                return order;
            }
            throw new Exception();
        }

        public Order Get(Predicate<Order> func)
        {
            return XMLTools.LoadListFromXMLSerializer<DO.Order>("Orders").Find(func);
        }

        public IEnumerable<Order> ReadAll(Func<Order, bool>? func = null)
        {
            var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>("Orders");
            return func == null ? listOrders.OrderBy(oi => ((DO.Order)oi).orderId) :
            listOrders.Where(func).
                OrderBy(oi => ((DO.Order)oi).orderId);
        }

        public void Update(Order item)
        {
            Delete(item.orderId);
            Add(item);
        }
        public int LoadFromConfigXML(string type)
        {
            var elements = XDocument.Load(@"../xml/Config.xml")?.Root;
            var res = elements?.Element(type);
            int ID = Convert.ToInt32(res?.Value) + 1;
            if (elements != null)
            {
                res?.Remove();
                XElement xElement = new XElement(type, ID);
                elements.Add(xElement);
                elements.Save(@"../xml/Config.xml");
            }
            return ID;
        }
        }
    }
