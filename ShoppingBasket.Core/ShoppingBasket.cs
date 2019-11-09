using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingBasket.SharedKernel;

namespace ShoppingBasket.Core
{
    public class ShoppingBasket : BaseEntity<Guid, ShoppingBasket>
    {
        public List<Item> Items { get; private set; }
        public decimal TotalPrice { get => Items.Sum(item => item.FinalPrice); }

        public ShoppingBasket(List<Item> items) : this(Guid.NewGuid(), items)
        {
        }

        public ShoppingBasket(Guid id, List<Item> items)
        {
            Id = id;
            Items = items;
        }
    }
}