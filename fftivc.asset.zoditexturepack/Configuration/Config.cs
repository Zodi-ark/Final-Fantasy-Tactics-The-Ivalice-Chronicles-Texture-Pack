using System;
using System.ComponentModel;

namespace fftivc.asset.zoditexturepack.Configuration
{
    public class Config : Configurable<Config>
    {
        [DisplayName("Disable Menu Filters")]
        [Description("Disables the menu screen filters.")]
        [DefaultValue(true)]
        public bool DisableMenuFilters { get; set; } = true;

        [DisplayName("Disable Battle Filter")]
        [Description("Disables the battle screen filter.")]
        [DefaultValue(true)]
        public bool DisableBattleFilter { get; set; } = true;
    }

    public class ConfiguratorMixin : ConfiguratorMixinBase
    {
        // Override methods here later if needed
    }
}
