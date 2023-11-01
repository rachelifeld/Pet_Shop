using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductItem
    {
        public int IdProduct { get; set; }
        public string? NameProduct { get; set; }
        public double PriceProduct { get; set; }
        public eCategory? ProductCategory { get; set; }
        public bool InStock { get; set; }

        public override string ToString() => $@"
       Product ID: {IdProduct},
       Product name: {NameProduct}, 
       category: {ProductCategory},
       Price: {PriceProduct},
       is in stock: {InStock}";
    }
}
