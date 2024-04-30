using EasyLogin.Notifications;
using EasyLogin.Settings;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Web.BackOffice.Security;


namespace EasyLogin.ProviderOptions
{
    public class GoogleBackOfficeExternalLoginProviderOptions
        :   ExternalLoginProviderOptionsBase<GoogleBackOfficeExternalLoginProviderOptions>,
            IConfigureNamedOptions<BackOfficeExternalLoginProviderOptions>
    {
        private const string SchemeName = GoogleDefaults.AuthenticationScheme;

        public GoogleBackOfficeExternalLoginProviderOptions(
            IOptions<EasyLoginSettings> easyLoginSettings,
            IEasyLoginNotifications easyLoginNotifications,
            ILogger<GoogleBackOfficeExternalLoginProviderOptions> logger
        ) : base(
            SchemeName,
            easyLoginSettings.Value,
            easyLoginSettings.Value.GoogleSettings,
            easyLoginNotifications,
            logger
        )
        {
        }

    }
}
