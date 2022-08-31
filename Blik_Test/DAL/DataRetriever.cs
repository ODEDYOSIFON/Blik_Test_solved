using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blik_Test.Models;
using static Blik_Test.Models.Enums;

namespace Blik_Test.DAL
{
    public static class DataRetriever
    {
        //sql connection string (make sure to change it when running the code)
        private static string connString = @"Server=localhost;Database=blikDB;Trusted_Connection=True;User Id=sa;Password=sql;";

        /// <summary>
        /// this function is used to retreive all pepole data from database 
        /// </summary>
        /// <returns> list of presons</returns>
        internal static List<Person> GetPepole()
        {
            string spName = "sp_getPersons";
            List<Person> lp = new List<Person>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(spName, conn);

                    conn.Open();

                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int perID;
                        string perName;
                        int perGender;
                       
                        while (dr.Read())
                        {
                            perID = dr.GetInt32("id");
                            perName = dr.GetString("Name");
                            perGender = dr.GetInt16("Gender");
                           
                            lp.Add(new Person(perID, perName, (Gender)perGender, GetPersonHobbiesList(perID)));

                        }
                    }


                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    conn.Close();

                }
            }
            return lp;
        }

        /// <summary>
        /// this function gets person's hobbies data records from a database
        /// </summary>
        /// <param name="perID">person id</param>
        /// <returns>list of  person's hobbies (List<Hobby>) </returns>
        public static List<Hobby> GetPersonHobbiesList(int perID)
        {
            string spName = "sp_getPersonHobbiesList";
            List<Hobby> lh = new List<Hobby>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(spName, conn);
                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@PersonId";
                    param1.SqlDbType = SqlDbType.Int;
                    param1.Value = perID;
                    cmd.Parameters.Add(param1);
                    conn.Open();

                    //set the SqlCommand type to stored procedure and execute
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                       
                        int hobbyID;
                        
                        string hobbyName;
                        while (dr.Read())
                        {

                            hobbyID = dr.GetInt32("HobbyID");
                            hobbyName = dr.GetString("HobbyName");

                            lh.Add(new Hobby(hobbyID, hobbyName));

                        }
                    }


                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    conn.Close();
                 
                }
                return lh;
            }
          
        }
       
        
        
        /// <summary>
        /// gets persons data by hobbies
        /// </summary>
        /// <param name="hobbiesNames">comma separated string containing the hobbies names</param>
        /// <returns> list of persons having all of the searched hobbies</returns>
        public static List<Person> FindPersonsByHobbiesName(string hobbiesNames)
        {
            string spName = "sp_getPersonsByHobbies";
            List<Person> lp = new List<Person>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(spName, conn);
                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@HobbiesList";
                    param1.SqlDbType = SqlDbType.Text;
                    param1.Value = hobbiesNames;
                    cmd.Parameters.Add(param1);
                    conn.Open();

                    //set the SqlCommand type to stored procedure and execute
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        int personID;
                        int personGender;

                        string personName;
                        while (dr.Read())
                        {

                            personID = dr.GetInt32("id");
                            personName = dr.GetString("Name");
                            personGender = dr.GetInt16("Gender");

                            lp.Add(new Person(personID, personName, (Gender)personGender, GetPersonHobbiesList(personID)));

                        }

                        
                    }


                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    conn.Close();

                }
                return lp;
            }

        }

    }
}
