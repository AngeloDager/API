using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace APIClient.CommonUtils.Services
{
    public static class ConnectDB
    {
        private static SqlConnection Connection;

        private static string StringConnectionWindowsAuthentication(string Host, string Database) => "Server=" + Host + "; Database=" + Database + "; Integrated Security=True;";
        private static string StringConnectionSqlServerAuthentication(string Host, string Database, string Username, string Password) => "Server=" + Host + "; Database=" + Database + "; User Id=" + Username + "; Password=" + Password + ";";

        public static void Connect(string Host, string Database, string Username, string Password)
        {
            if (Username == null)
                Connection = new SqlConnection(StringConnectionWindowsAuthentication(Host, Database));
            else
                Connection = new SqlConnection(StringConnectionSqlServerAuthentication(Host, Database, Username, Password));
        }

        public static string ConnectString(string Host, string Database, string Username, string Password)
        {
            if (Username == null)
                return StringConnectionWindowsAuthentication(Host, Database);
            else
                return StringConnectionSqlServerAuthentication(Host, Database, Username, Password);
        }

        public static void Close()
        {
            Connection.Close();
        }

        public static void BulkCopy<T>(string TableName, List<T> List, string cadena)
        {
            try
            {
                using (var connection = new SqlConnection(cadena))
                { 
                    
                  SqlBulkCopy bulkCopy = new SqlBulkCopy(
                  connection,
                  SqlBulkCopyOptions.TableLock |
                  SqlBulkCopyOptions.FireTriggers |
                  SqlBulkCopyOptions.UseInternalTransaction,
                  null
                  );
                    bulkCopy.DestinationTableName = TableName;
                    connection.Open();
                    try
                    {
                        DataTable dt = ToDataTable(List);
                        bulkCopy.BulkCopyTimeout = 180;
                        bulkCopy.WriteToServer(dt);
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        throw new Exception("bulk copy interno " + ex.Message);
                    }
                
                }
              
            }
            catch(Exception e)
            {
                throw new Exception("bulk copy "+e.Message);
            }
            
        }

        public static void BulkCopyWithIdentity<T>(string TableName, List<T> List)
        {
            SqlBulkCopy bulkCopy = new SqlBulkCopy(
                    Connection,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null
                    );
            bulkCopy.DestinationTableName = TableName;
            Connection.Open();
            DataTable dt = ToDataTableWithIdentity(List);

            foreach (DataColumn column in dt.Columns)
            {
                bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
            }

            bulkCopy.WriteToServer(dt);
            Connection.Close();
        }

        private static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private static DataTable ToDataTableWithIdentity<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, prop.PropertyType);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

    }
}
