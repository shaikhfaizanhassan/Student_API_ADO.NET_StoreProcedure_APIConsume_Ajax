using Student_API_ADO.NET_StoreProcedure_APIConsume_Ajax.Models;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace Student_API_ADO.NET_StoreProcedure_APIConsume_Ajax.DAL
{
    public class StudentDAL
    {
        private readonly string _connection;

        public StudentDAL(IConfiguration connection)
        {
            _connection = connection.GetConnectionString("Addcon"); 
        }


        public DataTable GetData(string SpName, NameValueCollection NV)
        {
            SqlConnection con = new SqlConnection();
            string dbTyper = "";

            try
            {
                con.ConnectionString = _connection;
                DataTable dt = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandText = SpName;
                cmd.CommandTimeout = 999999999;
                if (NV != null)
                {
                    //New code implemented for retrieving data
                    for (int i = 0; i < NV.Count; i++)
                    {
                        string[] arraySplit = NV.Keys[i].Split('-');

                        if (arraySplit.Length > 2)
                        {
                            dbTyper = "SqlDbType." + arraySplit[1].ToString() + "," + arraySplit[2].ToString();

                            if (NV[i].ToString() == "NULL")
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = NV[i].ToString();
                            }
                        }
                        else
                        {
                            dbTyper = "SqlDbType." + arraySplit[1].ToString();

                            if (NV[i].ToString() == "NULL")
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = NV[i].ToString();
                            }
                        }
                    }
                }
                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = cmd;
                ad.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception)
            {

                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        public DataTable GetStudentByID(string SpName, NameValueCollection NV, int id)
        {
            SqlConnection con = new SqlConnection();
            string dbTyper = "";
            DataTable dt = new DataTable();

            try
            {
                con.ConnectionString = _connection;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandText = SpName;
                cmd.CommandTimeout = 999999999;

                if (NV != null)
                {
                    //New code implemented for retrieving data
                    for (int i = 0; i < NV.Count; i++)
                    {
                        string[] arraySplit = NV.Keys[i].Split('-');

                        if (arraySplit.Length > 2)
                        {
                            dbTyper = "SqlDbType." + arraySplit[1].ToString() + "," + arraySplit[2].ToString();

                            if (NV[i].ToString() == "NULL")
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = NV[i].ToString();
                            }
                        }
                        else
                        {
                            dbTyper = "SqlDbType." + arraySplit[1].ToString();

                            if (NV[i].ToString() == "NULL")
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = NV[i].ToString();
                            }
                        }
                    }
                }

                //Add paramter student id
                SqlParameter paraID = new SqlParameter("@stdid", SqlDbType.Int);
                paraID.Value = id;
                cmd.Parameters.Add(paraID);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);


            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dt;
        }


        public DataTable CreateStudent(string SpName, NameValueCollection NV, Student model)
        {
            SqlConnection con = new SqlConnection(_connection);
            DataTable dt = new DataTable();
            string dbTyper = "";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandText = SpName;
                cmd.CommandTimeout = 999999999;

                if (NV != null)
                {
                    //New code implemented for retrieving data
                    for (int i = 0; i < NV.Count; i++)
                    {
                        string[] arraySplit = NV.Keys[i].Split('-');

                        if (arraySplit.Length > 2)
                        {
                            dbTyper = "SqlDbType." + arraySplit[1].ToString() + "," + arraySplit[2].ToString();

                            if (NV[i].ToString() == "NULL")
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = NV[i].ToString();
                            }
                        }
                        else
                        {
                            dbTyper = "SqlDbType." + arraySplit[1].ToString();

                            if (NV[i].ToString() == "NULL")
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = NV[i].ToString();
                            }
                        }
                    }
                }

                //add paramter student column
                cmd.Parameters.AddWithValue("@sname", model.StName);
                cmd.Parameters.AddWithValue("@semail", model.StEmail);
                cmd.Parameters.AddWithValue("@spassword", model.StPassword);
                cmd.Parameters.AddWithValue("@scontact", model.StContact);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dt;
        }

        public DataTable DeleteStudent(string SpName, NameValueCollection NV, int id)
        {
            SqlConnection con = new SqlConnection();
            string dbTyper = "";
            DataTable dt = new DataTable();

            try
            {
                con.ConnectionString = _connection;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandText = SpName;
                cmd.CommandTimeout = 999999999;

                if (NV != null)
                {
                    //New code implemented for retrieving data
                    for (int i = 0; i < NV.Count; i++)
                    {
                        string[] arraySplit = NV.Keys[i].Split('-');

                        if (arraySplit.Length > 2)
                        {
                            dbTyper = "SqlDbType." + arraySplit[1].ToString() + "," + arraySplit[2].ToString();

                            if (NV[i].ToString() == "NULL")
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = NV[i].ToString();
                            }
                        }
                        else
                        {
                            dbTyper = "SqlDbType." + arraySplit[1].ToString();

                            if (NV[i].ToString() == "NULL")
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = NV[i].ToString();
                            }
                        }
                    }
                }

                //Add paramter student id
                SqlParameter paraID = new SqlParameter("@stdid", SqlDbType.Int);

                paraID.Value = id;
                cmd.Parameters.Add(paraID);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);


            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dt;
        }

        public DataTable EditStudent(string SpName, NameValueCollection NV, Student model, int id)
        {
        
                SqlConnection con = new SqlConnection();
                string dbTyper = "";
                DataTable dt = new DataTable();
                try
                {
                con.ConnectionString = _connection;
                con.Open();
                    SqlCommand cmd = new SqlCommand(SpName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 999999999;

                    // Set parameters for student ID and model properties
                    cmd.Parameters.AddWithValue("@sid", id);
                    cmd.Parameters.AddWithValue("@sname", model.StName);
                    cmd.Parameters.AddWithValue("@semail", model.StEmail);
                    cmd.Parameters.AddWithValue("@spassword", model.StPassword);
                    cmd.Parameters.AddWithValue("@scontact", model.StContact);

                if (NV != null)
                {
                    foreach (string key in NV.AllKeys)
                    {
                        string[] arraySplit = key.Split('-');

                        if (arraySplit.Length >= 2)
                        {
                            string parameterName = arraySplit[0];
                            string dbType = arraySplit[1];

                            // Check if the value in NV is "NULL"
                            if (NV[key] == "NULL")
                            {
                                cmd.Parameters.AddWithValue(parameterName, DBNull.Value);
                            }
                            else
                            {
                                // You should also validate the dbType to ensure it's a valid SqlDbType
                                if (Enum.TryParse(typeof(SqlDbType), dbType, out object sqlDbType))
                                {
                                    cmd.Parameters.Add(new SqlParameter(parameterName, (SqlDbType)sqlDbType)).Value = NV[key];
                                }
                                else
                                {
                                    // Handle invalid dbType (e.g., log an error)
                                }
                            }
                        }
                        else
                        {
                            // Handle incorrect key format (e.g., log an error)
                        }
                    }
                }
                // Execute the stored procedure and return the number of affected rows
                cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    return dt;

                }
                catch (Exception ex)
                {
                    // Handle the exception or log it.
                    // You can also return a specific error code or message.
                    return null; // Return -1 to indicate an error
                }
            
        }
    }
}
