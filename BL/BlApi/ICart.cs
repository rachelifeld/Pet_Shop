using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface ICart
    {
        public void CheckOut(Cart cart, string? CustomerName, string? CustomerEmail, string? CustomerAddress);
        public Cart AddItem(Cart cart, int ProductId);
        public Cart UpdateProduct(Cart cart, int ProductId, int NewAmount);

    }
}
