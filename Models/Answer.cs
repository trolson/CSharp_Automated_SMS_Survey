using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
