using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BOL;
namespace DAL
{
    public static class AccountDAL
    {
        public static bool Validate(string username,string password)
        {
            bool status = false;

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user104\Downloads\DotNetEndModule_Preperation\EndModuleEcommApp1\ECommWebFE\App_Data\ECommDB.mdf;Integrated Security=True";
            conn.ConnectionString = connStr;
            string qry = "select * from Users where UserName=@uname and Password=@pwd";//ColName=@ParamName
            cmd.CommandText = qry;
            cmd.Parameters.Add(new SqlParameter("@uname",username));
            cmd.Parameters.Add(new SqlParameter("@pwd", password));
            cmd.Connection = conn;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    status = true;
                }
                reader.Close();
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }

            return status;
        }

        public static bool Insert(int id,string name, string city, string email,string password, string mob)
        {
            bool status = false;

            string qry = "insert into Customer values(@id,@name,@city,@email,@password,@mob)";
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user104\Downloads\DotNetEndModule_Preperation\EndModuleEcommApp1\ECommWebFE\App_Data\ECommDB.mdf;Integrated Security=True";
  
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(qry))
                {

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@name", name));
                    cmd.Parameters.Add(new SqlParameter("@city", city));
                    cmd.Parameters.Add(new SqlParameter("@email", email));
                    cmd.Parameters.Add(new SqlParameter("@password", password));
                    cmd.Parameters.Add(new SqlParameter("@mob", mob));
              
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    status = true;
                }
            }    
            return status;
        }
        public static bool update(int id,string password)
        {
            bool status = false;

            string query = "update Users set password=@password where id=@id";
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user104\Downloads\DotNetEndModule_Preperation\EndModuleEcommApp1\ECommWebFE\App_Data\ECommDB.mdf;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@password", password));
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    status = true;
                }
            }
            return status;
        }
        public static bool delete(int id)
        {
            bool status = false;

            string query = "delete from Users where id=@id";
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user104\Downloads\DotNetEndModule_Preperation\EndModuleEcommApp1\ECommWebFE\App_Data\ECommDB.mdf;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    //cmd.Parameters.Add(new SqlParameter("@password", password));
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    status = true;
                }
            }
            return status;
        }
        //public static bool display(int id)
        //{
        //    bool status = false;

        //    string query = "select name from customer where id=@id";
        //    string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user104\Downloads\DotNetEndModule_Preperation\EndModuleEcommApp1\ECommWebFE\App_Data\ECommDB.mdf;Integrated Security=True";

        //    using (SqlConnection conn = new SqlConnection(connStr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Parameters.Add(new SqlParameter("@id", id));
        //            //cmd.Parameters.Add(new SqlParameter("@password", password));
        //            conn.Open();
        //            cmd.Connection = conn;
        //            cmd.ExecuteReader();
        //            status = true;
        //        }
        //    }
        //    return status;
        //}
        public static List<customer> display()
        {
            List<customer> customers = new List<customer>();
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user104\Downloads\DotNetEndModule_Preperation\EndModuleEcommApp1\ECommWebFE\App_Data\ECommDB.mdf;Integrated Security=True";
            IDbConnection con = new SqlConnection(connStr);
            string query = "SELECT * FROM customer ";
            IDbCommand cmd = new SqlCommand(query, con as SqlConnection);
            try
            {
                con.Open();
                IDataReader reader = cmd.ExecuteReader();
                //Online data using streaming mechanism
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string name = reader["name"].ToString();
                    string city = reader["city"].ToString();
                    string email = reader["email"].ToString();
                    string password = reader["password"].ToString();
                    int mob = int.Parse(reader["mob"].ToString());
                    

                    customers.Add(new customer()
                    {
                        ID = id,
                        Name = name,
                        City = city,
                        Email = email,
                        Password = password,
                        
                    });

                }
                reader.Close();
            }

            catch (SqlException exp)
            {
                string message = exp.Message;
                

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return customers;

        }

    }
}
