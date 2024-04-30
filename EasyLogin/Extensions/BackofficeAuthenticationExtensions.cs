using EasyLogin.Data;
using EasyLogin.ProviderOptions;
using EasyLogin.Settings;
using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json;
using Umbraco.Cms.Web.BackOffice.Security;


namespace EasyLogin.Extensions
{
    public static class BackofficeAuthenticationExtensions
    {
        public static IUmbracoBuilder ConfigureEasyLoginAuthentication(this IUmbracoBuilder builder)
        {
            var easyLoginSettings = builder.Config
                .GetSection(EasyLoginConstants.SectionName)
                .Get<EasyLoginSettings>();

            if (easyLoginSettings == null)
            {
                return builder;
            }

            builder.Services.ConfigureIfSet<MicrosoftBackOfficeExternalLoginProviderOptions>(easyLoginSettings.MicrosoftSettings);
            builder.Services.ConfigureIfSet<GoogleBackOfficeExternalLoginProviderOptions>(easyLoginSettings.GoogleSettings);

            builder.AddBackOfficeExternalLogins(logins =>
            {
                logins.ConfigureExternalAuth(easyLoginSettings.MicrosoftSettings);
                logins.ConfigureExternalAuth(easyLoginSettings.GoogleSettings);               
            });

            return builder;
        }


        private static IServiceCollection ConfigureIfSet<TConfigureOptions>(
            this IServiceCollection services,
            ILoginProviderSettings? loginProviderOptions
        ) where TConfigureOptions : class
        {
            if (loginProviderOptions == null)
            {
                return services;
            }

            return services.ConfigureOptions<TConfigureOptions>();
        }


        private static BackOfficeExternalLoginsBuilder ConfigureExternalAuth(
            this BackOfficeExternalLoginsBuilder logins, 
            ILoginProviderSettings? loginProviderOptions
        )
        {
            if (loginProviderOptions == null)
            {
                return logins;
            }

            logins.AddBackOfficeLogin(backOfficeAuthenticationBuilder =>
            {
                var schemeName = backOfficeAuthenticationBuilder.SchemeForBackOffice(loginProviderOptions.SchemeName);
                ArgumentNullException.ThrowIfNull(schemeName);

                void oAuthOptions(OAuthOptions options)
                {
                    options.CallbackPath = loginProviderOptions.CallbackPath;
                    options.ClientId = loginProviderOptions.ClientId;
                    options.ClientSecret = loginProviderOptions.ClientSecret;
                    options.TokenEndpoint = loginProviderOptions.TokenEndpoint;
                    options.AuthorizationEndpoint = loginProviderOptions.AuthorizationEndpoint;
                    options.UserInformationEndpoint = loginProviderOptions.UserInformationEndpoint;
                }

                switch (loginProviderOptions.Provider)
                {
                    case ExternalLoginProvider.Microsoft:
                        backOfficeAuthenticationBuilder.AddMicrosoftAccount(
                            schemeName,
                            loginProviderOptions.DisplayName,
                            oAuthOptions
                        );
                        break;

                    case ExternalLoginProvider.Google:
                        backOfficeAuthenticationBuilder.AddGoogle(
                            schemeName,
                            loginProviderOptions.DisplayName,
                            oAuthOptions
                        );
                        break;

                    default:
                        break;
                };
            });

            return logins;
        }
    }
}
