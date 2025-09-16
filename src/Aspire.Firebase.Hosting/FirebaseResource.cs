using Aspire.Hosting.ApplicationModel;

namespace Aspire.Firebase.Hosting;

/// <summary>
/// Represents a Firebase project resource.
/// </summary>
/// <param name="name">The name of the resource.</param>
/// <param name="projectId">The Firebase project ID.</param>
public class FirebaseResource(string name, string projectId) : Resource(name), IResourceWithConnectionString, IResourceWithEnvironment
{
    /// <summary>
    /// Gets the Firebase project ID.
    /// </summary>
    public string ProjectId { get; } = projectId;

    /// <summary>
    /// Gets the connection string expression for the Firebase project.
    /// </summary>
    public ReferenceExpression ConnectionStringExpression =>
        ReferenceExpression.Create($"{ProjectId}");

    /// <summary>
    /// Gets the connection string environment variable name.
    /// </summary>
    public string ConnectionStringEnvironmentVariable => $"ConnectionStrings__{Name}";

    /// <summary>
    /// Gets the connection string for the Firebase project.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>The connection string for the Firebase project.</returns>
    public ValueTask<string?> GetConnectionStringAsync(CancellationToken cancellationToken = default)
    {
        return new ValueTask<string?>(ProjectId);
    }
}