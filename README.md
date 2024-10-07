## About the Project

This **API**, developed using **.NET 8**, adopts the principles of **Domain-Driven Design (DDD)** to provide a structured and effective solution for managing personal expenses. The main goal is to allow users to record their expenses, detailing information such as title, date and time, description, amount, and payment type, with the data being securely stored in a **MySQL** database.

The architecture of the **API** is based on **REST**, using standard **HTTP** methods for efficient and simplified communication. Additionally, it is complemented by **Swagger** documentation, which provides an interactive graphical interface for developers to easily explore and test the endpoints.

Among the NuGet packages used, **AutoMappe**r is responsible for mapping between domain objects and request/response objects, reducing the need for repetitive and manual code. FluentAssertions is used in unit tests to make assertions more readable, helping to write clear and understandable tests. For validations, **FluentValidation** is employed to implement validation rules simply and intuitively within the request classes, keeping the code clean and maintainable. Finally, **EntityFramework** acts as an ORM (Object-Relational Mapper) that simplifies interactions with the database, allowing the use of .NET objects to manipulate data directly without needing to deal with SQL queries.

### Features

- **Domain-Driven Design (DDD)**: A modular structure that facilitates understanding and maintenance of the application domain.
- **Unit Tests**: Comprehensive tests with FluentAssertions to ensure functionality and quality.
- **Report Generation**: Ability to export detailed reports to PDF and Excel, providing a visual and effective analysis of expenses.
- **RESTful API with Swagger Documentation**: Documented interface that eases integration and testing for developers.

### Built with
![badge-dot-net]
![badge-windows]
![badge-visual-studio]
![badge-mysql]
![badge-swagger]

## Getting Started

To get a working local copy, follow these simple steps.

### Requirements

* .NET SDK 8
* MySql Server

### Instalation

1. Clone the repository:
```sh
git clone https://github.com/joaogdoqr/CashFlow.git
```
2. Fill in the information in the appsettings.Development.json file.
3. Run the API.


<!-- Badges -->
[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge-windows]: https://img.shields.io/badge/Windows-0078D4?logo=windows&logoColor=fff&style=for-the-badge
[badge-visual-studio]: https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge
[badge-mysql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge