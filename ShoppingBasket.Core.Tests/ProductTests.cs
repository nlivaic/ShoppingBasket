using System;
using Xunit;

namespace ShoppingBasket.Core.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_CreateWithoutName_Throws()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => ProductBuilder.BuildWithoutName());
        }

        [Fact]
        public void Product_CreateWithInvalidPrice()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => ProductBuilder.BuildWithInvalidPrice());
        }

        [Fact]
        public void Product_CanBuildValidProduct()
        {
            // Arrange, Act
            var target = ProductBuilder.Butter;

            // Assert
            Assert.Equal("Butter", target.Name);
            Assert.Equal(0.8m, target.Price);
        }
    }
}
