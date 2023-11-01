using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlImplementation
{
    internal class BLProduct : IProduct
    {
        private DalApi.IDal? dal = DalApi.Factory.Get();
        public void AddProduct(BO.Product product)
        {

            if (product.productId >= 0 && product.productName != "" && product.productPrice > 0 && product.productAmountInStock >= 0)
            {
                DO.Product product1 = new DO.Product();
                product1.productName = product.productName;
                product1.productPrice = product.productPrice;
                product1.productId = product.productId;
                product1.productAmountInStock = product.productAmountInStock;
                product1.productCategory = (DO.eCategory?)product?.productCategory;
                try
                {
                    dal?.product.Add(product1);
                }
                catch (Exception ex)
                {
                    throw new BO.DalException(ex);
                }

            }
            else
                throw new BO.IncorrectData();
        }

        public void DeleteProduct(int ProductId)
        {
            try
            {
                dal?.product.Delete(ProductId);
            }
            catch (Exception ex)
            {
                throw new BO.DalException(ex);
            }

        }

        public BO.Product GetProductsDetails(int ProductId)
        {
            if (ProductId > 0)
            {
                try
                {
                    DO.Product item = new DO.Product();
                    item = dal?.product.Get(ProductId)??throw new();
                    BO.Product productForCatalog = new();
                    productForCatalog.productId = item.productId;
                    productForCatalog.productName = item.productName;
                    productForCatalog.productPrice = item.productPrice;
                    productForCatalog.productAmountInStock = item.productAmountInStock;
                    productForCatalog.productCategory = (BO.eCategory?)item.productCategory;
                    return productForCatalog;
                }
                catch (Exception ex)
                {
                    throw new BO.DalException(ex);
                }

            }
            else
            {
                throw new BO.IncorrectData();
            }
        }

        public IEnumerable<BO.ProductForList> GetProducts(Func<DO.Product, bool>? func = null)
        {
            List<BO.ProductForList> ProductList = new List<BO.ProductForList>();
            IEnumerable<DO.Product> TempProducts = dal?.product.ReadAll(func)??throw new Null();
            TempProducts.ToList().ForEach(item =>
            {
                BO.ProductForList tempProduct = new();
                tempProduct.NameProduct = item.productName;
                tempProduct.IdProduct = item.productId;
                tempProduct.PriceProduct = item.productPrice;
                tempProduct.CategoryProduct = (BO.eCategory?)item.productCategory;
                ProductList.Add(tempProduct);
            });
            return ProductList;
        }

        public BO.ProductItem GetProductForCatalog(int ProductId)
        {
            BO.ProductItem tempProduct = new();
            try
            {
                DO.Product DoProduct = dal?.product.Get(ProductId)??throw new Null();
                tempProduct.NameProduct = DoProduct.productName;
                tempProduct.IdProduct = DoProduct.productId;
                tempProduct.PriceProduct = DoProduct.productPrice;
                tempProduct.ProductCategory = (BO.eCategory?)DoProduct.productCategory;
           
                if (DoProduct.productAmountInStock > 0)
                {
                    tempProduct.InStock = true;
                }
                else
                {
                    tempProduct.InStock = false;
                }
                return tempProduct;
            }
            catch (Exception ex)
            {
                throw new BO.DalException(ex);
            }

        }

        public void UpdateProduct(BO.Product product)
        {
            try
            {
                DO.Product updateProduct = new();
                updateProduct = dal?.product.Get(product.productId)?? throw new Null();
                try
                {
                    updateProduct.productId = product.productId;
                    updateProduct.productPrice = product.productPrice;
                    updateProduct.productName = product.productName;
                    updateProduct.productCategory = (DO.eCategory?)product.productCategory;
                    updateProduct.productAmountInStock=product.productAmountInStock;
                    dal.product.Update(updateProduct);
                }
                catch (Exception ex)
                {
                    throw new BO.DalException(ex);
                }
            }

            catch (Exception ex)
            {
                throw new BO.DalException(ex);
            }
        }
        public IEnumerable<BO.ProductItem> GetProductsForCatalog(Func<BO.ProductItem, bool>? func = null)
        {
            IEnumerable<BO.ProductItem> ls =
                from product in dal?.product.ReadAll()
                select new BO.ProductItem()
                {
                    IdProduct = product.productId,
                    NameProduct = product.productName,
                    PriceProduct = product.productPrice,
                    ProductCategory = (BO.eCategory?)product.productCategory,
                    InStock = product.productAmountInStock > 0,
                };


            if (func != null)
            {
                return ls.Where(func);
            }
            return ls;

        }
    }

}
