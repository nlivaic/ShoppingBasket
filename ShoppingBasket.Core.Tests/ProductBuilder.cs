using System;

namespace ShoppingBasket.Core.Tests
{
    public class ProductBuilder
    {
        public static Product Butter => new Product(new Guid("cd1f6e6a-ce25-44dd-a222-0a07f370f49c"), "Butter", 0.8m);
        public static Product Milk => new Product(new Guid("eff5f4e8-0437-4229-a2f3-c9e41774a154"), "Milk", 1.15m);
        public static Product Bread => new Product(new Guid("fb6e7454-b889-4704-be73-8122718c77cb"), "Bread", 1m);
    }
}
