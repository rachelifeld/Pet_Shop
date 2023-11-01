using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductForList
    {
        public int IdProduct { get; set; }
        public string? NameProduct { get; set; }

        public double PriceProduct { get; set; }

        public eCategory? CategoryProduct { get; set; }

        public override string ToString() => $@"
       Product ID: {IdProduct},
       Product name: {NameProduct}, 
       category: {CategoryProduct},
       Price: {PriceProduct}";

    }
}
