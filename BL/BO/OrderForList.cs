using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        public int IdOrder { get; set; }
        public string? CustomersName { get; set; }

        public eCondition? OrderCondition { get; set; }

        public int AmountProducts { get; set; }

        public double TotalPrice { get; set; }
        public override string ToString() => $@"
       Id Order: {IdOrder},
       Customers name: {CustomersName}, 
       Order Condition: {OrderCondition},
        Amount Products: {AmountProducts},
        TotalPrice: {TotalPrice}";
    }
}

