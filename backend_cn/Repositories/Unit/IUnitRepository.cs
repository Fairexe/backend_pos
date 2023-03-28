using backend_cn.APIResult;
using backend_cn.ViewModels;

namespace backend_cn.Repositories.unit
{
    public interface IUnitRepository
    {
        List<UnitViewModel> GetUnits();
        UnitViewModel GetUnitById(int id);
        void Update(EditUnitViewModel editUnit);
        void Add(AddUnitViewModel unit);
        void RemoveById(int id);
        bool CheckDuplicateName(string name);
        bool CheckUnitIsUsed(int id);
    }
}
