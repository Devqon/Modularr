namespace Modularr.Modules.Repositories;

internal interface IModuleRepository
{
    Task DisableAsync(string moduleName);

    Task EnableAsync(string moduleName);

    Task<ModuleInfo> GetModuleAsync(string moduleName);

    Task<IEnumerable<ModuleInfo>> GetModulesAsync();

    Task<bool> IsEnabledAsync(string moduleName);
}
