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


        public static void addSurvey(SQLiteConnection db, string symbol, string phoneNumber)
        {
            var s = db.Insert(new Survey()
            {
              surveyName = symbol,
              phoneNumber = phoneNumber
            });
        }

        public static int getSurveyId(SQLiteConnection db, string surveyName)
        {
            var query = db.Table<Survey>().Where(v => v.surveyName.Equals(surveyName)).OrderByDescending(s => s.surveyId).ToArray<Survey>();
            var single = query[0];
            int ret = single.surveyId;
            return ret;            
        }

        public static int getSurveyIdFromPhoneNumber(SQLiteConnection db, string number)
        {
            var query = db.Table<Survey>().Where(v => v.phoneNumber.Equals(number)).OrderByDescending(s => s.surveyId).ToArray<Survey>();
            var single = query[0];
            int ret = single.surveyId;
            return ret;            
        }
    }

    
}
