using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using System.Diagnostics;

namespace SMS_Example_Survey
{
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int questionId { get; set; }
        [Indexed]
        public int surveyId { get; set; }
        public int questionIndex { get; set; }
        public string questionText { get; set; }

        public static void addQuestion(SQLiteConnection db, int SurveyId, int QuestionIndex, string QuestionText)
        {
            var s = db.Insert(new Question()
            {
                surveyId = SurveyId,
                questionIndex = QuestionIndex,
                questionText = QuestionText
            });
        }

        public static int getQuestionId(SQLiteConnection db, string text, int surveyID)
        {
            var query = db.Table<Question>().Where(v => v.questionText.Equals(text)).Where(s => s.surveyId.Equals(surveyID)).OrderByDescending(q => q.questionId).ToArray<Question>();
            var single = query[0];
            int ret = single.questionId;
            return ret;
        }

        public static int getQuestionIndex(SQLiteConnection db, int questionID, int surveyID)
        {
            var query = db.Table<Question>().Where(v => v.questionId.Equals(questionID)).Where(s => s.surveyId.Equals(surveyID)).ToArray<Question>();
            var single = query[0];
            int ret = single.questionIndex;
            return ret;
        }

        public static int getQuestionIdFromIndex(SQLiteConnection db, int index, int surveyID)
        {
            var query = db.Table<Question>().Where(v => v.questionIndex.Equals(index)).Where(s => s.surveyId.Equals(surveyID)).ToArray<Question>();
            var single = query[0];
            int ret = single.questionId;
            return ret;
        }

        public static string getQuestionFromSurveyAndIndex(SQLiteConnection db, int surveyId, int index)
        {
            var query = db.Table<Question>().Where(v => v.questionIndex.Equals(index)).Where(s => s.surveyId.Equals(surveyId)).ToArray<Question>();
            var single = query[0];
            string ret = single.questionText;
            return ret;
        }

        public static string getQuestionTextFromId(SQLiteConnection db, int questionId)
        {
            var query = db.Table<Question>().Where(v => v.questionId.Equals(questionId)).ToArray<Question>();
            var single = query[0];
            string ret = single.questionText;
            return ret;
        }

        public static int getSurveyIdFromQuestionId(SQLiteConnection db, int questionId)
        {
            var query = db.Table<Question>().Where(v => v.questionId.Equals(questionId)).ToArray<Question>();
            var single = query[0];
            int ret = single.surveyId;
            return ret;
        }

        public static int getMaxIndex(SQLiteConnection db, int surveyId) 
        {
            var query = db.Table<Question>().Where(v => v.surveyId.Equals(surveyId)).OrderByDescending(q => q.questionIndex).ToArray<Question>();
            var single = query[0];
            int ret = single.questionIndex;
            return ret;

        }

        public static List<Question> createQuestionListFromSurveyId(SQLiteConnection db, int surveyId) {
            List<Question> qList = new List<Question>();
            var query = db.Table<Question>().Where(v => v.surveyId.Equals(surveyId)).ToArray<Question>();
            foreach (var q in query) {
                qList.Add(q);
            }
            return qList;
        }
    }
}
