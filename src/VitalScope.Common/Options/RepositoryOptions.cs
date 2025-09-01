namespace VitalScope.Common.Options;

public sealed class RepositoryOptions
{
    public bool AutoSaveEnabled { get; set; } = true;

    public static RepositoryOptions DefaultValue => new RepositoryOptions();
}