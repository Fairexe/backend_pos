namespace backend_cn.ViewModels
{
    public class UnitViewModel
    {
        public int Uid { get; set; }
        public string UnitName { get; set; }
    }

    public class AddUnitViewModel
    {
        public string UnitName { get; set; }
    }

    public class EditUnitViewModel
    {
        public int Uid { get; set; }
        public string UnitName { get; set; }
    }
}
