using System;
using System.Collections.Generic;

namespace ShoppingBasket.Core.Tests
{
    public class DiscountBuilder
    {
        private Guid _id;
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
            _id = new Guid("e5e3c173-0c1c-4d88-969d-64d33d96722e");
            _name = "ButterBreadDiscount";
            _requirements.Add(ProductBuilder.Butter);
            _requirements.Add(ProductBuilder.Butter);
            _discountTarget = ProductBuilder.Bread;
            _priceReductionPercentage = 50;
            return this;
        }

        public DiscountBuilder MilkBreadDiscount()
        {
            _id = new Guid("e5e3c173-0c1c-4d88-969d-64d33d96722e");
            _name = "MilkBreadDiscount";
            _requirements.Add(ProductBuilder.Milk);
            _requirements.Add(ProductBuilder.Milk);
            _discountTarget = ProductBuilder.Bread;
            _priceReductionPercentage = 80;
            return this;
        }

        public DiscountBuilder ThreeMilksDiscount()
        {
            _id = new Guid("e5e3c173-0c1c-4d88-969d-64d33d96722e");
            _name = "ThreeMilksDiscount";
            _requirements.Add(ProductBuilder.Milk);
            _requirements.Add(ProductBuilder.Milk);
            _requirements.Add(ProductBuilder.Milk);
            _discountTarget = ProductBuilder.Milk;
            _priceReductionPercentage = 100;
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

        public Discount Build() =>
            new Discount(_id == default(Guid) ? Guid.NewGuid() : _id, _name, _priceReductionPercentage, _requirements, _discountTarget);

    }
}
