//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using DalApi;
using DO;
using System;
using static Dal.DataSource;

namespace Dal;

public class DalOrder : IOrder
{
     
    public int Add(DO.Order order)
    {
        order.orderId = config.OrderId;
        listOrder.Add(order);
        return order.orderId;
    }
    public DO.Order Get(int orderId)
    {
        Order order = listOrder.Find(item => item.orderId == orderId);
        if(order.orderId<0)
            throw new ObjectNotFound();
        return order;
        //for (int i = 0; i < listOrder.Count-1; i++)
        //{
        //    if (listOrder[i].orderId == orderId)
        //        return listOrder[i];
        //}
        
    }

    public void Delete(int orderId)
    {
        Order order = listOrder.Find(order => order.orderId == orderId);
        if (order.orderId < 0)
            throw new ObjectNotFound();
        listOrder.Remove(order);

        //for (int i = 0; i < listOrder.Count-1; i++)
        //{
        //    if (listOrder[i].orderId == orderId)
        //    {
        //       listOrder.RemoveAt(i);   
        //        return;
        //    }
        //}
        //throw new ObjectNotFound();
    }

    public void Update(DO.Order order)
    {
        int i;
        for (i = 0; i < listOrder.Count - 1 ; i++)
        {
            if (listOrder[i].orderId == order.orderId)
            {
                listOrder[i] = order;
                return;
            }
        }
        if (i == listOrder.Count)
            throw new ObjectNotFound();
    }
    //public IEnumerable<Order> ReadAll()
    //{
    //    List<DO.Order> allOrders = new();
    //    for (int i = 0; i < listOrder.Count - 1 ; i++)
    //    {
    //        allOrders.Add( listOrder[i]);
    //    }
    //    if (allOrders!=null)
    //        return allOrders;
    //    throw new Exception("no orders");
    //}

    public Order Read(int orderId)
    {
        
        throw new NotImplementedException();
    }
    public Order Get(Predicate<Order> P)
    {
        return listOrder.Find(P);
    }
    public IEnumerable<Order> ReadAll(Func<Order, bool>? func = null)
    {
        return (func == null) ? listOrder : listOrder.Where(func);
    }

}
