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

        public Product(Guid id, string name, decimal price) : base(id)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Product must have a name.");
            }
            if (price <= 0m)
            {
                throw new ArgumentException("Product must have a positive price.");
            }
            Name = name;
            Price = price;
        }

    }
}