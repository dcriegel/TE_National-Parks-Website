using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAL : IParkDAL
    {

        private string _connectionString;

        public ParkSqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<Park> GetAllParks()
        {
            var list = new List<Park>();

            string sql = "SELECT * FROM park ORDER BY parkName ASC;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    Park p = new Park() {

                        ParkCode = Convert.ToString(r["parkCode"]),
                        ParkName = Convert.ToString(r["parkName"]),
                        State = Convert.ToString(r["state"]),
                        Acreage = Convert.ToInt32(r["acreage"]),
                        ElevationInFeet = Convert.ToInt32(r["elevationInFeet"]),
                        MilesOfTrail = Convert.ToDouble(r["milesOfTrail"]),
                        NumberOfCampsites = Convert.ToInt32(r["numberOfCampsites"]),
                        Climate = Convert.ToString(r["climate"]),
                        YearFounded = Convert.ToInt32(r["yearFounded"]),
                        AnnualVisitorCount = Convert.ToInt32(r["annualVisitorCount"]),
                        InspirationalQuote = Convert.ToString(r["inspirationalQuote"]),
                        InspirationalQuoteSource = Convert.ToString(r["inspirationalQuoteSource"]),
                        ParkDescription = Convert.ToString(r["parkDescription"]),
                        EntryFee = Convert.ToInt32(r["entryFee"]),
                        NumberOfAnimalSpecies = Convert.ToInt32(r["numberOfAnimalSpecies"]),

                        //FiveDayForecastValue = Convert.ToInt32(r["fiveDayForecastValue"]),
                        //LowTemp = Convert.ToInt32(r["low"]),
                        //HighTemp = Convert.ToInt32(r["high"]),
                        //Forecast = Convert.ToString(r["forecast"])
                    };

                    list.Add(p);
                }
            }
            return list;
        }

        //public Park GetPark(string parkCode)
        //{
        //    throw new NotImplementedException();
        //}

        //public Park GetPark(string parkCode)
        //{
        //    throw new NotImplementedException();
        //}
    }
}