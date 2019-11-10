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
            get => DiscountTarget ?
                Product.Price - (Product.Price * Discount.PriceReductionPercentage / 100) :
                Product.Price;
        }
        public Discount Discount { get; private set; }
        public bool DiscountTarget { get; private set; }

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
            DiscountTarget = false;
        }

        public void ScopeDiscount(Discount discount)
        {
            if (discount == null)
            {
                throw new ArgumentException("Item can only scope a non-null discount.");
            }
            Discount = discount;
            DiscountTarget = false;
        }

        public void ScopeDiscountTarget(Discount discount)
        {
            if (discount == null)
            {
                throw new ArgumentException("Item can only scope a non-null discount.");
            }
            Discount = discount;
            DiscountTarget = true;
        }

        public void Descope()
        {
            Discount = null;
            DiscountTarget = false;
        }


        public override string ToString() => $"Item '{Product}', priced at '{FinalPrice}', scoped by '{Discount}'.";
    }
}