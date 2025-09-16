# Firebase AppHost Example

This is a minimal working example of using the Firebase extension with .NET Aspire.

## Prerequisites

- .NET 8.0 SDK or later
- Aspire workload: `dotnet workload install aspire`
- Properly configured Aspire development environment

## Running the Example

1. Navigate to this directory:
   ```bash
   cd examples/Firebase.AppHost
   ```

2. Run the AppHost:
   ```bash
   dotnet run
   ```

This will start the Aspire dashboard and orchestrate the Firebase resources.

## Troubleshooting

If you encounter errors about missing `ASPNETCORE_URLS` or `ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL`, this typically means:

1. **Aspire workload not installed**: Run `dotnet workload install aspire`
2. **Environment not supported**: Some environments (CI/CD, containers) may not support the full Aspire dashboard
3. **Configuration missing**: The Aspire development environment may need additional setup

### Alternative: Use Code Examples

If you can't run the full AppHost, refer to the comprehensive code examples in the main README.md file. These show how to configure Firebase resources without requiring the full Aspire infrastructure.

### Testing the Extension

The Firebase extension functionality can be tested using the unit tests:
```bash
cd ../../tests/Aspire.Firebase.Hosting.Tests
dotnet test
```

## Configuration

The example demonstrates:
- Basic Firebase project setup
- Firestore database configuration
- Firebase Authentication setup

## Using with Emulators

To use Firebase emulators for local development, uncomment the emulator configuration line in `Program.cs`:

```csharp
firebase.WithEmulator(firestorePort: 8080, authPort: 9099);
```

Then start the Firebase emulators before running the AppHost:

```bash
firebase emulators:start --only firestore,auth
```

## Customization

- Change the project ID in `Program.cs` to match your Firebase project
- Add service account key configuration for production use
- Add additional Firebase services as needed

See the main README for more comprehensive examples and configuration options.