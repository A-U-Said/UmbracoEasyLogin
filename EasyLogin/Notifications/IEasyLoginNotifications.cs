using Umbraco.Cms.Core.Security;

namespace EasyLogin.Notifications
{
    public interface IEasyLoginNotifications
    {
        Task SendFirstLoginNotificationEmail(BackOfficeIdentityUser newUser);
    }
}
