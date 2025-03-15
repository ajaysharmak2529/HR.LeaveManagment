namespace HR.LeaveManagement.Application.Models
{
    public class EmailSetting
    {
        public int SmtpPort { get; set; }
        public string SmtpServer { get; set; } = string.Empty;        
        public bool IsTcp { get; set; }
        public string FromName { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
