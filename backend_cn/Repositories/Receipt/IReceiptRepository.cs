using backend_cn.APIResult;
using backend_cn.Models;
using backend_cn.ViewModels;

namespace backend_cn.Repositories.receipt
{
    public interface IReceiptRepository
    {
        List<Receipt> GetReceipts();
        List<Receipt> GetReceiptByDate(ReceiptDateViewModel date);
        Receipt GetReceiptById(int id);
        List<ReceiptDetail> GetReceiptDetailById(int id);
        void Add(ReceiptViewModel receipt);
        string GenerateReceiptCode();
    }
}
