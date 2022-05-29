# Clean Architecture layers (onion view)
- The Clean Architecture relies heavily on the Dependency Inversion principle.​
- Users make requests through the Presentation layer, which interacts only with the Application. ​
- Application layer contains the business logic and types. ​
- The Application and Domain are at the center of the design. This is known as the Core of the application.​
- The Domain layer contains all entities, enums, exceptions, interfaces, types and logic specific to the domain layer

![image](https://user-images.githubusercontent.com/25796029/170863591-21d9bb75-c1b1-4926-b96e-ad73d67b8ffb.png)

# Organizing code in Clean Architecture
- Each layer usually is presented by his own separate project (but can have many)​
- Domain layer will contain all entities (business model classes that are persisted), enums​
- Application layer contains all application logic. It is dependent on the domain layer but has no dependencies on any other layer. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, access database, do a network call - a new interface would be added to application and an implementation would be created within infrastructure. Application types: interfaces, domain services (BLL services, CQRS), custom exceptions.​
- Infrastructure layer typically includes data access implementations and contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer. Infrastructure types: DbContext, Migration, Implementation of Repositories, SmtpNotifier.​
- Presentation layer is a single page application based on Angular/React and ASP.NET Core (MVC/API). This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only Program.cs/Startup.cs should reference Infrastructure. UI layer types: Controllers, Custom Filters, Custom middleware, Startup/Program
