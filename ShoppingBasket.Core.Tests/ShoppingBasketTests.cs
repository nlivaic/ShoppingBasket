using System;
using Xunit;

namespace ShoppingBasket.Core.Tests
{
    public class ShoppingBasketTests
    {
        [Fact]
        public void ShoppingBasket_CanCreateWithoutItems()
        {
            // Arrange, Act, Assert
            new ShoppingBasketBuilder().Build();
        }

        [Fact]
        public void ShoppingBasket_WithoutItems_ZeroTotalSum()
        {
            // Arrange, Act
            ShoppingBasket target = new ShoppingBasketBuilder().Build();

            // Assert
            Assert.Equal(0m, target.TotalSum);
        }

        [Fact]
        public void ShoppingBasket_CanCreateWithoutItemList_Throws()
        {
            // Arrange, Act, Assert
            ShoppingBasketBuilder.BuildWithoutItemList();
        }

    }
}
