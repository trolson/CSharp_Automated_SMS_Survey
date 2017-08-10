using SQLite;

namespace SMS_Example_Survey
{
    public class Answer
    {
        [PrimaryKey, AutoIncrement]
        public int answerId { get; set; }
        [Indexed]
        public int questionId { get; set; }
        public string phoneNumber { get; set; }
        public string answerText { get; set; }

        /// <summary>
        /// Adds and answer to the database
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="QuestionId">The id of the question answered</param>
        /// <param name="PhoneNumber">The phone number that responded to the question</param>
        /// <param name="AnswerText">The text of the answer</param>
        public static void addAnswer(SQLiteConnection db, int QuestionId, string PhoneNumber, string AnswerText)
        {
            var s = db.Insert(new Answer()
            {
                questionId = QuestionId,
                phoneNumber = PhoneNumber,
                answerText = AnswerText
            });
        }

    }
}
