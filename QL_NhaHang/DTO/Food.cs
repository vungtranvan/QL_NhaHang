using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NhaHang.DTO
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCategory { get; set; }
        public string NameCategory { get; set; }
        public float Price { get; set; }
        public string Unit { get; set; }
        public byte[] ImageFood { get; set; }
        public bool Status { get; set; }

        public Food() { }

        public Food(int id, string name, int idCategory, string nameCategory, float price, string unit, byte[] imageFood,bool status)
        {
            Id = id;
            Name = name;
            IdCategory = idCategory;
            NameCategory = nameCategory;
            Price = price;
            Unit = unit;
            ImageFood = imageFood;
            Status = status;
        }
        public Food(DataRow row)
        {
            Id = (int)row["Id"];
            Name = row["Name"].ToString();
            IdCategory = (int)row["IdCategory"];
            NameCategory = row["NameCategory"].ToString();
            Price = (float)Convert.ToDouble(row["Price"].ToString());
            Unit = row["Unit"].ToString();
            if (row["ImageFood"].ToString() != "")
                ImageFood = (byte[])row["ImageFood"];
            Status = Convert.ToBoolean(row["Status"].ToString());
        }
    }
}
