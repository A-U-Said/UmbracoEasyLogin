using System.ComponentModel;


namespace EasyLogin.Settings
{
    public class EasyLoginSettings
    {
        private const bool _denyLocalLogin = false;


        /// <summary>
        ///     Get or set the option to deny local Umbraco accounts
        /// </summary>
        [DefaultValue(_denyLocalLogin)]
        public bool DenyLocalLogin { get; set; } = _denyLocalLogin;

        /// <summary>
        ///     Get or set the path to a custom backoffice view file
        /// </summary>
        public string? CustomBackOfficeView { get; set; }

        /// <summary>
        ///     Get or set the settings to configure Microsoft account login
        /// </summary>
        public MicrosoftSettings? MicrosoftSettings  { get; set; }

        /// <summary>
        ///     Get or set the settings to configure Google account login
        /// </summary>
        public GoogleSettings? GoogleSettings { get; set; }

        /// <summary>
        ///     Get or set the settings for notifications
        /// </summary>
        public NotificationSettings? NotificationSettings { get; set; }
    }
}
