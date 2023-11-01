using BO;
using BlImplementation;
using BlApi;
using Dal;
using DO;
using System.Diagnostics;

namespace BLTest
{
    public class Program
    {
        private static IBL ibl = new Bl ();
        public static void Main(string[] args)
        {
            Cart cart = new Cart();
            cart.ListOfOrderDetails = new List<BO.OrderItem>();
            Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for Cart " + "\n2 for Order " + "\n3 for Product");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        _Cart(cart);
                        break;
                    case 2:
                        _Order();
                        break;
                    case 3:
                        _Product();
                        break;
                }
                Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for Cart " + "\n2 for Order " + "\n3 for Product");
                int.TryParse(Console.ReadLine(), out choice);
            }

        }
        public static Cart _Cart(Cart cart)
        {
            Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for UpdateProduct " + "\n2 for AddItem " + "\n3 for CheckOut");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("insert products id");
                        int id;
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine("insert amount of product");
                        int amount;
                        int.TryParse(Console.ReadLine(), out amount);
                        try
                        {
                            cart = ibl.Cart.UpdateProduct(cart, id, amount);
                            Console.WriteLine("product updated succesfully");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (ProductNotInStock e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (NoSuchProductInCart e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case 2:
                        Console.WriteLine("insert product id");
                        int productId;
                        int.TryParse(Console.ReadLine(), out productId);
                        try
                        {
                            cart = ibl.Cart.AddItem(cart, productId);
                            Console.WriteLine("product added succesfully!");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (ProductNotInStock e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case 3:
                        Console.WriteLine("insert customer name");
                        string? customerName = Console.ReadLine();
                        Console.WriteLine("insert customer email");
                        string? customerEmail = Console.ReadLine();
                        Console.WriteLine("insert customer address");
                        string? customerAddress = Console.ReadLine();
                        try
                        {
                            ibl.Cart.CheckOut(cart, customerName, customerEmail, customerAddress);
                            Console.WriteLine("order confirmed!" + "\n thank you for ordering at our shop!");
                            cart.CutomersEmail = "";
                            cart.CutomersAddress = "";
                            cart.CutomersName = "";
                            cart.ListOfOrderDetails?.Clear();
                            cart.TotalPriceOfBasket = 0;
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (ProductNotInStock e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                }
                Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for UpdateProduct " + "\n2 for AddItem " + "\n3 for CheckOut");
                int.TryParse(Console.ReadLine(), out choice);
            }
            return cart;
        }
        public static void _Order()
        {
            Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for GetOrders " + "\n2 for GetOrdersDetails " + "\n3 for UpdateSent" + "\n4 for UpdateDelivered");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        try
                        {
                            IEnumerable<OrderForList> allOrders = new List<OrderForList>();
                            allOrders = ibl.Order.GetOrders();
                            foreach (var item in allOrders)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        break;
                    case 2:
                        Console.WriteLine("insert order id:");
                        int orderId;
                        int.TryParse(Console.ReadLine(), out orderId);
                        try
                        {
                            BO.Order orderDetails = ibl.Order.GetOrdersDetails(orderId);
                            Console.WriteLine(orderDetails);
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (IncorrectData e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 3:
                        Console.WriteLine("insert order id:");
                        int orderId1;
                        int.TryParse(Console.ReadLine(), out orderId1);
                        try
                        {
                            ibl.Order.UpdateSent(orderId1);
                            Console.WriteLine("order updated succesfully!");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (OrderAlreadySent e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 4:
                        Console.WriteLine("insert order id:");
                        int orderId2;
                        int.TryParse(Console.ReadLine(), out orderId2);
                        try
                        {
                            ibl.Order.UpdateDelivered(orderId2);
                            Console.WriteLine("order updated succesfully!");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (OrderAlreadyDelivered e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                }
                Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for GetOrders " + "\n2 for GetOrdersDetails " + "\n3 for UpdateSent" + "\n4 for UpdateDelivered");
                int.TryParse(Console.ReadLine(), out choice);
            }
        }
        public static void _Product()
        {
            Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for UpdateProduct " + "\n2 for GetProductForCatalog " + "\n3 for GetProducts" + "\n4 for GetProductsDetails" + "\n5 for DeleteProduct" + "\n6 for AddProduct");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        BO.Product updateProduct = new BO.Product();
                        Console.WriteLine("please enter the products id");
                        int productId1=updateProduct.productId;
                        int.TryParse(Console.ReadLine(), out productId1);

                        try
                        {
                            BO.Product recentproduct = ibl.Product.GetProductsDetails(updateProduct.productId);
                            Console.WriteLine(recentproduct);
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                            break;
                        }
                        catch (IncorrectData e)
                        {
                            Console.WriteLine(e.Message);
                            break ;
                        }
                        Console.WriteLine("please reenter the products name");
                        updateProduct.productName = Console.ReadLine();
                        Console.WriteLine("please reenter the products category:" + "\n 0 for cats" + "\n 1 for dogs" + "\n 2 for fish" + "\n 3 for snakes ");
                        int productCategory;
                        int.TryParse(Console.ReadLine(), out productCategory);
                        updateProduct.productCategory = (BO.eCategory)productCategory;
                        Console.WriteLine("please reenter the products price");
                        int productPrice;
                        int.TryParse(Console.ReadLine(), out productPrice);
                        updateProduct.productPrice = productPrice;
                        Console.WriteLine("please reenter the products amount in stock");
                        int productAmountInStock;
                        int.TryParse(Console.ReadLine(), out productAmountInStock);
                        updateProduct.productAmountInStock = productAmountInStock;
                        try
                        {
                            ibl.Product.UpdateProduct(updateProduct);
                            Console.WriteLine("product updated succesfully");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            BO.ProductItem GetProductForCatalog = new BO.ProductItem();
                            Console.WriteLine("insert product's id");
                            int productId3;
                            int.TryParse(Console.ReadLine(), out productId3);
                            GetProductForCatalog = ibl.Product.GetProductForCatalog(productId3);
                            Console.WriteLine(GetProductForCatalog);
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        break;
                    case 3:
                        try
                        {
                            IEnumerable<BO.ProductForList> GetProducts = new List<BO.ProductForList>();
                            GetProducts= ibl.Product.GetProducts();
                            foreach (var item in GetProducts)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        break;
                    case 4:
                        Console.WriteLine("please enter the products id");
                        int productId;
                        int.TryParse(Console.ReadLine(), out productId);
                        try
                        {
                            BO.Product recentproduct = new BO.Product();
                            recentproduct= ibl.Product.GetProductsDetails(productId);
                            Console.WriteLine(recentproduct);
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (IncorrectData e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 5:
                        Console.WriteLine("insert product's id:");
                        int id;
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        {
                            ibl.Product.DeleteProduct(id);
                            Console.WriteLine("product deleted succesfully");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        break;
                    case 6:
                        BO.Product addProduct = new BO.Product();
                        Console.WriteLine("please enter the products id");
                        int productId2;
                        int.TryParse(Console.ReadLine(), out productId2);
                        addProduct.productId =productId2;
                        Console.WriteLine("please enter the products name");
                        addProduct.productName = Console.ReadLine();
                        Console.WriteLine("please enter the products category:" + "\n 0 for cats" + "\n 1 for dogs" + "\n 2 for fish" + "\n 3 for snakes ");
                        int productCategory1;
                        int.TryParse(Console.ReadLine(), out productCategory1);
                        addProduct.productCategory = (BO.eCategory)productCategory1;
                        Console.WriteLine("please enter the products price");
                        int productPrice1;
                        int.TryParse(Console.ReadLine(), out productPrice1);
                        addProduct.productPrice = productPrice1;
                        Console.WriteLine("please enter the products amount in stock");
                        int productAmountInStock1;
                        int.TryParse(Console.ReadLine(), out productAmountInStock1);
                        addProduct.productAmountInStock = productAmountInStock1;
                        try
                        {
                            ibl.Product.AddProduct(addProduct);
                            Console.WriteLine("product added succesfully");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (IncorrectData e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                }
                Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for UpdateProduct " + "\n2 for GetProductForCatalog " + "\n3 for GetProducts" + "\n4 for GetProductsDetails" + "\n5 for DeleteProduct" + "\n6 for AddProduct");
                int.TryParse(Console.ReadLine(), out choice);
            }
        }

    }
}
