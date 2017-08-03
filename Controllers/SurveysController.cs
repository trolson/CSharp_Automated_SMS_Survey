using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMS_Example_Survey.JsonModels;
using Newtonsoft.Json;
using SMS_Example_Survey.JsonBuilders;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace SMS_Example_Survey.Controllers
{
    //[Route("api/[action]/{id?}")]
    public class SurveysController : Controller
    {
    // Use Bandwidth.Net.Api interfaces via DI here if need
        [Route("api/[action]/{id}")]
        [HttpGet]
        public IActionResult survey(int id) {
            JSurvey survey = JSurveyBuilder.surveyObjectBuilder(id);            
            return Json(survey);            
        }

        [Route("api/[action]")]
        [HttpGet]
        public IActionResult surveys()
        {            
            List<Survey> sList = JSurveyBuilder.getSurveyList();
            return Json(sList);
        }

        [Route("api/[action]")]
        [HttpPost]
        public IActionResult surveys([FromBody] JSurvey s)
        {
            string name = s.surveyName;
            string phone = s.phoneNumber;
            
            Survey.addSurvey(FinishStartup.db, name, phone);
            int sId = Survey.getSurveyId(FinishStartup.db, name);

            List<JQuestion> questions = new List<JQuestion>();
            if (s.questions != null) {
                questions = s.questions;
                foreach (var q in questions) { 
                    //Debug.WriteLine(q.questionText);
                    Question.addQuestion(FinishStartup.db, sId, q.questionIndex, q.questionText);
                }
            }

            return Json(s); //want to return the id to the user
        }

        [Route("api/surveys/{id}/[action]")]
        [HttpPost]
        public IActionResult Questions(int id, [FromBody]List<JQuestion> questions)
        {
            
            foreach (var q in questions) { 
                //Debug.WriteLine(q.questionText);
                Question.addQuestion(FinishStartup.db, id, q.questionIndex, q.questionText);
            }
            

            return Json("Questions Added to survey"); 
        }

        [Route("api/surveys/{id}/[action]")]
        [HttpPost]
        public IActionResult PhoneNumbers(int id, [FromBody]List<string> numbers) {
            foreach (var n in numbers) {
                //send survey
                Debug.WriteLine(n);
            }
            FinishStartup.sendFirstQuestion(numbers, id);
            
            return Json(numbers);
        }

        [Route("surveys")]
        [HttpGet]
        public IActionResult createSurvey()
        {
            return View();
        }


    }
}