using Microsoft.AspNetCore.Mvc;
using backend_cn.ViewModels;
using backend_cn.BusinessLogic.Unit;

namespace backend_cn.Controllers
{
    [Route("[controller]/[action]")]
    public class UnitController : ControllerBase
    {
        readonly UnitManage unitManage;
        readonly UnitDetail unitDetail;
        public UnitController(UnitManage unitManage, UnitDetail unitDetail)
        {
            this.unitManage = unitManage;
            this.unitDetail = unitDetail;
        }
        
        [HttpPost]
        public IActionResult AddUnit([FromBody]AddUnitViewModel param)
        {
            var units = unitManage.Add(param);
            return new JsonResult(units);
        }

        [HttpGet]
        public IActionResult GetUnitById(int id)
        {
            var units = unitDetail.GetUnitById(id);
            return new JsonResult(units);
        }

        [HttpDelete]
        [Route("{id}")]//http://localhost:10001/Unit/RemoveUnitById/10
        public IActionResult RemoveUnitById([FromRoute]int id)
        {
            var units = unitManage.RemoveById(id);
            return new JsonResult(units);
        }

        [HttpPost]
        public IActionResult UpdateUnit([FromBody] EditUnitViewModel unit)
        {
            var units = unitManage.Update(unit);
            return new JsonResult(units);
        }

        [HttpGet]
        public IActionResult GetUnits()
        {
            var units = unitDetail.GetUnits();
            return new JsonResult(units);
        }
    }
}
