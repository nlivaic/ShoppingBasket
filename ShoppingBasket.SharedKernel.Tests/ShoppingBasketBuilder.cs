using System;

namespace ShoppingBasket.SharedKernel.Tests
{
    public class ShoppingBasketBuilder
    {
        public static Core.ShoppingBasket Build1 => new Core.ShoppingBasket(new Guid("54d7d622-b6c0-4791-b54f-086be047aee8"));
        public static Core.ShoppingBasket Build2 => new Core.ShoppingBasket(new Guid("689bf442-8016-42c8-884c-eab90e06609f"));
        public static Core.ShoppingBasket Build3 => new Core.ShoppingBasket(new Guid("4d79c403-9e54-4952-8727-a51b4cc6d84d"));
        public static Core.ShoppingBasket Build4 => new Core.ShoppingBasket(new Guid("0dd9be98-c1a9-4fd4-9cab-5ad87e14b67e"));
        public static Core.ShoppingBasket Build5 => new Core.ShoppingBasket(new Guid("ed6f0d5e-7d35-4367-ad60-1c237a6305e9"));
        public static Core.ShoppingBasket Build6 => new Core.ShoppingBasket(new Guid("b9b233d9-71df-47f5-b68a-2f36f3eb4145"));
        public static Core.ShoppingBasket Build7 => new Core.ShoppingBasket(new Guid("726f0287-db34-402e-8ea4-35e45b122847"));
    }
}