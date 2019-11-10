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
        public decimal TotalSum
        {
            get
            {
                var sum = Items.Sum(item => item.FinalPrice);
                _items.ForEach(item => _logger.Log(item.ToString()));
                _logger.Log($"Sum: {sum}");
                return sum;
            }
        }

        private IDiscountProcessor _discountProcessor;
        private List<Item> _items;
        private IEnumerable<Discount> _discounts;
        private IShoppingBasketLogger _logger;

        public ShoppingBasket(IDiscountProcessor discountProcessor)
            : this(Guid.NewGuid(), new List<Item>(), new List<Discount>(), new ShoppingBasketConsoleLogger(), discountProcessor)
        {
        }

        public ShoppingBasket(Guid id, IDiscountProcessor discountProcessor)
            : this(id, new List<Item>(), new List<Discount>(), new ShoppingBasketConsoleLogger(), discountProcessor)
        {
        }

        public ShoppingBasket(IEnumerable<Item> items, IDiscountProcessor discountProcessor)
            : this(Guid.NewGuid(),
                items == null ? new List<Item>() : items,
                new List<Discount>(),
                new ShoppingBasketConsoleLogger(),
                discountProcessor)
        {
        }

        public ShoppingBasket(IEnumerable<Item> items, IEnumerable<Discount> discounts, IDiscountProcessor discountProcessor)
            : this(Guid.NewGuid(),
                items == null ? new List<Item>() : items,
                discounts == null ? new List<Discount>() : discounts,
                new ShoppingBasketConsoleLogger(),
                discountProcessor)
        {
        }

        public ShoppingBasket(Guid id, IEnumerable<Item> items, IEnumerable<Discount> discounts, IShoppingBasketLogger logger, IDiscountProcessor discountProcessor) : base(id)
        {
            _items = new List<Item>(items);
            _discounts = new List<Discount>(discounts);
            _logger = logger;
            _discountProcessor = discountProcessor;
            _discountProcessor.ProcessDiscounts(_items, _discounts);
        }

        public void AddItem(Item item)
        {
            _items.ForEach(i => i.Descope());
            _items.Add(item);
            _discountProcessor.ProcessDiscounts(_items, _discounts);
        }

        public override string ToString() => $"Shopping Basket with '{_items.Count}' products, total sum {TotalSum}.";
    }
}