# Book Library Account Microservice

This project is a Book Library Account Microservice built using C#. It leverages several advanced software design principles and architectures to ensure maintainability, scalability, and testability.

## Features

- **Applied SOLID Principles**: Ensures that the codebase is modular, flexible, and easy to maintain.
- **DDD (Domain-Driven Design)**: Focuses on the core domain logic and complex business rules.
- **Hexagonal Architecture**: Also known as Ports and Adapters, this architecture pattern helps in creating loosely coupled application components.
- **Testing**: Comprehensive unit tests to ensure the reliability of the application.
- **CQRS (Command Query Responsibility Segregation)**: Separates read and write operations to optimize performance and scalability.
- **Event-Driven Architecture**: Uses events to communicate between different parts of the system, promoting loose coupling and scalability.


## Getting Started

### Prerequisites

- .net8.0 or later

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/EverEcheverri/ddd-hexagonal-cqrs-event-driven.git
    ```

2. Restore the dependencies:
    ```bash
    dotnet restore
    ```

3. Build the project:
    ```bash
    dotnet build
    ```

### Running the Application

1. Run the application:
    ```bash
    dotnet run
    ```

### Running Tests

To run the tests, use the following command:
```bash
dotnet test
