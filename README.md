# Notes for Reviewers

This task was implemented with the understanding that it was intended as a discussion topic rather than an elimination exercise, and that the available implementation time was limited. Because of that, I focused on demonstrating architectural decisions, extensibility, and maintainability rather than implementing every possible production-grade optimization.

## Architectural Approach

To showcase how the solution could evolve and be reused across different CRM systems, I chose to implement a simplified but representative domain model using Clean Architecture principles.

Project dependencies follow the direction:

```
Domain <- Application <- Infrastructure/Persistence <- API
```

The goal was to keep business rules independent from infrastructure concerns and external technologies.

## Domain-Driven Design

Although the business domain is intentionally small, the solution demonstrates key DDD concepts:

* Aggregate creation and lifecycle management
* Domain entities and value-oriented business logic
* Encapsulation of domain rules within the domain layer
* Separation of business concerns from application and infrastructure concerns

## CQRS

The solution applies CQRS principles by separating commands and queries into distinct application flows.

While a full event-driven architecture was not introduced due to the scope of the assignment, the command/query separation provides a foundation that could later be extended with domain events, integration events, or asynchronous processing if business requirements evolve.

## Security and Authentication

JWT authentication and role-based authorization have been implemented to demonstrate how APIs can be securely exposed and integrated with external systems.

Due to time constraints, OTP verification is simulated. Instead of sending emails through an actual email provider, generated OTP codes are written to the application console.

Token blacklisting and refresh-token revocation mechanisms are intentionally left as future improvements.

## Extensibility

The solution uses the Template Method pattern in reward-processing workflows to simplify future expansion and reduce duplication when introducing additional reward types or campaign rules.

The intention was to make new business scenarios easier to implement while keeping existing logic stable.

## Production Considerations

Several production-level concerns were intentionally not fully implemented due to the limited time available:

* Distributed caching
* Encryption of sensitive data at rest
* Comprehensive audit logging
* Rate limiting
* Background job processing
* Token blacklisting/revocation
* Monitoring and observability

These omissions were conscious trade-offs rather than oversights.

## Potential Next Steps

The first improvement I would implement is extracting the authenticated user identifier from the request context and automatically using it as the reward agent identifier. In a real-world system, the logged-in user should always be the agent issuing rewards, rather than allowing the value to be supplied externally.

Additionally, once the sales period is complete and the CSV report becomes available, the application can be extended to import the report and compare purchased customers against the previously rewarded customers. This would enable campaign effectiveness reporting and reward redemption analysis through the API.

## Final Note

The primary objective of this implementation was to demonstrate architectural thinking, maintainability, separation of concerns, and readiness for future requirements while remaining within the constraints of the assignment.

## Commands for Creating Migrations and Updating DB

dotnet ef migrations add NEW_MIGRATION -c MyDbContext -p Persistance -s Api -o Migrations

dotnet ef database update -c MyDbContext -p Persistance -s Api
