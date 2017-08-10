using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SMS_Example_Survey.JsonModels;
using SMS_Example_Survey.JsonBuilders;
using System.Diagnostics;

namespace SMS_Example_Survey.Controllers
{
    public class SurveysController : Controller
    {
        /// <summary>
        /// Handles the GET request to return details on a given survey.
        /// </summary>
        /// <param name="id"> The id of the desired survey</param>
        /// <returns>A JSON representation of the survey details</returns>
        [Route("api/[action]/{id}")]
        [HttpGet]
        public IActionResult survey(int id) {
            JSurvey survey = JSurveyBuilder.surveyObjectBuilder(id);            
            return Json(survey);            
        }
        /// <summary>
        /// Handles the GET request to get a list of all surveys
        /// </summary>
        /// <returns>A JSON object containing the list of surveys</returns>
        [Route("api/[action]")]
        [HttpGet]
        public IActionResult surveys()
        {            
            List<Survey> sList = JSurveyBuilder.getSurveyList();
            return Json(sList);
        }
        /// <summary>
        /// Handles the POST request to add a new survey
        /// </summary>
        /// <param name="s">The JSON POST</param>
        /// <returns>The ID of the survey created</returns>
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
                    Question.addQuestion(FinishStartup.db, sId, q.questionIndex, q.questionText);
                }
            }

            return Json(s); //want to return the id to the user
        }
        /// <summary>
        /// Handles the POST request to add questions to a survey
        /// </summary>
        /// <param name="id"> The survey id to add the questions to</param>
        /// <param name="questions">The JSON POST</param>
        /// <returns>A success message</returns>
        [Route("api/surveys/{id}/[action]")]
        [HttpPost]
        public IActionResult Questions(int id, [FromBody]List<JQuestion> questions)
        {
            
            foreach (var q in questions) { 
                Question.addQuestion(FinishStartup.db, id, q.questionIndex, q.questionText);
            }
            

            return Json("Questions Added to survey"); 
        }
        /// <summary>
        /// Handles the POST request to send a survey to a list of phone numbers
        /// </summary>
        /// <param name="id">The id of the survey to add questions to</param>
        /// <param name="numbers">The list of phone numbers from POST</param>
        /// <returns>A JSON representation of the numbers</returns>
        [Route("api/surveys/{id}/[action]")]
        [HttpPost]
        public IActionResult PhoneNumbers(int id, [FromBody]List<string> numbers) {
            FinishStartup.sendFirstQuestion(numbers, id);           
            return Json(numbers);
        }
        /// <summary>
        /// Handles the GET request to display the surveys webpage
        /// </summary>
        /// <returns>The webpage</returns>
        [Route("surveys")]
        [HttpGet]
        public IActionResult addSurveys()
        {
            return View();
        }
        /// <summary>
        /// Handles the GET request to display the questions webpage
        /// </summary>
        /// <returns>The webpage</returns>
        [Route("addQuestions/{id}")]
        [HttpGet]
        public IActionResult addQuestions(int id)
        {
            return View();
        }
        /// <summary>
        /// Handles the GET request to display the numbers webpage
        /// </summary>
        /// <returns>The webpage</returns>
        [Route("addNumbers/{id}")]
        [HttpGet]
        public IActionResult addNumbers(int id)
        {
            return View();
        }

    }
}