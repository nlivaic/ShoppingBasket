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
        public decimal TotalPrice { get => Items.Sum(item => item.FinalPrice); }

        private List<Item> _items;

        public ShoppingBasket() : this(Guid.NewGuid(), new List<Item>())
        {
        }

        public ShoppingBasket(List<Item> items) : this(Guid.NewGuid(), items)
        {
        }

        public ShoppingBasket(Guid id, List<Item> items)
        {
            Id = id;
            _items = items;
        }
    }
}