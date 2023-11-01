using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cart
    {
        public string? CutomersName { get; set; }
        public string? CutomersEmail { get; set; }
        public string? CutomersAddress { get; set; }
        public List<OrderItem>? ListOfOrderDetails { get; set; }
        public double TotalPriceOfBasket { get; set; }
        public Cart()
        {
            ListOfOrderDetails = new();
        }
    }
}
