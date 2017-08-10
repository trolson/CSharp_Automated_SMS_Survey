using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Bandwidth.Net.Extra;
using Bandwidth.Net.Api;
using Microsoft.Extensions.Configuration;
using System.IO;
using SQLite;
using System.Diagnostics;
using System.Collections;
using Bandwidth.Net;
using SMS_Example_Survey.Controllers;



namespace SMS_Example_Survey
{
    public class FinishStartup
    {
        public static SQLiteConnection db = new SQLiteConnection("sms_survey.sqlite"); //db name ex: test.sqlite 
        private const string UserId = ""; //{user_id}
        private const string Token = ""; //{token}
        private const string Secret = ""; //{secret}

        private const string SurveyNumber = ""; // <-- This must be a Bandwidth number on your account
        private const string CallbackUrl = ""; // <-- This is the callback url of the app created

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
            var client = new Client(UserId, Token, Secret); //I think the thing above may have setup a client
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
