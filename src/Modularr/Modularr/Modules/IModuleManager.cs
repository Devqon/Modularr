namespace Modularr.Modules;

internal interface IModuleManager
{
    Task<IEnumerable<ModuleInfo>> GetModulesAsync();
}
