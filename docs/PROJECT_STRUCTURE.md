# Project Structure

This document explains the organization and structure of the Aspire.Firebase.Hosting project.

## Directory Structure

```
aspire-fire/
├── src/
│   └── Aspire.Firebase.Hosting/          # Main library project
│       ├── FirebaseResource.cs           # Firebase project resource
│       ├── FirebaseFirestoreResource.cs  # Firestore database resource
│       ├── FirebaseAuthResource.cs       # Firebase Auth resource
│       ├── FirebaseExtensions.cs         # IDistributedApplicationBuilder extensions
│       ├── FirebaseResourceBuilderExtensions.cs  # Resource configuration extensions
│       └── Aspire.Firebase.Hosting.csproj # Project file with NuGet metadata
├── examples/
│   └── Aspire.Firebase.Examples.AppHost/ # Example AppHost demonstrating usage
│       ├── Program.cs                     # Example implementation
│       └── Aspire.Firebase.Examples.AppHost.csproj
├── tests/
│   └── Aspire.Firebase.Hosting.Tests/    # Unit tests
│       ├── FirebaseResourceTests.cs      # Resource class tests
│       ├── FirebaseExtensionsTests.cs    # Extension method tests
│       └── Aspire.Firebase.Hosting.Tests.csproj
├── docs/
│   └── NUGET_PUBLISHING.md               # Publishing guide
├── README.md                             # Main documentation
├── LICENSE                               # MIT license
├── .gitignore                           # Git ignore rules
└── AspireFire.sln                       # Solution file
```

## Core Components

### Resources (`src/Aspire.Firebase.Hosting/`)

#### `FirebaseResource.cs`
- Represents a Firebase project
- Implements `IResourceWithConnectionString`
- Provides project ID as connection string

#### `FirebaseFirestoreResource.cs`
- Represents a Firestore database
- Implements `IResourceWithParent<FirebaseResource>`
- Supports custom database IDs
- Formats connection string with project and database info

#### `FirebaseAuthResource.cs`
- Represents Firebase Authentication
- Implements `IResourceWithParent<FirebaseResource>`
- Provides project-based connection string

### Extensions (`src/Aspire.Firebase.Hosting/`)

#### `FirebaseExtensions.cs`
- Extension methods for `IDistributedApplicationBuilder`
- Core methods: `AddFirebase`, `AddFirebaseFirestore`, `AddFirebaseAuth`
- Supports both direct and resource-based configurations

#### `FirebaseResourceBuilderExtensions.cs`
- Extension methods for resource builders
- Configuration methods: `WithServiceAccountKey`, `WithEmulator`, `WithDatabaseId`
- Enables fluent configuration chaining

### Project File (`Aspire.Firebase.Hosting.csproj`)

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <IncludeSymbols>true</IncludeSymbols>
    <SymbolPackageFormat>snupkg</SymbolPackageFormat>
    
    <!-- NuGet package metadata -->
    <PackageId>Aspire.Firebase.Hosting</PackageId>
    <Version>1.0.0-preview.1</Version>
    <Authors>Aspire Firebase Community</Authors>
    <Description>Firebase integration for .NET Aspire hosting</Description>
    <!-- ... more metadata ... -->
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Aspire.Hosting" Version="8.0.0-preview.1.23557.2" />
    <PackageReference Include="Google.Cloud.Firestore" Version="3.7.0" />
    <PackageReference Include="FirebaseAdmin" Version="2.4.0" />
  </ItemGroup>

  <ItemGroup>
    <None Include="../../README.md" Pack="true" PackagePath="\" />
  </ItemGroup>
