using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using EasyLogin.Data;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;

namespace EasyLogin.Settings
{
    public class MicrosoftSettings: ILoginProviderSettings
    {
        private const string _icon = "icon-user";
        private const string _displayName = "Microsoft";
        private const bool _autoRedirectLoginToExternalProvider = false;
        private const string _callbackPath = "/signin-microsoft";
        private const string _tokenEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
        private const string _authorizationEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
        private const string _userInformationEndpoint = "https://graph.microsoft.com/v1.0/me";


        public ExternalLoginProvider Provider => ExternalLoginProvider.Microsoft;

        public string SchemeName => MicrosoftAccountDefaults.AuthenticationScheme;

        [DefaultValue(_icon)]
        public string Icon { get; set; } = _icon;

        [DefaultValue(_displayName)]
        public string DisplayName { get; set; } = _displayName;

        [DefaultValue(_autoRedirectLoginToExternalProvider)]
        public bool AutoRedirectLoginToExternalProvider { get; set; } = _autoRedirectLoginToExternalProvider;

        [Required]
        public string ClientId { get; set; } = string.Empty;

        [Required]
        public string ClientSecret { get; set; } = string.Empty;

        [DefaultValue(_callbackPath)]
        public string CallbackPath { get; set; } = _callbackPath;

        [DefaultValue(_tokenEndpoint)]
        public string TokenEndpoint { get; set; } = _tokenEndpoint;

        [DefaultValue(_authorizationEndpoint)]
        public string AuthorizationEndpoint { get; set; } = _authorizationEndpoint;

        [DefaultValue(_userInformationEndpoint)]
        public string UserInformationEndpoint { get; set; } = _userInformationEndpoint;

        public UserSettings? UserSettings { get; set; }
    }
}
