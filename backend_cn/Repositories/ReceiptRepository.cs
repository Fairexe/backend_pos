using backend_cn.Context;
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

        public ApiResultViewModel<List<Receipt>> GetReceipts()
        {
            using (var transaction = context.Database.BeginTransaction())
            {

                var receipts = context.Receipts.ToList();
                return new ApiResultViewModel<List<Receipt>> { StatusCode = 0, Message = "Successful", Data = receipts };
            }
        }

        public ApiResultViewModel<List<Receipt>> GetReceiptByDate(ReceiptDateViewModel date)
        {
            var receipts = (from d in context.Receipts
                            where (date.StartDate == null || d.CreateDate.Date >= date.StartDate)
                            && (date.EndDate == null || d.CreateDate.Date <= date.EndDate)
                            select d).ToList();
            return new ApiResultViewModel<List<Receipt>> { StatusCode = 0, Message = "Successful", Data = receipts };
        }

        public ApiResultViewModel<Receipt> GetReceiptById(int id)
        {
            var details = context.Receipts.Single(x => x.ReceiptId == id);
            return new ApiResultViewModel<Receipt> { StatusCode = 0, Message = "Successful", Data = details };
        }

        public ApiResultViewModel<List<ReceiptDetail>> GetReceiptDetailById(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                var details = context.ReceiptDetails.Where(x => x.ReceiptId == id).ToList();
                return new ApiResultViewModel<List<ReceiptDetail>> { StatusCode = 0, Message = "Successful", Data = details };
            }
        }

        public ApiResultViewModel Add(ReceiptViewModel receipt)
        {
            Receipt newReceipt = new Receipt();
            var result = new ApiResultViewModel();
            string code = "T";
            int zeroQuantity = (from r in receipt.ReceiptDetail 
                                where r.Amount == 0
                                select r).Count();
            if (receipt.ReceiptDetail.Count() == 0)
            {
                return new ApiResultViewModel { StatusCode = 0, Message = " Cart is empty" };
            }
            if(zeroQuantity > 0)
            {
                return new ApiResultViewModel { StatusCode = 1, Message = " Quantity is empty" };
            }
            
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var lastReceipt = context.Receipts.OrderByDescending(x => x.ReceiptCode).Select(x => x.ReceiptCode).FirstOrDefault();
                    string codeSubstring = "";
                    if(lastReceipt == null)
                    {
                        codeSubstring = "0";
                    }
                    else
                    {
                        codeSubstring = lastReceipt.Substring(1);
                    }
                    int onlyCode = int.Parse(codeSubstring);
                    onlyCode += 1;
                    string newCode = onlyCode.ToString();
                    string newString = newCode.PadLeft(4, '0');
                    code += newString;
                    newReceipt = new Receipt { ReceiptCode = code, CreateDate = DateTime.Now, FullTotal = receipt.FullTotal, DiscountTotal = receipt.DiscountTotal, SubTotal = receipt.SubTotal, TradeDiscount = receipt.TradeDiscount, GrandTotal = receipt.GrandTotal };
                    context.Receipts.Add(newReceipt);
                    context.SaveChanges();
                    for (int i = 0; i < receipt.ReceiptDetail.Length; i++)
                    {
                        context.ReceiptDetails.Add(new ReceiptDetail
                        {
                            Amount = receipt.ReceiptDetail[i].Amount,
                            DiscountPercent = receipt.ReceiptDetail[i].DiscountPercent,
                            DiscountTotal = receipt.ReceiptDetail[i].DiscountTotal,
                            Total = receipt.ReceiptDetail[i].Total,
                            ReceiptId = newReceipt.ReceiptId,
                            ProductId = receipt.ReceiptDetail[i].ProductId
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                    result = new ApiResultViewModel { StatusCode = 0, Message = "successful" };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result = new ApiResultViewModel { StatusCode = 1, Message = ex.Message };
                }
                return result;
            }
        }
    }
}
