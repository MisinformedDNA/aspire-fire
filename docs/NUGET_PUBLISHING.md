# Publishing to NuGet

This guide explains how to publish the `Aspire.Firebase.Hosting` library to NuGet.org or a private NuGet feed.

## Prerequisites

1. **.NET 8.0 SDK**: Ensure you have .NET 8.0 SDK or later installed
2. **NuGet Account**: Create an account at [nuget.org](https://nuget.org)
3. **API Key**: Generate an API key from your NuGet account settings

## Prepare for Publishing

### 1. Update Package Version

Update the version in `src/Aspire.Firebase.Hosting/Aspire.Firebase.Hosting.csproj`:

```xml
<Version>1.0.0</Version>  <!-- Update this for each release -->
```

### 2. Update Release Notes

Update the description and any other metadata in the project file as needed.

### 3. Ensure Quality

```bash
# Run tests to ensure everything works
dotnet test

# Run a clean build
dotnet clean
dotnet build --configuration Release
```

## Build the Package

### Clean Build

```bash
cd aspire-fire
dotnet clean
dotnet restore
dotnet build --configuration Release
```

### Create NuGet Package

```bash
# Pack the main library
dotnet pack src/Aspire.Firebase.Hosting/Aspire.Firebase.Hosting.csproj \
  --configuration Release \
  --output ./packages \
  --include-symbols \
  --include-source
```

This creates:
- `packages/Aspire.Firebase.Hosting.{version}.nupkg` - Main package
- `packages/Aspire.Firebase.Hosting.{version}.snupkg` - Symbol package

## Validate the Package

### Inspect Package Contents

```bash
# Use dotnet to inspect the package
dotnet nuget locals all --list

# Or manually inspect with any zip tool (rename .nupkg to .zip)
```

### Test Locally

```bash
# Create a local test project
mkdir test-nuget && cd test-nuget
dotnet new console
dotnet add package Aspire.Firebase.Hosting --source ../packages
```

## Publish to NuGet

### Public NuGet.org

```bash
# Replace YOUR_API_KEY with your actual NuGet API key
dotnet nuget push packages/Aspire.Firebase.Hosting.*.nupkg \
  --api-key YOUR_API_KEY \
  --source https://api.nuget.org/v3/index.json
```

### Private NuGet Feed

```bash
# For Azure DevOps
dotnet nuget push packages/Aspire.Firebase.Hosting.*.nupkg \
  --api-key YOUR_API_KEY \
  --source https://pkgs.dev.azure.com/yourorg/yourproject/_packaging/yourfeed/nuget/v3/index.json

# For GitHub Packages
dotnet nuget push packages/Aspire.Firebase.Hosting.*.nupkg \
  --api-key YOUR_GITHUB_TOKEN \
  --source https://nuget.pkg.github.com/OWNER/index.json

# For custom feed
dotnet nuget push packages/Aspire.Firebase.Hosting.*.nupkg \
  --api-key YOUR_API_KEY \
  --source https://your-custom-feed.com/nuget
```

## Package Configuration

The package is configured with the following metadata in the `.csproj` file:

```xml
<!-- Package Identity -->
<PackageId>Aspire.Firebase.Hosting</PackageId>
<Version>1.0.0-preview.1</Version>
<Authors>Aspire Firebase Community</Authors>

<!-- Package Details -->
<Description>Firebase integration for .NET Aspire hosting</Description>
<PackageTags>aspire;firebase;firestore;auth;hosting;cloud</PackageTags>
<PackageProjectUrl>https://github.com/MisinformedDNA/aspire-fire</PackageProjectUrl>
<RepositoryUrl>https://github.com/MisinformedDNA/aspire-fire</RepositoryUrl>
<RepositoryType>git</RepositoryType>

<!-- License and Documentation -->
<PackageLicenseExpression>MIT</PackageLicenseExpression>
<PackageReadmeFile>README.md</PackageReadmeFile>

<!-- Symbol Packages -->
<IncludeSymbols>true</IncludeSymbols>
<SymbolPackageFormat>snupkg</SymbolPackageFormat>
<GenerateDocumentationFile>true</GenerateDocumentationFile>
```

## Versioning Strategy

Use semantic versioning (SemVer):

- **Major.Minor.Patch** (e.g., `1.0.0`)
- **Pre-release**: Add a suffix (e.g., `1.0.0-preview.1`, `1.0.0-beta.1`, `1.0.0-rc.1`)

### Recommended Versioning

- `0.x.x` - Initial development
- `1.0.0-preview.x` - Preview releases
- `1.0.0-beta.x` - Beta releases  
- `1.0.0-rc.x` - Release candidates
- `1.0.0` - Stable release
- `1.0.x` - Patch releases
- `1.x.0` - Minor releases (new features)
- `x.0.0` - Major releases (breaking changes)

## Automation with GitHub Actions

Create `.github/workflows/nuget-publish.yml`:

```yaml
name: Publish to NuGet

on:
  release:
    types: [published]

jobs:
  publish:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'
        
    - name: Restore dependencies
      run: dotnet restore
      
    - name: Build
      run: dotnet build --configuration Release --no-restore
      
    - name: Test
      run: dotnet test --no-build --configuration Release
      
    - name: Pack
      run: dotnet pack src/Aspire.Firebase.Hosting/Aspire.Firebase.Hosting.csproj --configuration Release --output ./packages
      
    - name: Publish to NuGet
      run: dotnet nuget push packages/*.nupkg --api-key ${{ secrets.NUGET_API_KEY }} --source https://api.nuget.org/v3/index.json
```

## Post-Publication

1. **Verify Package**: Check that the package appears on NuGet.org
2. **Update Documentation**: Update any references to version numbers
3. **Test Installation**: Test installing the package in a fresh project
4. **Announce**: Share the release with the community

## Troubleshooting

### Common Issues

1. **Authentication Failed**: Verify your API key is correct and has push permissions
2. **Package Already Exists**: You cannot overwrite published packages; increment the version
3. **Dependencies Not Found**: Ensure all dependencies are available on the target feed
4. **Symbol Upload Failed**: Check that symbol packages are enabled on your feed

### Package Validation

```bash
# Validate package contents
dotnet nuget verify packages/Aspire.Firebase.Hosting.*.nupkg

# Check dependencies
dotnet list package --outdated
```

## Security Considerations

1. **API Keys**: Never commit API keys to source control
2. **Service Accounts**: Use environment variables or secure vaults
3. **Dependencies**: Regularly update dependencies for security patches
4. **Code Signing**: Consider code signing for production packages

## Resources

- [NuGet.org Publishing Guide](https://docs.microsoft.com/en-us/nuget/nuget-org/publish-a-package)
- [.NET Package Authoring](https://docs.microsoft.com/en-us/dotnet/core/tutorials/cli-create-console-app)
- [Semantic Versioning](https://semver.org)
- [NuGet Package Explorer](https://github.com/NuGetPackageExplorer/NuGetPackageExplorer)