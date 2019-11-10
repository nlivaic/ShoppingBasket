using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShoppingBasket.SharedKernel.Tests
{
    public class LinqExtensionsTests
    {
        [Fact]
        public void LinqExtensions_StrictIntersect_FirstListHasNoIntersect_ReturnsEmpty()
        {
            // Arrange
            List<Core.ShoppingBasket> first = new List<Core.ShoppingBasket> {
                ShoppingBasketBuilder.Build1,
                ShoppingBasketBuilder.Build2
            };
            List<Core.ShoppingBasket> second = new List<Core.ShoppingBasket> {
                ShoppingBasketBuilder.Build3,
                ShoppingBasketBuilder.Build4,
                ShoppingBasketBuilder.Build5
            };

            // Act
            var result = first.StrictIntersect(second);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void LinqExtensions_StrictIntersect_FirstListEmpty_ReturnsEmpty()
        {
            // Arrange
            List<Core.ShoppingBasket> first = new List<Core.ShoppingBasket>();
            List<Core.ShoppingBasket> second = new List<Core.ShoppingBasket> {
                ShoppingBasketBuilder.Build3,
                ShoppingBasketBuilder.Build4,
                ShoppingBasketBuilder.Build5
            };

            // Act
            var result = first.StrictIntersect(second);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void LinqExtensions_StrictIntersect_SecondListEmpty_ReturnsEmpty()
        {
            // Arrange
            List<Core.ShoppingBasket> first = new List<Core.ShoppingBasket> {
                ShoppingBasketBuilder.Build1
            };
            List<Core.ShoppingBasket> second = new List<Core.ShoppingBasket>();

            // Act
            var result = first.StrictIntersect(second);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void LinqExtensions_StrictIntersect_FirstListHasExactIntersect_ReturnsSecondList()
        {
            // Arrange
            List<Core.ShoppingBasket> first = new List<Core.ShoppingBasket> {
                ShoppingBasketBuilder.Build1,
                ShoppingBasketBuilder.Build2
            };
            List<Core.ShoppingBasket> second = new List<Core.ShoppingBasket> {
                ShoppingBasketBuilder.Build1,
                ShoppingBasketBuilder.Build2
            };

            // Act
            var result = first.StrictIntersect(second).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.True(result[0] == ShoppingBasketBuilder.Build1);
            Assert.True(result[1] == ShoppingBasketBuilder.Build2);
        }

        [Fact]
        public void LinqExtensions_StrictIntersect_FirstListHasPartialIntersect_ReturnsEmpty()
        {
            // Arrange
            List<Core.ShoppingBasket> first = new List<Core.ShoppingBasket> {
                ShoppingBasketBuilder.Build1,
                ShoppingBasketBuilder.Build2
            };
            List<Core.ShoppingBasket> second = new List<Core.ShoppingBasket> {
                ShoppingBasketBuilder.Build1,
                ShoppingBasketBuilder.Build2,
                ShoppingBasketBuilder.Build3
            };

            // Act
            var result = first.StrictIntersect(second);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void LinqExtensions_StrictIntersect_FirstListHasMultipleIntersects_ReturnsSecondList()
        {
            // Arrange
            List<Core.ShoppingBasket> first = new List<Core.ShoppingBasket> {
                ShoppingBasketBuilder.Build1,
                ShoppingBasketBuilder.Build2,
                ShoppingBasketBuilder.Build3,
                ShoppingBasketBuilder.Build1,
                ShoppingBasketBuilder.Build2,
                ShoppingBasketBuilder.Build3
            };
            List<Core.ShoppingBasket> second = new List<Core.ShoppingBasket> {
                ShoppingBasketBuilder.Build1,
                ShoppingBasketBuilder.Build2
            };

            // Act
            var result = first.StrictIntersect(second).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.True(result[0] == ShoppingBasketBuilder.Build1);
            Assert.True(result[1] == ShoppingBasketBuilder.Build2);
        }
    }
}