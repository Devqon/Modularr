namespace Modulith.Modules;

internal interface IModuleManager
{
    Task<IEnumerable<ModuleInfo>> GetModulesAsync();
}