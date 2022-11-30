using backend_cn.Repositories;
using Microsoft.AspNetCore.Mvc;
using backend_cn.ViewModels;

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
        
        [HttpPost]
        public IActionResult AddUnit([FromBody]AddUnitViewModel param)
        {
            var units = unitRepository.Add(param);
            return new JsonResult(units);
        }

        [HttpGet]
        public IActionResult GetUnitById(int id)
        {
            var units = unitRepository.GetUnitById(id);
            return new JsonResult(units);
        }

        [HttpDelete]
        [Route("{id}")]//http://localhost:10001/Unit/RemoveUnitById/10
        public IActionResult RemoveUnitById([FromRoute]int id)
        {
            var units = unitRepository.RemoveById(id);
            return new JsonResult(units);
        }

        [HttpPost]
        public IActionResult UpdateUnit([FromBody] EditUnitViewModel unit)
        {
            var units = unitRepository.Update(unit);
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
