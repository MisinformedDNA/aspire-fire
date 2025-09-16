using Aspire.Hosting.ApplicationModel;

namespace Aspire.Firebase.Hosting;

/// <summary>
/// Represents a Firebase Authentication resource.
/// </summary>
/// <param name="name">The name of the resource.</param>
/// <param name="firebase">The Firebase project resource.</param>
public class FirebaseAuthResource(string name, FirebaseResource firebase)
    : Resource(name), IResourceWithParent<FirebaseResource>, IResourceWithConnectionString, IResourceWithEnvironment
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
    /// Gets the connection string expression for Firebase Authentication.
    /// </summary>
    public ReferenceExpression ConnectionStringExpression =>
        ReferenceExpression.Create($"ProjectId={ProjectId}");

    /// <summary>
    /// Gets the connection string environment variable name.
    /// </summary>
    public string ConnectionStringEnvironmentVariable => $"ConnectionStrings__{Name}";

    /// <summary>
    /// Gets the connection string for Firebase Authentication.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>The connection string for Firebase Authentication.</returns>
    public ValueTask<string?> GetConnectionStringAsync(CancellationToken cancellationToken = default)
    {
        return new ValueTask<string?>($"ProjectId={ProjectId}");
    }
}