# Aspire.Firebase.Hosting - Firebase Extension for .NET Aspire

A comprehensive Firebase integration library for .NET Aspire that provides seamless hosting and configuration of Firebase services including Firestore, Authentication, and more.

## Features

- 🔥 **Firebase Project Integration** - Simple setup for Firebase projects
- 📊 **Firestore Database Support** - Full support for Firestore databases with custom database IDs
- 🔐 **Firebase Authentication** - Built-in Firebase Auth integration
- 🧪 **Emulator Support** - Complete local development with Firebase emulators
- ⚙️ **Fluent Configuration** - Intuitive extension methods with method chaining
- 🏗️ **Aspire Patterns** - Follows .NET Aspire resource and builder conventions
- 📦 **NuGet Ready** - Packaged for easy distribution and consumption

## Installation

Install the package via NuGet:

```bash
dotnet add package Aspire.Firebase.Hosting
```

Or via Package Manager:

```powershell
Install-Package Aspire.Firebase.Hosting
```

## Quick Start

### Basic Firebase Project Setup

```csharp
using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Add a Firebase project
var firebase = builder.AddFirebase("firebase", "your-project-id");

var app = builder.Build();
await app.RunAsync();
```

### Firebase with Firestore

```csharp
var builder = DistributedApplication.CreateBuilder(args);

// Method 1: Direct Firestore setup
var firestore = builder.AddFirebaseFirestore("firestore", "your-project-id", "(default)");

// Method 2: Shared Firebase project with separate Firestore
var firebase = builder.AddFirebase("firebase", "your-project-id");
var firestore2 = builder.AddFirebaseFirestore("firestore2", firebase, "custom-db");

var app = builder.Build();
await app.RunAsync();
```

### Firebase Authentication

```csharp
var builder = DistributedApplication.CreateBuilder(args);

// Method 1: Direct Auth setup
var auth = builder.AddFirebaseAuth("firebase-auth", "your-project-id");

// Method 2: Shared Firebase project
var firebase = builder.AddFirebase("firebase", "your-project-id");
var auth2 = builder.AddFirebaseAuth("auth2", firebase);

var app = builder.Build();
await app.RunAsync();
```

### Development with Emulators

```csharp
var builder = DistributedApplication.CreateBuilder(args);

// Firebase with emulators for local development
var firebase = builder.AddFirebase("firebase", "dev-project-id")
    .WithEmulator(firestorePort: 8080, authPort: 9099)
    .WithServiceAccountKey("/path/to/service-account.json");

// Individual service emulators
var firestore = builder.AddFirebaseFirestore("firestore", "dev-project-id")
    .WithEmulator(8080);

var auth = builder.AddFirebaseAuth("auth", "dev-project-id")
    .WithEmulator(9099);

var app = builder.Build();
await app.RunAsync();
```

### Production Setup

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var firebase = builder.AddFirebase("firebase", "production-project-id")
    .WithServiceAccountKey("/secure/path/to/production-service-account.json");

var firestore = builder.AddFirebaseFirestore("firestore", firebase, "production-db")
    .WithDatabaseId("production-db");

var auth = builder.AddFirebaseAuth("auth", firebase);

var app = builder.Build();
await app.RunAsync();
```

### Switching Between Emulator and Cloud

You can easily switch between Firebase emulators (for local development) and cloud Firebase (for production) using configuration or environment variables:

```csharp
var builder = DistributedApplication.CreateBuilder(args);

// Get configuration to determine environment
var useEmulator = builder.Configuration.GetValue<bool>("Firebase:UseEmulator");
var projectId = builder.Configuration.GetValue<string>("Firebase:ProjectId") ?? "my-project-id";

var firebase = builder.AddFirebase("firebase", projectId);

IResourceBuilder<FirebaseFirestoreResource> firestore;
IResourceBuilder<FirebaseAuthResource> auth;

if (useEmulator)
{
    // Development: Use Firebase emulators
    firebase.WithEmulator(firestorePort: 8080, authPort: 9099);
    
    firestore = builder.AddFirebaseFirestore("firestore", firebase)
        .WithEmulator(8080);
    
    auth = builder.AddFirebaseAuth("auth", firebase)
        .WithEmulator(9099);
}
else
{
    // Production: Use cloud Firebase with service account
    var serviceAccountPath = builder.Configuration.GetValue<string>("Firebase:ServiceAccountPath");
    if (!string.IsNullOrEmpty(serviceAccountPath))
    {
        firebase.WithServiceAccountKey(serviceAccountPath);
    }
    
    firestore = builder.AddFirebaseFirestore("firestore", firebase, "production-db");
    auth = builder.AddFirebaseAuth("auth", firebase);
}

