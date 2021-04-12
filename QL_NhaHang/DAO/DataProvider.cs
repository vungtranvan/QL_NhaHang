using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NhaHang.DAO
{
    public class DataProvider
    {
        private const string ConnectionString = "server=localhost;database=QL_Nhahang;uid=sa;pwd=1234$";

        private static DataProvider instance;

        public static DataProvider Instance { get => instance == null ? instance = new DataProvider() : instance; set => instance = value; }

        private DataProvider() { }

        public DataTable ExcuteQuery(string querry, object[] parameter = null)
        {
            DataTable data = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(querry, conn);

                    if (parameter != null)
                    {
                        string[] lstPara = querry.Split(' ');
                        int i = 0;
                        foreach (var item in lstPara)
                        {
                            if (item.Contains('@'))
                            {
                                cmd.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(data);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} => {1}", querry, ex));
            }

            return data;
        }

        public int ExcuteNonQuerry(string querry, object[] parameter = null)
        {
            int data = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(querry, conn);

                    if (parameter != null)
                    {
                        string[] lstPara = querry.Split(' ');
                        int i = 0;

                        foreach (var item in lstPara)
                        {
                            if (item.Contains('@'))
                            {
                                cmd.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }

                    data = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} => {1}", querry, ex));
            }

            return data;
        }

        public object ExecuteScalar(string querry, object[] parameter = null)
        {
            object data = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(querry, conn);

                    if (parameter != null)
                    {
                        string[] lstPara = querry.Split(' ');
                        int i = 0;
                        foreach (var item in lstPara)
                        {
                            if (item.Contains('@'))
                            {
                                cmd.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }
                    data = cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(String.Format("{0} => {1}", querry, ex));
            }
            return data;
        }

    }
}
