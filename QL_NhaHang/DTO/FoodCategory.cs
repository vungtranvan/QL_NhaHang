using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NhaHang.DTO
{
    public class FoodCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public FoodCategory()
        {
        }

        public FoodCategory(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public FoodCategory(DataRow row)
        {
            Id = (int)row["Id"];
            Name = row["Name"].ToString();
        }
    }
}
