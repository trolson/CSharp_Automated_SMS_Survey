using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bandwidth.Net.Api;
using SQLite;
using Bandwidth.Net;



namespace SMS_Example_Survey
{
    public class FinishStartup
    {
        public static SQLiteConnection db = new SQLiteConnection("sms_survey.sqlite"); //db name
        private const string UserId = "u-spj2plotunygiwpvxxzjera"; //{user_id}
        private const string Token = "t-zx7hi5ryulp2wihxszes2ba"; //{token}
        private const string Secret = "3nynyppdgl62r73gg5kkuitldavogezq3ps75fi"; //{secret}

        private const string SurveyNumber = "+19104153043"; // <-- This must be a Bandwidth number on your account
        private const string CallbackUrl = "https://4ab3eede.ngrok.io/bandwidth/callback/message"; // <-- This is the callback url of the app created

        public static void Start() {            
            createTables();
        }

        public static void createTables() {
            db.CreateTable<Survey>();
            db.CreateTable<Question>();
            db.CreateTable<Answer>();
            db.CreateTable<Status>();
        }

        public static void sendFirstQuestion(List<string> phoneNumbers, int surveyId)
        {
            var client = new Client(UserId, Token, Secret); 
            string text = Question.getQuestionFromSurveyAndIndex(db, surveyId, 0);

            foreach (var number in phoneNumbers) {
                RunAsync(number, text, client, surveyId).Wait();
            }
        }

        private static async Task RunAsync(String phoneNumber, String message, Client client, int surveyId)
        {
            var sms = await client.Message.SendAsync(new MessageData
            {
                From = SurveyNumber,
                To = phoneNumber,
                Text = message,
                CallbackUrl = CallbackUrl
            });

            int qId = Question.getQuestionIdFromIndex(db, 0, surveyId);
            if (Status.checkIfPhoneNumberExists(db, phoneNumber)) {
                Status.updateStatus(db, phoneNumber, qId); //update if already exists
            }
            else {
                Status.addStatus(db, phoneNumber, qId); //insert into status table after first message sent
            }           
           
        }

    }
}
