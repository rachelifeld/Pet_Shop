using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal class DalProduct : IProduct
    {
      public int Add(Product item)
        {
            XElement? Products = XDocument.Load("../xml/Product.xml").Root;
            var list = Products?.Elements().ToList().Where
                (product => 
                product?.Element("ID")?.Value.ToString() == item.productId.ToString());
            if (list?.Count() > 0)
                throw new Exception();
            XElement? product = new XElement("Product");
            XElement? ID = new XElement("ID", item.productId);
            product.Add(ID);
            XElement? Name = new XElement("Name", item.productName);
            product.Add(Name);
            XElement? Price = new XElement("Price", item.productPrice);
            product.Add(Price);
            XElement? Category = new XElement("Category", item.productCategory);
            product.Add(Category);
            XElement? InStock = new XElement("InStock", item.productAmountInStock);
            product.Add(InStock);
            Products?.Add(product);
            Products?.Save("../xml/Product.xml");
            var list1 = Products?.Elements().Where(product =>
                product?.Element("ID")?.Value.ToString() == item.productId.ToString());

            return item.productId;
        }

       public void Delete(int id)
        {
            XElement? Products = XDocument.Load("../xml/Product.xml").Root;
            Products?.Elements().ToList().
                Find(product => Convert.ToInt32(product?.Element("ID")?.Value) == id)?.Remove();
            Products?.Save("../xml/Product.xml");
        }
         
      public Product Get(int id)
        {
            XElement? Products = XDocument.Load("../xml/Product.xml").Root;
            var product1 = Products?.Elements().ToList().Find(product =>
            Convert.ToInt32(product?.Element("ID")?.Value) == id);
            var product =Products?.Elements().ToList().
                Find(product => Convert.ToInt32(product?.Element("ID")?.Value) == id);
            DO.eCategory.TryParse(product1?.Element("Category")?.Value, out DO.eCategory myCategory); ;
            return new DO.Product { productId = Convert.ToInt32(product1?.Element("ID")?.Value),
                productName = product1?.Element("Name")?.Value.ToString(),
                productAmountInStock = Convert.ToInt32(product1?.Element("InStock")?.Value),
                productCategory = myCategory,
                productPrice = Convert.ToInt32(product1?.Element("Price")?.Value) };
        }

        public Product Get(Predicate<Product> func)
        {
            IEnumerable<DO.Product> products = ReadAll();
            return products.ToList().Find(func);
        }

        public IEnumerable<Product> ReadAll(Func<Product, bool>? func=null)
        {
            XElement? Products = XDocument.Load("../xml/Product.xml").Root;
            List<DO.Product> products = new List<DO.Product> { };
            Products?.Elements().ToList().ForEach(product =>
            {
                DO.eCategory.TryParse(product?.Element("Category")?.Value, out DO.eCategory myCategory); ;
                products.Add(new DO.Product
                {
                    productId = Convert.ToInt32(product?.Element("ID")?.Value),
                    productName = product?
                    .Element("Name")?.Value.ToString(),
                    productAmountInStock = Convert.ToInt32(product?.Element("InStock")?.Value),
                    productCategory = myCategory,
                    productPrice = Convert.ToInt32(product?.Element("Price")?.Value)
                });
            });

            return func == null ? products : products.Where(func);
        }

        public void Update(Product item)
        {
            Delete(item.productId);
            Add(item);
        }
    }
}
