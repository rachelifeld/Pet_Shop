using BlApi;
using BO;
using DO;
using PL.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private static IBL bl = BlApi.Factory.Get();
        public ProductListWindow(string status)
        {
            InitializeComponent();
            if (status == "newOrder")
            {
                AddProductBtn.Content = "view cart";
                try
                {
                    ProductListview.ItemsSource = bl.Product.GetProductsForCatalog();
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductListview.ItemsSource);
                    PropertyGroupDescription groupDescription = new PropertyGroupDescription("category");
                    view.GroupDescriptions.Add(groupDescription);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                AddProductBtn.Content = "Add New Product";
                try
                {
                    ProductListview.ItemsSource = bl.Product.GetProducts();
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductListview.ItemsSource);
                    PropertyGroupDescription groupDescription = new PropertyGroupDescription("category");
                    view.GroupDescriptions.Add(groupDescription);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (AddProductBtn.Content=="Add New Product")
                {
                    ProductListview.ItemsSource = bl.Product.GetProducts((x) => x.productCategory.ToString() == CategorySelector.SelectedItem.ToString());
                }
                else
                    ProductListview.ItemsSource = bl.Product.GetProductsForCatalog((x) => x.ProductCategory.ToString() == CategorySelector.SelectedItem.ToString());

            }
            catch (Exception)
            {

                throw;
            }
        }


        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (AddProductBtn.Content == "view cart")
            {
                //string status = "view cart";
                //new ProductWindow(bl, null, status).ShowDialog();
                new CartView().ShowDialog();

            }
            else
            {
                string status = "Add New Product";
                ProductForList product = new();
                new ProductWindow().ShowDialog();
                try
                {
                    ProductListview.ItemsSource = bl.Product.GetProducts();
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductListview.ItemsSource);
                    PropertyGroupDescription groupDescription = new PropertyGroupDescription("category");
                    view.GroupDescriptions.Add(groupDescription);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (AddProductBtn.Content == "view cart")
            {
                ProductItem product = (BO.ProductItem)ProductListview.SelectedItem;
                new ProductWindow(product.IdProduct, 0).ShowDialog();
                try
                {
                    ProductListview.ItemsSource = bl.Product.GetProductsForCatalog();
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductListview.ItemsSource);
                    PropertyGroupDescription groupDescription = new PropertyGroupDescription("category");
                    view.GroupDescriptions.Add(groupDescription);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            else
            {
                ProductForList product = (BO.ProductForList)ProductListview.SelectedItem;
                new ProductWindow(product).ShowDialog();
                try
                {
                    ProductListview.ItemsSource = bl.Product.GetProducts();
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductListview.ItemsSource);
                    PropertyGroupDescription groupDescription = new PropertyGroupDescription("category");
                    view.GroupDescriptions.Add(groupDescription);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


    }
}



