using StoreLib.action.sqlHelper;
using StoreLib.models;
using System;
using System.Linq;

namespace StoreLib.action
{
    public class CartAction : ICartActionInterface
    {
        public double GetTotal(string connectionString)
        {
            double result = 0;
            string print = string.Empty;

            //Add
            foreach (var itemProduct in ProductCart.Instance.product)
            {
                result += itemProduct.ProductPrice * itemProduct.Quantity;
                print += itemProduct.ProductName + "  No> " + itemProduct.Quantity + "  Price> " + itemProduct.ProductPrice + "  + " + itemProduct.ProductPrice * itemProduct.Quantity + Environment.NewLine;
                //Get discount
                if (ProductCart.Instance.productDiscount.Count > 0)
                {
                    foreach (var itemDiscount in ProductCart.Instance.productDiscount)
                    {
                        //Check if it is in the basket
                        if (itemProduct.ProductId == itemDiscount.ConditionProductId && itemProduct.Quantity >= itemDiscount.ConditionQuantity &&
                            ProductCart.Instance.product.Exists(x => x.ProductId == itemDiscount.OnProductId))
                        {
                            //What the customer gets back
                            int numberOfPotentialDiscounts = itemProduct.Quantity / itemDiscount.ConditionQuantity;
                            int numberOfElementsInTheBasket = ProductCart.Instance.product.FirstOrDefault(x => x.ProductId == itemDiscount.ConditionProductId).Quantity;
                            int numberOfDiscounts = numberOfElementsInTheBasket < numberOfPotentialDiscounts ? numberOfElementsInTheBasket : numberOfPotentialDiscounts;

                            //Give back
                            for (int i = 0; i < numberOfDiscounts; i++)
                            {
                                var ProductDiscountName = ProductCart.Instance.product.FirstOrDefault(x => x.ProductId == itemDiscount.ConditionProductId).ProductName;
                                switch (itemDiscount.DiscountType)
                                {
                                    //Percent
                                    case 1:
                                        result -= ProductCart.Instance.product.FirstOrDefault(x => x.ProductId == itemDiscount.OnProductId).ProductPrice * itemDiscount.DiscountNumber / 100;
                                        print += "Discount on " + ProductDiscountName + " " + itemDiscount.DiscountNumber + "%" + "  - " + ProductCart.Instance.product.FirstOrDefault(x => x.ProductId == itemDiscount.OnProductId).ProductPrice * itemDiscount.DiscountNumber / 100 + Environment.NewLine; ;
                                        break;
                                    //GetFree
                                    case 2:
                                        result -= ProductCart.Instance.product.FirstOrDefault(x => x.ProductId == itemDiscount.OnProductId).ProductPrice;
                                        print += "Discount on " + ProductDiscountName + " GetFree: " + ProductCart.Instance.product.FirstOrDefault(x => x.ProductId == itemDiscount.OnProductId).ProductName + "  - " + ProductCart.Instance.product.FirstOrDefault(x => x.ProductId == itemDiscount.OnProductId).ProductPrice + Environment.NewLine; ;
                                        break;
                                    //GetMoneyDiscount
                                    case 3:
                                        result -= itemDiscount.DiscountNumber;
                                        print += "Discount on " + ProductDiscountName + "  MoneyBack:  - " + itemDiscount.DiscountNumber + Environment.NewLine; ;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            //Print
            print += "Total:....... " + result;
            ProductCart.Instance.printResult = print;
            sqlAction.InsertData(connectionString, print);

            return result;
        }
    }
}
