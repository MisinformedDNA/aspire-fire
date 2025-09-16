using Aspire.Hosting;

// Create the distributed application builder
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

Console.WriteLine("Firebase Aspire Extension Examples");
Console.WriteLine("==================================");
Console.WriteLine();

// Display the configured resources
Console.WriteLine("Configured Firebase Resources:");
Console.WriteLine($"1. Firebase: {firebase.Resource.Name} - Project: {firebase.Resource.ProjectId}");
Console.WriteLine($"2. Firebase Dev: {firebaseDev.Resource.Name} - Project: {firebaseDev.Resource.ProjectId}");
Console.WriteLine($"3. Firestore: {firestore.Resource.Name} - Project: {firestore.Resource.ProjectId}, DB: {firestore.Resource.DatabaseId}");
Console.WriteLine($"4. Firebase Auth: {auth.Resource.Name} - Project: {auth.Resource.ProjectId}");
Console.WriteLine($"5. Main Firebase: {mainFirebase.Resource.Name} - Project: {mainFirebase.Resource.ProjectId}");
Console.WriteLine($"6. Main Firestore: {mainFirestore.Resource.Name} - Project: {mainFirestore.Resource.ProjectId}, DB: {mainFirestore.Resource.DatabaseId}");
Console.WriteLine($"7. Main Auth: {mainAuth.Resource.Name} - Project: {mainAuth.Resource.ProjectId}");
Console.WriteLine($"8. Dev Firebase: {devFirebase.Resource.Name} - Project: {devFirebase.Resource.ProjectId}");

// Display connection strings
Console.WriteLine();
Console.WriteLine("Connection Strings:");
var firebaseConnectionString = await firebase.Resource.GetConnectionStringAsync();
var firestoreConnectionString = await firestore.Resource.GetConnectionStringAsync();
var authConnectionString = await auth.Resource.GetConnectionStringAsync();
Console.WriteLine($"Firebase: {firebaseConnectionString}");
Console.WriteLine($"Firestore: {firestoreConnectionString}");
Console.WriteLine($"Auth: {authConnectionString}");

// Build and run the application
var app = builder.Build();

// This would normally start the application host
// For this example, we'll just demonstrate the setup
Console.WriteLine();
Console.WriteLine("Firebase extension setup completed successfully!");
Console.WriteLine("This example demonstrates various ways to configure Firebase resources with .NET Aspire.");
Console.WriteLine();
Console.WriteLine("Usage patterns shown:");
Console.WriteLine("- Simple Firebase project setup");
Console.WriteLine("- Firebase with emulator configuration");
Console.WriteLine("- Separate Firestore and Auth resources");  
Console.WriteLine("- Chained resource configuration");
Console.WriteLine("- Development vs production setups");

// In a real application, you would call:
// await app.RunAsync();

return 0;
