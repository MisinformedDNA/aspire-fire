using Aspire.Firebase.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

/// <summary>
/// Provides extension methods for adding Firebase resources to an <see cref="IDistributedApplicationBuilder"/>.
/// </summary>
public static class FirebaseExtensions
{
    /// <summary>
    /// Adds a Firebase project resource to the application.
    /// </summary>
    /// <param name="builder">The distributed application builder.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="projectId">The Firebase project ID.</param>
    /// <returns>A reference to the Firebase resource.</returns>
    public static IResourceBuilder<FirebaseResource> AddFirebase(
        this IDistributedApplicationBuilder builder,
        string name,
        string projectId)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(projectId);

        var firebase = new FirebaseResource(name, projectId);
        return builder.AddResource(firebase);
    }

    /// <summary>
    /// Adds a Firebase Firestore resource to the application.
    /// </summary>
    /// <param name="builder">The distributed application builder.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="firebase">The Firebase project resource.</param>
    /// <param name="databaseId">The Firestore database ID (defaults to "(default)").</param>
    /// <returns>A reference to the Firebase Firestore resource.</returns>
    public static IResourceBuilder<FirebaseFirestoreResource> AddFirebaseFirestore(
        this IDistributedApplicationBuilder builder,
        string name,
        IResourceBuilder<FirebaseResource> firebase,
        string databaseId = "(default)")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(firebase);

        var firestore = new FirebaseFirestoreResource(name, firebase.Resource, databaseId);
        return builder.AddResource(firestore);
    }

    /// <summary>
    /// Adds a Firebase Firestore resource directly with project configuration.
    /// </summary>
    /// <param name="builder">The distributed application builder.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="projectId">The Firebase project ID.</param>
    /// <param name="databaseId">The Firestore database ID (defaults to "(default)").</param>
    /// <returns>A reference to the Firebase Firestore resource.</returns>
    public static IResourceBuilder<FirebaseFirestoreResource> AddFirebaseFirestore(
        this IDistributedApplicationBuilder builder,
        string name,
        string projectId,
        string databaseId = "(default)")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(projectId);

        var firebase = new FirebaseResource($"{name}-firebase", projectId);
        var firestore = new FirebaseFirestoreResource(name, firebase, databaseId);
        return builder.AddResource(firestore);
    }

    /// <summary>
    /// Adds a Firebase Authentication resource to the application.
    /// </summary>
    /// <param name="builder">The distributed application builder.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="firebase">The Firebase project resource.</param>
    /// <returns>A reference to the Firebase Authentication resource.</returns>
    public static IResourceBuilder<FirebaseAuthResource> AddFirebaseAuth(
        this IDistributedApplicationBuilder builder,
        string name,
        IResourceBuilder<FirebaseResource> firebase)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(firebase);

        var auth = new FirebaseAuthResource(name, firebase.Resource);
        return builder.AddResource(auth);
    }

    /// <summary>
    /// Adds a Firebase Authentication resource directly with project configuration.
    /// </summary>
    /// <param name="builder">The distributed application builder.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="projectId">The Firebase project ID.</param>
    /// <returns>A reference to the Firebase Authentication resource.</returns>
    public static IResourceBuilder<FirebaseAuthResource> AddFirebaseAuth(
        this IDistributedApplicationBuilder builder,
        string name,
        string projectId)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(projectId);

        var firebase = new FirebaseResource($"{name}-firebase", projectId);
        var auth = new FirebaseAuthResource(name, firebase);
        return builder.AddResource(auth);
    }
}