using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NhaHang.DTO
{
    public class Account
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool Sex { get; set; }
        public int Type { get; set; }

        public Account()
        {
        }

        public Account(string displayName, string userName, string password, string phone, string address, bool sex, int type)
        {
            DisplayName = displayName;
            UserName = userName;
            Password = password;
            Phone = phone;
            Address = address;
            Sex = sex;
            Type = type;
        }

        public Account(DataRow row)
        {
            DisplayName = row["DisplayName"].ToString();
            UserName = row["UserName"].ToString();
            Password = row["Password"].ToString();
            Phone = row["Phone"].ToString();
            Address = row["Address"].ToString();
            Sex = (bool)row["Sex"];
            Type = (int)row["Type"];
        }
    }
}
