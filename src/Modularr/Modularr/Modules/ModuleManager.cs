using Modularr.Modules.Repositories;

namespace Modularr.Modules;

internal class ModuleManager : IModuleManager
{
    private readonly IModuleRepository _moduleRepository;

    public ModuleManager(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public Task<IEnumerable<ModuleInfo>> GetModulesAsync()
    {
        return _moduleRepository.GetModulesAsync();
    }
}
