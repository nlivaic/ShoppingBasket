# ShoppingBasket

A hobby project intended as a learning excercise in creating my own class library.

### Architecture

Library is a [DDD principles](https://dddcommunity.org/library/vernon_2011/)-ish implementation of a typical Shopping Basket. I made the `ShoppingBasket` the aggregate root, even though some of the API is in other non-root entities as well, stuff just made more sense that way.

I had the idea of laying everything out according to [Onion architecture](https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/), however because the domain is rather simple, all the classes are entities. There are no domain services as everything fit nicely in the domain classes.

Domain logic is entirely in the `ShoppingBasket.Core` project, backed by tests in an accompanying project.

###### Shared Kernel

Basic functionalities for every entity and value object can be found in `ShoppingBasket.SharedKernel` project. This covers:

- Identity property
- Equality (by identity for entities and by property values for value objects).
- Equality operator overloads.

#### Domain and business rules

**Shopping Basket** can be created by adding a list of items and discounts. Additional items can be added on-the-fly, triggering recalculation of discounts.

**Items** are a represenation of a single unit of a specific product. So two milks in the basket would be represented by two separate `Item` objects. Having items one by one facilitated determining the scope of each discount. Shopping Basket is a container of Items.

**Discounts** have several important characteristics:

- Requirements: list of products required in order to trigger the discount.
- Target: single product the discount is applied to.
- Scope: concatenation of Requirements and Target, used to determine whether items in a Shopping Basket can be combined to trigger a discount.
- Price reduction: a discount percentage.

**Products** are just that, products :)

###### Business rules

Multiple discounts cannot be compounded towards the same product.

Same product cannot be used towards achieving multiple discounts.

### Scenarios

Scenarios are implemented as part of `ShoppingBasket.Core.Tests/ShoppingBasketScenarios.cs`.

### Unit Tests

Cover all the above domain classes and shared kernel.

### Usage

#### Compiling:

    cd ./ShoppingBasket.Core/
    dotnet build

Or you can build the whole solution from the root with:

    dotnet build

### ASP.NET Core integration:

1. Drop the .dll in your bin folder.
2. using ShoppingBasket.Core;

   // ...

   var shoppingBasket = new ShoppingBasket(new List<Item>(), new List<Discount>());
   shoppingBasket.Add(new Item(new Product("Butter", 0.8m)));

#### Logging

A default console logger is provided by default. Every time you request `ShoppingBasket.TotalSum` a log entry is created.
If you want to inject your own logger, you must implement `IShoppingBasketLogger` and inject into ShoppingBasket constructor.
