using backend_cn.APIResult;
using backend_cn.Context;
using backend_cn.Models;
using backend_cn.ViewModels;

namespace backend_cn.Repositories.unit
{
    public class UnitRepositoryMySql : IUnitRepository
    {
        readonly PosDbContext context;

        public UnitRepositoryMySql(PosDbContext context)
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

        public UnitViewModel GetUnitById(int id)
        {
            var unit = context.Units.Single(item => item.UId == id);
            return new UnitViewModel
            {
                Uid = unit.UId,
                UnitName = unit.UnitName
            };
        }

        public void Update(EditUnitViewModel editUnit)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var unit = context.Units.Single(x => x.UId == editUnit.Uid);
                    unit.UnitName = editUnit.UnitName;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool CheckDuplicateName(string name)
        {
            var duplicatename = context.Units.Any(u => u.UnitName == name);

            if (duplicatename)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckUnitIsUsed(int id)
        {
            var alreadyUse = context.Products.Any(item => item.UnitId == id);
            if (alreadyUse)
            {
                return true;
            }
            return false;
        }

        public void Add(AddUnitViewModel unit)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Units.Add(new Unit { UnitName = unit.UnitName });
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.ToString());
                }
            }
        }

        public void RemoveById(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var unit = context.Units.Single(item => item.UId == id);
                    context.Units.Remove(unit);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
