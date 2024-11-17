

namespace JadwaEmailConnector.Application.Entites
{
    public class EmailSendAttempt
    {
        public int EmailSendAttemptId { get; set; }
        public int EmailRequestId { get; set; }
        public string? ExceptionJson { get; set; }
        public string? Message { get; set; }
        public DateTime AttemptDate { get; set; }
        public char EmailStaus { get; set; }
       
    }
}
