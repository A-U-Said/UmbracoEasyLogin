using EasyLogin.Notifications;
using EasyLogin.Settings;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Web.BackOffice.Security;


namespace EasyLogin.ProviderOptions
{
    public class MicrosoftBackOfficeExternalLoginProviderOptions 
        :   ExternalLoginProviderOptionsBase<MicrosoftBackOfficeExternalLoginProviderOptions>, 
            IConfigureNamedOptions<BackOfficeExternalLoginProviderOptions>
    {
        private const string SchemeName = MicrosoftAccountDefaults.AuthenticationScheme;

        public MicrosoftBackOfficeExternalLoginProviderOptions(
            IOptions<EasyLoginSettings> easyLoginSettings,
            IEasyLoginNotifications easyLoginNotifications,
            ILogger<MicrosoftBackOfficeExternalLoginProviderOptions> logger
        ) : base(
            SchemeName,
            easyLoginSettings.Value,
            easyLoginSettings.Value.MicrosoftSettings,
            easyLoginNotifications,
            logger
        )
        {
        }

    }
}