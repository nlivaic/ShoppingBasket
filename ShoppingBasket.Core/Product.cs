using System;
using ShoppingBasket.SharedKernel;

namespace ShoppingBasket.Core
{
    public class Product : BaseEntity<Guid, Product>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, decimal price) : this(Guid.NewGuid(), name, price)
        {
        }

        public Product(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

    }
}