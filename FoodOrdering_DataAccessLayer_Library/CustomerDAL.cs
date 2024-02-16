using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering_DataAccessLayer_Library
{
    public class CustomerDAL
    {
        public bool IsLoginCredentialsValid(Customer cust)
        {
            bool OperationStatus = false;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE CustomerEmail = @CustomerEmail AND Password = @Password", connection);
            try
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@CustomerEmail", cust.CustomerEmail);
                command.Parameters.AddWithValue("@Password", cust.Password);
                //command.Parameters.Add("@CustomerEmail",SqlDbType.VarChar, 50).Value = cust.CustomerEmail;
                //command.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = cust.Password;
                connection.Open();
                int countOfOutput = Convert.ToInt32(command.ExecuteScalar());
                if(countOfOutput > 0)
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

        public bool RegisterNewUser(Customer cust)
        {
            bool OperationStatus = false;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO Customers (CustomerEmail,Password) VALUES (@CustomerEmail,@Password)", connection);
            try
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@CustomerEmail", cust.CustomerEmail);
                command.Parameters.AddWithValue("@Password", cust.Password);
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

    }
}
