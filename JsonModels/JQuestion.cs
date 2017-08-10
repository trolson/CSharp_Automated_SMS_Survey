using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS_Example_Survey.JsonModels
{
    /// <summary>
    /// A JSON model for an answer
    /// </summary>
    public class JQuestion
    {
        public int questionId { get; set; }
        public string questionText { get; set; }
        public int questionIndex { get; set; }
        public List<JAnswer> answers { get; set; }
    }
}
