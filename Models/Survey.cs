using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using System.Diagnostics;

namespace SMS_Example_Survey
{
    public class Survey
    {
        [PrimaryKey, AutoIncrement]
        public int surveyId { get; set; }
        [Indexed]
        public string surveyName { get; set; }
        public string phoneNumber { get; set; }

        /// <summary>
        /// Adds a survey to the database
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="name">The name of the new survey</param>
        /// <param name="phoneNumber">The phone number associated with the survey</param>
        public static void addSurvey(SQLiteConnection db, string name, string phoneNumber)
        {
            var s = db.Insert(new Survey()
            {
              surveyName = name,
              phoneNumber = phoneNumber
            });
        }
        /// <summary>
        /// Gets a survey id based on survey name
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="surveyName">The name of the survey</param>
        /// <returns>The survey's id</returns>
        public static int getSurveyId(SQLiteConnection db, string surveyName)
        {
            var query = db.Table<Survey>().Where(v => v.surveyName.Equals(surveyName)).OrderByDescending(s => s.surveyId).ToArray<Survey>();
            var single = query[0];
            int ret = single.surveyId;
            return ret;            
        }
        /// <summary>
        /// Gets a survey id based on phone number
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="number">The phone number associated with the survey</param>
        /// <returns>The survey's id</returns>
        public static int getSurveyIdFromPhoneNumber(SQLiteConnection db, string number)
        {
            var query = db.Table<Survey>().Where(v => v.phoneNumber.Equals(number)).OrderByDescending(s => s.surveyId).ToArray<Survey>();
            var single = query[0];
            int ret = single.surveyId;
            return ret;            
        }
    }

    
}
