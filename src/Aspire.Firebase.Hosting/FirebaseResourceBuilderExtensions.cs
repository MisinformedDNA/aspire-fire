using Aspire.Firebase.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

/// <summary>
/// Provides extension methods for configuring Firebase resources.
/// </summary>
public static class FirebaseResourceBuilderExtensions
{
    /// <summary>
    /// Configures a Firebase resource with a service account key file.
    /// </summary>
    /// <param name="builder">The Firebase resource builder.</param>
    /// <param name="serviceAccountKeyPath">The path to the service account key JSON file.</param>
    /// <returns>The Firebase resource builder.</returns>
    public static IResourceBuilder<FirebaseResource> WithServiceAccountKey(
        this IResourceBuilder<FirebaseResource> builder,
        string serviceAccountKeyPath)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrEmpty(serviceAccountKeyPath);

        return builder.WithEnvironment("GOOGLE_APPLICATION_CREDENTIALS", serviceAccountKeyPath);
    }

    /// <summary>
    /// Configures a Firebase Firestore resource with a custom database ID.
    /// </summary>
    /// <param name="builder">The Firebase Firestore resource builder.</param>
    /// <param name="databaseId">The Firestore database ID.</param>
    /// <returns>The Firebase Firestore resource builder.</returns>
    public static IResourceBuilder<FirebaseFirestoreResource> WithDatabaseId(
        this IResourceBuilder<FirebaseFirestoreResource> builder,
        string databaseId)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrEmpty(databaseId);

        return builder.WithEnvironment("FIRESTORE_DATABASE_ID", databaseId);
    }

    /// <summary>
    /// Configures a Firebase resource with emulator settings for local development.
    /// </summary>
    /// <param name="builder">The Firebase resource builder.</param>
    /// <param name="firestorePort">The port for the Firestore emulator (default: 8080).</param>
    /// <param name="authPort">The port for the Auth emulator (default: 9099).</param>
    /// <returns>The Firebase resource builder.</returns>
    public static IResourceBuilder<FirebaseResource> WithEmulator(
        this IResourceBuilder<FirebaseResource> builder,
        int firestorePort = 8080,
        int authPort = 9099)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder
            .WithEnvironment("FIRESTORE_EMULATOR_HOST", $"localhost:{firestorePort}")
            .WithEnvironment("FIREBASE_AUTH_EMULATOR_HOST", $"localhost:{authPort}")
            .WithEnvironment("GCLOUD_PROJECT", builder.Resource.ProjectId);
    }

    /// <summary>
    /// Configures a Firebase Firestore resource with emulator settings for local development.
    /// </summary>
    /// <param name="builder">The Firebase Firestore resource builder.</param>
    /// <param name="port">The port for the Firestore emulator (default: 8080).</param>
    /// <returns>The Firebase Firestore resource builder.</returns>
    public static IResourceBuilder<FirebaseFirestoreResource> WithEmulator(
        this IResourceBuilder<FirebaseFirestoreResource> builder,
        int port = 8080)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder
            .WithEnvironment("FIRESTORE_EMULATOR_HOST", $"localhost:{port}")
            .WithEnvironment("GCLOUD_PROJECT", builder.Resource.ProjectId);
    }

    /// <summary>
    /// Configures a Firebase Authentication resource with emulator settings for local development.
    /// </summary>
    /// <param name="builder">The Firebase Authentication resource builder.</param>
    /// <param name="port">The port for the Auth emulator (default: 9099).</param>
    /// <returns>The Firebase Authentication resource builder.</returns>
    public static IResourceBuilder<FirebaseAuthResource> WithEmulator(
        this IResourceBuilder<FirebaseAuthResource> builder,
        int port = 9099)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder
            .WithEnvironment("FIREBASE_AUTH_EMULATOR_HOST", $"localhost:{port}")
            .WithEnvironment("GCLOUD_PROJECT", builder.Resource.ProjectId);
    }
}