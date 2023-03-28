using backend_cn.APIResult;
using backend_cn.Context;
using backend_cn.Models;
using backend_cn.ViewModels;

namespace backend_cn.Repositories.receipt
{
    public class ReceiptRepositoryMySql : IReceiptRepository
    {
        readonly PosDbContext context;

        public ReceiptRepositoryMySql(PosDbContext context)
        {
            this.context = context;
        }

        public List<Receipt> GetReceipts()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                var receipts = context.Receipts.ToList();
                return receipts;
            }
        }

        public List<Receipt> GetReceiptByDate(ReceiptDateViewModel date)
        {
            var receipts = (from d in context.Receipts
                            where (date.StartDate == null || d.CreateDate.Date >= date.StartDate)
                            && (date.EndDate == null || d.CreateDate.Date <= date.EndDate)
                            select d).ToList();
            return receipts;
        }

        public Receipt GetReceiptById(int id)
        {
            var details = context.Receipts.Single(x => x.ReceiptId == id);
            return details;
        }

        public List<ReceiptDetail> GetReceiptDetailById(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                var details = context.ReceiptDetails.Where(x => x.ReceiptId == id).ToList();
                return details;
            }
        }

        public string GenerateReceiptCode()
        {
            string code = "T";
            var lastReceipt = context.Receipts.OrderByDescending(x => x.ReceiptCode).Select(x => x.ReceiptCode).FirstOrDefault();
            string codeSubstring = "";
            if (lastReceipt == null)
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
            return code;
        }

        public void Add(ReceiptViewModel receipt)
        {
            string code = GenerateReceiptCode();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Receipt newReceipt = new Receipt
                    {
                        ReceiptCode = code,
                        CreateDate = DateTime.Now,
                        FullTotal = receipt.FullTotal,
                        DiscountTotal = receipt.DiscountTotal,
                        SubTotal = receipt.SubTotal,
                        TradeDiscount = receipt.TradeDiscount,
                        GrandTotal = receipt.GrandTotal
                    };
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
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
