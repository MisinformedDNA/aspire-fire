using Aspire.Hosting.ApplicationModel;

namespace Aspire.Firebase.Hosting;

/// <summary>
/// Represents a Firebase Firestore database resource.
/// </summary>
/// <param name="name">The name of the resource.</param>
/// <param name="firebase">The Firebase project resource.</param>
/// <param name="databaseId">The Firestore database ID (defaults to "(default)").</param>
public class FirebaseFirestoreResource(string name, FirebaseResource firebase, string databaseId = "(default)")
    : Resource(name), IResourceWithParent<FirebaseResource>, IResourceWithConnectionString
{
    /// <summary>
    /// Gets the parent Firebase project resource.
    /// </summary>
    public FirebaseResource Parent => firebase;

    /// <summary>
    /// Gets the Firestore database ID.
    /// </summary>
    public string DatabaseId { get; } = databaseId;

    /// <summary>
    /// Gets the Firebase project ID.
    /// </summary>
    public string ProjectId => firebase.ProjectId;

    /// <summary>
    /// Gets the connection string for Firestore.
    /// </summary>
    /// <returns>The connection string for Firestore.</returns>
    public string GetConnectionString()
    {
        return $"ProjectId={ProjectId};DatabaseId={DatabaseId}";
    }
}