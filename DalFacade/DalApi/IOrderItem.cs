using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IOrderItem : Icrud<DO.OrderItem>
    {
        public List<DO.OrderItem> ReadAllProductInOrder(int orderId);
        public DO.OrderItem ProductItemByOrderIDProductID(int orderId, int productId);
    }
}