var app = builder.Build();
await app.RunAsync();
```

**Configuration Example (`appsettings.Development.json`)**:
```json
{
  "Firebase": {
    "UseEmulator": true,
    "ProjectId": "dev-project-id"
  }
}
```

**Configuration Example (`appsettings.Production.json`)**:
```json
{
  "Firebase": {
    "UseEmulator": false,
    "ProjectId": "production-project-id",
    "ServiceAccountPath": "/secure/path/to/service-account.json"
  }
}
```

### Aspire AppHost Project Setup

To use Firebase resources in an Aspire application, your AppHost project needs to be configured properly:

**Project File (`.csproj`)**:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <Sdk Name="Aspire.AppHost.Sdk" Version="9.4.2" />
  
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <UserSecretsId>your-unique-id</UserSecretsId>
  </PropertyGroup>
  
  <ItemGroup>
    <PackageReference Include="Aspire.Hosting.AppHost" Version="9.4.2" />
    <PackageReference Include="Aspire.Firebase.Hosting" Version="1.0.0" />
  </ItemGroup>
</Project>
```

**Program.cs**:
```csharp
var builder = DistributedApplication.CreateBuilder(args);

var firebase = builder.AddFirebase("firebase", "my-project-id");
var firestore = builder.AddFirebaseFirestore("firestore", firebase);

var app = builder.Build();
await app.RunAsync(); // This will start the Aspire dashboard and orchestration
```

### Running the Example

The included example project is a minimal Aspire AppHost that demonstrates Firebase resource configuration.

**Running the Example**:
```bash
# Run the example
cd examples/Firebase.AppHost
dotnet run
```

**Prerequisites**:
- Aspire workload installed: `dotnet workload install aspire`
- Proper Aspire development environment setup

**Troubleshooting**: If you encounter dashboard configuration errors, see the example's README for guidance or use the comprehensive code examples in this README instead.

## API Reference

### Extension Methods

#### IDistributedApplicationBuilder Extensions

- `AddFirebase(name, projectId)` - Adds a Firebase project resource
- `AddFirebaseFirestore(name, firebase, databaseId?)` - Adds a Firestore resource with Firebase project
- `AddFirebaseFirestore(name, projectId, databaseId?)` - Adds a Firestore resource directly
- `AddFirebaseAuth(name, firebase)` - Adds Firebase Auth with Firebase project
- `AddFirebaseAuth(name, projectId)` - Adds Firebase Auth directly

#### Resource Builder Extensions

- `WithServiceAccountKey(path)` - Configures service account key file
- `WithEmulator(ports...)` - Enables emulator mode for local development
- `WithDatabaseId(id)` - Sets custom Firestore database ID

### Resources

#### FirebaseResource

Represents a Firebase project with:
- `Name` - Resource name
- `ProjectId` - Firebase project identifier
- `GetConnectionString()` - Returns the project ID as connection string

#### FirebaseFirestoreResource

Represents a Firestore database with:
- `Name` - Resource name  
- `ProjectId` - Firebase project identifier
- `DatabaseId` - Firestore database identifier (default: "(default)")
- `Parent` - Reference to parent Firebase resource
- `GetConnectionString()` - Returns formatted connection string with project and database

#### FirebaseAuthResource

Represents Firebase Authentication with:
- `Name` - Resource name
- `ProjectId` - Firebase project identifier  
- `Parent` - Reference to parent Firebase resource
- `GetConnectionString()` - Returns formatted connection string with project ID

## Environment Variables

The extension configures the following environment variables for Firebase services:

- `GOOGLE_APPLICATION_CREDENTIALS` - Path to service account key file
- `FIRESTORE_EMULATOR_HOST` - Firestore emulator host (when using emulator)
- `FIREBASE_AUTH_EMULATOR_HOST` - Auth emulator host (when using emulator)
- `GCLOUD_PROJECT` - Google Cloud project ID
- `FIRESTORE_DATABASE_ID` - Custom Firestore database ID

## Examples

### Comprehensive Configuration Examples

Here are various ways to configure Firebase resources in your Aspire AppHost:

```csharp
using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Example 1: Simple Firebase project setup
var firebase = builder.AddFirebase("firebase", "my-firebase-project-id");

// Example 2: Firebase with emulator for local development
var firebaseDev = builder.AddFirebase("firebase-dev", "my-dev-project")
    .WithEmulator(firestorePort: 8080, authPort: 9099);

// Example 3: Separate Firestore resource
var firestore = builder.AddFirebaseFirestore("firestore", "my-project-id", "(default)")
    .WithEmulator(8080);

// Example 4: Firebase Auth resource
var auth = builder.AddFirebaseAuth("firebase-auth", "my-project-id")
    .WithEmulator(9099);

// Example 5: Chained configuration - Firebase with both Firestore and Auth
var mainFirebase = builder.AddFirebase("main-firebase", "production-project");
var mainFirestore = builder.AddFirebaseFirestore("main-firestore", mainFirebase)
    .WithDatabaseId("production-db");
var mainAuth = builder.AddFirebaseAuth("main-auth", mainFirebase);

// Example 6: Development setup with emulators
var devFirebase = builder.AddFirebase("dev-firebase", "development-project")
    .WithEmulator()
    .WithServiceAccountKey("/path/to/service-account.json");

var app = builder.Build();
await app.RunAsync();
```

