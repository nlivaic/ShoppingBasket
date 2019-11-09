using System;
using Xunit;

namespace ShoppingBasket.SharedKernel.Tests
{
    public class SharedKernelTests
    {
        [Fact]
        public void EntityObjects_CanTestForEquality()
        {
            // Arrange
            Core.ShoppingBasket target1 = new Core.ShoppingBasket(new Guid("54d7d622-b6c0-4791-b54f-086be047aee8"), null);
            Core.ShoppingBasket target2 = new Core.ShoppingBasket(new Guid("54d7d622-b6c0-4791-b54f-086be047aee8"), null);

            // Act
            bool result = target1 == target2;

            // Assert
            Assert.True(result);
        }


        [Fact]
        public void EntityObjects_CanTestForInequality()
        {
            // Arrange
            Core.ShoppingBasket target1 = new Core.ShoppingBasket(new Guid("54d7d622-b6c0-4791-b54f-086be047aee8"), null);
            Core.ShoppingBasket target2 = new Core.ShoppingBasket(new Guid("7088126c-79a1-478f-85c5-a9a5881f4702"), null);

            // Act
            bool result = target1 == target2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EntityObjects_DefaultId_Throws()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => new Core.ShoppingBasket(default(Guid), null));
        }

        [Fact]
        public void EntityObjects_GetNonDefaultId()
        {
            // Arrange
            Core.ShoppingBasket target1 = new Core.ShoppingBasket();

            // Act
            bool result = target1.Id != default(Guid);

            // Assert
            Assert.True(result);
        }
    }
}
