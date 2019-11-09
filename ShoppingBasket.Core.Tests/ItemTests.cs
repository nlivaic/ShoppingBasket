using System;
using Xunit;

namespace ShoppingBasket.Core.Tests
{
    public class ItemTests
    {
        [Fact]
        public void Item_WithoutProduct_Throws()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => ItemBuilder.BuildWithoutProduct());
        }

        [Fact]
        public void Item_WithoutDiscount_IsValid()
        {
            // Arrange, Act
            Item target = new ItemBuilder()
                .AddProduct(ProductBuilder.Bread)
                .Build();

            // Assert
            Assert.Equal(ProductBuilder.Bread, target.Product);
        }

        [Fact]
        public void Item_CanScopeDiscount()
        {
            // Arrange, Act
            Discount breadDiscount = new DiscountBuilder()
                .ButterBreadDiscount()
                .Build();
            Item target = new ItemBuilder()
                .AddProduct(ProductBuilder.Bread)
                .AddDiscount(breadDiscount)
                .Build();

            // Assert
            Assert.Equal(breadDiscount, target.Discount);
        }

        [Fact]
        public void Item_WithoutDiscount_CalculatesProductPrice()
        {
            // Arrange, Act
            Item target = new ItemBuilder()
                .AddProduct(ProductBuilder.Bread)
                .Build();

            // Assert
            Assert.Equal(ProductBuilder.Bread.Price, target.FinalPrice);
        }

        [Fact]
        public void Item_WithDiscount_CalculatesDiscountedPrice()
        {
            // Arrange, Act
            Item target = new ItemBuilder()
                .AddProduct(ProductBuilder.Bread)
                .AddDiscount(new DiscountBuilder()
                    .ButterBreadDiscount()
                    .Build())
                .Build();

            // Assert
            Assert.Equal(0.5m, target.FinalPrice);
        }
    }
}
