using StoreLib.action;
using StoreLib.action.sqlHelper;
using StoreLib.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreWebTest
{
    public partial class _Default : Page
    {

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        public static List<Product> Products
        {
            get
            {
                if (HttpContext.Current.Session["ProductsSession"] != null)
                {
                    return (List<Product>)HttpContext.Current.Session["ProductsSession"];
                }
                else
                    return new List<Product>();
            }
            set
            {
                HttpContext.Current.Session["ProductsSession"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.DataTextField = "ProductName";
                Products = sqlAction.GetProduct(connectionString);
                ddlProduct.DataSource = Products;
                ddlProduct.DataBind();
            }
        }


        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            var selected = Products.FirstOrDefault(x => x.ProductId == int.Parse(ddlProduct.SelectedItem.Value));
            var discounts = sqlAction.AddProductCart(connectionString, selected.ProductId);
            if (ProductCart.Instance.product.Exists(x => x.ProductId == selected.ProductId))
            {
                ProductCart.Instance.product.FirstOrDefault(x => x.ProductId == selected.ProductId).Quantity += int.Parse(tbQuantitie.Text);
            }
            else
            {
                selected.Quantity = int.Parse(tbQuantitie.Text);
                ProductCart.Instance.product.Add(selected);
                ProductCart.Instance.productDiscount.AddRange(discounts);
            }

            tbCart.Text += Environment.NewLine + selected.ProductName + " " + selected.ProductPrice + " " + selected.Quantity;
        }

        protected void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            var selected = Products.FirstOrDefault(x => x.ProductId == int.Parse(ddlProduct.SelectedItem.Value));
            if (ProductCart.Instance.product.Exists(x => x.ProductId == selected.ProductId))
            {
                ProductCart.Instance.product.Remove(ProductCart.Instance.product.Single(x => x.ProductId == selected.ProductId));
                foreach (var item in ProductCart.Instance.productDiscount)
                {
                    if (item.ConditionProductId == selected.ProductId)
                    {
                        ProductCart.Instance.productDiscount.Remove(item);
                    }
                }
            }

            tbCart.Text += Environment.NewLine + " - " + selected.ProductName;
        }

        protected void btnTotal_Click(object sender, EventArgs e)
        {
            try
            {
                ICartActionInterface iCartActionInterface = new CartAction();
                iCartActionInterface.GetTotal(connectionString).ToString();
                tbCart.Text += Environment.NewLine + ProductCart.Instance.printResult;
            }
            catch (Exception ex)
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