namespace PlexRipper.Application.UnitTests;

public class GetAllDownloadTasksQueryHandler_UnitTests : BaseUnitTest<GetAllDownloadTasksQueryHandler>
{
    public GetAllDownloadTasksQueryHandler_UnitTests(ITestOutputHelper output) : base(output) { }

    [Fact]
    public async Task ShouldReturnNoDownloadTasks_WhenNoDownloadTasksAreInDb()
    {
        // Arrange
        await SetupDatabase();
        var request = new GetAllDownloadTasksQuery();
        var handler = mock.Create<GetAllDownloadTasksQueryHandler>();

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEmpty();
    }

    [Fact]
    public async Task ShouldReturnMovieDownloadTasks_WhenMovieDownloadTasksAreInDB()
    {
        // Arrange
        Seed = 21467;
        await SetupDatabase(config =>
        {
            config.PlexServerCount = 1;
            config.PlexLibraryCount = 2;
            config.MovieCount = 20;
            config.MovieDownloadTasksCount = 10;
        });

        // Act
        var request = new GetAllDownloadTasksQuery();
        var handler = mock.Create<GetAllDownloadTasksQueryHandler>();
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        var downloadTasks = result.Value;
        downloadTasks.ShouldNotBeEmpty();
        downloadTasks.Count.ShouldBe(10);

        var flatList = downloadTasks.Flatten(x => x.Children).ToList();
        flatList.ShouldAllBe(x => x.PlexServer != null);
        flatList.ShouldAllBe(x => x.PlexLibrary != null);
    }

    [Fact]
    public async Task ShouldAllTvShowDownloadTasksWithAllIncludes_WhenTvShowDownloadTasksAreInDB()
    {
        // Arrange
        Seed = 2767;
        await SetupDatabase(config =>
        {
            config.PlexServerCount = 1;
            config.PlexLibraryCount = 2;
            config.TvShowCount = 20;
            config.TvShowDownloadTasksCount = 5;
            config.TvShowSeasonDownloadTasksCount = 5;
            config.TvShowEpisodeDownloadTasksCount = 5;
        });

        // Act
        var request = new GetAllDownloadTasksQuery();
        var handler = mock.Create<GetAllDownloadTasksQueryHandler>();
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        var downloadTasks = result.Value;
        downloadTasks.ShouldNotBeEmpty();

        void ValidateDownloadTasks(List<DownloadTaskGeneric> shouldDownloadTasks)
        {
            if (shouldDownloadTasks is null)
                return;

            downloadTasks.Count.ShouldBe(5);
            foreach (var downloadTask in shouldDownloadTasks)
            {
                downloadTask.PlexServer.ShouldNotBeNull($"DownloadTaskType {downloadTask.DownloadTaskType} has PlexServer null");
                downloadTask.PlexLibrary.ShouldNotBeNull($"DownloadTaskType {downloadTask.DownloadTaskType} has PlexLibrary null");
                ValidateDownloadTasks(downloadTask.Children);
            }
        }

        ValidateDownloadTasks(downloadTasks);
        downloadTasks.Flatten(x => x.Children).ToList().Count.ShouldBe(280);
    }
}