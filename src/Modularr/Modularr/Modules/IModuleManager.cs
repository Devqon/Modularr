namespace Modularr.Modules;

public interface IModuleManager
{
    Task<IEnumerable<ModuleInfo>> GetModulesAsync();
}
