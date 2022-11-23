using System;
using System.Collections.Generic;

namespace backend_cn.Models
{
    public partial class Receipt
    {
        public Receipt()
        {
            ReceiptDetails = new HashSet<ReceiptDetail>();
        }

        public int ReceiptId { get; set; }
        public string ReceiptCode { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public decimal FullTotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TradeDiscount { get; set; }
        public decimal GrandTotal { get; set; }

        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
