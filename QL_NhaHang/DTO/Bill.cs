using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NhaHang.DTO
{
    public class Bill
    {
        public int Id { get; set; }
        public DateTime DateCheckIn { get; set; }
        public DateTime DateCheckOut { get; set; }
        public string TableName { get; set; }
        public int QuantityTable { get; set; }
        public int Discount { get; set; }
        public float TotalPrice { get; set; }
        public int Status { get; set; }

        public Bill() { }

        public Bill(int id, DateTime dateCheckIn, DateTime dateCheckOut, string tableName, int quantityTable, int discount, float totalPrice, int status)
        {
            Id = id;
            DateCheckIn = dateCheckIn;
            DateCheckOut = dateCheckOut;
            TableName = tableName;
            QuantityTable = quantityTable;
            Discount = discount;
            TotalPrice = totalPrice;
            Status = status;
        }
        public Bill(DataRow row)
        {
            Id = (int)row["Id"];
            DateCheckIn = (DateTime)row["DateCheckIn"];

            var DateCheckOutTemp = row["DateCheckOut"];
            if (DateCheckOutTemp.ToString() != "") { DateCheckOut = (DateTime)DateCheckOutTemp; }
            TableName = row["TableName"].ToString();
            QuantityTable = (int)row["QuantityTable"];
            if (row["Discount"].ToString() != "")
                Discount = (int)row["Discount"];
            TotalPrice = (float)Convert.ToDouble(row["TotalPrice"].ToString());
            Status = (int)row["Status"];
        }
    }
}
