namespace backend_cn.ViewModels
{
    public class ProductViewModel
    {
        public int Pid { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int UnitId { get; set; }
    }

    public class AddProductViewModel
    {
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public int UnitId { get; set; } = 0;
    }

    public class UpdateProductViewModel
    {
        public int Pid { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int UnitId { get; set; }
    }
}
