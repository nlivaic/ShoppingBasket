using System;
using ShoppingBasket.SharedKernel;

namespace ShoppingBasket.Core
{
    public class Item : BaseEntity<Guid, Item>
    {
        public Product Product { get; private set; }
        public decimal FinalPrice
        {
            // Apply discount only if the discount is scoped to the item's product.
            get => Discount?.Target != Product ?
                Product.Price :
                Product.Price - (Product.Price * Discount.PriceReductionPercentage / 100);
        }
        public Discount Discount { get; private set; }


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
        }

        public void ScopeDiscount(Discount discount)
        {
            if (discount == null)
            {
                throw new ArgumentException("Item can only scope a non-null discount.");
            }
            Discount = discount;
        }

        public override string ToString() => $"Item '{Product}', priced at '{FinalPrice}', scoped by '{Discount}'.";
    }
}