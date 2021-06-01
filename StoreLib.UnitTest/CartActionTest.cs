using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreLib.action;
using StoreLib.action.sqlHelper;
using StoreLib.models;
using System.Configuration;
using System.Collections.Generic;

namespace StoreLib.UnitTest
{
    [TestClass]
    public class CartActionTest
    {
        private string connectionString
        { get { return ConfigurationManager.ConnectionStrings["Default"].ConnectionString; } }

        private List<Product> products { get { return sqlAction.GetProduct(connectionString); } }

        [TestMethod]
        public void GetTotal_Scenario1_295()
        {
            //Arrange 
            ProductCart.Instance.product = new List<Product>();
            ProductCart.Instance.productDiscount = new List<ProductDiscount>();

            ProductCart.Instance.product.Add(new Product() { ProductId = 1, ProductName = "Butter", ProductPrice = 0.8, Quantity = 1 });
            ProductCart.Instance.productDiscount.AddRange(sqlAction.AddProductCart(connectionString, 1));

            ProductCart.Instance.product.Add(new Product() { ProductId = 2, ProductName = "Milk", ProductPrice = 1.15, Quantity = 1 });
            ProductCart.Instance.productDiscount.AddRange(sqlAction.AddProductCart(connectionString, 2));

            ProductCart.Instance.product.Add(new Product() { ProductId = 3, ProductName = "Bread", ProductPrice = 1, Quantity = 1 });
            ProductCart.Instance.productDiscount.AddRange(sqlAction.AddProductCart(connectionString, 3));

            //Act
            ICartActionInterface cartAction = new CartAction();
            var result = cartAction.GetTotal(connectionString);

            //Assert
            Assert.AreEqual(2.95, result);
        }

        [TestMethod]
        public void GetTotal_Scenario2_310()
        {
            //Arrange 
            ProductCart.Instance.product = new List<Product>();
            ProductCart.Instance.productDiscount = new List<ProductDiscount>();

            ProductCart.Instance.product.Add(new Product() { ProductId = 1, ProductName = "Butter", ProductPrice = 0.8, Quantity = 2 });
            ProductCart.Instance.productDiscount.AddRange(sqlAction.AddProductCart(connectionString, 1));

            ProductCart.Instance.product.Add(new Product() { ProductId = 3, ProductName = "Bread", ProductPrice = 1, Quantity = 2 });
            ProductCart.Instance.productDiscount.AddRange(sqlAction.AddProductCart(connectionString, 3));

            //Act
            ICartActionInterface cartAction = new CartAction();
            var result = cartAction.GetTotal(connectionString);

            //Assert
            Assert.AreEqual(3.1, result);
        }

        [TestMethod]
        public void GetTotal_Scenario3_345()
        {
            //Arrange 
            ProductCart.Instance.product = new List<Product>();
            ProductCart.Instance.productDiscount = new List<ProductDiscount>();

            ProductCart.Instance.product.Add(new Product() { ProductId = 2, ProductName = "Milk", ProductPrice = 1.15, Quantity = 4 });
            ProductCart.Instance.productDiscount.AddRange(sqlAction.AddProductCart(connectionString, 2));

            //Act
            ICartActionInterface cartAction = new CartAction();
            var result = cartAction.GetTotal(connectionString);

            //Assert
            Assert.AreEqual(3.45, result);
        }

        [TestMethod]
        public void GetTotal_Scenario4_9()
        {
            //Arrange 
            ProductCart.Instance.product = new List<Product>();
            ProductCart.Instance.productDiscount = new List<ProductDiscount>();

            ProductCart.Instance.product.Add(new Product() { ProductId = 1, ProductName = "Butter", ProductPrice = 0.8, Quantity = 2 });
            ProductCart.Instance.productDiscount.AddRange(sqlAction.AddProductCart(connectionString, 1));

            ProductCart.Instance.product.Add(new Product() { ProductId = 2, ProductName = "Milk", ProductPrice = 1.15, Quantity = 8 });
            ProductCart.Instance.productDiscount.AddRange(sqlAction.AddProductCart(connectionString, 2));

            ProductCart.Instance.product.Add(new Product() { ProductId = 3, ProductName = "Bread", ProductPrice = 1, Quantity = 1 });
            ProductCart.Instance.productDiscount.AddRange(sqlAction.AddProductCart(connectionString, 3));

            //Act
            ICartActionInterface cartAction = new CartAction();
            var result = cartAction.GetTotal(connectionString);

            //Assert
            Assert.AreEqual(9, result);
        }

    }
}
