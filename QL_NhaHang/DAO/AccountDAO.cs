using QL_NhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NhaHang.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance { get => instance == null ? instance = new AccountDAO() : instance; set => instance = value; }

        private AccountDAO() { }

        public bool CheckLogin(string userName, string password)
        {
            string querry = "EXEC LoginApp @UserName , @Password";
            DataTable data = DataProvider.Instance.ExcuteQuery(querry, new object[] { userName, password });
            return data.Rows.Count > 0;
        }

        public Account GetAccountByUserName(string userName)
        {
            string querry = "EXEC GetAccountByUserName @UserName";
            DataTable data = DataProvider.Instance.ExcuteQuery(querry, new object[] { userName });

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        public DataTable GetListAccount(string key)
        {
            string querry = "EXEC GetListAccount @Key";
            return DataProvider.Instance.ExcuteQuery(querry, new object[] { key });
        }

        public bool InsertAccount(Account acc)
        {
            int result = DataProvider.Instance.ExcuteNonQuerry("EXEC InsertAccount @DisplayName , @UserName , @Password , @Phone , @Address , @Sex , @Type", new object[] { acc.DisplayName, acc.UserName, acc.Password, acc.Phone, acc.Address, acc.Sex, acc.Type });
            return result > 0;
        }

        public bool UpdateAccount(Account acc)
        {
            int result = DataProvider.Instance.ExcuteNonQuerry("EXEC UpdateAccount @DisplayName , @UserName , @Password , @Phone , @Address , @Sex , @Type", new object[] { acc.DisplayName, acc.UserName, acc.Password, acc.Phone, acc.Address, acc.Sex, acc.Type });
            return result > 0;
        }

        public bool DeleteAccount(string userName)
        {
            int result = DataProvider.Instance.ExcuteNonQuerry("EXEC DeleteAccount @UserName", new object[] { userName });
            return result > 0;
        }

        public bool UpdatePasswordAccount(string userName, string passwordNew)
        {
            int result = DataProvider.Instance.ExcuteNonQuerry("EXEC UpdatePasswordAccount @UserName , @PasswordNew", new object[] { userName, passwordNew });
            return result > 0;
        }
    }
}
