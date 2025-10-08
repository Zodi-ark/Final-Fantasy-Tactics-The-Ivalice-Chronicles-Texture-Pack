using System.ComponentModel;

namespace fftivc.asset.zoditexturepack.Configuration
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
        // Future overrides go here
    }
}
