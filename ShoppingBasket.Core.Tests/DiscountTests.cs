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

        [Fact]
        public void Discount_CreateDiscountWithInvalidPriceReduction_Throws()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => DiscountBuilder.BuildWithInvalidPriceReduction());
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
    }
}
