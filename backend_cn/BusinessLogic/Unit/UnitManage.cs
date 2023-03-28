using backend_cn.APIResult;
using backend_cn.Constants;
using backend_cn.Repositories.Product;
using backend_cn.Repositories.unit;
using backend_cn.ViewModels;

namespace backend_cn.BusinessLogic.Unit
{
    public class UnitManage : IAPIResult
    {
        readonly IUnitRepository unitRepository;

        public UnitManage(IUnitRepository unitRepository)
        {
            this.unitRepository = unitRepository;
        }

        public ApiResultViewModel Add(AddUnitViewModel unit)
        {
            if (string.IsNullOrEmpty(unit.UnitName))
            {
                throw new Exception(UnitMessage.CANTDELETE);
            }
            try
            {
                if (unitRepository.CheckDuplicateName(unit.UnitName))
                {
                    throw new Exception(UnitMessage.DUPLICATEDNAME);
                }
                unitRepository.Add(unit);
                return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL);
            }
            catch (Exception ex)
            {
                return this.ApiResultViewModel(APICode.InvalidArgument, ex.ToString());
            }
        }

        public ApiResultViewModel Update(EditUnitViewModel editUnit)
        {
            if (string.IsNullOrEmpty(editUnit.UnitName))
            {
                throw new Exception(UnitMessage.CANTDELETE);
            }
            var result = new ApiResultViewModel();


            try
            {
                var unit = unitRepository.GetUnitById(editUnit.Uid);

                if (unit.UnitName != editUnit.UnitName)
                {

                    if (unitRepository.CheckDuplicateName(editUnit.UnitName))
                    {
                        throw new Exception(UnitMessage.DUPLICATEDNAME);
                    }
                    unitRepository.Update(editUnit);
                }
                return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL);

            }
            catch (Exception ex)
            {
                return this.ApiResultViewModel(APICode.InvalidArgument, ex.ToString());
            }

        }

        public ApiResultViewModel RemoveById(int id)
        {
            try
            {
                if (unitRepository.CheckUnitIsUsed(id))
                {
                    throw new Exception(UnitMessage.CANTDELETE);
                }
                unitRepository.RemoveById(id);
                return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL);
            }
            catch (Exception ex)
            {
                return this.ApiResultViewModel(APICode.InvalidArgument, ex.ToString());
            }     
        }
    }
}
