using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

public struct Product
{
    public int productId { get; set; }
    public string? productName { get; set; }
    public eCategory? productCategory { get; set; }
    public double productPrice { get; set; }
    public int productAmountInStock { get; set; }

    public override string ToString() => $@"
       Product ID: {productId},
       Product name: {productName}, 
       category: {productCategory},
       Price: {productPrice},
       Amount in stock: {productAmountInStock}";
}
