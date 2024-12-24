# PsyShield Backend

This is the backend API for the PsyShield project, designed to address smartphone addiction among adolescents. This backend solution provides APIs, user management, and seamless integration with a Machine Learning model hosted on Azure Machine Learning Studio.

# Features
- **User Management**: APIs for user registration, authentication, and role-based access control.
- **Device Tracking**: Manage and monitor multiple devices per user.
- **Activity Logging**: Log and analyze smartphone usage data.
- **Psychological Assessments**: Support for the SAS-SV scale to measure smartphone addiction.
- **Reporting**: Generate detailed reports on smartphone usage patterns.
- **Machine Learning Integration**: Real-time prediction using a logistic regression model deployed in Azure.

# Tech Stack
- Framework: ASP.NET Core
- Database: Entity Framework Core with a relational database
- Authentication: JWT for secure token-based authentication
- Machine Learning: Azure Machine Learning Studio
- Email Notifications: SMTP integration for user notifications
- Dependency Injection: Built-in DI for flexible and maintainable code

# Project Structure
The backend is organized into modular components for scalability and maintainability:

 ```
PsyShield-Backend/
├── Users/
│   ├── Persistence/        # Repositories for user and device data
│   ├── Services/           # Business logic for user and device management
│   ├── Controllers/        # API endpoints for users and devices
│   ├── Domain/             # Interfaces and models
│   └── Resources/          # Data transfer objects
├── Reports/
│   ├── Persistence/        # Repositories for reports and assessments
│   ├── Services/           # Business logic for reporting
│   ├── Controllers/        # API endpoints for reports and assessments
│   └── Resources/          # Data transfer objects
├── Activities/
│   ├── Persistence/        # Repositories for device activities
│   ├── Services/           # Business logic for activities
│   ├── Controllers/        # API endpoints for activities
│   └── Resources/          # Data transfer objects
├── MachineLearning/
│   ├── Services/           # Business logic for ML integration
│   └── Controllers/        # API endpoints for ML predictions
├── Security/
│   ├── Services/           # Authentication and authorization
│   └── Communication/      # DTOs for authentication requests
├── Shared/
│   ├── Persistence/        # Common database context and repositories
│   ├── Mapping/            # AutoMapper profiles
│   ├── Extensions/         # Utility methods
│   └── Domain/             # Shared interfaces
 ```


# Installation and Setup
Clone the repository:
 ```bash
git clone https://github.com/your-repo/psyshield-backend.git
 ```
Navigate to the project directory:
 ```bash
cd psyshield-backend
 ```
Restore dependencies:
 ```bash
dotnet restore
 ```
Update the appsettings.json with your database connection string, JWT secret, and SMTP credentials.
Apply migrations:
 ```bash
dotnet ef database update
 ```
Run the application:
 ```bash
dotnet run
 ```

# Contributing
We welcome contributions! Please fork the repository, make your changes, and submit a pull request.
