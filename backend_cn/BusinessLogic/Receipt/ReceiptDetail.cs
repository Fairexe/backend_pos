using backend_cn.APIResult;
using backend_cn.Constants;
using backend_cn.Models;
using backend_cn.Repositories.receipt;
using backend_cn.ViewModels;

namespace backend_cn.BusinessLogic.receipt
{
    public class ReceiptDetail : IAPIResult
    {
        readonly IReceiptRepository receiptRepository;
        public ReceiptDetail(IReceiptRepository receiptRepository)
        {
            this.receiptRepository = receiptRepository;
        }

        public ApiResultViewModel<List<Models.Receipt>> GetReceipts()
        {
            var receipts = receiptRepository.GetReceipts();
            return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL, receipts);
        }

        public ApiResultViewModel<List<Models.Receipt>> GetReceiptByDate(ReceiptDateViewModel date)
        {
            var receipts = receiptRepository.GetReceiptByDate(date);
            return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL, receipts);
        }

        public ApiResultViewModel<Models.Receipt> GetReceiptById(int id)
        {
            var receipt = receiptRepository.GetReceiptById(id);
            return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL, receipt);
        }

        public ApiResultViewModel<List<Models.ReceiptDetail>> GetReceiptDetailById(int id)
        {
            var details = receiptRepository.GetReceiptDetailById(id);
            return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL, details);
        }
    }
}
