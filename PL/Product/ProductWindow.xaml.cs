using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public partial class ProductWindow : Window
    {
        BO.Product product = new();
        private static IBL bl = BlApi.Factory.Get();
        public static BO.Cart cart = new BO.Cart();
        public static int productId1;

        public ProductWindow(int productId, int amountOfProduct = 0)
        {
            InitializeComponent();
            ComboBoxCategory.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            productId1 = productId;
            try
            {
                product = bl.Product.GetProductsDetails(productId);
                TextBoxID.Text = product.productId.ToString();
                TextBoxName.Text = product?.productName?.ToString();
                ComboBoxCategory.SelectedItem = product?.productCategory;
                TextBoxPrice.Text = product?.productPrice.ToString();
                TextBoxInStock.Text = product?.productAmountInStock.ToString();
                if (product?.productAmountInStock == 0)
                {
                    AddProduct.IsEnabled = false;
                }
                TextBoxAmount.Visibility = Visibility.Hidden;
                Amount.Visibility = Visibility.Hidden;
                deleteProductBtn.Visibility = Visibility.Hidden;
                TextBoxID.IsEnabled = false;
                TextBoxName.IsEnabled = false;
                TextBoxPrice.IsEnabled = false;
                ComboBoxCategory.IsEnabled = false;
                TextBoxInStock.IsEnabled = false;
                if (amountOfProduct > 0)
                {
                    TextBoxInStock.Visibility = Visibility.Hidden;
                    TextBoxAmount.Visibility = Visibility.Visible;
                    inStock.Visibility = Visibility.Hidden;
                    Amount.Visibility = Visibility.Visible;

                    TextBoxAmount.Text = amountOfProduct.ToString();
                    AddProduct.Content = "update Amount";
                    deleteProductBtn.Visibility = Visibility.Hidden;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ProductWindow(BO.ProductForList product1)//עדכון ע"י המנהל
        {
            InitializeComponent();
            ComboBoxCategory.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            try
            {
                product = bl.Product.GetProductsDetails(Convert.ToInt32(product1.IdProduct.ToString()));
                TextBoxID.Text = product.productId.ToString();
                TextBoxName.Text = product?.productName?.ToString();
                ComboBoxCategory.SelectedItem = product?.productCategory;
                TextBoxPrice.Text = product?.productPrice.ToString();
                TextBoxInStock.Text = product?.productAmountInStock.ToString();
                deleteProductBtn.Visibility = Visibility.Visible;
                TextBoxAmount.Visibility = Visibility.Hidden;
                Amount.Visibility = Visibility.Hidden;
                AddProduct.Content = "update";
            }
            catch (Exception)
            {

                throw;
            }


        }
        public ProductWindow()// פעולה בונה לחלון הוספת מוצר לקטלוג המוצרים ע"י מנהל
        {
            InitializeComponent();
            ComboBoxCategory.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            AddProduct.Content = "add";
            deleteProductBtn.Visibility= Visibility.Hidden;
            Amount.Visibility = Visibility.Hidden;
            TextBoxAmount.Visibility=Visibility.Hidden;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            product.productCategory = (BO.eCategory)ComboBoxCategory.SelectedItem;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (AddProduct.Content == "add")
            {
                try
                {
                    bl.Product.AddProduct(product);
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("אחד מהפרטים שהכנסת שגויים");
                }
            }
            else if (AddProduct.Content == "update")
            {
                try
                {
                    bl.Product.UpdateProduct(product);
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("אחד מהפרטים שהכנסת שגויים");
                    Close();
                }
            }
            else if (AddProduct.Content == "update Amount")
            {
                try
                {
                    bl.Cart.UpdateProduct(cart, productId1, Convert.ToInt32(TextBoxAmount.Text));
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Product Not InStock");
                }
            }
            else
            {
                OrderItem item = new OrderItem();
                item.orderItemId = 0;
                item.itemId = product.productId;
                item.itemName = product.productName;
                item.priceForUnit = product.productPrice;
                item.amount = 1;
                item.sumPrice = product.productPrice * item.amount;
                try
                {
                    bl.Cart.AddItem(cart, item.itemId);
                    Close();
                }
                catch (Exception)
                {

                    throw;
                }

            }

        }
        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? name = TextBoxName.Text;
            product.productName = name;
        }

        private void TextBoxID_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? id = TextBoxID.Text;
            int.TryParse(id, out int idInt);
            product.productId = idInt;
        }


        private void TextBoxPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? Price = TextBoxPrice.Text;
            double.TryParse(Price, out double TextBoxPriceDouble);
            product.productPrice = TextBoxPriceDouble;
        }

        private void TextBoxInStock_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? inStock = TextBoxInStock.Text;
            int.TryParse(inStock, out int inStockInt);
            product.productAmountInStock = inStockInt;
        }

        private void deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            bl.Product.DeleteProduct(product.productId);
            Close();
        }
    }
}

