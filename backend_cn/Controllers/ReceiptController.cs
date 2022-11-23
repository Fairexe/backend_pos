using backend_cn.Models;
using backend_cn.Repositories;
using backend_cn.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend_cn.Controllers
{
    [Route("[controller]/[action]")]
    public class ReceiptController : ControllerBase
    {
        readonly ReceiptRepository receiptRepository;

        public ReceiptController(ReceiptRepository receiptRepository)
        {
            this.receiptRepository = receiptRepository;
        }

        [HttpPost]
        public IActionResult AddReceipt([FromBody] ReceiptViewModel receipt)
        {
            var result = receiptRepository.Add(receipt);
            return new JsonResult(receipt.ReceiptDetail);
        }
    }
}