### Configuration-Based Environment Switching

```csharp
using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Get configuration to determine environment
var useEmulator = builder.Configuration.GetValue<bool>("Firebase:UseEmulator");
var projectId = builder.Configuration.GetValue<string>("Firebase:ProjectId") ?? "my-project-id";

var firebase = builder.AddFirebase("firebase", projectId);

if (useEmulator)
{
    // Development: Use Firebase emulators
    firebase.WithEmulator(firestorePort: 8080, authPort: 9099);
    
    var firestore = builder.AddFirebaseFirestore("firestore", firebase)
        .WithEmulator(8080);
    
    var auth = builder.AddFirebaseAuth("auth", firebase)
        .WithEmulator(9099);
}
else
{
    // Production: Use cloud Firebase with service account
    var serviceAccountPath = builder.Configuration.GetValue<string>("Firebase:ServiceAccountPath");
    if (!string.IsNullOrEmpty(serviceAccountPath))
    {
        firebase.WithServiceAccountKey(serviceAccountPath);
    }
    
    var firestore = builder.AddFirebaseFirestore("firestore", firebase, "production-db");
    var auth = builder.AddFirebaseAuth("auth", firebase);
}

var app = builder.Build();
await app.RunAsync();
```

### Complete Application Setup

```csharp
using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Production Firebase setup
var prodFirebase = builder.AddFirebase("prod-firebase", "my-production-project")
    .WithServiceAccountKey("/etc/secrets/prod-service-account.json");

var prodFirestore = builder.AddFirebaseFirestore("prod-firestore", prodFirebase, "production-db");
var prodAuth = builder.AddFirebaseAuth("prod-auth", prodFirebase);

// Development Firebase setup with emulators
var devFirebase = builder.AddFirebase("dev-firebase", "my-development-project")
    .WithEmulator(firestorePort: 8080, authPort: 9099);

// Add your application services
var webApp = builder.AddProject<WebApp>("webapp")
    .WithReference(prodFirestore)
    .WithReference(prodAuth);

var app = builder.Build();
await app.RunAsync();
```

## Building and Publishing to NuGet

### Prerequisites

1. .NET 8.0 SDK or later
2. NuGet account and API key

### Build the Package

```bash
# Clean and restore
dotnet clean
dotnet restore

# Build in Release mode
dotnet build --configuration Release

# Pack the NuGet package
dotnet pack src/Aspire.Firebase.Hosting/Aspire.Firebase.Hosting.csproj --configuration Release --output ./packages
```

### Publish to NuGet

```bash
# Publish to NuGet.org
dotnet nuget push packages/Aspire.Firebase.Hosting.*.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json

# Or publish to a private feed
dotnet nuget push packages/Aspire.Firebase.Hosting.*.nupkg --api-key YOUR_API_KEY --source https://your-private-feed.com/nuget
```

### Package Configuration

The package is pre-configured with:

- **PackageId**: `Aspire.Firebase.Hosting`
- **Version**: `1.0.0-preview.1` (update as needed)
- **Authors**: Aspire Firebase Community
- **Description**: Firebase integration for .NET Aspire hosting
- **Tags**: aspire, firebase, firestore, auth, hosting, cloud
- **License**: MIT
- **Project URL**: Repository URL
- **Symbols**: Included for debugging

Update the version in `src/Aspire.Firebase.Hosting/Aspire.Firebase.Hosting.csproj` before publishing.

## Firebase Emulator Setup

For local development, install and configure Firebase tools:

```bash
# Install Firebase CLI
npm install -g firebase-tools

# Initialize Firebase project
firebase init

# Start emulators (run this before starting your Aspire application)
firebase emulators:start --only firestore,auth
```

The default emulator ports are:
- Firestore: `8080`
- Authentication: `9099`

## Contributing

1. Fork the repository
2. Create a feature branch
3. Add tests for new functionality  
4. Ensure all tests pass: `dotnet test`
5. Submit a pull request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Dependencies

- `.NET 8.0` or later
- `Aspire.Hosting 9.0+`
- `Google.Cloud.Firestore 3.7.0+`
- `FirebaseAdmin 2.4.0+`

## Support

- **Issues**: Report bugs and request features via GitHub Issues
- **Documentation**: Additional docs available in the `/docs` folder
- **Discussions**: Community discussions on GitHub Discussions

## Roadmap

- [ ] Firebase Storage integration
- [ ] Firebase Functions support  
- [ ] Cloud Messaging integration
- [ ] Advanced authentication providers
- [ ] Metrics and monitoring integration
- [ ] Support for Firebase Extensions

---

Built with ❤️ for the .NET Aspire community
