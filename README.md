# Cranks.Result
A simple, strongly typed and boilerplate poor implementation of the Result pattern.

[![.NET](https://github.com/crazycrank/Cranks.Result/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/crazycrank/Cranks.Result/actions/workflows/dotnet.yml)

## Description

Results.Crank aims to be a modern implementation of the results pattern, making use of new language features the got introduced in the recent years for C#, like Records and Pattern Matching.
It's supposed to allow writing typed error handling code without having to write lots of boilerplate and focus on what's important: You're code.

## Concepts

### Designing methods for failure
If a method can fail by design, it should indicate so.
The extent of how far you go with this is your decision.
Personally I try to differentiate between errors that can happen by design, and errors that are unexpected.
* A `GetById` method can fail by design, as the id can simply not exist. In this case, indicate so and use the `IResult`/`IResult<TValue>` types this library provides
* A `GetAll` method can not fail by design (in most cases at least). It could fail if there is network error, but this would be an unexpected error which should still be thrown. In this case your method should return the result directly, without encapsulating it in a `IResult<TValue>`.

A few good and more indepth reads on this topic:
* [Error handling: Exception or Result?](https://enterprisecraftsmanship.com/posts/error-handling-exception-or-result/) by Enterprise Craftsmanship
* [Exceptions for flow control in C#](https://enterprisecraftsmanship.com/posts/exceptions-for-flow-control/) by Enterprise Craftsmanship
* [Operation result](https://www.forevolve.com/en/articles/2018/03/19/operation-result/) by ForEvolve

### `IReason` and `IResult`
An `IReason` can either be an `Error` or a `Success` and is nothing more than a typed 1object encapsulating some error/success information.

An `IResult` on the other hand indicates if a method has `Passed` or `Failed` during its execution.
In that sense (and also implementation wise), each `IResult` is also a `IReason`.
The same way, each `Passed` is also a `Success` and `Failed` is also an `Error`.

An `IResult` can either be valueless, or it can contain a value, in which case `IResult<TValue>` should be used.

`IResult` should never be derived, whereas `IReason` can (and often should) be derived to provide typed error or success objects.
Do not derive `IReason` directly though, but derive from either `Error` or `Success`.

All implementations of `IReason` and `IResult` are records and fully immutable.
Modify them using the extension methods provided in `ResultExtensions`.
To create new `IResult` use the factory methods in the static class `Result`.
Extension methods and factory methods are fully alligned, allowing you to fluently type together result objects.
For the technically interested, this is done using a source generator, converting the extension methods to normal static methods which forward to the extension methods.

## Usage & Examples

### Indicate a failable method
When there are designed for scenarios the make your fail, it should indicate so.
You can do this by return `IResult` for methods without a return value, or `IResult<TValue>` for methods with a return value.
```csharp
public IResult Method()
{
    if (successful)
    {
        return Result.Pass();
    }
    else
    {
        return Result.Fail();
    }
}

public IResult<string> Method()
{
    if (successful)
    {
        return Result.Pass("success value);
    }
    else
    {
        return Result.Fail<string>();
    }
}
```

### Provide a message with your result
It can be useful to provide a quick message with your result, e.g. for logging purposes, to indicate what happend
```csharp
public IResult Method()
{
    if (successful)
    {
        return Result.Pass()
                     .WithMessage("all good");
    }
    else
    {
        return Result.Fail()
                     .WithMessage("oh no!");
    }
}
```

### Add errors or successes to indicate what caused your result
You can add Error and Success objects as causes for your result, to better understand what happend.
```csharp
public IResult Method()
{
    if (successful)
    {
        return Result.Pass()
                     .WithSuccess(new Success("I did everything right"));
    }
    else
    {
        return Result.Fail()
                     .WithError(new Error("I mad a boo"));
    }
}
```

Since all `IResult`s are `IReason`s you can also add another methods result as the reasons for your result.
```csharp
public IResult Method()
{
    if (Validate is Failed failed)
    {
        return Result.WithError(failed)
                     .WithMessage("Validation failed");
    }

    return Result.Passed();
}

private IResult Validate()
{
    // validate and return result indicating validation success
}
```

### Use factory methods and the fluent api to build up your results
In most cases, passing or failing in a method depends on different operations, and is not clear from the beginning.
You can use a handful of useful methods to build up your results.
```csharp
public IResult<int> Method(int a, int b)
{
    return Result.WithErrorIf(a < b, new Error("a is smaller than b"))
                 .WithErrorIf(a > b, "b is smaller than a") // strings get casted to Error/Success records if appropriate
                 .WithSuccessIf(a == b, new Success("a and b are equal"))
                 .WithValue(a * b); // value only gets added if Passed. In Failed scenarios it gets dropped.
}
```

### Checking a methods `IResult`
When you have a method that returns an `IResult`, you can take different actions depending on the result.
It is recommended to use pattern matching for this case, of course you can also use a more classical approach.
```csharp
public IResult Method(int a, int b)
{
    var result = MethodThatReturnsResultOfInt();

    switch (result)
    {
        case Passed<int> { Value: var value}:
            _logger.LogInfo($"The method returned {value}");
            break;

        case Failed { Message: var message }:
            _logger.LogWarning($"Something went wrong: {message}");
            break;
    }
}
```

### Custom `Error`s and `Success`es
It is possible (and recommended) to use your own typed `Error` and `Success` objects.
Although it is possible to use the Cranks.Result with just the default `Error` and `Success` objects, you get much more out of it when using them.

Errors are simple records and can be defined with a single line of code!
This makes them easy to define without cluttering your application in lots of boilerplate code.
```csharp
public record MySimpleError : Error;

public record MyErrorWithDataFields(int value) : Error;

public record MyErrorWithCustomMessage(int value) : Error($"This value is invalid: {value}");
```

You can use your customized errors everywhere you would otherwise use an error:
```csharp
public IResult Method()
{
    return Result.WithError<MySimpleError>()
                 .WithError(new MyErrorWithDataFields(42))
                 .WithErrorIf(condition, new MyErrorWithCustomMessage(1337));
}
```

## Todos and possible enhancements
- [ ] Analyzer which throws an error when trying to derive from IResult outside of the library
- [ ] Improved Usage: Try, TryOrError Others?
- [ ] Idea to improve source generation to provide Result<TValue>.FactoryMethods. But it has downsides... Analyze more.
- [ ] ASP.NET Core wrapper. Provide base error types and mapping rules for IActionResult so that results can be simply returned
- [ ] Stringifier to modify how errors get converted to strings?
