namespace ShoppingBasket.Core.Tests
{
    public class ItemBuilder
    {
        private Item _target;

        public static Item BuildWithoutProduct() => new Item(null);

        public ItemBuilder AddProduct(Product product)
        {
            _target = new Item(product);
            return this;
        }

        public ItemBuilder AddDiscount(Discount discount)
        {
            _target.ScopeDiscountTarget(discount);
            return this;
        }

        public Item Build() => _target;
    }
}
