## Summary

- [Introduction](#introduction)
- [Single responsibility principle](#single-responsibility)
- [Open Closed Principle](#open-closed-principle)
- [Liskov Substitution Principle](#liskov-substitution-principle)
- [Interface Segregation Principle](#interface-segregation-principle)
- [Dependency Inversion Principle](#dependency-inversion-principle)

### Introduction

S.O.L.I.D stands for:

1. **S**ingle Responsibility Principle: classes and methods should have high cohesion.
2. **O**pen Closed Principle: keep a class/method opened to extension, but closed for updates.
3. **L**iskov Substitution Principle: fulfill abstraction premisses.
4. **I**nterface Segregation Principle: interfaces should be well coupled and have high cohesion.
5. **D**ependency Inversion Principle: extend from abstractions instead of concrete classes.

More details on each principle in the following sections.

### Single responsibility principle

- Why should we avoid duplicated code?

Besides easing and optimizing code maintenance, when editing code that is duplicated somewhere else, it's not uncommon to miss or forget to update all instances of that code, risking code cohesion.

Also, code duplication could mean that a method or class has way too much responsibilities, when they should only have a single one.

- What should I do when I find duplicated code?
    - Extract a collection of instructions and create a method responsible for executing those instructions;
    - Extract a collection of methods and create a class responsible for the implementation of those methods;

- What's the difference between method responsibility and class responsibility?
    - A method should only do one thing
    - A class should answer to changes triggered by a single agent (entity, function or use case) 

- What is the purpose of DAO pattern?

> In software, a data access object (DAO) is a pattern that provides an abstract interface to some type of database or other persistence mechanism. **By mapping application calls to the persistence layer, the DAO provides some specific data operations without exposing details of the database. This isolation supports the single responsibility principle.** It separates what data access the application needs, in terms of domain-specific objects and data types (the public interface of the DAO), from how these needs can be satisfied with a specific DBMS, database schema, etc. (the implementation of the DAO).

Source: https://en.wikipedia.org/wiki/Data_access_object

- Which principle of S.O.L.I.D is related to cohesion?

That would be the 'S', which stands for _Single Responsibility Principle_ and it states that each software module should have one and only one reason to change.

Consider the example below:

```java
public class Employee {
  public Money calculatePay();
  public void save();
  public String reportHours();
}
```

The `Employee` class has three reasons to change:

1. If the calculation of payment changes
2. If the way that we save the `Employee` in the database changes
3. If the generation of the worked hours report changes

Therefore what we need to do in this case is to separate responsibilities:

1. Create a `Financial` class that takes care of `calculatePay` and other similar methods.
2. Create a `EmployeeDAO` class that takes care of the interface to the database, including the `save` method.
3. Create a `Operations` class that takes care of `reportHours` and other similar methods.
4. The `Employee` class should only trigger those methods when necessary and not implement them.

### Open Closed Principle

> You should be able to extend the behavior of a system without having to modify that system.

[Open Closed Principle, by Uncle Bob](https://blog.cleancoder.com/uncle-bob/2014/05/12/TheOpenClosedPrinciple.html)

One way of applying that principle is by creating a service layer.

> A Service Layer defines an application's boundary and its set of available operations from the perspective of interfacing client layers. It encapsulates the application's **business logic, controlling transactions and coor-dinating responses** in the implementation of its operations.

[Service layer, by Randy Stafford](https://martinfowler.com/eaaCatalog/serviceLayer.html)

When extension is needed, we can use the [decorator pattern](https://refactoring.guru/design-patterns/decorator).

This pattern allow us to create a new service class, that implements the same interface as the default service class, and then we create new methods and/or override methods we want to modify in the new service class.

The new service class should have an instance of the default service class, that way, for the methods that should keep the default behavior, it's possible to call them directly from the default service instance.

### Liskov Substitution Principle

> Simply put, the Liskov Substitution Principle (LSP) states that objects of a superclass should be replaceable with objects of its subclasses without breaking the application. 

> In other words, what we want is to have the objects of our subclasses behaving the same way as the objects of our superclass.

[Mohit Saxena, knóldus](https://blog.knoldus.com/what-is-liskov-substitution-principle-lsp-with-real-world-examples/#:~:text=Simply%20put%2C%20the%20Liskov%20Substitution,the%20objects%20of%20our%20superclass.)

You can also apply this principle to interfaces, not only classes. Meaning that when you implement an interface you should implement all behaviors that it requires the class to have.

### Interface Segregation Principle

> In the field of software engineering, the interface segregation principle (ISP) states that no code should be forced to depend on methods it does not use.

Robert C. Martin

Therefore, if you add a new method to an interface, all classes that implement it should also have an implementation of this method, otherwise a new interface should be created, and only the classes that use this new method should implement the new one.

#### CQRS pattern

CQRS stands for Command Query Responsibility Segregation and it means that the responsibility of writing and reading data should be separated. This pattern is strongly associated with ISP.

For more details check [this article](https://www.eduardopires.net.br/2016/07/cqrs-o-que-e-onde-aplicar/)

### Dependency Inversion Principle

- Coupling
Coupling means dependency between two types. In an object-oriented system coupled objects are inevitable, but we must be aware of their quality. Good coupling are for stable types, which are the ones that rarely change, e.g. primitive types and types from external libraries.
Bad coupling is associated to unstable types, meaning the ones we created for our system, which we have control of.

- The concept
The 'D' in S.O.L.I.D stands for _Dependency Inversion Principle_ and it states that we should create abstractions and depend on those to improve the quality of coupling in our system.

One way of doing this is to make explicit the dependencies of a class through the parameters of the its constructor. This method is called _Dependency Injection_. 

When the class no longer resolves those dependencies directly and gives their control to others, it's called _Inversion of control_.