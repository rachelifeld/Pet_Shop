using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IProduct
    {
        public IEnumerable<BO.ProductForList>  GetProducts(Func<DO.Product, bool>? func=null);
        public IEnumerable<BO.ProductItem> GetProductsForCatalog(Func<BO.ProductItem, bool>? func = null);
        public BO.Product GetProductsDetails(int ProductId);
        public BO.ProductItem GetProductForCatalog(int ProductId);
        public void AddProduct(BO.Product product);
        public void DeleteProduct(int ProductId);
        public void UpdateProduct(BO.Product product);
    }
}
