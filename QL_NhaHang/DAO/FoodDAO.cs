using QL_NhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NhaHang.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance { get => instance == null ? instance = new FoodDAO() : instance; private set => instance = value; }

        private FoodDAO() { }

        public List<Food> GetFoodByCategoryId(int cateId)
        {
            List<Food> lstFood = new List<Food>();
            DataTable data = DataProvider.Instance.ExcuteQuery("EXEC GetFoodByCategoryId @IdCategory", new object[] { cateId });
            foreach (DataRow item in data.Rows)
            {
                lstFood.Add(new Food(item));
            }
            return lstFood;
        }
        public Food GetFoodById(int id)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("EXEC GetFoodById @Id", new object[] { id });
            foreach (DataRow item in data.Rows)
            {
                return new Food(item);
            }
            return null;
        }

        public DataTable GetListFood(string name)
        {
            return DataProvider.Instance.ExcuteQuery("EXEC GetListFood @Name", new object[] { name });
        }
        public DataTable GetListFood(int idFoodCate)
        {
            return DataProvider.Instance.ExcuteQuery("EXEC GetFoodByIdFoodCategory @IdFoodCategory", new object[] { idFoodCate });
        }

        public bool InsertFood(string name, int idCate, float price, string unit, byte[] imagefood, bool status)
        {
            int result = DataProvider.Instance.ExcuteNonQuerry("EXEC InsertFood @Name , @IdCategory , @Price , @Unit , @ImageFood , @Status", new object[] { name, idCate, price, unit, imagefood , status });

            return result > 0;
        }

        public bool UpdateFood(int idFood, string name, int idCate, float price, string unit, byte[] imagefood, bool status)
        {
            int result = DataProvider.Instance.ExcuteNonQuerry("EXEC UpdateFood @Id , @Name , @IdCategory , @Price , @Unit , @ImageFood , @Status", new object[] { idFood, name, idCate, price, unit, imagefood , status});

            return result > 0;
        }

        public bool DeleteFood(int idFood)
        {
            int result = DataProvider.Instance.ExcuteNonQuerry("EXEC DeleteFood @IdFood", new object[] { idFood });

            return result > 0;
        }
    }
}
