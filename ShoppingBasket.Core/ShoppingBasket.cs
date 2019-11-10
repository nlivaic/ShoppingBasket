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

        public ShoppingBasket() : this(Guid.NewGuid(), new List<Item>())
        {
        }

        public ShoppingBasket(Guid id) : this(id, new List<Item>())
        {
        }

        public ShoppingBasket(IEnumerable<Item> items) : this(Guid.NewGuid(), items == null ? new List<Item>() : items)
        {
        }

        public ShoppingBasket(Guid id, IEnumerable<Item> items) : base(id)
        {
            _items = new List<Item>(items);
        }

        public override string ToString() => $"Shopping Basket with '{_items.Count}' products, total sum {TotalSum}.";
    }
}