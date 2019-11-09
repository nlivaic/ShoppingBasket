using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingBasket.SharedKernel;

namespace ShoppingBasket.Core
{
    public class Discount : BaseEntity<Guid, Discount>
    {
        public string Name { get; private set; }
        public decimal PriceReductionPercentage { get; private set; }
        public IEnumerable<Product> Scope
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

        public Discount(string name, decimal priceReductionPercentage, List<Product> requirements, Product target)
            : this(Guid.NewGuid(), name, priceReductionPercentage, requirements, target)
        {
        }

        public Discount(Guid id, string name, decimal priceReductionPercentage, List<Product> requirements, Product target)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Discount must have a name.");
            }
            if (requirements == null || requirements.Count == 0 || target == null)
            {
                throw new ArgumentException("Discount must have at least one requirement and one target.");
            }
            if (requirements.Any(r => r == target))
            {
                throw new ArgumentException("Discount cannot have target as a requirement.");
            }
            if (priceReductionPercentage <= 0m || priceReductionPercentage > 100m)
            {
                throw new ArgumentException("Discount must have a positive price reduction percentage, up to (and including) 100.");
            }
            Id = id;
            Name = name;
            PriceReductionPercentage = priceReductionPercentage;
            _scope = new List<Product>(requirements);
            _scope.Add(target);
            _requirements = requirements;
        }
    }
}