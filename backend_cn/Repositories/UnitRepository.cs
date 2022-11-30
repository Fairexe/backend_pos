using backend_cn.Context;
using backend_cn.Models;
using backend_cn.ViewModels;

namespace backend_cn.Repositories
{
    public class UnitRepository
    {
        readonly PosDbContext context;

        public UnitRepository(PosDbContext context)
        {
            this.context = context;
        }

        //public List<UnitViewModel> test()
        //{
        //    using (var transaction = context.Database.BeginTransaction())
        //    {
        //try
        //{
        //    //context.Units.Add(new Unit { UnitName = "ea" });
        //    var unit = context.Units.Single(x => x.UId == 2);
        //    ////unit.UnitName += "1";
        //    ////context.Units.Remove(unit);
        //    context.SaveChanges();
        //    transaction.Commit();
        //}
        //catch(Exception ex)
        //{
        //    transaction.Rollback();
        //}


        //}
        //var units = context.Units
        //    .Where(x => x.UId > 3)
        //    .Select( x => new UnitViewModel
        //    { 
        //        Uid = x.UId ,
        //        UnitName = x.UnitName ,
        //    })
        //    .ToList();
        //    var units = (from u in context.Units
        //                 where u.UId > 3
        //orderby u.uid
        //                 select new UnitViewModel
        //                 {
        //                     Uid = u.UId,
        //                     UnitName = u.UnitName
        //                 }).ToList();


        //    return units;
        //}
        public ApiResultViewModel<List<UnitViewModel>> GetUnits()
        {
            var units = (from u in context.Units
                         select new UnitViewModel
                         {
                             Uid = u.UId,
                             UnitName = u.UnitName
                         })
                         .OrderBy(x => x.Uid)
                         .ToList();
            var result = new ApiResultViewModel<List<UnitViewModel>> {
                StatusCode = 0,
                Message = "successful" ,
                Data = units
            };
            return result;
        }

        public ApiResultViewModel<UnitViewModel> GetUnitById(int id)
        {
            var unit = context.Units.Single(item => item.UId == id);
            var reult = new ApiResultViewModel<UnitViewModel> { 
                StatusCode = 0,
                Message = "successful",
                Data = new UnitViewModel {
                    Uid = unit.UId,
                    UnitName = unit.UnitName 
                } 
            };
            return reult;
        }

        public ApiResultViewModel Update(EditUnitViewModel editUnit)
        {
            if (editUnit.UnitName == "")
            {
                return new ApiResultViewModel { StatusCode = 1, Message = " Cannot be null" };
            }
            var result = new ApiResultViewModel();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var unit = context.Units.Single(x => x.UId == editUnit.Uid);

                    if (unit.UnitName != editUnit.UnitName)
                    {
                        var duplicatename = context.Units.Any(u => u.UnitName == editUnit.UnitName);
                        if (duplicatename)
                        {
                            result = new ApiResultViewModel { StatusCode = 1, Message = " duplicated name" };
                            return result;
                        }
                    }

                    unit.UnitName = editUnit.UnitName;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                result = new ApiResultViewModel { StatusCode = 0, Message = editUnit.UnitName + " successful" };
                return result;
            }
        }

        public ApiResultViewModel Add(AddUnitViewModel param)
        {
            if(param.UnitName == "")
            {
                return new ApiResultViewModel { StatusCode = 0, Message = " Cannot be null" };
            }
            var result = new ApiResultViewModel();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var duplicatename = context.Units.Any(u => u.UnitName == param.UnitName);

                    if (duplicatename)
                    {
                        result = new ApiResultViewModel { StatusCode = 1, Message = " duplicated name" };
                        return result;
                    }
                    context.Units.Add(new Unit { UnitName = param.UnitName });
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                result = new ApiResultViewModel { StatusCode = 0, Message = param.UnitName + "successful" };
                return result;
            }
        }

        public ApiResultViewModel RemoveById(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var alreadyUse = context.Products.Any(item => item.UnitId == id);
                    if (alreadyUse)
                    {
                        return new ApiResultViewModel { StatusCode = 1, Message = "cant delete unit already in use by product" };
                    }
                    var unit = context.Units.Single(item => item.UId == id);
                    context.Units.Remove(unit);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                return new ApiResultViewModel { StatusCode = 0, Message = "Successful" };
            }
        }
    }
}
