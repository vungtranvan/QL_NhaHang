using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NhaHang.DTO
{
    public class BillDetail
    {
        public int Id { get; set; }
        public DateTime DateCheckIn { get; set; }
        public DateTime DateCheckOut { get; set; }
        public string TableName { get; set; }
        public int QuantityTable { get; set; }
        public int Discount { get; set; }
        public float FinalToTalPrice { get; set; }
        public string FoodName { get; set; }
        public string Unit { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }
        public float ToTalPrice { get; set; }

        public BillDetail() {  }

        public BillDetail(int id, DateTime dateCheckIn, DateTime dateCheckOut, string tableName, int quantityTable, int discount, float finalToTalPrice, string fooodName, string unit, int count, float price, float toTalPrice)
        {
            Id = id;
            DateCheckIn = dateCheckIn;
            DateCheckOut = dateCheckOut;
            TableName = tableName;
            QuantityTable = quantityTable;
            Discount = discount;
            FinalToTalPrice = finalToTalPrice;
            FoodName = fooodName;
            Unit = unit;
            Count = count;
            Price = price;
            ToTalPrice = toTalPrice;
        }
        public BillDetail(DataRow row)
        {
            Id = (int)row["Id"];
            DateCheckIn = (DateTime)row["DateCheckIn"];
            DateCheckOut = (DateTime)row["DateCheckOut"]; 
            TableName = row["TableName"].ToString();
            QuantityTable = (int)row["QuantityTable"];
            if (row["Discount"].ToString() != "")
                Discount = (int)row["Discount"];
            FinalToTalPrice = (float)Convert.ToDouble(row["FinalToTalPrice"].ToString());
            FoodName = row["FoodName"].ToString();
            Unit = row["Unit"].ToString();
            Count = (int)row["Count"];
            Price = (float)Convert.ToDouble(row["Price"].ToString());
            ToTalPrice = (float)Convert.ToDouble(row["ToTalPrice"].ToString());
        }
    }
}
