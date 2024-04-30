using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace EasyLogin.Settings
{
    public class NotificationSettings
    {
        private const bool _sendFirstLoginNotification = false;
        private const string _subject = "New External Account linked to Umbraco";

        [DefaultValue(_sendFirstLoginNotification)]
        public bool SendFirstLoginNotification { get; set; } = _sendFirstLoginNotification;

        [Required]
        public string To { get; set; } = string.Empty;

        [DefaultValue(_subject)]
        public string Subject { get; set; } = _subject;

        public string? Body { get; set; }
    }
}
