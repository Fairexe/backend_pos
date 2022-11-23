using backend_cn.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend_cn.Controllers
{
    [Route("[controller]/[action]")]
    public class UnitController : ControllerBase
    {
        readonly UnitRepository unitRepository;
        
        public UnitController(UnitRepository unitRepository)
        {
            this.unitRepository = unitRepository;
        }

        //[HttpGet]
        //public IActionResult test()
        //{
        //    var units = unitRepository.test();
        //    return new JsonResult(units);
        //}
        
        [HttpPost]
        public IActionResult AddUnit(string name)
        {
            var units = unitRepository.Add(name);
            return new JsonResult(units);
        }

        [HttpPost]
        public IActionResult GetUnitById(int id)
        {
            var units = unitRepository.GetUnitById(id);
            return new JsonResult(units);
        }

        [HttpDelete]
        public IActionResult RemoveUnitById(int id)
        {
            var units = unitRepository.RemoveById(id);
            return new JsonResult(units);
        }

        [HttpPost]
        public IActionResult UpdateUnit(int id,string name)
        {
            var units = unitRepository.Update(id,name);
            return new JsonResult(units);
        }

        [HttpGet]
        public IActionResult GetUnits()
        {
            var units = unitRepository.GetUnits();
            return new JsonResult(units);
        }
    }
}
