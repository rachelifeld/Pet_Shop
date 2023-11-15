// See https://aka.ms/new-console-template for more information

namespace Dal;

using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

enum type
{
    order, orderItem, product
}
public class Program
{
    //private static DalList dalList = new DalList();
    //private static DalOrder dalOrder = new DalOrder();
    //private static DalOrderItem dalOrderItem = new DalOrderItem();
    //private static DalProduct dalProduct = new DalProduct();
    private static IDal ? dalList = DalApi.Factory.Get();

    public static void Main(string[] args)
    {
        Console.WriteLine("Please enter " +
            "\n0 for exit " +
            "\n1 for product " +
            "\n2 for order " +
            "\n3 for orderitem");
        int choice;
        int.TryParse(Console.ReadLine(), out choice);
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:
                    _product();
                    break;
                case 2:
                    _order();
                    break;
                case 3:
                    _orderItem();
                    break;
            }
            Console.WriteLine(
                "Please enter " +
                "\n0 for exit " +
                "\n1 for product " +
                "\n2 for order" +
                "\n3 for orderitem");

            int.TryParse(Console.ReadLine(), out choice);
        }

    }

    public static void _product()
    {
        Console.WriteLine("Please enter " +
    "\na to add one product" +
    "\nb to present one product" +
    "\nc to present all product" +
    "\nd to update product product" +
    "\ne to delete product product");
        char chosenAction;
        char.TryParse(Console.ReadLine(), out chosenAction);
        switch (chosenAction)
        {
            case 'a':
                //add product
                DO.Product product = new DO.Product();
                Console.WriteLine("please enter the products name");
                string? name = Console.ReadLine();
                product.productName = name;
                Console.WriteLine("please enter the products category:" +
                    "\n 0 for cats" +
                    "\n 1 for dogs" +
                    "\n 2 for fish" +
                    "\n 3 for snakes ");
                int productCategory;
                int.TryParse(Console.ReadLine(), out productCategory);
                product.productCategory = (eCategory)productCategory;
                Console.WriteLine("please enter the products id");
                int productId;
                int.TryParse(Console.ReadLine(), out productId);
                product.productId = productId;
                Console.WriteLine("please enter the products price");
                int productPrice;
                int.TryParse(Console.ReadLine(), out productPrice);
                product.productPrice = productPrice;
                Console.WriteLine("please enter the products amount in stock");
                int productAmountInStock;
                int.TryParse(Console.ReadLine(), out productAmountInStock);
                product.productAmountInStock = productAmountInStock;
                try
                {
                    dalList.product.Add(product);
                }
                catch (NoMoreSpaceInArray ex)
                {
                    Console.WriteLine(ex.Message);
                }

                break;
            case 'b':
                //present product by id
                Console.WriteLine("enter id");
                int readProductId;
                int.TryParse(Console.ReadLine(), out readProductId);
                try
                {
                    Product product1 = dalList.product.Get(readProductId);
                    Console.WriteLine(product1);
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'c':
                //present all products
                try
                {
                    IEnumerable<Product> allProducts = dalList.product.ReadAll();
                    foreach (var item in allProducts)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (ObjectNotFound ex)
                {

                    Console.WriteLine(ex.Message);
                }

                break;
            case 'd':
                //update product
                DO.Product updateProduct = new DO.Product();
                Console.WriteLine("please enter the products id");
                int productId1;
                int.TryParse(Console.ReadLine(), out productId1);
                Console.WriteLine(dalList.product.Get(productId1));
                Console.WriteLine("please enter the products name");
                updateProduct.productName = Console.ReadLine();
                Console.WriteLine("please enter the products category:" +
                    "\n 0 for cats" +
                    "\n 1 for dogs" +
                    "\n 2 for fish" +
                    "\n 3 for snakes ");
                int productCategory1;
                int.TryParse(Console.ReadLine(), out productCategory1);
                updateProduct.productCategory = (eCategory)productCategory1;
                Console.WriteLine("please enter the products id");
                int productId2;
                int.TryParse(Console.ReadLine(), out productId2);
                updateProduct.productId = productId2;
                Console.WriteLine("please enter the products price");
                int productPrice1;
                int.TryParse(Console.ReadLine(), out productPrice1);
                updateProduct.productPrice = productPrice1;
                Console.WriteLine("please enter the products amount in stock");
                int productAmountInStock1;
                int.TryParse(Console.ReadLine(), out productAmountInStock1);
                updateProduct.productAmountInStock = productAmountInStock1;
                try
                {
                    dalList.product.Update(updateProduct);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                break;
            case 'e':
                //delete product
                try
                {
                    Console.WriteLine("enter id");
                    int deleteProductId;
                    int.TryParse(Console.ReadLine(), out deleteProductId);
                    dalList.product.Delete(deleteProductId);
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

        }
    }
    private static void _order()
    {
        Console.WriteLine("Please enter " +
"\na to add one order" +
"\nb to present one order" +
"\nc to present all order" +
"\nd to update product order" +
"\ne to delete product order");
        char chosenAction;
        char.TryParse(Console.ReadLine(), out chosenAction);
        switch (chosenAction)
        {
            case 'a':
                //add order
                DO.Order order = new DO.Order();
                Console.WriteLine("please enter clientName");
                order.clientName = Console.ReadLine();
                Console.WriteLine("please enter clientEmail");
                order.clientEmail = Console.ReadLine();
                Console.WriteLine("please enter address For Delivery");
                order.addressForDelivery = Console.ReadLine();
                Console.WriteLine("please enter date ordered");
                order.dateOrdered = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("please enter date sent");
                order.dateSent = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("please enter date delivered");
                order.dateDelivered = Convert.ToDateTime(Console.ReadLine());
                try
                {

                    dalList.order.Add(order);
                }
                catch (NoMoreSpaceInArray ex)
                {

                    Console.WriteLine(ex.Message);
                }
                break;
            case 'b':
                //present order by id
                Console.WriteLine("enter id");
                int readOrderId;
                int.TryParse(Console.ReadLine(), out readOrderId);
                try
                {
                    Order order1 = dalList.order.Get(readOrderId);
                    Console.WriteLine(order1);
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'c':
                try
                {
                    IEnumerable<Order> allOrders = new List<Order>();
                    allOrders = dalList.order.ReadAll();
                    foreach (var item in allOrders)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
                break;
            case 'd':
                //update order
                DO.Order updateOrder = new DO.Order();
                Console.WriteLine("please enter oreders id");
                int orderId;
                int.TryParse(Console.ReadLine(), out orderId);
                Console.WriteLine(dalList.order.Get(orderId));
                Console.WriteLine("please enter clientName");
                updateOrder.clientName = Console.ReadLine();
                Console.WriteLine("please enter clientEmail");
                updateOrder.clientEmail = Console.ReadLine();
                Console.WriteLine("please enter address For Delivery");
                updateOrder.addressForDelivery = Console.ReadLine();
                Console.WriteLine("please enter date ordered");
                updateOrder.dateOrdered = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("please enter date sent");
                updateOrder.dateSent = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("please enter date delivered");
                updateOrder.dateDelivered = Convert.ToDateTime(Console.ReadLine());

                try
                {
                    dalList.order.Update(updateOrder);
                }
                catch (ObjectNotFound ex)
                {

                    Console.WriteLine(ex.Message);
                }
                break;
            case 'e':
                //delete order
                int deleteOrderId;
                int.TryParse(Console.ReadLine(), out deleteOrderId);
                try
                {
                    dalList.order.Delete(deleteOrderId);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
                break;
        }
    }
    private static void _orderItem()
    {
        Console.WriteLine("Please enter " +
"\na to add one orderItem" +
"\nb to present one orderItem" +
"\nc to present all orderItem" +
"\nd to update product orderItem" +
"\ne to delete product orderItem" +
"\nf to present orderProduct by oderId and productId" +
"\ng to present all orderProducts in a specific order by orderId");
        char chosenAction;
        char.TryParse(Console.ReadLine(), out chosenAction);
        switch (chosenAction)
        {
            case 'a':
                //add orderProduct
                DO.OrderItem orderItem = new DO.OrderItem();
                Console.WriteLine("please enter order id");
                int orderId;
                int.TryParse(Console.ReadLine(), out orderId);
                orderItem.orderId = orderId;
                Console.WriteLine("please enter item id");
                int itemId;
                int.TryParse(Console.ReadLine(), out itemId);
                orderItem.itemId = itemId;
                Console.WriteLine("please enter price for unit");
                int priceForUnit;
                int.TryParse(Console.ReadLine(), out priceForUnit);
                orderItem.priceForUnit = priceForUnit;
                Console.WriteLine("please enter amount");
                int amount;
                int.TryParse(Console.ReadLine(), out amount);
                orderItem.amount = amount;
                try
                {
                    dalList.orderItem.Add(orderItem);
                }
                catch (NoMoreSpaceInArray ex)
                {
                    Console.WriteLine(ex.Message);
                }

                break;
            case 'b':
                //present orderOrderProduct by id
                Console.WriteLine("enter id");
                int id;
                int.TryParse(Console.ReadLine(), out id);
                try
                {
                    OrderItem orderItem3 = dalList.orderItem.Get(id);
                    Console.WriteLine(orderItem3);
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'c':
                //present all orderOrderProducts
                try
                {
                    IEnumerable<OrderItem> allOrderItems = dalList.orderItem.ReadAll();
                    foreach (var item in allOrderItems)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (ObjectNotFound ex)
                {

                    Console.WriteLine(ex.Message);
                }
                break;
            case 'd':
                //update orderOrderProduct
                DO.OrderItem updateOrderItem = new DO.OrderItem();
                Console.WriteLine("please enter orderItemId");
                int orderItemId;
                int.TryParse(Console.ReadLine(), out orderItemId);
                Console.WriteLine(dalList.orderItem.Get(orderItemId));
                Console.WriteLine("please enter order id");
                int orderId1;
                int.TryParse(Console.ReadLine(), out orderId1);
                updateOrderItem.orderId = orderId1;
                Console.WriteLine("please enter item id");
                int itemId1;
                int.TryParse(Console.ReadLine(), out itemId1);
                updateOrderItem.itemId = itemId1;
                Console.WriteLine("please enter price for unit");
                int priceForUnit1;
                int.TryParse(Console.ReadLine(), out priceForUnit1);
                updateOrderItem.priceForUnit = priceForUnit1;
                Console.WriteLine("please enter amount");
                int amount1;
                int.TryParse(Console.ReadLine(), out amount1);
                updateOrderItem.amount = amount1;
                try
                {
                    dalList.orderItem.Update(updateOrderItem);
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'e':
                //delete orderOrderProduct
                int deleteOrderItemId;
                int.TryParse(Console.ReadLine(), out deleteOrderItemId);
                try
                {
                    dalList.orderItem.Delete(deleteOrderItemId);

                }
                catch (ObjectNotFound ex)
                {

                    Console.WriteLine(ex.Message);
                }
                break;
            case 'f':
                //present orderProduct by oderId and productId
                Console.WriteLine("enter orderItem and productId");
                int orderItem1;
                int.TryParse(Console.ReadLine(), out orderItem1);
                int productId;
                int.TryParse(Console.ReadLine(), out productId);
                try
                {
                    OrderItem orderItem4 = dalList.orderItem.ProductItemByOrderIDProductID(orderItem1, productId);
                    Console.WriteLine(orderItem4);
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }

                break;
            case 'g':
                // present all orderProducts in a specific order by orderId
                Console.WriteLine("enter orderId");
                int orderItem2;
                int.TryParse(Console.ReadLine(), out orderItem2);
                try
                {
                    List<OrderItem> arrOrderItems = dalList.orderItem.ReadAllProductInOrder(orderItem2);

                    foreach (var item in arrOrderItems)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }

                break;
        }
    }
}