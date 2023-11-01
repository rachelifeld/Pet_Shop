using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        public int orderItemId { get; set; }
        public int itemId { get; set; }
        public string? itemName { get; set; }
        public double priceForUnit { get; set; }
        public int amount { get; set; }
        public double sumPrice { get; set; }

        public override string ToString() => $@"
       OrderItem ID: {orderItemId},
       Item ID: {itemId},
       Item Name: {itemName},
       Price For Unit: {priceForUnit},
       Amount: {amount}";
    }
}
