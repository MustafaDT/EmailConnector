

namespace JadwaEmailConnector.Application.Entites
{
    public class EmailRequest
    {
        public int EmailRequestId { get; set; }
        public string ToName { get; set; }
        public string ToAddress { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailFromName { get; set; }
        public string EmailSmtpHost { get; set; }
        public int EmailSmtpPort { get; set; }
        public bool EmailAuthenticate { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string? EmailCc { get; set; }
        public string? EmailBcc { get; set; }
        public char EmailType { get; set; }
        public char EmailStatus { get; set; }
        public bool IsResend { get; set; }
        public int ApplicationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastAttemptedDate { get; set; }
    }
}
