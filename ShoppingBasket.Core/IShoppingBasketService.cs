using System.Collections.Generic;

namespace ShoppingBasket.Core
{
    public interface IShoppingBasketService
    {
        ShoppingBasket CreateShoppingBasket(IEnumerable<Item> items, IEnumerable<Discount> discounts);
    }
}