using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using DO;


namespace BlImplementation
{
    internal class BLCart : BlApi.ICart
    {
        private static DalApi.IDal? dal = DalApi.Factory.Get();
        public BO.Cart AddItem(BO.Cart cart, int ProductId)
        {
            bool flag = false;
            cart.ListOfOrderDetails?.ForEach(i =>
            {
                if (i.itemId == ProductId)
                {
                    int amountInStock = dal?.product.Get(ProductId).productAmountInStock ?? throw new BO.Null();
                    if (amountInStock > 0)
                    {
                        i.amount++;
                        i.sumPrice = i.amount * i.priceForUnit;
                        cart.TotalPriceOfBasket += i.priceForUnit;
                        flag= true;
                    }
                }
            });
            if (flag)
            {
                return cart;
            }
 
            try
            {
                DO.Product tempProduct = dal?.product.Get(ProductId) ?? throw new Null();

                if (tempProduct.productAmountInStock>0)
                {
                    BO.OrderItem product = new BO.OrderItem();
                    product.amount = 1;
                    product.priceForUnit = tempProduct.productPrice;
                    product.sumPrice = product.amount*product.priceForUnit;
                    product.itemId = ProductId;
                    product.itemName = tempProduct.productName;
                    product.orderItemId = 0;
                    cart.TotalPriceOfBasket += tempProduct.productPrice;
                    tempProduct.productAmountInStock--;
                    cart.ListOfOrderDetails?.Add(product);
                    return cart;
                }
                throw new BO.ProductNotInStock();
            }
            catch (Exception ex)
            {
                throw new BO.DalException(ex);
            }

        }

        public void CheckOut(BO.Cart cart, string? CustomerName, string? CustomerEmail, string? CustomerAddress)
        {
            if (CustomerName != null && CustomerEmail != null && CustomerAddress != null)
            {
                cart.ListOfOrderDetails?.ForEach(item =>
                {
                    try
                    {
                        DO.Product TempProduct = dal?.product.Get(item.itemId) ?? throw new Null();
                        if (!(item.amount > 0 && item.amount <= TempProduct.productAmountInStock))
                        {
                            throw new BO.ProductNotInStock();
                        }
                    }
                    catch (Exception ex)
                    {

                        throw new BO.DalException(ex);
                    }
                });
                //foreach (var item in cart.ListOfOrderDetails)
                //{
                //    try
                //    {
                //        DO.Product TempProduct = dal.product.Get(item.itemId);
                //        if (item.amount > 0 && item.amount <= TempProduct.productAmountInStock)
                //        {
                //            continue;
                //        }
                //        else
                //        {
                //            throw new BO.ProductNotInStock();
                //        }
                //    }
                //    catch (Exception ex)
                //    {

                //        throw new BO.DalException(ex);
                //    }

                //}
                DO.Order order = new();
                order.dateOrdered = DateTime.Now;
                order.dateDelivered = DateTime.MaxValue;
                order.dateSent = DateTime.MaxValue;
                order.clientEmail = CustomerEmail;
                order.clientName = CustomerName;
                order.addressForDelivery = CustomerAddress;
                try
                {
                    int orderId = dal?.order.Add(order) ?? throw new Null();
                    cart.ListOfOrderDetails?.ForEach(item =>
                    {
                        DO.OrderItem orderItem = new();
                        orderItem.orderItemId = 0;
                        orderItem.amount = item.amount;
                        orderItem.priceForUnit = item.priceForUnit;
                        orderItem.orderId = orderId;
                        orderItem.itemId = item.itemId;
                        try
                        {
                            dal.orderItem.Add(orderItem);
                        }
                        catch (Exception ex)
                        {
                            throw new BO.DalException(ex);
                        }
                    });
                    //foreach (var item in cart.ListOfOrderDetails)
                    //{
                    //    DO.OrderItem orderItem = new();
                    //    orderItem.orderItemId = 0;
                    //    orderItem.amount = item.amount;
                    //    orderItem.priceForUnit = item.priceForUnit;
                    //    orderItem.orderId = orderId;
                    //    orderItem.itemId = item.itemId;
                    //    try
                    //    {
                    //        dal.orderItem.Add(orderItem);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        throw new BO.DalException(ex);
                    //    }
                    //}

                }
                catch (Exception ex)
                {

                    throw new BO.DalException(ex);
                }
            }
            cart.ListOfOrderDetails?.ForEach(item =>
            {
                DO.Product product = new();
                product = dal?.product.Get(item.itemId) ?? throw new Null(); // we did not do try or catch because we already checked that all products exists
                product.productAmountInStock -= item.amount;
                dal.product.Update(product); // we did not do try or catch because we already checked that all products exists 
            });
            //foreach (var item in cart.ListOfOrderDetails)
            //{
            //    DO.Product product = new();
            //    product = dal.product.Get(item.itemId); // we did not do try or catch because we already checked that all products exists
            //    product.productAmountInStock -= item.amount;
            //    dal.product.Update(product); // we did not do try or catch because we already checked that all products exists 
            //}
         }

        public BO.Cart UpdateProduct(BO.Cart cart, int ProductId, int NewAmount)
        {
      
            bool flag = false;
            BO.OrderItem productDelete = new();
            cart.ListOfOrderDetails?.ForEach(product =>
            {
                if (product.itemId == ProductId)
                {
                    if (product.amount != NewAmount)
                    {
                        DO.Product productData = dal?.product.Get(ProductId)?? throw new Null();
                        if (productData.productAmountInStock >= NewAmount)//יש אפשרות לשנות כמות
                        {
                            cart.TotalPriceOfBasket += (NewAmount - product.amount) * (product.priceForUnit);//משנה את המחיר לפי ההפרש בין מספר הפריטים שהיו לעכשיו כפול מחיר לפריט
                            product.amount = NewAmount;
                            product.sumPrice = NewAmount * product.priceForUnit;
                        }

                        else
                            throw new ProductNotInStock();
                    }
                    if (product.amount == 0)
                    {
                       productDelete = product;
                    }
                    flag= true; 
                }
            });
            if (flag)
            {
                cart.ListOfOrderDetails?.Remove(productDelete);
                return cart;
            }
            throw new BO.NoSuchProductInCart();

        }

    }
}
