namespace PlexRipper.Domain;

public class DownloadTaskTvShow : DownloadTaskParentBase
{
    #region Relationships

    public required List<DownloadTaskTvShowSeason> Children { get; set; } = new();

    #endregion

    #region Helpers

    public override PlexMediaType MediaType => PlexMediaType.TvShow;

    public override DownloadTaskType DownloadTaskType => DownloadTaskType.TvShow;

    public override bool IsDownloadable => false;

    public override int Count => Children.Sum(x => x.Count) + 1;

    #endregion
}