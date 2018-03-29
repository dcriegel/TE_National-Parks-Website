using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAL : ISurveyDAL
    {
        private string _connectionString;

        public SurveySqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Survey> GetAllSurveys()
        {
            List<Survey> surveys = new List<Survey>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string survey = @"SELECT parkCode, COUNT(parkCode) AS parkVotes FROM survey_result GROUP BY parkCode ORDER BY parkVotes DESC;";
                SqlCommand cmd = new SqlCommand(survey, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    surveys.Add(GetSurveyFromReader(reader));
                }

            }
            return surveys;
        }
        private Survey GetSurveyFromReader(SqlDataReader reader)
        {
            Survey survey = new Survey()
            {

                SurveyId = Convert.ToInt32(reader["surveyId"]),
                ParkCode = Convert.ToString(reader["parkCode"]),
                EmailAddress = Convert.ToString(reader["emailAddress"]),
                State = Convert.ToString(reader["state"]),
                ActivityLevel = Convert.ToString(reader["activityLevel"]),

            };
            return survey;
        }

        public bool SaveNewSurvey(Survey newSurvey)
        {
            Survey sPost = new Survey();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string survey = @"INSERT INTO survey_result(parkCode, emailAddress, state, activityLevel)
                                VALUES(@parkCode, @emailAddress, @state, @activityLevel) SELECT CAST(SCOPE_IDENTITY() AS INT)";

                SqlCommand cmd = new SqlCommand(survey, conn);
                
                cmd.Parameters.AddWithValue("@parkCode", newSurvey.ParkCode);
                cmd.Parameters.AddWithValue("@emailAddress", newSurvey.EmailAddress);
                cmd.Parameters.AddWithValue("@state", newSurvey.State);
                cmd.Parameters.AddWithValue("@activityLevel", newSurvey.ActivityLevel);

                int newId = (int)cmd.ExecuteScalar();
                sPost.SurveyId = newId;

            }
            return true;
        }
    }
}