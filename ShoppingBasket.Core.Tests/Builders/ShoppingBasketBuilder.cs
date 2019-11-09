using System.Collections.Generic;

namespace ShoppingBasket.Core.Tests
{
    public class ShoppingBasketBuilder
    {
        private ShoppingBasket _target;
        private List<Product> _products;

        public ShoppingBasketBuilder()
        {
            _target = new ShoppingBasket();
        }

        public static ShoppingBasket BuildWithoutItemList() => new ShoppingBasket(null);

        public ShoppingBasketBuilder AddButter()
        {
            _products.Add(ProductBuilder.Butter);
            return this;
        }

        public ShoppingBasketBuilder AddMilk()
        {
            _products.Add(ProductBuilder.Milk);
            return this;
        }

        public ShoppingBasketBuilder AddBread()
        {
            _products.Add(ProductBuilder.Bread);
            return this;
        }

        public ShoppingBasket Build() => _target;
    }
}
