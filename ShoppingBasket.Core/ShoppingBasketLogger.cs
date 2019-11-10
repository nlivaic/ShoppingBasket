using System;
using System.Diagnostics;

namespace ShoppingBasket.Core
{
    public class ShoppingBasketConsoleLogger : IShoppingBasketLogger
    {
        public void Log(string message)
        {
            Trace.WriteLine(message);
        }
    }
}