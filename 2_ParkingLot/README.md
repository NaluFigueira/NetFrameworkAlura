## Summary

- [Creating a test project](#creating-a-test-project)
- [AAA test standard](#aaa-test-standard)
- [Test attributes](#test-attributes)

### Creating a test project

It's possible to create a test project by using Visual Studio interface, when selecting xUnit Test project. After that, click with the right mouse button over the recently created project and add a reference to the main project.

An alternative is to use the following commands on the main project folder:

```bash
# Creating test project
dotnet new xunit -o YourProject.Tests

# Adding project to solution
dotnet sln add ./YourProject.Tests/YourProject.Tests.csproj

# Adding main project reference to the test project
dotnet add ./YourProject.Tests/YourProject.Tests.csproj reference ./YourMainProject/YourMainProject.csproj
```

> :warning: **To add the main project as reference, both main and test projects must be using the same version of .NET**

To run the tests, you can either use Visual Studio own test manager or run the following command on the projects folder:

```bash
dotnet test
```

### AAA test standard

While creating your tests try follow the AAA standard:

1. **A**rrange: prepare variables and data for the test.
2. **A**ct: execute the necessary commands for testing.
3. **A**ssert: validate a rule or a result.

### Test attributes

#### Fact

`Fact` purpose is to test a single occurrence, without any parameters.

```csharp
[Fact]
public void AccelerateVehicleTest()
{
    //other code
}
```

You can change the displayed name for the test by using `DisplayName`.

```csharp
[Fact(DisplayName = "Accelerate vehicle")]
public void AccelerateVehicleTest()
{
    //...other code
}
```

Also, it's possible to skip execution of a test by adding the `Skip` parameter.

```csharp
[Fact(Skip = "Method not yet implemented")]
public void AccelerateVehicleTest()
{
    //...other code
}
```

#### Trait

`Trait` purpose is to organize your tests in categories, to ease maintenance and debugging.

```csharp
[Fact(DisplayName = "Accelerate vehicle")]
[Trait("Feature", "Accelerate")]
public void AccelerateVehicleTest()
{
    //...other code
}
```

#### Theory

`Theory` purpose is to test multiple occurrences with parameters. The most common way to use `Theory` is through `InlineData`.

> Its recommended to use `InlineData` when **your method parameters are constants, and you don't have too many cases to test**. Otherwise, try using `ClassData` or `MemberData`.

```csharp
[Theory]
[InlineData(1, 2, 3)]
[InlineData(-4, -6, -10)]
[InlineData(-2, 2, 0)]
[InlineData(int.MinValue, -1, int.MaxValue)]
public void CanAddTheory(int value1, int value2, int expected)
{
    var calculator = new Calculator();

    var result = calculator.Add(value1, value2);

    Assert.Equal(expected, result);
}
```

##### MemberData

```csharp
public class CalculatorTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public void CanAddTheoryMemberDataProperty(int value1, int value2, int expected)
    {
        var calculator = new Calculator();

        var result = calculator.Add(value1, value2);

        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 1, 2, 3 },
            new object[] { -4, -6, -10 },
            new object[] { -2, 2, 0 },
            new object[] { int.MinValue, -1, int.MaxValue },
        };
}
```

##### ClassData

To use `ClassData` is necessary to create a separate class to process the test data.

```csharp
[Theory]
[ClassData(typeof(CalculatorTestData))]
public void CanAddTheoryClassData(int value1, int value2, int expected)
{
    var calculator = new Calculator();

    var result = calculator.Add(value1, value2);

    Assert.Equal(expected, result);
}
```

```csharp
public class CalculatorTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 1, 2, 3 };
        yield return new object[] { -4, -6, -10 };
        yield return new object[] { -2, 2, 0 };
        yield return new object[] { int.MinValue, -1, int.MaxValue };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
```
