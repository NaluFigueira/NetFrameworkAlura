## Summary

- [Creating test project](#creating-test-project)
- [AAA test standard](#aaa-test-standard)

### Creating test project

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

### AAA test standard

While creating your tests try follow the AAA standard:

1. **A**rrange: prepare variables and data for the test.
2. **A**ct: execute the necessary commands for testing.
3. **A**ssert: validate a rule or a result.
