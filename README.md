# ResultZ
A simple, strongly typed and boilerplate poor implementation of the Result pattern.

[![.NET](https://github.com/crazycrank/ResultZ/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/crazycrank/ResultZ/actions/workflows/dotnet.yml)

## Description

ResultZ aims to be a modern implementation of the results pattern, making use of new language features the got introduced in the recent years for C#, like Records and Pattern Matching.
It's supposed to allow writing typed error handling code without having to write lots of boilerplate and focus on what's important: You're code.

## Concepts

## Usage & Examples

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
