using Aspire.Hosting.ApplicationModel;

namespace Aspire.Firebase.Hosting;

/// <summary>
/// Represents a Firebase project resource.
/// </summary>
/// <param name="name">The name of the resource.</param>
/// <param name="projectId">The Firebase project ID.</param>
public class FirebaseResource(string name, string projectId) : Resource(name), IResourceWithConnectionString
{
    /// <summary>
    /// Gets the Firebase project ID.
    /// </summary>
    public string ProjectId { get; } = projectId;

    /// <summary>
    /// Gets the connection string for the Firebase project.
    /// </summary>
    /// <returns>The connection string for the Firebase project.</returns>
    public string GetConnectionString()
    {
        return ProjectId;
    }
}