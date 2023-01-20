using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces;

public interface IIventoryRepository
{

    Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name);
}