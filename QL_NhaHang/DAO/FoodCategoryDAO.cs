using QL_NhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NhaHang.DAO
{
    public class FoodCategoryDAO
    {
        private static FoodCategoryDAO instance;

        public static FoodCategoryDAO Instance { get => instance == null ? instance = new FoodCategoryDAO() : instance; private set => instance = value; }

        private FoodCategoryDAO() { }

        public List<FoodCategory> GetCategory(string nameSeach)
        {
            List<FoodCategory> lstCate = new List<FoodCategory>();
            DataTable data = DataProvider.Instance.ExcuteQuery("EXEC GetFoodCategoryList @Name", new object[] { nameSeach });
            foreach (DataRow item in data.Rows)
            {
                lstCate.Add(new FoodCategory(item));
            }
            return lstCate;
        }

        public DataTable GetCategoryToDataTable(string name)
        {
            return DataProvider.Instance.ExcuteQuery("EXEC GetFoodCategoryList @Name", new object[] { name });
        }

        public FoodCategory GetCategoryById(int id)
        {
            FoodCategory cate = null;
            DataTable data = DataProvider.Instance.ExcuteQuery("EXEC GetFoodCategoryById @Id", new object[] { id });
            foreach (DataRow item in data.Rows)
            {
                cate = new FoodCategory(item);
                return cate;
            }
            return null;
        }

        public bool GetCategoryByName(string name)
        {
            string querry = "EXEC GetFoodCategoryByName @Name";
            DataTable data = DataProvider.Instance.ExcuteQuery(querry, new object[] { name });
            return data.Rows.Count > 0;
        }

        public bool InsertFoodCategory(string name)
        {
            int result = DataProvider.Instance.ExcuteNonQuerry("EXEC InsertFoodCategory @Name", new object[] { name});
            return result > 0;
        }

        public bool UpdateFoodCategory(int id, string name)
        {
            int result = DataProvider.Instance.ExcuteNonQuerry("EXEC UpdateFoodCategory @Id , @Name", new object[] { id, name});
            return result > 0;
        }

        public bool DeleteFoodCategory(int id)
        {
            int result = DataProvider.Instance.ExcuteNonQuerry("EXEC DeleteFoodCategory @Id", new object[] { id});
            return result > 0;
        }
    }
}
