using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        public int orderId { get; set; }
        public string? clientName { get; set; }
        public string? clientEmail { get; set; }
        public string? addressForDelivery { get; set; }
        public DateTime dateOrdered { get; set; }
        public DateTime dateSent { get; set; }
        public DateTime dateDelivered { get; set; }
        public eCondition? orderCondition { get; set; }
        public List<BO.OrderItem>? items { get; set; } 
        public double totalPrice { get; set; }

        public override string ToString(){
            String toString = "customer Name: " + clientName + "\ncustomer Email:" + clientEmail + "\ncustomers Address" + addressForDelivery + "\nlist Of Items";
            for (int i = 0; i < items?.Count; i++)
            {
                toString += "\nItem " + (i + 1) + ": " + items[i] + "\n";
            }
            toString += "totalPrice: " + totalPrice;
            return toString;
        }

    }
}
