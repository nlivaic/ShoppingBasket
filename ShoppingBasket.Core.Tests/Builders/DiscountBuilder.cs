using System.Collections.Generic;

namespace ShoppingBasket.Core.Tests
{
    public class DiscountBuilder
    {
        private decimal _priceReductionPercentage;
        private List<Product> _requirements;
        private Product _discountTarget;
        private string _name;

        public DiscountBuilder(string name = "Name")
        {
            _name = name;
            _requirements = new List<Product>();
        }

        public DiscountBuilder ButterBreadDiscount()
        {
            return this;
        }

        public DiscountBuilder ThreeMilksDiscount()
        {
            return this;
        }

        public DiscountBuilder AddRequirements(params Product[] products)
        {
            _requirements.AddRange(products);
            return this;
        }

        public DiscountBuilder AddTarget(Product target)
        {
            _discountTarget = target;
            return this;
        }

        public DiscountBuilder AddPriceReductionPercentage(decimal priceReductionPercentage)
        {
            _priceReductionPercentage = priceReductionPercentage;
            return this;
        }


        public static Discount BuildWithoutName() => new Discount("", 1m,
            new List<Product> { new Product("Product #1", 1m) }, new Product("Product #2", 1m));

        public static Discount BuildWithoutRequirements() => new Discount("Name", 1m,
            new List<Product>(), new Product("Product #2", 1m));

        public static Discount BuildWithoutTarget() => new Discount("Name", 1m,
            new List<Product> { new Product("Product #1", 1m) }, null);

        public static Discount BuildWithRequirementAndTargetSame() => new Discount("Name", 1m,
            new List<Product>
            {
                ProductBuilder.Bread,
                ProductBuilder.Milk,
            },
            ProductBuilder.Milk);

        public Discount Build() =>
            new Discount(_name, _priceReductionPercentage, _requirements, _discountTarget);
    }
}
