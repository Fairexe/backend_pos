using backend_cn.APIResult;
using backend_cn.Constants;
using backend_cn.Repositories.receipt;
using backend_cn.ViewModels;

namespace backend_cn.BusinessLogic.Receipt
{
    public class ReceiptManage : IAPIResult
    {
        readonly IReceiptRepository receiptRepository;
        public ReceiptManage(IReceiptRepository receiptRepository)
        {
            this.receiptRepository = receiptRepository;
        }

        public ApiResultViewModel Add(ReceiptViewModel newReceipt)
        {           
            try
            {
                int zeroQuantity = (from r in newReceipt.ReceiptDetail
                                    where r.Amount == 0
                                    select r).Count();
                if (newReceipt.ReceiptDetail.Count() == 0)
                {
                    throw new Exception("Cart is empty");
                }
                if (zeroQuantity > 0)
                {
                    throw new Exception("Quantity is empty");
                }
                receiptRepository.Add(newReceipt);
                return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL);
            }
            catch (Exception ex)
            {
                return this.ApiResultViewModel(APICode.Success, ex.ToString());
            }

        }
    }
}
