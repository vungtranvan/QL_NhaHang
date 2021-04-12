using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NhaHang.DTO
{
    public class BillInfo
    {
        public int Id { get; set; }
        public int IdBill { get; set; }
        public int IdFood { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }

        public BillInfo()
        {
        }

        public BillInfo(int id, int idBill, int idFood, int count, float price)
        {
            Id = id;
            IdBill = idBill;
            IdFood = idFood;
            Count = count;
            Price = price;
        }
        public BillInfo(DataRow row)
        {
            Id = (int)row["Id"];
            IdBill = (int)row["IdBill"];
            IdFood = (int)row["IdFood"];
            Count = (int)row["Count"];
            Price = (float)Convert.ToDouble(row["Price"].ToString());
        }

    }
}
