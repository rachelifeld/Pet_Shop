using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

public struct OrderItem
{
    public int orderItemId { get; set; }
    public int orderId { get; set; }
    public int itemId { get; set; }

    public double priceForUnit { get; set; }
    public int amount { get; set; }

    public override string ToString() => $@"
       OrderItem ID: {orderItemId},
       Order ID: {orderId}, 
       Item ID: {itemId},
       Price For Unit: {priceForUnit},
       Amount: {amount}";
}
