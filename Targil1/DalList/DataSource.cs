using DO;
using System;
using System.Net;
using static Dal.DataSource;
using System.Reflection;
using DalApi;

namespace Dal;

static internal class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    readonly static Random random = new Random();
    static internal List<DO.Order> listOrder = new ();
    private static void addOrder(DO.Order order)
    {
        listOrder.Add(order);
    }
    static internal List< DO.OrderItem> listOrderItem = new();
    private static void addOrderItem(DO.OrderItem orderItem)
    {
        listOrderItem.Add( orderItem);
    }
    static internal List<DO.Product> listProduct = new();
    private static void addProduct(DO.Product product)
    {
        listProduct.Add( product);
    }
    private static void s_Initialize()
    {
        int daysForSending, daysForDeliver;
        (DO.eCategory productCategory, string productName)[] animals = new[] {
            (DO.eCategory.dogs, "Sausage Dog"),
            (DO.eCategory.cats,"Ragdoll Cat"),
            (DO.eCategory.fish, "Gold Fish"),
            (DO.eCategory.snakes,"Corn Snake"),
            (DO.eCategory.snakes,"Milk Snake"),
            (DO.eCategory.dogs,"Germans Shepherd Dog"),
            (DO.eCategory.fish,"angelfish"),
            (DO.eCategory.snakes,"Garter Snake"),
            (DO.eCategory.dogs,"Labrador Retriever Dog"),
            (DO.eCategory.dogs,"Poodle Dog")};

        for (int i = 0; i < 10; i++)
        {
            DO.Product product = new DO.Product();
            int randomProductId = config.ProductId;
            for (int j = 0; j < listProduct.Count; j++)
            {
                if (randomProductId == listProduct[j].productId)
                {
                    randomProductId = config.ProductId;
                    j = 0;
                }
            }
            product.productId = randomProductId;
            product.productName = animals[i].productName;
            product.productCategory = animals[i].productCategory;
           double num = random.NextDouble() * 1000;
            product.productPrice = (int)num;
            product.productAmountInStock = (int)random.NextInt64(0, 20);
            addProduct(product);
        }
        (string clientName, string clientEmail, string addressForDelivery)[] orders = new[] {
            ("Tamar Boyer", "Tamar3758@gmail.com","Uziel 78"),
            ("Rachel Cohen", "Rachel1234@gmail.com","Argov 8"),
            ("Ruty Elyiach", "Ruty4567@gmail.com","David Meretz 72"),
            ("Batya Shapira", "Batya8520@gmail.com","Agasi 1"),
            ("Chana Rotenberg", "Chana7542@gmail.com","Hapisga 29"),
            ("Michal Levy", "Michal8888@gmail.com","Casuto 2"),
            ("Yudit Shub", "Yudit7832@gmail.com","Rashi 111"),
            ("Tzipi Chevroni", "Tzipi2222@gmail.com","Lilach 87"),
            ("Shira Rozenthal", "Shira8543@gmail.com","Kadish Looz 3"),
            ("Esther Ebert", "Esther7544@gmail.com","Bayit Vegan 75"),
            ("Leah Yudlov", "Leah5632@gmail.com","Elyashiv 19"),
            ("Chaya Rosenberg", "Chaya1111@gmail.com","Moshe Zilberg 15"),
            ("Shani Zeevi", "Shani1254@gmail.com","Mutzafi 79"),
            ("Hadassah Teib", "Hadassah4566@gmail.com","Broyer 84"),
            ("Orit Reiter", "Orit7520@gmail.com","Mintz 44"),
            ("Dasi Feld", "Dasi7899@gmail.com","sulam Yaacov 6"),
            ("Shifra Fisher", "Shifra4500@gmail.com","Zolti 96"),
            ("Tzvi Madmon", "Tzvi3333@gmail.com","Fatal 82"),
            ("Yaacov Goldsmidth", "Yaacov5566@gmail.com","Brand 102"),
            ("Yitzchak Omesi", "Yitzchak1010@gmail.com","Druk 73"),};

        //for (int i = 0; i < 20; i++)
        //{
        //    daysForSending = (int)random.NextInt64(2, 3);
        //    daysForDeliver = (int)random.NextInt64(4, 12);
        //    DO.Order order = new DO.Order();
        //    order.orderId = config.OrderId;
        //    order.clientName = orders[i].clientName;
        //    order.clientEmail = orders[i].clientEmail;
        //    order.addressForDelivery = orders[i].addressForDelivery;
        //    order.dateOrdered = DateTime.MinValue;
        //    TimeSpan tdaysForSending = new TimeSpan(daysForSending, 0, 0, 0);
        //    order.dateSent = order.dateOrdered.Add(tdaysForSending);
        //    TimeSpan tdaysForDeliver = new TimeSpan(daysForDeliver, 0, 0, 0);
        //    order.dateDelivered = order.dateSent.Add(tdaysForDeliver);
        //    addOrder(order);
        //}

        for (int i = 0; i < orders.Length; i++)
        {
            Order order = new Order();
            int index1 = (int)random.Next(20);
            daysForSending = (int)random.Next(1, 3);
            daysForDeliver = (int)random.Next(3, 7);
            order.orderId = config.OrderId;
            order.clientName = orders[i].clientName;
            order.clientEmail = orders[i].clientEmail;
            order.addressForDelivery = orders[i].addressForDelivery;
            order.dateOrdered = DateTime.Now;
            if (i < orders.Length * 0.2)//20% with just order date
            {
                order.dateSent = DateTime.MaxValue;
                order.dateDelivered = DateTime.MaxValue;
            }
            else
            {
                TimeSpan tDaysShip = new TimeSpan(daysForSending, 0, 0, 0);
                order.dateSent = order.dateOrdered.Add(tDaysShip);
                if (i < orders.Length * 0.2 + (orders.Length * 0.8 * 0.6))//60% of 80% with order, ship and delivery dates.
                {
                    TimeSpan tDaysDelivery = new TimeSpan(daysForDeliver, 0, 0, 0);
                    order.dateDelivered = order.dateOrdered.Add(tDaysDelivery);
                }
                else
                    order.dateDelivered = DateTime.MaxValue;//the other with just order and ship dates.
            }
            addOrder(order);
        }

        DO.OrderItem orderItem = new DO.OrderItem();
        for (int j = 0; j < listOrder.Count; j++)
        {
            int randomProduct = (int)random.NextInt64(0, listProduct.Count);
            int randomAmount = (int)random.NextInt64(1, 4);
            orderItem.orderItemId = config.OrderItemId;
            orderItem.orderId = listOrder[j].orderId;
            orderItem.itemId = listProduct[randomProduct].productId;
            orderItem.priceForUnit = listProduct[randomProduct].productPrice;
            orderItem.amount = randomAmount;
            addOrderItem(orderItem);
        }
        int index=0;
        for (int i = 0; i < 20; i += index)
        {
            index = (int)random.NextInt64(1, 3);
            int randomOrder = -1;
            randomOrder++;
            for (int j = 0; j < index; j++)
            {
                int randomProduct = (int)random.NextInt64(0, listProduct.Count);
                int randomAmount = (int)random.NextInt64(1, 4);
                orderItem.orderItemId = config.OrderItemId;
                orderItem.orderId = listOrder[randomOrder].orderId;
                orderItem.itemId = listProduct[randomProduct].productId;
                orderItem.priceForUnit = listProduct[randomProduct].productPrice;
                orderItem.amount = randomAmount;
                addOrderItem(orderItem);
            }

        }

    }

    static internal class config
    {
        static private int productId = (int)random.NextInt64(100000, 1000000);
        static public int ProductId
        {
            get { return productId = (int)random.NextInt64(100000, 1000000); }

        }
        static private int orderId = 0;

        static public int OrderId
        {
            get { return orderId++; }
        }
        static private int orderItemId = 0;
        static public int OrderItemId
        {
            get { return orderItemId++; }

        }


    }

}