</Project>
```

## Design Patterns

### Aspire Resource Pattern

All Firebase resources follow the .NET Aspire resource pattern:

1. **Resource Classes**: Inherit from `Resource` base class
2. **Interface Implementation**: Implement appropriate interfaces (`IResourceWithConnectionString`, `IResourceWithParent`)
3. **Immutable State**: Resources are immutable after creation
4. **Connection Strings**: Provide standardized connection information

### Builder Pattern

Extension methods follow the Aspire builder pattern:

1. **Fluent API**: Method chaining for configuration
2. **Resource Builders**: Return `IResourceBuilder<T>` for further configuration  
3. **Consistent Naming**: `Add{Service}` pattern for creation methods
4. **Configuration Extensions**: `With{Option}` pattern for configuration

### Factory Pattern

Resource creation uses factory methods:

1. **Extension Methods**: Static factory methods on `IDistributedApplicationBuilder`
2. **Overloads**: Multiple overloads for different configuration scenarios
3. **Validation**: Argument validation with meaningful exceptions
4. **Resource Registration**: Automatic registration with the application builder

## Code Organization

### Namespace Structure

- `Aspire.Firebase.Hosting` - Resource classes and core types
- `Aspire.Hosting` - Extension methods (following Aspire convention)

### File Naming Conventions

- `{Service}Resource.cs` - Resource class definitions
- `{Service}Extensions.cs` - Extension methods for `IDistributedApplicationBuilder`
- `{Service}ResourceBuilderExtensions.cs` - Extension methods for resource builders

### Testing Structure

```
tests/Aspire.Firebase.Hosting.Tests/
├── FirebaseResourceTests.cs          # Resource class unit tests
├── FirebaseExtensionsTests.cs        # Extension method unit tests
└── Aspire.Firebase.Hosting.Tests.csproj
```

## Dependencies

### Core Dependencies

- **.NET 8.0**: Minimum framework version
- **Aspire.Hosting**: Core Aspire hosting library
- **Google.Cloud.Firestore**: Official Google Firestore client
- **FirebaseAdmin**: Official Firebase Admin SDK

### Development Dependencies

- **xunit**: Testing framework
- **Microsoft.NET.Test.Sdk**: Test SDK
- **coverlet.collector**: Code coverage

## Build and Deployment

### Build Configuration

- **Debug**: Development builds with full debugging information
- **Release**: Optimized builds for production deployment
- **Documentation**: XML documentation generated for all public APIs
- **Symbols**: Symbol packages (.snupkg) for debugging

### CI/CD Pipeline

1. **Build**: Compile all projects
2. **Test**: Run unit tests with coverage
3. **Pack**: Create NuGet packages
4. **Publish**: Deploy to NuGet feeds

## Extensibility

### Adding New Firebase Services

To add support for additional Firebase services:

1. **Create Resource Class**:
   ```csharp
   public class FirebaseStorageResource : Resource, IResourceWithParent<FirebaseResource>
   {
       // Implementation
   }
   ```

2. **Add Extension Methods**:
   ```csharp
   public static IResourceBuilder<FirebaseStorageResource> AddFirebaseStorage(
       this IDistributedApplicationBuilder builder, ...)
   {
       // Implementation
   }
   ```

3. **Add Configuration Extensions**:
   ```csharp
   public static IResourceBuilder<FirebaseStorageResource> WithBucket(
       this IResourceBuilder<FirebaseStorageResource> builder, ...)
   {
       // Implementation
   }
   ```

4. **Add Tests**: Comprehensive unit tests for new functionality

### Configuration Patterns

New configuration options should follow these patterns:

- **Environment Variables**: Use standard Firebase/Google Cloud variable names
- **Fluent API**: Enable method chaining
- **Validation**: Validate inputs and provide clear error messages
- **Documentation**: Comprehensive XML documentation

## Best Practices

### Code Quality

- **Nullable Reference Types**: Enabled for type safety
- **Immutability**: Resources should be immutable
- **Validation**: Validate all inputs with `ArgumentException` types
- **Documentation**: XML documentation for all public APIs

### Testing

- **Unit Tests**: Test all public APIs
- **Integration Tests**: Test with real Aspire hosting scenarios
- **Parameter Validation**: Test argument validation
- **Edge Cases**: Test boundary conditions

### Performance

- **Lazy Initialization**: Defer expensive operations
- **Memory Efficiency**: Minimize allocations
- **Thread Safety**: Resources must be thread-safe

## Future Enhancements

### Planned Features

- [ ] Firebase Storage integration
- [ ] Cloud Messaging support
- [ ] Firebase Functions integration
- [ ] Real-time Database support
- [ ] Advanced configuration options
- [ ] Monitoring and metrics integration

### Breaking Changes

Major version updates may include:
- New required dependencies
- Changed method signatures
- Updated minimum framework versions
- Modified configuration patterns