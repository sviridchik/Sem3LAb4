using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
//using ClassLibraryLab3;

namespace DataAccessLayer
{

    public struct DataEntity
    {
        public List<string> names;
        public List<object[]> values;
    }


    public struct AdressKnot
    {
        int businessEID;
        int addressid;
        int addressTypeid;
        string city;
        string name;
        string addressLine1;
    }

    public class DataAccessLayerClass
    {
        string ser;
        string db;
        bool con;

        public DataAccessLayerClass(string ser, string db, bool con)
        {
            this.ser = ser;
            this.db = db;
            this.con = con;
        }

        SqlConnection connection;

        public void StartDb()
        {
            // "Data Source=USER-PC\SQLEXPRESS;Initial Catalog=AdventureWorks2012;Integrated Security=True"
            //string connectionString = $"Server={ser}; Database={db}; Trusted_Connection ={con}";
            string connectionString = $"Data Source={ser.Replace("\"", "").Replace(@"\\", @"\")}; Initial Catalog={db.Replace("\"", "").Replace(@"\\", @"\")}; Integrated Security ={con}";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }


        public DataEntity GetAdd(string nameOfProc, params SqlParameter[] sqlParameters)
        {
            DataEntity dataEntity = new DataEntity();
            SqlCommand command = new SqlCommand(nameOfProc, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddRange(sqlParameters);
            SqlDataReader reader = command.ExecuteReader();
            int colCount = reader.FieldCount;
            List<object[]> table = new List<object[]>();
            while (reader.Read())
            {
                table.Add(new object[colCount]);
                reader.GetValues(table.Last());

            }            
            dataEntity.values = table;
            dataEntity.names = new List<string>();
            for (int i = 0; i < colCount; i++)
            {
                dataEntity.names.Add(reader.GetName(i));
            }
            reader.Close();
            return dataEntity;
        }

        
            

    }
}


/*
 * Person.PhoneNumberType
 * 
 SELECT TOP (1000) [PhoneNumberTypeID]
      ,[Name]
      ,[ModifiedDate]
  FROM [AdventureWorks2017].[Person].[PhoneNumberType]
 
 */
