## Summary

- [Architecture based on Domain Driven Design](#architecture-based-on-domain-driven-design)
- [Add user secrets to a console application](#add-user-secrets-to-a-console-application)
- [Dependency injection](#dependency-injection)
- [Using Moq library](#using-moq-library)

### Architecture based on Domain Driven Design

> More details and examples [here](https://www.infoq.com/articles/ddd-in-practice/)

A typical enterprise application architecture consists of the following four conceptual layers:

- **User Interface (Presentation Layer)**: Responsible for presenting information to the user and interpreting user commands.
- **Application Layer**: This layer coordinates the application activity. It doesn't contain any business logic. It does not hold the state of business objects, but it can hold the state of an application task's progress.
- **Domain Layer**: This layer contains information about the business domain. The state of business objects is held here. Persistence of the business objects and possibly their state is delegated to the infrastructure layer.
- **Infrastructure Layer**: This layer acts as a supporting library for all the other layers. It provides communication between layers, implements persistence for business objects, contains supporting libraries for the user interface layer, etc.

Let's look at the application and domain layers in more detail. 

#### The application layer

- Is responsible for the navigation between the UI screens in the application as well as the interaction with the application layers of other systems.
- Can also perform the basic (non-business related) validation on the user input data before transmitting it to the other (lower) layers of the application.
- Doesn't contain any business or domain related logic or data access logic.
- Doesn't have any state reflecting a business use case but it can manage the state of the user session or the progress of a task.

#### The domain layer

- Is responsible for the concepts of business domain, information about the business use case and the business rules. Domain objects encapsulate the state and behavior of business entities. Examples of business entities in a loan processing application are Mortgage, Property, and Borrower.
- Can also manage the state (session) of a business use case if the use case spans multiple user requests (e.g. loan registration process which consists of multiple steps: user entering the loan details, system returning the products and rates based on the loan parameters, user selecting a specific product/rate combination, and finally the system locking the loan for that rate).
- Contains service objects that only have a defined operational behavior which is not part of any domain object. Services encapsulate behavior of the business domain that doesn't fit in the domain objects themselves.
- Is the heart of the business application and should be well isolated from the other layers of the application. Also, it should not be dependent on the application frameworks used in the other layers (JSP/JSF, Struts, EJB, Hibernate, XMLBeans and so-on).

### Add user secrets to a console application

1. [Configure appsettings.json](https://makolyte.com/csharp-how-to-read-custom-configuration-from-appsettings-json/).
2. Install package `Microsoft.Extensions.Configuration.UserSecrets`.
3. Add your user-secrets to the project.
4. Include the user secrets keys in `appsettings.json` with a default value. The `appsettings.json` will be overwritten by the user secrets when application is built.
5. Usage example:
```csharp
var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<ByteBankContexto>()
                .Build();
string stringconexao = builder.GetConnectionString("ByteBankConnection");
optionsBuilder.UseMySql(stringconexao, ServerVersion.AutoDetect(stringconexao));
```
### Dependency injection (DI)

For a complete overview of DI and its advantages check [this documentation](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection).

### Using Moq library

```csharp
var agenciaRepositorioMock = new Mock<IAgenciaRepositorio>();
var mock = agenciaRepositorioMock.Object;
var service = new AgenciaServico(mock);
```