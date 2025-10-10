using fftivc.config.zodioverwriter.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System;
using System.ComponentModel;

namespace fftivc.config.zodioverwriter.Configuration
{
    public enum BattlePointerChoice
    {
        Removed,
        Original,
        Custom,
        PSX
    }

    public class Config : Configurable<Config>
    {
        [DisplayName("Battle Pointer Option")]
        [Description("Choose which battle pointer to use.")]
        [DefaultValue(BattlePointerChoice.Removed)]
        public BattlePointerChoice BattlePointerOption { get; set; } = BattlePointerChoice.Removed;

        [DisplayName("Disable Menu Filter")]
        [Description("Disables the menu screen filter.")]
        [DefaultValue(true)]
        public bool DisableMenuFilter { get; set; } = true;

        [DisplayName("Disable Battle Filter")]
        [Description("Disables the battle screen filter.")]
        [DefaultValue(true)]
        public bool DisableBattleFilter { get; set; } = true;
    }

    public class ConfiguratorMixin : ConfiguratorMixinBase
    {
        // Reserved for future UI hooks
    }
}
