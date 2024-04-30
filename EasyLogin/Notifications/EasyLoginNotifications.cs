using Umbraco.Cms.Core.Mail;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Security;
using System.Text;
using Umbraco.Cms.Core.Models.Email;
using EasyLogin.Settings;


namespace EasyLogin.Notifications
{
    public class EasyLoginNotifications : IEasyLoginNotifications
    {
        private readonly ILogger<EasyLoginNotifications> _logger;
        private readonly IEmailSender _emailSender;
        private readonly string? _senderAddress;
        private readonly NotificationSettings? _notificationSettings;

        public EasyLoginNotifications(
            ILogger<EasyLoginNotifications> logger,
            IEmailSender emailSender,
            IOptions<GlobalSettings> globalSettings,
            IOptions<EasyLoginSettings> easyLoginSettings
        )
        {
            _logger = logger;
            _emailSender = emailSender;
            _senderAddress = globalSettings.Value.Smtp?.From;
            _notificationSettings = easyLoginSettings.Value.NotificationSettings;
        }

        public async Task SendFirstLoginNotificationEmail(BackOfficeIdentityUser newUser)
        {
            try
            {
                if (_notificationSettings == null)
                {
                    throw new Exception("No notification settings have been configured");
                }

                if (string.IsNullOrEmpty(_senderAddress))
                {
                    throw new Exception("No sender address has been configured");
                }

                var hasCustomBody = !string.IsNullOrEmpty(_notificationSettings.Body);
                var emailBody = new StringBuilder();

                emailBody.Append(hasCustomBody 
                    ? _notificationSettings.Body 
                    : $"<p>New account linked for {newUser.Email}.</p>"
                );

                var message = new EmailMessage(
                    _senderAddress, 
                    _notificationSettings.To, 
                    _notificationSettings.Subject, 
                    emailBody.ToString(),
                    !hasCustomBody
                );

                await _emailSender.SendAsync(message, emailType: "Contact");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when sending new user notification email");
            }
        }
    }
}
