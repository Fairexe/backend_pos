using System;
using System.Collections.Generic;

namespace backend_cn.Models
{
    public partial class Product
    {
        public Product()
        {
            ReceiptDetails = new HashSet<ReceiptDetail>();
        }

        public int ProductId { get; set; }
        public string ProductCode { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int UnitId { get; set; }

        public virtual Unit Unit { get; set; } = null!;
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
