using backend_cn.APIResult;
using backend_cn.Constants;
using backend_cn.Models;
using backend_cn.Repositories.unit;
using backend_cn.ViewModels;

namespace backend_cn.BusinessLogic.Unit
{
    public class UnitDetail: IAPIResult
    {
        readonly IUnitRepository unitRepository;

        public UnitDetail(IUnitRepository unitRepository)
        {
            this.unitRepository = unitRepository;
        }

        public ApiResultViewModel<UnitViewModel> GetUnitById(int id)
        {
            var unit = unitRepository.GetUnitById(id);
            return this.ApiResultViewModel(APICode.Success,APIMessage.SUCCESSFUL,unit);
        }

        public ApiResultViewModel<List<UnitViewModel>> GetUnits()
        {
            var units = unitRepository.GetUnits();
            return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL, units);
        }
    }
}
