﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS_Example_Survey.JsonModels
{
    public class JSurvey
    {
        public int surveyId { get; set; }
        public string surveyName { get; set; }
        public string phoneNumber { get; set; }
        public List<JQuestion> questions { get; set; }

    }
}
