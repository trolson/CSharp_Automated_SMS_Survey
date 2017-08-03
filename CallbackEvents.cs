using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bandwidth.Net.Extra;
using Bandwidth.Net.Api;
using Bandwidth.Net;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace SMS_Example_Survey
{
    public static class CallbackEvents
    {
        public static List<Question> questionPack = new List<Question>();

        public static readonly Dictionary<CallbackEventType, System.Func<CallbackEvent, HttpContext, Task>> Messages = new Dictionary<CallbackEventType, System.Func<CallbackEvent, HttpContext, Task>>
        {
            {CallbackEventType.Sms, async (data, context) =>
                {
                    if(data.To == context.GetPhoneNumber() && data.Direction == MessageDirection.In)
                    {
                        var message = context.GetRequestService<IMessage>();
                        Debug.WriteLine(data.Text);

                        string fromNum = data.From;
                        string response = data.Text;

                        //check status table to see if from # is in middle of survey
                        //if (!Status.checkIfPhoneNumberExists(FinishStartup.db, fromNum)) {
                        //    int surveyId = Survey.getSurveyIdFromPhoneNumber(FinishStartup.db, data.To);
                        //    questionPack = Question.createQuestionListFromSurveyId(FinishStartup.db, surveyId);
                        //    //Send message
                        //    await message.SendAsync(new MessageData
                        //    {
                        //        From = data.To,
                        //        To = data.From,
                        //        Text = questionPack[0].questionText
                        //    });
                        //    //update Status table
                        //    int nextQID = Question.getQuestionIdFromIndex(FinishStartup.db, 1, surveyId);
                        //    Status.updateStatus(FinishStartup.db, fromNum, nextQID);
                        //}

                        //query to get questionID from status table to perform answer insert
                        int QID = Status.getQuestionId(FinishStartup.db, fromNum);

                        //now perform answer insert
                        Answer.addAnswer(FinishStartup.db, QID, fromNum, response);

                        //now have to update the Status table to have the next questionId from questionList...
                        //first get current index
                        //next, increment it
                        //next, check that it is less than maxNumOfQuestions
                        int SurveyId = Question.getSurveyIdFromQuestionId(FinishStartup.db, QID);
                        int idx = Question.getQuestionIndex(FinishStartup.db, QID, SurveyId);
                        int max = Question.getMaxIndex(FinishStartup.db, SurveyId);
                        if (idx+1 <= max) {
                            idx++;
                            int nextQID = Question.getQuestionIdFromIndex(FinishStartup.db, idx, SurveyId);
                            string text = Question.getQuestionTextFromId(FinishStartup.db, nextQID);
                            Status.updateStatus(FinishStartup.db, fromNum, nextQID);
                            await message.SendAsync(new MessageData
                            {
                                From = data.To,
                                To = data.From,
                                Text = text
                            });
                        }

                      
                    }
                }
            }
        };
    }
}
