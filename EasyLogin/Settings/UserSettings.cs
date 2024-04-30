using System.ComponentModel;

namespace EasyLogin.Settings
{
    public class UserSettings
    {
        private const bool _autoLinkExternalAccount = true;
        private const bool _allowManualLinking = false;
        private const bool _approved = true;

        [DefaultValue(_autoLinkExternalAccount)]
        public bool AutoLinkExternalAccount { get; set; } = _autoLinkExternalAccount;

        public string[]? DefaultUserGroups { get; set; }

        public string? DefaultCulture { get; set; }

        [DefaultValue(_allowManualLinking)]
        public bool AllowManualLinking { get; set; } = _allowManualLinking;

        [DefaultValue(_approved)]
        public bool Approved { get; set; } = _approved;
    }
}
