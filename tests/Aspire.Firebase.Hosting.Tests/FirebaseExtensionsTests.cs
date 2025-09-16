using Aspire.Firebase.Hosting;
using Aspire.Hosting;
using Xunit;

namespace Aspire.Firebase.Hosting.Tests;

public class FirebaseExtensionsTests
{
    [Fact]
    public void AddFirebase_ShouldCreateFirebaseResource()
    {
        // Arrange
        var builder = DistributedApplication.CreateBuilder();
        const string name = "test-firebase";
        const string projectId = "test-project-123";

        // Act
        var resourceBuilder = builder.AddFirebase(name, projectId);

        // Assert
        Assert.NotNull(resourceBuilder);
        Assert.Equal(name, resourceBuilder.Resource.Name);
        Assert.Equal(projectId, resourceBuilder.Resource.ProjectId);
    }

    [Fact]
    public void AddFirebaseFirestore_WithFirebaseResource_ShouldCreateFirestoreResource()
    {
        // Arrange
        var builder = DistributedApplication.CreateBuilder();
        const string firebaseName = "test-firebase";
        const string firestoreName = "test-firestore";
        const string projectId = "test-project-123";
        const string databaseId = "custom-db";
        
        var firebase = builder.AddFirebase(firebaseName, projectId);

        // Act
        var firestore = builder.AddFirebaseFirestore(firestoreName, firebase, databaseId);

        // Assert
        Assert.NotNull(firestore);
        Assert.Equal(firestoreName, firestore.Resource.Name);
        Assert.Equal(projectId, firestore.Resource.ProjectId);
        Assert.Equal(databaseId, firestore.Resource.DatabaseId);
        Assert.Equal(firebase.Resource, firestore.Resource.Parent);
    }

    [Fact]
    public void AddFirebaseFirestore_WithProjectId_ShouldCreateFirestoreResource()
    {
        // Arrange
        var builder = DistributedApplication.CreateBuilder();
        const string firestoreName = "test-firestore";
        const string projectId = "test-project-123";
        const string databaseId = "custom-db";

        // Act
        var firestore = builder.AddFirebaseFirestore(firestoreName, projectId, databaseId);

        // Assert
        Assert.NotNull(firestore);
        Assert.Equal(firestoreName, firestore.Resource.Name);
        Assert.Equal(projectId, firestore.Resource.ProjectId);
        Assert.Equal(databaseId, firestore.Resource.DatabaseId);
    }

    [Fact]
    public void AddFirebaseAuth_WithFirebaseResource_ShouldCreateAuthResource()
    {
        // Arrange
        var builder = DistributedApplication.CreateBuilder();
        const string firebaseName = "test-firebase";
        const string authName = "test-auth";
        const string projectId = "test-project-123";
        
        var firebase = builder.AddFirebase(firebaseName, projectId);

        // Act
        var auth = builder.AddFirebaseAuth(authName, firebase);

        // Assert
        Assert.NotNull(auth);
        Assert.Equal(authName, auth.Resource.Name);
        Assert.Equal(projectId, auth.Resource.ProjectId);
        Assert.Equal(firebase.Resource, auth.Resource.Parent);
    }

    [Fact]
    public void AddFirebaseAuth_WithProjectId_ShouldCreateAuthResource()
    {
        // Arrange
        var builder = DistributedApplication.CreateBuilder();
        const string authName = "test-auth";
        const string projectId = "test-project-123";

        // Act
        var auth = builder.AddFirebaseAuth(authName, projectId);

        // Assert
        Assert.NotNull(auth);
        Assert.Equal(authName, auth.Resource.Name);
        Assert.Equal(projectId, auth.Resource.ProjectId);
    }

    [Theory]
    [InlineData(null, "project-id")]
    [InlineData("", "project-id")]
    [InlineData("name", null)]
    [InlineData("name", "")]
    public void AddFirebase_WithInvalidArguments_ShouldThrow(string? name, string? projectId)
    {
        // Arrange
        var builder = DistributedApplication.CreateBuilder();

        // Act & Assert
        Assert.ThrowsAny<ArgumentException>(() => builder.AddFirebase(name!, projectId!));
    }
}