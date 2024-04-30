using Umbraco.Cms.Core.Composing;
using EasyLogin.Settings;
using EasyLogin.Notifications;
using EasyLogin.Extensions;


namespace EasyLogin
{
    public class EasyLoginComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services
                .AddSingleton<IEasyLoginNotifications, EasyLoginNotifications>();

            builder.Services
                .AddOptions<EasyLoginSettings>()
                .BindConfiguration(EasyLoginConstants.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.ConfigureEasyLoginAuthentication();
        }
    }
}