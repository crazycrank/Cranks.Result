# ResultZ
A simple, strongly typed and boilerplate poor implementation of the Result pattern.

[![.NET](https://github.com/crazycrank/ResultZ/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/crazycrank/ResultZ/actions/workflows/dotnet.yml)

# Todos
- [ ] Finish core implementation. Provide a simple and straight forward API
- [ ] Unit Tests
- [ ] Analyzer which throws an error when trying to derive from IResult outside of the library
- [ ] Usage documentation & examples

## Possible future enhancements
- [ ] ASP.NET Core wrapper. Provide base error types and mapping rules for IActionResult so that results can be simply returned
- [ ] Capsulate validation logic for FailIf/PassIf
