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

public class DalOrderItem:IOrderItem
{


    public int Add(DO.OrderItem orderItem)
    {
        orderItem.orderItemId = config.OrderItemId;
        listOrderItem.Add(orderItem);
        return orderItem.orderId;
    }

    public DO.OrderItem Get(int orderItemId)
    {
        for (int i = 0; i < listOrderItem.Count-1; i++)
        {
            if (listOrderItem[i].orderItemId == orderItemId)
                return listOrderItem[i];
        }
        throw new ObjectNotFound();
    }

    public void Delete(int orderItemId)
    {
        for (int i = 0; i < listOrderItem.Count - 1; i++)
        {
            if (listOrderItem[i].orderItemId == orderItemId)
            {
                listOrderItem.RemoveAt(i);
                return;
            }

        }
    }

    public void Update(DO.OrderItem orderItem)
    {
        int i;
        for (i = 0; i < listOrderItem.Count - 1; i++)
        {
            if (listOrderItem[i].orderItemId == orderItem.orderItemId)
            {
                listOrderItem[i] = orderItem;
                return;
            }
        }
        if (i == listOrderItem.Count)
            throw new ObjectNotFound();
    }
    //public IEnumerable<OrderItem> ReadAll()
    //{
    //    List <DO.OrderItem> allOrderItems = new ();
    //    for (int i = 0; i < listOrderItem.Count-1; i++)
    //    {
    //        allOrderItems.Add( listOrderItem[i]);
    //    }
    //    return allOrderItems;
    //}


    //function to  read all the orderProducts in specific order
    //public List<int> ReadAllProductInOrder(int orderId)
    //{
    //    //try
    //    //{
    //    //    Order.Get(orderId);
    //    //}
    //    //catch (Exception)
    //    //{

    //    //    throw new ObjectNotFound();
    //    //}
    //    List<int> _arrOrderProductsToReturn = new();
    //    for (int i = 0, j = 0; i < config.OrderItemId; i++)
    //    {
    //        if (listOrderItem[i].orderId == orderId)
    //        {
    //            _arrOrderProductsToReturn.Add(listOrderItem[i].orderItemId);
    //            j++;
    //        }   
    //    }
    //    return _arrOrderProductsToReturn;
    //}
    public List<OrderItem> ReadAllProductInOrder(int orderId)
    {
       
            int i;
            List<OrderItem> itemsInOrderArr = new List<OrderItem>();
            for (i = 0; i < listOrderItem.Count; i++)
            {
                if (listOrderItem[i].orderId == orderId)
                {
                    itemsInOrderArr.Add(listOrderItem[i]);
                }
            }
            //if (itemsInOrderArr.Count == 0)
            //{
            //    throw new ObjectNotFound();
            //}
            return itemsInOrderArr;
    }
    //function to read order product by order id and  product id
    public DO.OrderItem ProductItemByOrderIDProductID(int orderId, int productId)
    {
        try
        {
            Get(orderId);
        }
        catch (Exception)
        {

            throw new ObjectNotFound();
        }
        try
        {
            Get(productId);
        }
        catch (Exception)
        {

            throw new ObjectNotFound();
        }
        for (int i = 0; i < config.OrderItemId; i++)
        {
            if (listOrderItem[i].orderId == orderId && listOrderItem[i].itemId == productId)
            {
                return listOrderItem[i];
            }

        }
        throw new ObjectNotFound();

    }
    public OrderItem Get(Predicate<OrderItem> p)
    {
        return listOrderItem.Find(p);
    }
    public IEnumerable<OrderItem> ReadAll(Func<OrderItem, bool>? func = null)
    {
        return (func == null) ? listOrderItem : listOrderItem.Where(func);
    }


}
