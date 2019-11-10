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
    }
}
