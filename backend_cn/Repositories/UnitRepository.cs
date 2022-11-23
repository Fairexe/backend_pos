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
        public List<UnitViewModel> GetUnits()
        {
            using (var transaction = context.Database.BeginTransaction())
            {

                    var units = (from u in context.Units
                                 select new UnitViewModel
                                 {
                                     Uid = u.UId,
                                     UnitName = u.UnitName
                                 })
                                 .OrderBy(x => x.Uid)
                                 .ToList();

                return units;
            }
        }

        public UnitViewModel GetUnitById(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {

                var unit = context.Units.Single(item => item.UId == id);
                var reult = new UnitViewModel { Uid = unit.UId, UnitName = unit.UnitName };
                return reult;
            }
        }

        public ApiResultViewModel Update(int id, string name)
        {
            var result = new ApiResultViewModel();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var units = (from u in context.Units
                                 .Where(u => u.UnitName == name)
                                 select new UnitViewModel
                                 {
                                     Uid = u.UId,
                                     UnitName = u.UnitName
                                 })
                                 .ToList();
                    if (units.Count > 0)
                    {
                        result = new ApiResultViewModel { statusCode = 1, message = " duplicated name" };
                        return result;
                    }
                    var unit = context.Units.Single(x => x.UId == id);
                    unit.UnitName = name;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                result = new ApiResultViewModel { statusCode = 0, message = name + "successful" };
                return result;
            }
        }

        public ApiResultViewModel Add(string name)
        {
            var result = new ApiResultViewModel();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var units = (from u in context.Units
                                 .Where(u => u.UnitName == name)
                                 select new UnitViewModel
                                 {
                                     Uid = u.UId,
                                     UnitName = u.UnitName
                                 })
                                 .ToList();
                    if(units.Count > 0)
                    {
                        result = new ApiResultViewModel { statusCode = 1, message = " duplicated name" };
                        return result;
                    }
                    context.Units.Add(new Unit { UnitName = name });    
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                result = new ApiResultViewModel { statusCode = 0, message = name + "successful" };
                return result;
            }
        }

        public ApiResultViewModel RemoveById(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if(id == 0)
                    {
                        return new ApiResultViewModel { statusCode = 2, message = "cant delete unit already in use by product" };
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
                return new ApiResultViewModel { statusCode = 0, message = "Successful" };
            }
        }
    }
}
