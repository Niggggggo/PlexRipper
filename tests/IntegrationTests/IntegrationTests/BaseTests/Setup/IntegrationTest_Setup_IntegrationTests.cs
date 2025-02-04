﻿namespace IntegrationTests.BaseTests.Setup;

public class IntegrationTest_Setup : BaseIntegrationTests
{
    public IntegrationTest_Setup(ITestOutputHelper output)
        : base(output) { }

    [Fact]
    public async Task ShouldHaveValidApiHttpClient_WhenStartingAnIntegrationTest()
    {
        await CreateContainer();
        Container.ApiClient.ShouldNotBeNull();
    }

    [Fact]
    public async Task ShouldHaveAllResolvePropertiesValid_WhenStartingAnIntegrationTest()
    {
        await CreateContainer();
        Container.FileSystem.ShouldNotBeNull();
        Container.GetDownloadQueue.ShouldNotBeNull();
        Container.GetPlexApiService.ShouldNotBeNull();
        Container.Mediator.ShouldNotBeNull();
        Container.PathProvider.ShouldNotBeNull();
        DbContext.ShouldNotBeNull();
    }

    [Fact]
    public async Task ShouldHaveUniqueInMemoryDatabase_WhenConfigFileIsGivenToContainer()
    {
        // Arrange
        await CreateContainer(x => x.Seed = 9999);

        // Assert
        Container.ShouldNotBeNull();
        DbContext.ShouldNotBeNull();
        DbContext.DatabaseName.ShouldNotBeEmpty();
        DbContext.DatabaseName.ShouldContain("memory_database");
    }

    [Fact]
    public async Task ShouldAllowForMultipleContainersToBeCreated_WhenMultipleAreCalled()
    {
        // Arrange
        await CreateContainer(x => x.Seed = 3457);
        await CreateContainer(x => x.Seed = 9654);

        // Assert
        Container.ShouldNotBeNull();
        DbContext.ShouldNotBeNull();
    }
}
