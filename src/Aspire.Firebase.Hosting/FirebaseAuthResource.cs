using Aspire.Hosting.ApplicationModel;

namespace Aspire.Firebase.Hosting;

/// <summary>
/// Represents a Firebase Authentication resource.
/// </summary>
/// <param name="name">The name of the resource.</param>
/// <param name="firebase">The Firebase project resource.</param>
public class FirebaseAuthResource(string name, FirebaseResource firebase)
    : Resource(name), IResourceWithParent<FirebaseResource>, IResourceWithConnectionString
{
    /// <summary>
    /// Gets the parent Firebase project resource.
    /// </summary>
    public FirebaseResource Parent => firebase;

    /// <summary>
    /// Gets the Firebase project ID.
    /// </summary>
    public string ProjectId => firebase.ProjectId;

    /// <summary>
    /// Gets the connection string for Firebase Authentication.
    /// </summary>
    /// <returns>The connection string for Firebase Authentication.</returns>
    public string GetConnectionString()
    {
        return $"ProjectId={ProjectId}";
    }
}