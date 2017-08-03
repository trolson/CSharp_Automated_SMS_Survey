using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS_Example_Survey.JsonModels
{
    public class JAnswer
    {
        public int answerId { get; set; }
        public string phoneNumber { get; set; }
        public string answerText { get; set; }
    }
}
