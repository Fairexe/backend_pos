namespace backend_cn.ViewModels
{
    public class ReceiptViewModel
    {
        public string ReceiptCode { get; set; } = string.Empty;
        public decimal FullTotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public ReceiptDetailViewModel[] ReceiptDetail { get; set; } = new ReceiptDetailViewModel[0];
    }

    public class ReceiptDetailViewModel
    {
        public decimal Amount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal Total { get; set; }
        public int ProductId { get; set; }
    }
}
