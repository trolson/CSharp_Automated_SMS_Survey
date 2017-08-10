using System.Collections.Generic;
using System.Linq;
using SQLite;

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

        /// <summary>
        /// Adds a question to the database
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="SurveyId">The id of the survey that the question belongs to</param>
        /// <param name="QuestionIndex">The index of the question</param>
        /// <param name="QuestionText">The text of the question</param>
        public static void addQuestion(SQLiteConnection db, int SurveyId, int QuestionIndex, string QuestionText)
        {
            var s = db.Insert(new Question()
            {
                surveyId = SurveyId,
                questionIndex = QuestionIndex,
                questionText = QuestionText
            });
        }
        /// <summary>
        /// Returns a question id given a survey id and text
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="text">The text of the question</param>
        /// <param name="surveyID">The id of the survey the question belongs to</param>
        /// <returns>The id of the desired question</returns>
        public static int getQuestionId(SQLiteConnection db, string text, int surveyID)
        {
            var query = db.Table<Question>().Where(v => v.questionText.Equals(text)).Where(s => s.surveyId.Equals(surveyID)).OrderByDescending(q => q.questionId).ToArray<Question>();
            var single = query[0];
            int ret = single.questionId;
            return ret;
        }
        /// <summary>
        /// Gets the index of a question based on question id and survey id
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="questionID">The id of the desired question</param>
        /// <param name="surveyID">The survey id of the question</param>
        /// <returns>The index of the question</returns>
        public static int getQuestionIndex(SQLiteConnection db, int questionID, int surveyID)
        {
            var query = db.Table<Question>().Where(v => v.questionId.Equals(questionID)).Where(s => s.surveyId.Equals(surveyID)).ToArray<Question>();
            var single = query[0];
            int ret = single.questionIndex;
            return ret;
        }
        /// <summary>
        /// Gets a question id based on survey id and index
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="index">The index of the question</param>
        /// <param name="surveyID">The survey id of the question</param>
        /// <returns>The question id</returns>
        public static int getQuestionIdFromIndex(SQLiteConnection db, int index, int surveyID)
        {
            var query = db.Table<Question>().Where(v => v.questionIndex.Equals(index)).Where(s => s.surveyId.Equals(surveyID)).ToArray<Question>();
            var single = query[0];
            int ret = single.questionId;
            return ret;
        }
        /// <summary>
        /// Gets the text of a question based on survey id and index
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="surveyId">The survey id</param>
        /// <param name="index">The index of the question</param>
        /// <returns>The question index</returns>
        public static string getQuestionFromSurveyAndIndex(SQLiteConnection db, int surveyId, int index)
        {
            var query = db.Table<Question>().Where(v => v.questionIndex.Equals(index)).Where(s => s.surveyId.Equals(surveyId)).ToArray<Question>();
            var single = query[0];
            string ret = single.questionText;
            return ret;
        }
        /// <summary>
        /// Gets the text of a question based on a question id
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="questionId">The id of the question</param>
        /// <returns>The text of the question</returns>
        public static string getQuestionTextFromId(SQLiteConnection db, int questionId)
        {
            var query = db.Table<Question>().Where(v => v.questionId.Equals(questionId)).ToArray<Question>();
            var single = query[0];
            string ret = single.questionText;
            return ret;
        }
        /// <summary>
        /// Gets the id of a survey that a given question belongs to
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="questionId">The id of the question</param>
        /// <returns>The survey id</returns>
        public static int getSurveyIdFromQuestionId(SQLiteConnection db, int questionId)
        {
            var query = db.Table<Question>().Where(v => v.questionId.Equals(questionId)).ToArray<Question>();
            var single = query[0];
            int ret = single.surveyId;
            return ret;
        }
        /// <summary>
        /// Gets the maximum index for a given survey
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="surveyId">The id of the survey</param>
        /// <returns>The highest index for the given survey</returns>
        public static int getMaxIndex(SQLiteConnection db, int surveyId) 
        {
            var query = db.Table<Question>().Where(v => v.surveyId.Equals(surveyId)).OrderByDescending(q => q.questionIndex).ToArray<Question>();
            var single = query[0];
            int ret = single.questionIndex;
            return ret;

        }
        /// <summary>
        /// Gets all of the questions for a given survey and returns them as a list
        /// </summary>
        /// <param name="db">A connection to the database</param>
        /// <param name="surveyId">The id of the survey</param>
        /// <returns>A list of questions</returns>
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
