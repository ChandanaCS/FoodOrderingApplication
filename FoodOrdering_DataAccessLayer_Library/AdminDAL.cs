using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering_DataAccessLayer_Library
{
    public class AdminDAL
    {
        public bool IsLoginCredentialsValidForAdmin(Admin admin)
        {
            bool OperationStatus = false;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Admin WHERE AdminEmail = @AdminEmail AND Password = @Password", connection);
            try
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@AdminEmail", admin.AdminEmail);
                command.Parameters.AddWithValue("@Password", admin.Password);
                connection.Open();
                int countOfOutput = Convert.ToInt32(command.ExecuteScalar());
                if (countOfOutput > 0)
                {
                    OperationStatus = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }
            return OperationStatus;
        }

        public bool AddNewAdmin(Admin admin)
        {
            bool OperationStatus = false;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO Admin (AdminEmail,Password) VALUES (@AdminEmail,@Password)", connection);
            try
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@AdminEmail", admin.AdminEmail);
                command.Parameters.AddWithValue("@Password", admin.Password);
                connection.Open();
                command.ExecuteNonQuery();
                OperationStatus = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }
            return OperationStatus;
        }

        public List<ProductList> GetAllProducts()
        {
            List<ProductList> productLists = new List<ProductList>();
            string str = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(str);
            SqlCommand command = new SqlCommand("select * from ProductList", connection);
            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                ProductList pl = new ProductList();
                pl.ProductID = Convert.ToInt32(dr["ProductID"]);
                pl.ProductName = dr["ProductName"].ToString();
                pl.Price = Convert.ToDecimal(dr["Price"]);
                productLists.Add(pl);
            }
            connection.Close();
            connection.Dispose();
            return productLists;
        }
        public bool AddNewProduct(ProductList productList)
        {
            bool OperationStatus = false;
            string str = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(str);
            SqlCommand command = new SqlCommand("[dbo].sp_InsertProduct", connection);
            try
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                
                command.Parameters.AddWithValue("@p_ProductName", productList.ProductName);
                command.Parameters.AddWithValue("@p_Price", productList.Price);
                connection.Open();
                command.ExecuteNonQuery();
                OperationStatus = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }
            return OperationStatus;
        }

        public bool EditProduct(ProductList productList, int productId)
        {
            bool OperationStatus = false;
            string str = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(str);
            SqlCommand command = new SqlCommand("[dbo].[sp_UpdateProduct]", connection);
            try
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_ProductID", productId);
                command.Parameters.AddWithValue("@p_ProductName", productList.ProductName);
                command.Parameters.AddWithValue("@p_Price", productList.Price);
                connection.Open();
                command.ExecuteNonQuery();
                OperationStatus = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }
            return OperationStatus;
        }

        public bool RemoveProduct(int productId)
        {
            bool OperationStatus = false;
            string str = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("Delete from ProductList where ProductID = " + productId, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            OperationStatus = true;
            connection.Close();
            connection.Dispose();
            return OperationStatus;
        }

        public ProductList FindProduct(int productId)
        {
            ProductList prodlist = new ProductList();
            string str = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("Select * from ProductList where ProductID = " + productId, connection);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                prodlist.ProductID = Convert.ToInt32(dr["ProductID"]);
                prodlist.ProductName = dr["ProductName"].ToString();
                prodlist.Price = Convert.ToDecimal(dr["Price"]);
            }
            connection.Close();
            connection.Dispose();
            return prodlist;
        }   
    }
}
