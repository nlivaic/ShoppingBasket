using System;
using ShoppingBasket.SharedKernel;

namespace ShoppingBasket.Core
{
    public class Item : BaseEntity<Guid, Item>
    {
        public Product Product { get; private set; }
        public decimal FinalPrice
        {
            // Apply discount only if the discount's target is scoped to the item's product.
            get => IsDiscountTarget ?
                Product.Price - (Product.Price * Discount.PriceReductionPercentage / 100) :
                Product.Price;
        }
        public Discount Discount { get; private set; }
        public bool IsDiscountTarget { get; private set; }

        public Item(Product product) : this(Guid.NewGuid(), product)
        {
        }

        public Item(Guid id, Product product) : base(id)
        {
            if (product == null)
            {
                throw new ArgumentException("Item must have a product.");
            }
            Product = product;
            IsDiscountTarget = false;
        }

        public void ScopeDiscount(Discount discount)
        {
            if (discount == null)
            {
                throw new ArgumentException("Item can only scope a non-null discount.");
            }
            Discount = discount;
            IsDiscountTarget = false;
        }

        public void ScopeDiscountTarget(Discount discount)
        {
            if (discount == null)
            {
                throw new ArgumentException("Item can only scope a non-null discount.");
            }
            Discount = discount;
            IsDiscountTarget = true;
        }

        public void Descope()
        {
            Discount = null;
            IsDiscountTarget = false;
        }


        public override string ToString()
        {
            string target = IsDiscountTarget ? "targeted by '{Discount}'" : "";
            return $"Item '{Product}', priced at '{FinalPrice}', {target}.";
        }
    }
}