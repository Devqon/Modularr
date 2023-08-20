namespace Modularr.Modules.Repositories;

internal class ModuleRepository : IModuleRepository
{
    private static readonly HashSet<string> _disabledModules = new HashSet<string>();

    private readonly IEnumerable<IModule> _modules;

    public ModuleRepository(IEnumerable<IModule> modules)
    {
        _modules = modules;
    }

    public Task<ModuleInfo> GetModuleAsync(string moduleName)
    {
        var module = _modules.FirstOrDefault(m => m.Name == moduleName);
        return MapToModuleInfo(module);
    }

    public async Task<IEnumerable<ModuleInfo>> GetModulesAsync()
    {
        var result = new List<ModuleInfo>();

        foreach (var module in _modules)
        {
            var moduleInfo = await MapToModuleInfo(module);
            result.Add(moduleInfo);
        }

        return result;
    }

    private async Task<ModuleInfo> MapToModuleInfo(IModule module)
    {
        var isEnabled = await IsEnabledAsync(module.Name);

        return new ModuleInfo
        {
            Name = module.Name,
            IsEnabled = isEnabled,
            Description = module.Description,
            Version = module.Version,
            Category = module.Category,
            Author = module.Author,
            AuthorUrl = module.AuthorUrl,
            Dependencies = module.Dependencies,
            Tags = module.Tags,
        };
    }

    public Task<bool> IsEnabledAsync(string moduleName)
    {
        if (_disabledModules.Contains(moduleName))
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }

    public Task DisableAsync(string moduleName)
    {
        _disabledModules.Add(moduleName);
        return Task.CompletedTask;
    }

    public Task EnableAsync(string moduleName)
    {
        _disabledModules.Remove(moduleName);
        return Task.CompletedTask;
    }
}
