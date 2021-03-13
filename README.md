# ResultZ
A simple, strongly typed and boilerplate poor implementation of the Result pattern.

[![.NET](https://github.com/crazycrank/ResultZ/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/crazycrank/ResultZ/actions/workflows/dotnet.yml)

## Description

ResultZ aims to be a modern implementation of the results pattern, making use of new language features the got introduced in the recent years for C#, like Records and Pattern Matching.
It's supposed to allow writing typed error handling code without having to write lots of boilerplate and focus on what's important: You're code.

## Concepts

Design your methods for failure.
This doesn't mean you should handle each and every possible failure scenario (like a database that isn't reachable) in your methods.
But if a method has a failure scenario it is designed for that can occur during normal execution (like a user provided string that doesn't match your expectiations), indicate that failure scenario on your method.
Unexpected failure scenarios on the other hand should still throw an exception - after all, that would be an exceptional scenario.

This possibility for failure can be indicated by letting your method return `IResult` or `IResult<TValue>`.
Use the generic variant when your method returns a value, and the non generic variant if it would by of type `void` otherwise.
Then return Result.Pass() / Result.Pass(value) in case of success, or return Result.Fail() / Result.Fail<TValue>() in case of an error

A result can have a Value, a Message, and both `Error` or `Success` reasons.
A Passed result cannot have any Error reasons. Adding an Error to a Passed result will transform it to a Failed object and drop the value.
A Failed result on the other hand can have both Success and Error reasons. A Failed result will never expose a value, and trying to access the Value of a Failed<TValue> will throw an exception



## Usage & Examples

### Indicate a failable method

### 

Creating errors
```csharp
Result.Pass(); // Indicate success without value

Result.Pass<int>(42); // Indicate success with value

Result.Fail(); // Indicate failure without value

Result.Fail<int>(); // Indicate failure with value. A Failed result never exposes it's value
```


## Todos
- [ ] Finish core implementation. Provide a simple and straight forward API
- [ ] Unit Tests
- [ ] Analyzer which throws an error when trying to derive from IResult outside of the library
- [ ] Usage documentation & examples

### Possible future enhancements
- [ ] ASP.NET Core wrapper. Provide base error types and mapping rules for IActionResult so that results can be simply returned
- [ ] Capsulate validation logic for FailIf/PassIf
