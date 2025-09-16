using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Add Firebase project
var firebase = builder.AddFirebase("firebase", "my-firebase-project-id");

// Add Firestore database
var firestore = builder.AddFirebaseFirestore("firestore", firebase);

// Add Firebase Auth
var auth = builder.AddFirebaseAuth("auth", firebase);

// For local development, uncomment the following line to use emulators:
// firebase.WithEmulator(firestorePort: 8080, authPort: 9099);

var app = builder.Build();

try
{
    app.Run();
}
catch (Exception ex) when (ex.Message.Contains("ASPNETCORE_URLS") || ex.Message.Contains("ASPIRE_DASHBOARD"))
{
    Console.WriteLine("============================================================================");
    Console.WriteLine("Error: Unable to start Aspire AppHost - Dashboard configuration missing");
    Console.WriteLine("============================================================================");
    Console.WriteLine();
    Console.WriteLine("This error occurs when the Aspire development environment is not fully configured.");
    Console.WriteLine();
    Console.WriteLine("To resolve this issue:");
    Console.WriteLine("1. Ensure the Aspire workload is installed:");
    Console.WriteLine("   dotnet workload install aspire");
    Console.WriteLine();
    Console.WriteLine("2. If you're running in an unsupported environment (like CI/CD), consider");
    Console.WriteLine("   using the comprehensive code examples in the README instead.");
    Console.WriteLine();
    Console.WriteLine("3. For testing the Firebase extension without full Aspire infrastructure,");
    Console.WriteLine("   refer to the unit tests in the tests/ directory.");
    Console.WriteLine();
    Console.WriteLine("Original error:");
    Console.WriteLine(ex.Message);
    Console.WriteLine("============================================================================");
    
    Environment.Exit(1);
}