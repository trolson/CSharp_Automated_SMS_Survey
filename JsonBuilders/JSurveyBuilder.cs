using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SMS_Example_Survey.JsonModels;
using Newtonsoft.Json;

namespace SMS_Example_Survey.JsonBuilders
{
    public class JSurveyBuilder
    {
        public static JSurvey surveyObjectBuilder(int id) {
            JSurvey survey;

            var surveyQuery = FinishStartup.db.Table<Survey>().Where(v => v.surveyId == id).ToArray<Survey>();
            if (surveyQuery.Length < 1) {
                //post something like No Survey Matches the provided Id
                //maybe just return something
            }
            
            string surveyName = surveyQuery[0].surveyName;
            string phoneNumber = surveyQuery[0].phoneNumber;
            int surveyId = id;
            survey = new JSurvey();
            survey.surveyId = surveyId;
            survey.surveyName = surveyName;
            survey.phoneNumber = phoneNumber;
            
            List<JQuestion> questions = new List<JQuestion>();            
            var questionQuery = FinishStartup.db.Table<Question>().Where(v => v.surveyId == id).ToArray<Question>(); 
            if (questionQuery.Length < 1) {
                //create blank obj in json for question
            }
            else {
                //for each question, save all answers
                //also add each to questions list                
                foreach (var q in questionQuery) {
                    JQuestion jq = new JQuestion(); 
                    jq.questionId = q.questionId;
                    jq.questionIndex = q.questionIndex;
                    jq.questionText = q.questionText;                    
                    List<JAnswer> answers = new List<JAnswer>();
                    var answerQuery = FinishStartup.db.Table<Answer>().Where(v => v.questionId == q.questionId).ToArray<Answer>();
                    foreach (var a in answerQuery) {
                        JAnswer ja = new JAnswer();
                        ja.answerId = a.answerId;
                        ja.answerText = a.answerText;
                        ja.phoneNumber = a.phoneNumber;
                        answers.Add(ja);                        
                    }
                    jq.answers = answers;
                    questions.Add(jq);
                }
                survey.questions = questions;
            }
            return survey;
        }

        public static List<Survey> getSurveyList()
        {
            var surveys = FinishStartup.db.Table<Survey>().ToArray<Survey>();
            List<Survey> sList = new List<Survey>();
            foreach (var s in surveys) {
                sList.Add(s);
            }
            return sList;
        }
    }
}
