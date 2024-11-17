

namespace JadwaEmailConnector.Application.Dtos
{
    public class EmailRequestViewModel
    {
        public int EmailRequestId { get; set; }
        public string ToName { get; set; } = null!;
        public string ToAddress { get; set; } = null!;
        public string EmailFromAddress { get; set; } = null!;
        public string EmailFromName { get; set; } = null!;
        public string EmailSmtpHost { get; set; } = null!;
        public int EmailSmtpPort { get; set; }
        public bool EmailAuthenticate { get; set; }
        public string? EmailPassword { get; set; }
        public string EmailSubject { get; set; } = null!;
        public string EmailBody { get; set; } = null!;
        public string? EmailCc { get; set; }
        public string? EmailBcc { get; set; }
        public char? EmailType { get; set; }
        public char? EmailStatus { get; set; }
        public bool IsResend { get; set; }
        public int ApplicationId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastAttemptedDate { get; set; }

        public bool IsUrgent { get; set; }
    }
}
