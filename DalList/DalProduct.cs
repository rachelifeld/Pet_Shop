//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DalApi;
using DO;
using System;
using static Dal.DataSource;

namespace Dal;

public class DalProduct : IProduct
{
    public int Add(DO.Product product)
    {
        for (int i = 0; i < listProduct.Count-1; i++)
        {
            if (listProduct[i].productId == product.productId)
                throw new AlreadyExistsSuchAnObject();
        }
        listProduct.Add(product);
         return product.productId;

    }

    public DO.Product Get(int productId)
    {
        for (int i = 0; i < listProduct.Count; i++)
        {
            if (listProduct[i].productId == productId)
                return listProduct[i];
        }
        throw new ObjectNotFound();
    }

    public void Delete(int productId)
    {
        for (int i = 0; i < listProduct.Count; i++)
        {
            if (listProduct[i].productId == productId)
            {
                listProduct.RemoveAt(i);
                return;
            }


        }
    }
    public void Update(DO.Product product)
    {
        int i;
        for (i = 0; i < listProduct.Count ; i++)
        {
            if (listProduct[i].productId == product.productId)
            {
                listProduct[i] = product;
                return;
            }
        }
        if (i == listProduct.Count)
            throw new ObjectNotFound();
    }

    //public IEnumerable<Product> ReadAll()
    //{
    // List<DO.Product> allProducts = new();
    //    for (int i = 0; i < listProduct.Count; i++)
    //    {
    //        allProducts.Add(listProduct[i]);
            
    //    }
    //    return allProducts;
    //}
    public Product Get(Predicate<Product> p)
    {
        return listProduct.Find(p);
    }
    public IEnumerable<Product> ReadAll(Func<Product, bool>? func = null)
    {
        return (func == null) ? listProduct : listProduct.Where(func);
    }
}
