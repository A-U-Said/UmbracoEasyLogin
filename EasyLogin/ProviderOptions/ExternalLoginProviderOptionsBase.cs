using EasyLogin.Notifications;
using EasyLogin.Settings;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core;
using Umbraco.Cms.Web.BackOffice.Security;


namespace EasyLogin.ProviderOptions
{
    public abstract class ExternalLoginProviderOptionsBase<TProviderOptions>
        where TProviderOptions : class, IConfigureNamedOptions<BackOfficeExternalLoginProviderOptions>
    {
        private readonly string _schemeName;
        private readonly EasyLoginSettings _easyLoginSettings;
        private readonly ILoginProviderSettings? _loginProviderSettings;
        private readonly IEasyLoginNotifications _easyLoginNotifications;
        private readonly ILogger<TProviderOptions> _logger;

        public ExternalLoginProviderOptionsBase(
            string schemeName,
            EasyLoginSettings easyLoginSettings,
            ILoginProviderSettings? loginProviderSettings,
            IEasyLoginNotifications easyLoginNotifications,
            ILogger<TProviderOptions> logger
        )
        {
            _schemeName = schemeName;
            _easyLoginSettings = easyLoginSettings;
            _loginProviderSettings = loginProviderSettings;
            _easyLoginNotifications = easyLoginNotifications;
            _logger = logger;
        }


        public void Configure(string? name, BackOfficeExternalLoginProviderOptions options)
        {
            ArgumentNullException.ThrowIfNull(name);

            if (name != Constants.Security.BackOfficeExternalAuthenticationTypePrefix + _schemeName)
            {
                return;
            }

            Configure(options);
        }


        public void Configure(BackOfficeExternalLoginProviderOptions options)
        {
            options.DenyLocalLogin = _easyLoginSettings.DenyLocalLogin;
            options.CustomBackOfficeView = _easyLoginSettings.CustomBackOfficeView;

            if (_loginProviderSettings == null)
            {
                _logger.LogError($"Login Provider Settings for {_schemeName} are not configured");
                return;
            }

            options.Icon = _loginProviderSettings.Icon;
            options.AutoRedirectLoginToExternalProvider = _loginProviderSettings.AutoRedirectLoginToExternalProvider;

            if (_loginProviderSettings.UserSettings == null)
            {
                _logger.LogError($"Login Provider User Settings for {_schemeName} are not configured");
                return;
            }

            options.AutoLinkOptions = new ExternalSignInAutoLinkOptions(
                autoLinkExternalAccount: _loginProviderSettings.UserSettings.AutoLinkExternalAccount,
                defaultUserGroups: _loginProviderSettings.UserSettings.DefaultUserGroups,
                defaultCulture: _loginProviderSettings.UserSettings.DefaultCulture,
                allowManualLinking: _loginProviderSettings.UserSettings.AllowManualLinking
            )
            {
                OnAutoLinking = async (autoLinkUser, loginInfo) =>
                {
                    autoLinkUser.IsApproved = _loginProviderSettings.UserSettings.Approved;
                    if (autoLinkUser.LastLoginDateUtc == null && _easyLoginSettings.NotificationSettings?.SendFirstLoginNotification == true)
                    {
                        await _easyLoginNotifications.SendFirstLoginNotificationEmail(autoLinkUser);
                    }
                },
                OnExternalLogin = (user, loginInfo) =>
                {
                    return true;
                }
            };
        }
    }
}
