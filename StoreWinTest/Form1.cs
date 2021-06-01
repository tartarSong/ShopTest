using StoreLib.action;
using StoreLib.action.sqlHelper;
using StoreLib.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace StoreWinTest
{
    public partial class Form1 : Form
    {
        private List<Product> products {get; set;}
        private string connectionString 
        { get { return ConfigurationManager.ConnectionStrings["Default"].ConnectionString; } }

        public Form1()
        {
            InitializeComponent();

            comboBox1.ValueMember = "ProductId";
            comboBox1.DisplayMember = "ProductName";
            products = sqlAction.GetProduct(connectionString);
            comboBox1.DataSource = products;
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            tbNum.ForeColor = Color.Black;

            if (!int.TryParse(tbNum.Text, out int temp))
            {
                tbNum.ForeColor = Color.Red;
                return;
            }

            var selected = products.FirstOrDefault(x => x.ProductId == (int)comboBox1.SelectedValue);
            var discounts = sqlAction.AddProductCart(connectionString, selected.ProductId);
            if (ProductCart.Instance.product.Exists(x => x.ProductId == selected.ProductId))
            {
                ProductCart.Instance.product.FirstOrDefault(x => x.ProductId == selected.ProductId).Quantity += int.Parse(tbNum.Text);
            }
            else
            {
                selected.Quantity = int.Parse(tbNum.Text);
                ProductCart.Instance.product.Add(selected);
                ProductCart.Instance.productDiscount.AddRange(discounts);
            }

            tbCart.Text += Environment.NewLine + tbNum.Text + " " + selected.ProductName + " " + selected.ProductPrice;
        }

        private void RemoveFromCart_Click(object sender, EventArgs e)
        {
            var selected = products.FirstOrDefault(x => x.ProductId == (int)comboBox1.SelectedValue);
            if (ProductCart.Instance.product.Exists(x => x.ProductId == selected.ProductId))
            {
                ProductCart.Instance.product.Remove(ProductCart.Instance.product.Single(x => x.ProductId == selected.ProductId));
                foreach (var item in ProductCart.Instance.productDiscount)
                {
                    if(item.ConditionProductId == selected.ProductId)
                    {
                        ProductCart.Instance.productDiscount.Remove(item);
                    }
                }
            }

            tbCart.Text += Environment.NewLine + " - " + selected.ProductName;
        }

        private void btnGetTotal_Click(object sender, EventArgs e)
        {
            try
            {
                ICartActionInterface iCartActionInterface = new CartAction();
                tbTotal.Text = iCartActionInterface.GetTotal(connectionString).ToString();
            }
            catch(Exception ex)
            {
                if (LogAction.WriteLog(connectionString, ex.Message) != 0)
                {
                    StreamWriter sw = new StreamWriter("C:\\StoreTest_log.txt");
                    sw.WriteLine(DateTime.Now + " => " + ex.Message);
                    sw.Close();
                }
            }
        }
    }
}
