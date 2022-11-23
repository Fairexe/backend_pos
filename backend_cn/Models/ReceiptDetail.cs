using System;
using System.Collections.Generic;

namespace backend_cn.Models
{
    public partial class ReceiptDetail
    {
        public int RdId { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal Total { get; set; }
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Receipt Receipt { get; set; } = null!;
    }
}
