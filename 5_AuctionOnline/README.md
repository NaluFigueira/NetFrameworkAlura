## Summary

- [Single responsibility](#single-responsibility)

### Single responsibility

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
