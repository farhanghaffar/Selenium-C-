using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
namespace MRO.ROI.Automation.Utility
{
    public class MRODBConnection
    {

        public static void SampleDBConnection()
        {
            using (SqlConnection db = new SqlConnection("uid=webclient_MRO;pwd=icc48*PLX;server=HQSQLSTG06L;database=MRO_Facilities"))//Connectionsrting)
            {
                string sSQL = "select * from tblFacilities where nfacilityid = 1";

                var result = db.Query(sSQL);
                Console.WriteLine("DB Results" + result);
            }
        }

        public static void GetQueryResult()
        {
            SqlConnection conn = new SqlConnection(
               new SqlConnectionStringBuilder()
               {
                   DataSource = "hqsqlstg06a",
                   InitialCatalog = "MRO_Facillities",
                   UserID = "MRO\tmirza",
                   Password = "Mihran2007$"
               }.ConnectionString
         );
            Console.WriteLine("Error in getting result of query.");
            conn.Open();

            conn.Close();
            conn.Dispose();
        }

        public static string GetQueryResult(string query)
        {
            string connetionString = @"Data Source=hqsqlstg06a;Initial Catalog=MRO_ROI;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connetionString);
            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText = query;
            command.CommandType = CommandType.Text;
            string boeRequest = "";
            using (var result = command.ExecuteReader())
            {

                while (result.Read())
                {
                    var BOERequest = result.GetSqlInt32(0);
                    var nRequestID = result.GetSqlInt32(1);
                    Console.WriteLine("BOERequest : " + BOERequest.ToString());
                    Console.WriteLine("nRequestID : " + nRequestID.ToString());
                    boeRequest = BOERequest.ToString();

                }

            }
            Console.WriteLine("Count");
            conn.Close();
            conn.Dispose();

            return boeRequest;
        }

        public static bool isUpdate(string query)
        {
            string connetionString = @"Data Source=hqsqlstg06a;Initial Catalog=MRO_ROI;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connetionString);
            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText = query;
            command.CommandType = CommandType.Text;
            try
            {
               var test =  command.ExecuteNonQuery();
                if (test == 0)
                    return false;
                else
                   return true;

            }
            catch (Exception e)
            {
                throw new Exception();
            }

        }

    }
}
