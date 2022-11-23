using backend_cn.Context;
using backend_cn.Controllers;
using backend_cn.Models;
using backend_cn.ViewModels;

namespace backend_cn.Repositories
{
    public class ReceiptRepository
    {
        readonly PosDbContext context;

        public ReceiptRepository(PosDbContext context)
        {
            this.context = context;
        }

        public ApiResultViewModel Add(ReceiptViewModel receipt)
        {
            Receipt newReceipt = new Receipt();
            var result = new ApiResultViewModel();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    newReceipt = new Receipt { ReceiptCode = receipt.ReceiptCode, CreateDate = DateTime.Now, FullTotal = receipt.FullTotal, DiscountTotal = receipt.DiscountTotal, SubTotal = receipt.SubTotal, GrandTotal = receipt.GrandTotal };
                    context.Receipts.Add(newReceipt);
                    context.SaveChanges();
                    for(int i = 0; i < receipt.ReceiptDetail.Length; i++)
                    {
                        context.ReceiptDetails.Add(new ReceiptDetail { Amount = receipt.ReceiptDetail[i].Amount, DiscountPercent = receipt.ReceiptDetail[i].DiscountPercent, DiscountTotal = receipt.ReceiptDetail[i].DiscountTotal, Total = receipt.ReceiptDetail[i].Total, ReceiptId = newReceipt.ReceiptId, ProductId = receipt.ReceiptDetail[i].ProductId });
                        
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    transaction.Rollback();
                }
                result = new ApiResultViewModel { statusCode = 0, message = "successful" };
                return result;
            }
        }
    }
}
