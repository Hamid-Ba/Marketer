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

    public class AdminUserAuthVM
    {
        public long Id { get; set; }
        public long AdminRoleId { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }
        public bool KeepMe { get; set; }

        public AdminUserAuthVM() { }

        public AdminUserAuthVM(long id, long adminRoleId, string fullname, string mobile, bool keepMe)
        {
            Id = id;
            AdminRoleId = adminRoleId;
            Fullname = fullname;
            Mobile = mobile;
            KeepMe = keepMe;
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