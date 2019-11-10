using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShoppingBasket.Core.Tests
{
    public class ShoppingBasketScenarios
    {
        [Fact]
        public void Scenario1()
        {
            // Arrange
            var items = new List<Item>();
            var discounts = new List<Discount> {
                new DiscountBuilder().ButterBreadDiscount().Build(),
                new DiscountBuilder().ThreeMilksDiscount().Build()
            };
            var target = new ShoppingBasket(items, discounts, new DiscountProcessor());

            // Act
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Bread).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Butter).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());

            // Assert
            Assert.Equal(2.95m, target.TotalSum);
        }

        [Fact]
        public void Scenario2()
        {
            // Arrange
            var items = new List<Item>();
            var discounts = new List<Discount> {
                new DiscountBuilder().ButterBreadDiscount().Build(),
                new DiscountBuilder().ThreeMilksDiscount().Build()
            };
            var target = new ShoppingBasket(items, discounts, new DiscountProcessor());

            // Act
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Butter).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Butter).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Bread).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Bread).Build());

            // Assert
            Assert.Equal(3.10m, target.TotalSum);
        }

        [Fact]
        public void Scenario3()
        {
            // Arrange
            var items = new List<Item>();
            var discounts = new List<Discount> {
                new DiscountBuilder().ButterBreadDiscount().Build(),
                new DiscountBuilder().ThreeMilksDiscount().Build()
            };
            var target = new ShoppingBasket(items, discounts, new DiscountProcessor());

            // Act
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());

            // Assert
            Assert.Equal(3.45m, target.TotalSum);
        }

        [Fact]
        public void Scenario4()
        {
            // Arrange
            var items = new List<Item>();
            var discounts = new List<Discount> {
                new DiscountBuilder().ButterBreadDiscount().Build(),
                new DiscountBuilder().ThreeMilksDiscount().Build()
            };
            var target = new ShoppingBasket(items, discounts, new DiscountProcessor());

            // Act
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Butter).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Butter).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Bread).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());
            target.AddItem(new ItemBuilder().AddProduct(ProductBuilder.Milk).Build());

            // Assert
            Assert.Equal(9m, target.TotalSum);
        }
    }
}
