namespace Modularr.Modules;

public interface IModule
{
    /// <summary>
    /// Name of the module.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Description of the module.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// The current version.
    /// </summary>
    public string Version { get; }

    /// <summary>
    /// The category.
    /// </summary>
    public string Category { get; }

    /// <summary>
    /// The author.
    /// </summary>
    public string Author { get; }

    /// <summary>
    /// An optional url for the author, for example a website or a github profile.
    /// </summary>
    public string AuthorUrl { get; }

    /// <summary>
    /// A collection of module dependencies.
    /// </summary>
    public string[] Dependencies { get; }

    /// <summary>
    /// A collection of tags.
    /// </summary>
    public string[] Tags { get; }
}
