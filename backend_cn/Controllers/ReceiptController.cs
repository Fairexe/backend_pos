using backend_cn.BusinessLogic.receipt;
using backend_cn.BusinessLogic.Receipt;
using backend_cn.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend_cn.Controllers
{
    [Route("[controller]/[action]")]
    public class ReceiptController : ControllerBase
    {
        readonly ReceiptManage receiptManage;
        readonly ReceiptDetail receiptDetail;

        public ReceiptController(ReceiptManage receiptManage, ReceiptDetail receiptDetail)
        {
            this.receiptManage =  receiptManage;
            this.receiptDetail = receiptDetail;
        }

        [HttpPost]
        public IActionResult AddReceipt([FromBody] ReceiptViewModel receipt)
        {
            var result = receiptManage.Add(receipt);
            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult GetReceipts()
        {
            var result = receiptDetail.GetReceipts();
            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult GetReceiptById(int id)
        {
            var result = receiptDetail.GetReceiptById(id);
            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult GetReceiptByDate(ReceiptDateViewModel date)
        {
            Console.WriteLine(date);
            var result = receiptDetail.GetReceiptByDate(date);
            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult GetReceiptDetailById(int id)
        {
            var result = receiptDetail.GetReceiptDetailById(id);
            return new JsonResult(result);
        }
    }
}
