using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShoppingBasket.Core.Tests
{
    public class ShoppingBasketTests
    {
        [Fact]
        public void ShoppingBasket_CanCreateWithoutItems()
        {
            // Arrange, Act
            ShoppingBasket target = new ShoppingBasketBuilder().Build();

            // Assert
            Assert.Equal(0m, target.TotalSum);
        }

        [Fact]
        public void ShoppingBasket_WithoutItems_ZeroTotalSum()
        {
            // Arrange, Act
            ShoppingBasket target = new ShoppingBasketBuilder().Build();

            // Assert
            Assert.Empty(target.Items);
        }

        [Fact]
        public void ShoppingBasket_WithMultipleProducts_CalculatesTotalPrice()
        {
            // Arrange, Act
            ShoppingBasket target = new ShoppingBasketBuilder()
                .AddButter()
                .AddButter()
                .AddMilk()
                .AddMilk()
                .AddBread()
                .Build();
            var items = target.Items.ToList();

            // Assert
            Assert.Equal(5, target.Items.Count());
            Assert.Equal(4.9m, target.TotalSum);
        }

        /******************** Foo Foo ********************/
        [Fact]
        public void ShoppingBasketService_CanBuildShoppingBasketWithoutItemsAndDiscounts()
        {
            // Arrange
            var items = new List<Item>();
            IEnumerable<Discount> discounts = null;
            var target = new ShoppingBasketService();

            // Act
            var shoppingBasket = target.CreateShoppingBasket(items, discounts);

            // Assert
            Assert.Empty(shoppingBasket.Items);
            Assert.Equal(0m, shoppingBasket.TotalSum);
        }

        [Fact]
        public void ShoppingBasketService_CanBuildShoppingBasketWithSingleDiscount_CalculatesDiscountsAndTotalPrice()
        {
            // Arrange
            var items = new List<Item>
            {
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Bread).Build()
            };
            var discounts = new List<Discount> {
                new DiscountBuilder().ButterBreadDiscount().Build()
            };
            var target = new ShoppingBasketService();

            // Act
            var shoppingBasket = target.CreateShoppingBasket(items, discounts);
            var discountedItems = shoppingBasket.Items.ToList();

            // Assert
            Assert.Equal(4, discountedItems.Count);
            Assert.Equal(2.9m, shoppingBasket.TotalSum);
            Assert.Equal(3, discountedItems.Where(i => i.Discount != null).Count());
            Assert.Single(discountedItems.Where(i => i.Product.Name == "Butter" && i.Discount == null));
            Assert.All(discountedItems.Where(i => i.Product.Name == "Butter" && i.Discount == null), item => Assert.Equal(item.Product.Price, item.FinalPrice));      // Discount was not applied.
            Assert.NotEqual(discountedItems.Single(item => item.Product.Name == "Bread").Product.Price, discountedItems.Single(item => item.Product.Name == "Bread").FinalPrice);   // Discount was applied.
        }

        [Fact]
        public void ShoppingBasketService_CanBuildShoppingBasketWithTwoDiscounts_WithOverlappingTarget_CalculatesDiscountsOnlyOnce()
        {
            // Arrange
            var items = new List<Item>
            {
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Milk).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Milk).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Bread).Build()
            };
            var discounts = new List<Discount> {
                new DiscountBuilder().ButterBreadDiscount().Build(),    // Gives 50% off on bread.
                new DiscountBuilder().MilkBreadDiscount().Build()       // Gives 80% off on bread.
            };
            var target = new ShoppingBasketService();

            // Act
            var shoppingBasket = target.CreateShoppingBasket(items, discounts);
            var discountedItems = shoppingBasket.Items.ToList();

            // Assert - discounts cannot be compounded on same target.
            Assert.Equal(5, discountedItems.Count);
            Assert.Equal(4.1m, shoppingBasket.TotalSum);
            Assert.Null(discountedItems[0].Discount);
            Assert.Null(discountedItems[1].Discount);
            Assert.NotNull(discountedItems[2].Discount);
            Assert.NotNull(discountedItems[3].Discount);
            Assert.NotNull(discountedItems[4].Discount);
            Assert.Equal(discountedItems[0].Product.Price, discountedItems[0].FinalPrice);      // Discount was not applied.
            Assert.Equal(discountedItems[1].Product.Price, discountedItems[1].FinalPrice);      // Discount was not applied.
            Assert.Equal(discountedItems[2].Product.Price, discountedItems[2].FinalPrice);   // Discount was applied.
            Assert.Equal(discountedItems[3].Product.Price, discountedItems[3].FinalPrice);   // Discount was applied.
            Assert.NotEqual(discountedItems[4].Product.Price, discountedItems[4].FinalPrice);   // Discount was applied.

        }

        [Fact]
        public void ShoppingBasketService_CanBuildShoppingBasketWithSingleDiscountAndMultipleScopedProductGroups_CalculatesDiscountsAndTotalPrice()
        {
            // Arrange
            var items = new List<Item>
            {
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Bread).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Bread).Build()
            };
            var discounts = new List<Discount> {
                new DiscountBuilder().ButterBreadDiscount().Build()
            };
            var target = new ShoppingBasketService();

            // Act
            var shoppingBasket = target.CreateShoppingBasket(items, discounts);
            var discountedItems = shoppingBasket.Items.ToList();

            // Assert
            Assert.Equal(7, discountedItems.Count);
            Assert.Equal(5.0m, shoppingBasket.TotalSum);
            Assert.Equal(6, discountedItems.Where(i => i.Discount != null).Count());
            Assert.Single(discountedItems.Where(i => i.Product.Name == "Butter" && i.Discount == null));
            Assert.All(discountedItems.Where(i => i.Product.Name == "Butter" && i.Discount == null), item => Assert.Equal(item.Product.Price, item.FinalPrice));      // Discount was not applied.
            Assert.All(discountedItems.Where(item => item.Product.Name == "Bread"), item => Assert.NotEqual(item.Product.Price, item.FinalPrice));   // Discount was applied.
        }

        [Fact]
        public void ShoppingBasketService_CanBuildShoppingBasketWithDiscount_WithRequirementsInBasket_WithoutTargetProductInBasket_NoDiscountsApplied()
        {
            // Arrange
            var items = new List<Item>
            {
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build()
            };
            var discounts = new List<Discount> {
                new DiscountBuilder().ButterBreadDiscount().Build()
            };
            var target = new ShoppingBasketService();

            // Act
            var shoppingBasket = target.CreateShoppingBasket(items, discounts);
            var discountedItems = shoppingBasket.Items.ToList();

            // Assert
            Assert.Equal(2, discountedItems.Count);
            Assert.Equal(1.6m, shoppingBasket.TotalSum);
            Assert.Empty(discountedItems.Where(i => i.Discount != null));       // No discounts applied.
        }

        [Fact]
        public void ShoppingBasketService_CanBuildShoppingBasketWithNonEligibleDiscounts_NotImpactingPrice()
        {
            // Arrange
            var items = new List<Item>
            {
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Bread).Build()
            };
            var discounts = new List<Discount> {
                new DiscountBuilder().MilkBreadDiscount().Build()
            };
            var target = new ShoppingBasketService();

            // Act
            var shoppingBasket = target.CreateShoppingBasket(items, discounts);
            var discountedItems = shoppingBasket.Items.ToList();

            // Assert
            Assert.Equal(3, discountedItems.Count);
            Assert.Equal(2.6m, shoppingBasket.TotalSum);
            Assert.Empty(discountedItems.Where(i => i.Discount != null));
        }

        [Fact]
        public void ShoppingBasketService_CanBuildShoppingBasketWithBothEligibleAndNonEligibleDiscounts_CalculatesDiscountsAndTotalPrice()
        {
            // Arrange
            var items = new List<Item>
            {
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Butter).Build(),
                new ItemBuilder().AddProduct(ProductBuilder.Bread).Build()
            };
            var discounts = new List<Discount> {
                new DiscountBuilder().ButterBreadDiscount().Build(),
                new DiscountBuilder().MilkBreadDiscount().Build()
            };
            var target = new ShoppingBasketService();

            // Act
            var shoppingBasket = target.CreateShoppingBasket(items, discounts);
            var discountedItems = shoppingBasket.Items.ToList();

            // Assert
            Assert.Equal(3, discountedItems.Count);
            Assert.Equal(2.1m, shoppingBasket.TotalSum);
            Assert.All(discountedItems, i => Assert.Equal(i.Discount, new DiscountBuilder().ButterBreadDiscount().Build()));    //  Only 'ButterBreadDiscount' is applied.
            Assert.NotEqual(
                discountedItems
                    .Single(item => item.Product.Name == "Bread")
                    .Product
                    .Price,
                    discountedItems
                        .Single(item => item.Product.Name == "Bread")
                        .FinalPrice);   // Discount was applied.
        }
    }
}
