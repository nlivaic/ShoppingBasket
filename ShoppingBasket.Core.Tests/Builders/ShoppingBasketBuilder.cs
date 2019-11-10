using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Core.Tests
{
    public class ShoppingBasketBuilder
    {
        private ShoppingBasket _target;
        private List<Product> _products;

        public ShoppingBasketBuilder()
        {
            _products = new List<Product>();
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

        public ShoppingBasket Build() => new ShoppingBasket(_products.Select(p => new ItemBuilder().AddProduct(p).Build()));
    }
}
