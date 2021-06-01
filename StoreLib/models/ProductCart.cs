using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLib.models
{
    public sealed class ProductCart
    {
        public List<Product> product { get; set; }

        public List<ProductDiscount> productDiscount { get; set; }

        public string printResult { get; set; }

        private ProductCart()
        {
            product = new List<Product>();
            productDiscount = new List<ProductDiscount>();
        }

        private static ProductCart instance = null;
        public static ProductCart Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductCart();
                }
                return instance;
            }
        }
    }
}
