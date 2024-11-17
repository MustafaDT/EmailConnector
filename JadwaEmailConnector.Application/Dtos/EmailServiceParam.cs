namespace JadwaEmailConnector.Application.Dtos
{
    public class EmailServiceParam
    {
        public string ToName { get; set; } = null!;
        public string ToAddress { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string EmailSmtpHost { get; set; } = null!;
        public int EmailSmtpPort { get; set; }
        public string EmailFromAddress { get; set; } = null!;
        public string? EmailPassword { get; set; } = null!;
        public string EmailFromName { get; set; } = null!;
        public string? EmailBcc { get; set; } = null!;
        public bool EmailAuthenticate { get; set; }

    }
}
