using System;
using System.ComponentModel;

namespace fftivc.asset.zoditexturepack.Configuration
{
    public class Config : Configurable<Config>
    {
        [DisplayName("Disable Menu Filter")]
        [Description("Disables the menu screen filter.")]
        [DefaultValue(true)]
        public bool DisableMenuFilter { get; set; } = true;

        [DisplayName("Disable Battle Filters")]
        [Description("Disables the battle screen filters.")]
        [DefaultValue(true)]
        public bool DisableBattleFilter { get; set; } = true;
    }

    public class ConfiguratorMixin : ConfiguratorMixinBase
    {
        // Future overrides go here if needed
    }
}
