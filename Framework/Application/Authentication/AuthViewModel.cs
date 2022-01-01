namespace Framework.Application.Authentication
{
    public class VisitorAuthViewModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }

        public VisitorAuthViewModel()
        {
        }

        public VisitorAuthViewModel(long id,string code, string fullname, string mobile)
        {
            Id = id;
            Code = code;
            Fullname = fullname;
            Mobile = mobile;
        }
    }

    public class OperatorAuthViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }

        public OperatorAuthViewModel() { }

        public OperatorAuthViewModel(long id, long roleId, string fullname, string mobile)
        {
            Id = id;
            RoleId = roleId;
            Fullname = fullname;
            Mobile = mobile;
        }
    }

    public class StoreUserAuthVM
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
        public string StoreCode { get; set; }
        public long RoleId { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public bool KeepMe { get; set; }

        public StoreUserAuthVM(long id, long storeId, string storeCode,long roleId, string fullname, string mobile, string city, string province, string address, bool keepMe)
        {
            Id = id;
            StoreId = storeId;
            StoreCode = storeCode;
            RoleId = roleId;
            Fullname = fullname;
            Mobile = mobile;
            City = city;
            Province = province;
            Address = address;
            KeepMe = keepMe;
        }
    }
}