using EasyLogin.Data;


namespace EasyLogin.Settings
{
    public interface ILoginProviderSettings
    {
        ExternalLoginProvider Provider { get; }
        string SchemeName { get; }

        /// <summary>
        ///     Get or set the icon to display in the backoffice login page
        /// </summary>
        string Icon { get; set; }
        string DisplayName { get; set; }
        bool AutoRedirectLoginToExternalProvider { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string CallbackPath { get; set; }
        string TokenEndpoint { get; set; }
        string AuthorizationEndpoint { get; set; }
        string UserInformationEndpoint { get; set; }
        UserSettings? UserSettings { get; set; }
    }
}
