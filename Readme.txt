To Run the projects please make sure you are using Visual Studio 2017 with .NET Core 2.1.

Simply open solutions in Visual Studio and hit run button.

Both projects are developed with .NET Core and using NUnit for Unit Tests.

In PartA String Manipulation methods have been added to as extension methods and are done using Linq, Iterator and Generator in C#.

In PartB Glossary Application is a .NET Core MVC project with 2 Layers including Unit Testing and Leveraging Entity Framework Code First.
It includes BusinessServices in which DbContext is injected. 

In a commercial project where there are multiple Business Domain involved I would have implemented Domain Driven Development for UnitOfWork and Repository patterns. However, it is kind of overkilled for Glossary project.
