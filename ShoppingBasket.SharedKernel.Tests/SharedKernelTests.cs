using System;
using ShoppingBasket.Core;
using Xunit;

namespace ShoppingBasket.SharedKernel.Tests
{
    public class SharedKernelTests
    {
        [Fact]
        public void EntityObjects_CanTestForEquality()
        {
            // Arrange
            Core.ShoppingBasket target1 = ShoppingBasketBuilder.Build1;
            Core.ShoppingBasket target2 = ShoppingBasketBuilder.Build1;

            // Act
            bool result = target1 == target2;

            // Assert
            Assert.True(result);
        }


        [Fact]
        public void EntityObjects_CanTestForInequality()
        {
            // Arrange
            Core.ShoppingBasket target1 = ShoppingBasketBuilder.Build1;
            Core.ShoppingBasket target2 = ShoppingBasketBuilder.Build2;

            // Act
            bool result = target1 == target2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EntityObjects_DefaultId_Throws()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => new Core.ShoppingBasket(default(Guid), new DiscountProcessor()));
        }

        [Fact]
        public void EntityObjects_GetNonDefaultId()
        {
            // Arrange
            Core.ShoppingBasket target1 = new Core.ShoppingBasket(new DiscountProcessor());

            // Act
            bool result = target1.Id != default(Guid);

            // Assert
            Assert.True(result);
        }
    }
}
