using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ECIB.App_Code
{
    public class DAL
    {
        private string connectionString;
        private string procedureName;
        private SqlCommand thisCommand;
        private SqlDataAdapter thisAdapter;
        private SqlConnection thisConnection;
        public DAL()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ECIBDB"].ToString();
        }
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        public SqlConnection openConnection()
        {
            try
            {
                if (thisConnection == null)
                {

                    thisConnection = new SqlConnection(ConnectionString);
                    thisConnection.Open();
                }
            }
            catch (Exception exp)
            {
                BLL.CreateErrorLog("System","conn error", exp.ToString());
            }
            return thisConnection;
        }


        public void CloseConnection()
        {
            try
            {
                if (thisConnection != null)
                    thisConnection.Close();
            }
            catch (Exception ex)
            { }
        }
        public void DisposeConnection()
        {
            if (thisConnection != null)
            {
                thisConnection.Dispose();
                thisConnection = null;
            }
        }
        public DataSet FillDataSet(string q, string tableName)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(q, thisConnection);
                da.Fill(ds, tableName);
                return ds;
            }
            catch (Exception error)
            {
                return ds;
            }
        }
        public DataSet FillDataSet(string storedProcedureName, string tableName, string Pram)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = thisConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                cmd.Parameters.AddWithValue("@BatchNumber", Pram);
                cmd.CommandTimeout = 3000;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds, tableName);
                return ds;
            }
            catch (Exception error)
            {
                return ds;
            }
        }
        public SqlDataReader ExecuteQueryReturnValue(string q)
        {
            thisCommand = thisConnection.CreateCommand();
            thisCommand.CommandText = q;
            thisCommand.CommandType = CommandType.Text;
            return thisCommand.ExecuteReader();
        }
        public string ProcName
        {
            set
            {
                procedureName = value;
            }
        }
        public void CreateProcedureCommand()
        {
            thisCommand = thisConnection.CreateCommand();
            thisCommand.CommandText = procedureName;
            thisCommand.CommandType = CommandType.StoredProcedure;
            thisCommand.CommandTimeout = 175;
        }

        public void CreateCommand()
        {
            thisCommand = thisConnection.CreateCommand();
            thisCommand.CommandText = procedureName;
            thisCommand.CommandType = CommandType.StoredProcedure;
            thisCommand.CommandTimeout = 175;
        }

        public SqlDataReader ExecuteDataReader()
        {
            return thisCommand.ExecuteReader();
        }

        public SqlDataAdapter createAdapter()
        {
            thisAdapter = new SqlDataAdapter(thisCommand);
            return thisAdapter;
        }

        public DataSet CreateDataSet()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(thisCommand);
            da.Fill(ds);
            return ds;
        }

        public string ExecuteReturnValue()
        {
            thisCommand.ExecuteNonQuery();
            return (string)thisCommand.Parameters["@retVal"].Value;
        }

        public int Execute()
        {
            return (int)thisCommand.ExecuteNonQuery();
        }

        public int ExecuteWithTimeOut(int timeout)
        {
            this.thisCommand.CommandTimeout = timeout;
            return (int)thisCommand.ExecuteNonQuery();
        }

        public SqlDataAdapter createAdapter(string qry)
        {
            thisAdapter = new SqlDataAdapter(qry, thisConnection);
            return thisAdapter;
        }

        public DataTable ExecuteFillTable()
        {
            DataTable dt = new DataTable();
            createAdapter();
            thisAdapter.Fill(dt);
            return dt;
        }

        public string Parameters()
        {
            return thisCommand.Parameters["@RetVal"].Value.ToString();
        }
        public void Parameters(string name, SqlDbType type, int size, string val)
        {
            thisCommand.Parameters.Add(name, type, size);
            thisCommand.Parameters[name].Value = val;
        }

        public void Parameters(string name, SqlDbType type, int size, bool val)
        {
            thisCommand.Parameters.Add(name, type, size);
            thisCommand.Parameters[name].Value = val;
        }

        public void Parameters(string name, SqlDbType type, int size)
        {
            thisCommand.Parameters.Add(name, type, size);
            thisCommand.Parameters[name].Direction = ParameterDirection.Output;
        }

        public void Parameters(string name, SqlDbType type, int size, double val)
        {
            thisCommand.Parameters.Add(name, type, size);
            thisCommand.Parameters[name].Value = val;
        }

        public void Parameters(string name, SqlDbType type, int size, int val)
        {
            thisCommand.Parameters.Add(name, type, size);
            thisCommand.Parameters[name].Value = val;
        }

        public void Parameters(string name, SqlDbType type, int size, DateTime val)
        {
            thisCommand.Parameters.Add(name, type, size);
            thisCommand.Parameters[name].Value = val;
        }

        public void Parameters(string name, SqlDbType type, int size, Decimal val)
        {
            thisCommand.Parameters.Add(name, type, size);
            thisCommand.Parameters[name].Value = val;
        }
        //********************************************************//
        public SqlCommand createCommand(string qry)
        {
            thisCommand = new SqlCommand(qry, thisConnection);
            return thisCommand;
        }


        public void Parameters(string name, string var)
        {
            thisCommand.Parameters.Add(new SqlParameter(name, var));
        }


    }
}