using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingBasket.SharedKernel;

namespace ShoppingBasket.Core
{
    public class ShoppingBasket : BaseEntity<Guid, ShoppingBasket>
    {
        public IEnumerable<Item> Items
        {
            get => _items;
        }
        public decimal TotalSum { get => Items.Sum(item => item.FinalPrice); }

        private List<Item> _items;
        private IEnumerable<Discount> _discounts;

        public ShoppingBasket() : this(Guid.NewGuid(), new List<Item>(), new List<Discount>())
        {
        }

        public ShoppingBasket(Guid id) : this(id, new List<Item>(), new List<Discount>())
        {
        }

        public ShoppingBasket(IEnumerable<Item> items)
            : this(Guid.NewGuid(),
                items == null ? new List<Item>() : items,
                new List<Discount>())
        {
        }

        public ShoppingBasket(IEnumerable<Item> items, IEnumerable<Discount> discounts)
            : this(Guid.NewGuid(),
                items == null ? new List<Item>() : items,
                discounts == null ? new List<Discount>() : discounts)
        {
        }

        public ShoppingBasket(Guid id, IEnumerable<Item> items, IEnumerable<Discount> discounts) : base(id)
        {
            _items = new List<Item>(items);
            _discounts = new List<Discount>(discounts);
            ProcessDiscounts();
        }

        public void AddItem(Item item)
        {
            _items.ForEach(i => i.Descope());
            _items.Add(item);
            ProcessDiscounts();
        }

        public override string ToString() => $"Shopping Basket with '{_items.Count}' products, total sum {TotalSum}.";

        private void ProcessDiscounts()
        {
            if (_discounts?.Count() > 0)
            {
                var eligibleDiscounts = FindEligibleDiscounts();
                var eligibleDiscountsByTotalBenefit = eligibleDiscounts.OrderByDescending(discount => discount.Target.Price * discount.PriceReductionPercentage / 100);
                ApplyDiscounts(eligibleDiscountsByTotalBenefit);
            }
        }

        private List<Discount> FindEligibleDiscounts()
        {
            var productsInBasket = _items.Select(item => item.Product).ToList();
            var eligibleDiscounts = new List<Discount>();
            foreach (var discount in _discounts)
            {
                if (productsInBasket.Intersect(discount.Scope).Any())
                {
                    eligibleDiscounts.Add(discount);
                }
            }
            return eligibleDiscounts;
        }

        private void ApplyDiscounts(IEnumerable<Discount> eligibleDiscountsByTotalBenefit)
        {
            foreach (var discount in eligibleDiscountsByTotalBenefit)
            {
                do
                {
                    var scopedProducts = _items
                        .Where(item => item.Discount == null)       // At this point we have only products without any discounts assigned.
                        .Select(item => item.Product)
                        .ToList()
                        .StrictIntersect(discount.Scope)           // A list of products that are scoped within current discount.
                        .ToList();

                    scopedProducts.ForEach(scopedProduct => _items
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
}