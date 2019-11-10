using System.Collections.Generic;
using System.Linq;
using ShoppingBasket.SharedKernel;

namespace ShoppingBasket.Core
{
    public class DiscountProcessor : IDiscountProcessor
    {
        public void ProcessDiscounts(IEnumerable<Item> items, IEnumerable<Discount> discounts)
        {
            if (discounts?.Count() > 0)
            {
                var eligibleDiscounts = FindEligibleDiscounts(items, discounts);
                var eligibleDiscountsByTotalBenefit = eligibleDiscounts.OrderByDescending(discount => discount.Target.Price * discount.PriceReductionPercentage / 100);
                ApplyDiscounts(items, eligibleDiscountsByTotalBenefit);
            }
        }

        private List<Discount> FindEligibleDiscounts(IEnumerable<Item> items, IEnumerable<Discount> discounts)
        {
            var productsInBasket = items.Select(item => item.Product).ToList();
            var eligibleDiscounts = new List<Discount>();
            foreach (var discount in discounts)
            {
                if (productsInBasket.StrictIntersect(discount.Scope).Any())
                {
                    eligibleDiscounts.Add(discount);
                }
            }
            return eligibleDiscounts;
        }

        private void ApplyDiscounts(IEnumerable<Item> items, IEnumerable<Discount> eligibleDiscountsByTotalBenefit)
        {
            bool itemTargeted = false;
            foreach (var discount in eligibleDiscountsByTotalBenefit)
            {
                do
                {
                    itemTargeted = false;
                    var scopedProducts = items
                        .Where(item => item.Discount == null)       // At this point we have only products without any discounts assigned.
                        .Select(item => item.Product)
                        .ToList()
                        .StrictIntersect(discount.Scope)           // A list of products that are scoped within current discount.
                        .ToList();
                    scopedProducts.ForEach(scopedProduct =>
                    {
                        var scopedItem = items
                            .Where(item => item.Discount == null)
                            .First(item => item.Product == scopedProduct);
                        if (!itemTargeted && scopedItem.Product == discount.Target)
                        {
                            scopedItem.ScopeDiscountTarget(discount);
                            itemTargeted = true;
                        }
                        else
                        {
                            scopedItem.ScopeDiscount(discount);
                        }
                    });
                    // Same discount is applied as long as there are available scoped products w/o discount applied to them.
                    if (scopedProducts.Count == 0)
                    {
                        break;
                    }
                } while (true);
            }
        }
    }
}