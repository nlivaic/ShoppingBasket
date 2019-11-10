using System.Collections.Generic;

namespace ShoppingBasket.Core
{
    public interface IDiscountProcessor
    {
        void ProcessDiscounts(IEnumerable<Item> items, IEnumerable<Discount> discounts);
    }
}