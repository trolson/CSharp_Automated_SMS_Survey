using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using System.Diagnostics;

namespace SMS_Example_Survey
{
    public class Status
    {
        [PrimaryKey]
        public string phoneNumber { get; set; }
        [Indexed]
        public int questionId { get; set; }

        public static void addStatus(SQLiteConnection db, string phoneNumber, int questionId)
        {
            var s = db.Insert(new Status()
            {
                phoneNumber = phoneNumber,
                questionId = questionId
             });
        }

        public static void updateStatus(SQLiteConnection db, string phoneNumber, int questionID)
        {
            db.Execute("update Status set questionId = " + questionID + " where phoneNumber = '" + phoneNumber + "'");      
        }

        public static int getQuestionId(SQLiteConnection db, string phoneNumber)
        {
            var query = db.Table<Status>().Where(v => v.phoneNumber.Equals(phoneNumber)).ToArray<Status>();
            var single = query[0];
            int ret = single.questionId;
            return ret;
        }

        public static bool checkIfPhoneNumberExists(SQLiteConnection db, string phoneNumber)
        {
            var query = db.Table<Status>().Where(v => v.phoneNumber.Equals(phoneNumber)).ToArray<Status>();
            if (query.Length == 0) {
                return false;
            }
            else {
                return true;
            }
        }
    }
}
