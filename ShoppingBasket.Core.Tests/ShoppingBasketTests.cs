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
    }
}
