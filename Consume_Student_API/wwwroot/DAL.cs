using System.Collections.Specialized;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace NewProjectAPI.Data
{

    public class DAL
    {
        public string Connectionstring;

        public DAL(IConfiguration config)
        {

            Connectionstring = config.GetConnectionString("Addcon");

        }


        public DataTable GetData(string spname, NameValueCollection nv, string CNString)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection())
            {
                try
                {
                    connection.ConnectionString = CNString;
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.CommandText = spname;
                    cmd.CommandTimeout = 999999999;

                    string dbTyper = "";

                    if (nv != null)
                    {
                        for (int i = 0; i < nv.Count; i++)
                        {
                            string[] arraySplit = nv.Keys[i].Split('-');

                            if (arraySplit.Length > 2)
                            {
                                dbTyper = "SqlDbType." + arraySplit[1].ToString() + "," + arraySplit[2].ToString();

                                if (nv[i].ToString() == "NULL")
                                {
                                    cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = nv[i].ToString();
                                }
                            }
                            else
                            {
                                dbTyper = "SqlDbType." + arraySplit[1].ToString();

                                if (nv[i].ToString() == "NULL")
                                {
                                    cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = nv[i].ToString();
                                }
                            }
                        }
                    }

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    da.Dispose();

                    return dt;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }


        public bool InserData(string SpName, NameValueCollection nv, string CNString)
        {
            bool Result = true;
            string dbTyper = "";
            using (SqlConnection connection = new SqlConnection())
            {
                try
                {
                    connection.ConnectionString = CNString;
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    SqlCommand com = new SqlCommand();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Connection = connection;
                    com.CommandText = SpName;

                    if (nv != null)
                    {
                        for (int i = 0; i < nv.Count; i++)
                        {
                            string[] arraySplit = nv.Keys[i].Split('-');

                            if (arraySplit.Length > 2)
                            {
                                dbTyper = "SqlDbType." + arraySplit[1].ToString() + "," + arraySplit[2].ToString();

                                if (nv[i].ToString() == "NULL")
                                {
                                    com.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                                }
                                else
                                {
                                    com.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = nv[i].ToString();
                                }
                            }
                            else
                            {
                                dbTyper = "SqlDbType." + arraySplit[1].ToString();

                                if (nv[i].ToString() == "NULL")
                                {
                                    com.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                                }
                                else
                                {
                                    com.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = nv[i].ToString();
                                }
                            }
                        }
                    }

                    int j = com.ExecuteNonQuery();

                    if (j != 0)
                    {
                        Result = true;
                    }
                }
                catch (Exception ex)
                {
                    Result = false;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return Result;
        }
        public void Dispose() { }
    }


}
