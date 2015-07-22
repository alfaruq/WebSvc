using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebSvc.DTOs;

namespace WebSvc.Providers
{
    public class PeopleProvider
    {
        public static void InsertPeople(People people)
        {
            var factory = SqlClientFactory.Instance;

            try
            {
                using (var con = factory.CreateConnection())
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["FaruqConnectionString"].ConnectionString;
                    con.Open();

                    var cmd = con.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "p_People_i";

                    var outputParameter = new SqlParameter();
                    outputParameter.ParameterName = "@PeopleID";
                    outputParameter.SqlDbType = System.Data.SqlDbType.Int;
                    outputParameter.Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@FullName", Value = people.FullName });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Age", Value = people.Age });
                    cmd.Parameters.Add(outputParameter);

                    cmd.ExecuteNonQuery();

                    people.Id = (int)outputParameter.Value;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}