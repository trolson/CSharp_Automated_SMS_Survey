using System.Linq;
using SQLite;

namespace SMS_Example_Survey
{
    public class Status
    {
        [PrimaryKey]
        public string phoneNumber { get; set; }
        [Indexed]
        public int questionId { get; set; }
        /// <summary>
        /// Ihnserts a new status into the database
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="phoneNumber">The phone number that is receiving the survey</param>
        /// <param name="questionId">Id for the most recent question sent</param>
        public static void addStatus(SQLiteConnection db, string phoneNumber, int questionId)
        {
            var s = db.Insert(new Status()
            {
                phoneNumber = phoneNumber,
                questionId = questionId
             });
        }
        /// <summary>
        /// Updates a status with a new question id
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="phoneNumber">The phone number that is receiving the survey</param>
        /// <param name="questionId">Id for the most recent question sent</param>
        public static void updateStatus(SQLiteConnection db, string phoneNumber, int questionID)
        {
            db.Execute("update Status set questionId = " + questionID + " where phoneNumber = '" + phoneNumber + "'");      
        }
        /// <summary>
        /// Gets the id for the most recent question for a given number
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="phoneNumber">The phone number that is receiving the survey</param>
        /// <returns>Id for the most recent question sent</returns>
        public static int getQuestionId(SQLiteConnection db, string phoneNumber)
        {
            var query = db.Table<Status>().Where(v => v.phoneNumber.Equals(phoneNumber)).ToArray<Status>();
            var single = query[0];
            int ret = single.questionId;
            return ret;
        }
        /// <summary>
        /// Checks the database to see if a given number is currently receiving a survey
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="phoneNumber">The phone number that is receiving the survey</param>
        /// <returns>True if the phone number is currently taking a survey</returns>
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
