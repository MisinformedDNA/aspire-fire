using Aspire.Firebase.Hosting;
using Aspire.Hosting;
using Xunit;

namespace Aspire.Firebase.Hosting.Tests;

public class FirebaseResourceTests
{
    [Fact]
    public async Task FirebaseResource_ShouldCreateWithCorrectProperties()
    {
        // Arrange
        const string name = "test-firebase";
        const string projectId = "test-project-123";

        // Act
        var resource = new FirebaseResource(name, projectId);

        // Assert
        Assert.Equal(name, resource.Name);
        Assert.Equal(projectId, resource.ProjectId);
        var connectionString = await resource.GetConnectionStringAsync();
        Assert.Equal(projectId, connectionString);
        Assert.Equal($"ConnectionStrings__{name}", resource.ConnectionStringEnvironmentVariable);
    }

    [Fact]
    public async Task FirebaseFirestoreResource_ShouldCreateWithCorrectProperties()
    {
        // Arrange
        const string firebaseName = "test-firebase";
        const string firestoreName = "test-firestore";
        const string projectId = "test-project-123";
        const string databaseId = "custom-db";
        var firebase = new FirebaseResource(firebaseName, projectId);

        // Act
        var firestore = new FirebaseFirestoreResource(firestoreName, firebase, databaseId);

        // Assert
        Assert.Equal(firestoreName, firestore.Name);
        Assert.Equal(projectId, firestore.ProjectId);
        Assert.Equal(databaseId, firestore.DatabaseId);
        Assert.Equal(firebase, firestore.Parent);
        var connectionString = await firestore.GetConnectionStringAsync();
        Assert.Equal($"ProjectId={projectId};DatabaseId={databaseId}", connectionString);
        Assert.Equal($"ConnectionStrings__{firestoreName}", firestore.ConnectionStringEnvironmentVariable);
    }

    [Fact]
    public async Task FirebaseFirestoreResource_ShouldUseDefaultDatabaseId()
    {
        // Arrange
        const string firebaseName = "test-firebase";
        const string firestoreName = "test-firestore";
        const string projectId = "test-project-123";
        var firebase = new FirebaseResource(firebaseName, projectId);

        // Act
        var firestore = new FirebaseFirestoreResource(firestoreName, firebase);

        // Assert
        Assert.Equal("(default)", firestore.DatabaseId);
        var connectionString = await firestore.GetConnectionStringAsync();
        Assert.Equal($"ProjectId={projectId};DatabaseId=(default)", connectionString);
    }

    [Fact]
    public async Task FirebaseAuthResource_ShouldCreateWithCorrectProperties()
    {
        // Arrange
        const string firebaseName = "test-firebase";
        const string authName = "test-auth";
        const string projectId = "test-project-123";
        var firebase = new FirebaseResource(firebaseName, projectId);

        // Act
        var auth = new FirebaseAuthResource(authName, firebase);

        // Assert
        Assert.Equal(authName, auth.Name);
        Assert.Equal(projectId, auth.ProjectId);
        Assert.Equal(firebase, auth.Parent);
        var connectionString = await auth.GetConnectionStringAsync();
        Assert.Equal($"ProjectId={projectId}", connectionString);
        Assert.Equal($"ConnectionStrings__{authName}", auth.ConnectionStringEnvironmentVariable);
    }
}