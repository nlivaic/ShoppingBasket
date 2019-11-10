using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingBasket.SharedKernel;

namespace ShoppingBasket.Core
{
    public class ShoppingBasketService
    {
        public ShoppingBasket CreateShoppingBasket(IEnumerable<Item> items, IEnumerable<Discount> discounts)
        {
            var shoppingBasket = new ShoppingBasket(items);
            if (discounts?.Count() > 0)
            {
                var eligibleDiscounts = FindEligibleDiscounts(shoppingBasket, discounts);
                var eligibleDiscountsByTotalBenefit = eligibleDiscounts.OrderByDescending(discount => discount.Target.Price * discount.PriceReductionPercentage / 100);
                ApplyDiscounts(shoppingBasket, eligibleDiscountsByTotalBenefit);
            }
            return shoppingBasket;
        }

        private List<Discount> FindEligibleDiscounts(ShoppingBasket shoppingBasket, IEnumerable<Discount> discounts)
        {
            var productsInBasket = shoppingBasket.Items.Select(item => item.Product).ToList();
            var eligibleDiscounts = new List<Discount>();
            foreach (var discount in discounts)
            {
                if (productsInBasket.Intersect(discount.Scope).Any())
                {
                    eligibleDiscounts.Add(discount);
                }
            }
            return eligibleDiscounts;
        }

        private void ApplyDiscounts(ShoppingBasket shoppingBasket, IEnumerable<Discount> eligibleDiscountsByTotalBenefit)
        {
            foreach (var discount in eligibleDiscountsByTotalBenefit)
            {
                do
                {
                    var scopedProducts = shoppingBasket
                        .Items
                        .Where(item => item.Discount == null)       // At this point we have only products without any discounts assigned.
                        .Select(item => item.Product)
                        .ToList()
                        .StrictIntersect(discount.Scope)           // A list of products that are scoped within current discount.
                        .ToList();

                    scopedProducts.ForEach(scopedProduct => shoppingBasket
                        .Items
                        .Where(item => item.Discount == null)
                        .First(item => item.Product == scopedProduct)
                        .ScopeDiscount(discount));
                    // Same discount is applied as long as there are available scoped products w/o discount applied to them.
                    if (scopedProducts.Count == 0)
                    {
                        break;
                    }
                } while (true);
            }
        }
    }

    public static class LinqExtensions
    {
        /// <summary>
        /// Returns `second` if all elements of `second` can be found in `first` list.
        /// Returns empty list if at least one element of `second` is not found in `first` list.
        /// </summary>
        public static IEnumerable<T> StrictIntersect<T>(this IEnumerable<T> first, IEnumerable<T> second) where T : BaseEntity<Guid, T>
        {
            var groupedFirst = first.GroupBy(i => i.Id);
            var groupedSecond = second.GroupBy(d => d.Id);
            var isStrictIntersect = groupedSecond.All(gd => groupedFirst.SingleOrDefault(gi => gi.Key == gd.Key)?.Count() >= gd.Count());
            return isStrictIntersect ? second : new List<T>();
        }
    }
}

