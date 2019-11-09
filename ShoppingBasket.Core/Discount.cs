using System;
using System.Collections.Generic;
using ShoppingBasket.SharedKernel;

namespace ShoppingBasket.Core
{
    public class Discount : BaseEntity<Guid, Discount>
    {
        public string Name { get; private set; }
        public decimal PriceReductionPercentage { get; private set; }
        public List<Product> Scope
        {
            get => _scope;
        }
        public List<Product> Requirements
        {
            get => _requirements;
        }
        public Product Target;

        private List<Product> _scope;
        private List<Product> _requirements;

        public Discount(string name, decimal priceReductionPercentage, List<Product> scope, List<Product> requirements)
            : this(Guid.NewGuid(), name, priceReductionPercentage, scope, requirements)
        {
        }

        public Discount(Guid id, string name, decimal priceReductionPercentage, List<Product> scope, List<Product> requirements)
        {
            Id = id;
            Name = name;
            PriceReductionPercentage = priceReductionPercentage;
            _scope = scope;
            _requirements = requirements;
        }
    }
}