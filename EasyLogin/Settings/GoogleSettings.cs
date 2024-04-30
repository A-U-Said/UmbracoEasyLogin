using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using EasyLogin.Data;
using Microsoft.AspNetCore.Authentication.Google;


namespace EasyLogin.Settings
{
    public class GoogleSettings : ILoginProviderSettings
    {
        private const string _icon = "icon-user";
        private const string _displayName = "Google";
        private const bool _autoRedirectLoginToExternalProvider = false;
        private const string _callbackPath = "/signin-google";
        private const string _tokenEndpoint = "https://oauth2.googleapis.com/token";
        private const string _authorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string _userInformationEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";


        public ExternalLoginProvider Provider => ExternalLoginProvider.Google;

        public string SchemeName => "Google";

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
