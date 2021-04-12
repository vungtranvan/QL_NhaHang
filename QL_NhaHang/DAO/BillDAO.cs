using QL_NhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NhaHang.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance { get => instance == null ? instance = new BillDAO() : instance; set => instance = value; }

        private BillDAO() { }

        public DataTable GetListBillByDate(DateTime checkin, DateTime checkout)
        {
            return DataProvider.Instance.ExcuteQuery("EXEC GetListBillByDate @DateCheckIn , @DateCheckOut", new object[] { checkin, checkout });
        }

        public DataTable GetListBillUnCheckOut()
        {
            return DataProvider.Instance.ExcuteQuery("EXEC GetListBillUnCheckOut");
        }

        public bool Delete(int id)
        {
            int row = DataProvider.Instance.ExcuteNonQuerry("EXEC DeleteBill @Id", new object[] { id });
            return row > 0;
        }
        public bool Insert(string tableName, int quantityTable)
        {
            int row = DataProvider.Instance.ExcuteNonQuerry("EXEC InsertBill @TableName , @QuantityTable", new object[] { tableName, quantityTable });
            return row > 0;
        }
        public bool Update(int id, string tableName, int quantityTable)
        {
            int row = DataProvider.Instance.ExcuteNonQuerry("EXEC UpdateBill @Id , @TableName , @QuantityTable", new object[] { id, tableName, quantityTable });
            return row > 0;
        }
        public int GetMaxBillId()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("EXEC GetMaxBillId");
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public bool CheckOut(int id, int discount, float totalprice)
        {
            int row = DataProvider.Instance.ExcuteNonQuerry("EXEC CheckOut @Id , @Discount , @TotalPrice", new object[] { id, discount, totalprice });
            return row > 0;
        }

        public DataTable GetBillDetailByIdBill(int idBill)
        {
            return DataProvider.Instance.ExcuteQuery("EXEC GetBillDetailByIdBill @IdBill", new object[] { idBill });
        }
    }
}
