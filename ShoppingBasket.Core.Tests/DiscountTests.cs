using System;
using System.Linq;
using Xunit;

namespace ShoppingBasket.Core.Tests
{
    public class DiscountTests
    {
        [Fact]
        public void Discount_CreateDiscountWithoutName_Throws()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => DiscountBuilder.BuildWithoutName());
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(0)]
        [InlineData(101)]
        public void Discount_CreateDiscountWithInvalidPriceReductionPercentage_Throws(decimal priceReductionPercentage)
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => new DiscountBuilder()
                .AddPriceReductionPercentage(priceReductionPercentage)
                .AddRequirements(ProductBuilder.Butter)
                .AddTarget(ProductBuilder.Milk)
                .Build());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        [InlineData(100)]
        public void Discount_CanCreateDiscountWithValidPriceReductionPercentage(decimal priceReductionPercentage)
        {
            // Arrange, Act
            Discount target = new DiscountBuilder()
                .AddPriceReductionPercentage(priceReductionPercentage)
                .AddRequirements(ProductBuilder.Butter)
                .AddTarget(ProductBuilder.Milk)
                .Build();

            // Assert
            Assert.Equal(priceReductionPercentage, target.PriceReductionPercentage);
        }

        [Fact]
        public void Discount_CreateDiscountWithoutRequirements_Throws()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => DiscountBuilder.BuildWithoutRequirements());
        }

        [Fact]
        public void Discount_CreateDiscountWithoutTarget_Throws()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => DiscountBuilder.BuildWithoutTarget());
        }

        [Fact]
        public void Discount_CreateDiscountWithRequirementAndTargetSame_Throws()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => DiscountBuilder.BuildWithRequirementAndTargetSame());
        }

        [Fact]
        public void Discount_CanScope()
        {
            // Arrange, Act
            var target = new DiscountBuilder()
                .AddPriceReductionPercentage(1m)
                .AddRequirements(ProductBuilder.Butter, ProductBuilder.Bread)
                .AddTarget(ProductBuilder.Milk)
                .Build()
                .Scope;

            // Assert
            Assert.Equal(3, target.Count());
            Assert.True(target.Any(t => t == ProductBuilder.Butter));
            Assert.True(target.Any(t => t == ProductBuilder.Bread));
            Assert.True(target.Any(t => t == ProductBuilder.Milk));
        }

        [Fact]
        public void Discount_CanBe100Percent()
        {
            // Arrange, Act
            var target = new DiscountBuilder()
                .AddPriceReductionPercentage(1m)
                .AddRequirements(ProductBuilder.Butter, ProductBuilder.Bread)
                .AddTarget(ProductBuilder.Milk)
                .Build()
                .Scope;

            // Assert
            Assert.Equal(3, target.Count());
            Assert.True(target.Any(t => t == ProductBuilder.Butter));
            Assert.True(target.Any(t => t == ProductBuilder.Bread));
            Assert.True(target.Any(t => t == ProductBuilder.Milk));
        }
    }
}
