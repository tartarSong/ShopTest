namespace StoreLib.models
{
    public class ProductDiscount
    {
        /*
         * 1- Percent
         * 2- GetFree
         */
        public int DiscountType { get; set; }
        public int OnProductId { get; set; }
        public int ConditionProductId { get; set; }
        public int ConditionQuantity { get; set; }
        public double DiscountNumber { get; set; }

    }
}
