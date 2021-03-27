using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplicationProductModule.Models
{
    public class ProductDb
    {
        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<Product> ListAll()
        {
            List<Product> list = new List<Product>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("GetProducts", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    list.Add(new Product
                    {
                        ProductId = Convert.ToInt32(rdr["Id"]),
                        ProductName = rdr["ProductName"].ToString(),
                        ProductDescription = rdr["ProductDescription"].ToString(),
                    });
                }

                return list;
            }
        }

        public int Add(Product product)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("AddNewProduct", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ProductName", product.ProductName);
                com.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);

                i = com.ExecuteNonQuery();
                con.Close();
            }

            return i;
        }

        public int Update(Product product)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("UpdateProduct", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", product.ProductId);
                com.Parameters.AddWithValue("@ProductName", product.ProductName);
                com.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);

                i = com.ExecuteNonQuery();
            }

            return i;
        }

        public List<Product> GetListByName(string str)
        {
            List<Product> list = new List<Product>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SearchProduct", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@search",Convert.ToString(str));
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    list.Add(new Product
                    {
                        ProductId = Convert.ToInt32(rdr["Id"]),
                        ProductName = rdr["ProductName"].ToString(),
                        ProductDescription = rdr["ProductDescription"].ToString(),
                    });
                }

                return list;
            }
        }
    }
}