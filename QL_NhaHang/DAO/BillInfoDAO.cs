using QL_NhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NhaHang.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance { get => instance == null ? instance = new BillInfoDAO() : instance; private set => instance = value; }

        private BillInfoDAO() { }

        public DataTable GetListBillInfoByIdBill(int idBill)
        {
            return DataProvider.Instance.ExcuteQuery("EXEC GetListBillInfoByIdBill @IdBill", new object[] { idBill });
        }
        public List<BillInfo> GetListBillInfoByIdBillToLst(int idBill)
        {
            List<BillInfo> data = new List<BillInfo>();
            DataTable dataTable = DataProvider.Instance.ExcuteQuery("EXEC GetListBillInfoByIdBill @IdBill", new object[] { idBill });
          
            foreach (DataRow item in dataTable.Rows)
            {
                data.Add(new BillInfo(item));
            }

            return data;
        }
        public bool Insert(int idBill, int idFood, int count)
        {
            int row = DataProvider.Instance.ExcuteNonQuerry("EXEC InsertBillInfo @IdBill , @IdFood , @Count", new object[] { idBill, idFood, count });
            return row > 0;
        }

        public BillInfo GetById(int id)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("EXEC GetBillInfoById @Id", new object[] { id });
            foreach (DataRow item in data.Rows)
            {
                return new BillInfo(item);
            }
            return null;
        }

        public int GetByIdFood(int idFood)
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("EXEC GetBillInfoByIdFood @IdFood",new object[] { idFood});
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
