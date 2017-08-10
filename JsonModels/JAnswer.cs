
namespace SMS_Example_Survey.JsonModels
{
    /// <summary>
    /// A JSON model for an answer
    /// </summary>
    public class JAnswer
    {
        public int answerId { get; set; }
        public string phoneNumber { get; set; }
        public string answerText { get; set; }
    }
}
