namespace Modularr.Modules;

internal class ModuleInfo
{
    /// <summary>
    /// Name of the module.
    /// </summary>
    public string Name { get; internal set; }

    /// <summary>
    /// Is the module enabled.
    /// </summary>
    public bool IsEnabled { get; internal set; }

    /// <summary>
    /// Description of the module.
    /// </summary>
    public string Description { get; internal set; }

    /// <summary>
    /// The current version.
    /// </summary>
    public string Version { get; internal set; }

    /// <summary>
    /// The author.
    /// </summary>
    public string Author { get; internal set; }

    /// <summary>
    /// An optional url for the author, for example a website or a github profile.
    /// </summary>
    public string AuthorUrl { get; internal set; }

    /// <summary>
    /// A collection of module dependencies.
    /// </summary>
    public string[] Dependencies { get; internal set; }

    /// <summary>
    /// A collection of tags.
    /// </summary>
    public string[] Tags { get; internal set; }
}
