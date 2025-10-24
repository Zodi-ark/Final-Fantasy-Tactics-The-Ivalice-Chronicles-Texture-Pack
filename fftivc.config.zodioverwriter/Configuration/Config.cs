using fftivc.config.zodioverwriter.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System;
using System.ComponentModel;

namespace fftivc.config.zodioverwriter.Configuration
{
    public enum SpriteOption
    {
        Mobile,
        Original
    }

    public enum PortraitOption
    {
        Upscaled,
        Original
    }

    public enum PartyMenuColorOption
    {
        Original,
        Black,
        Blackara,
        Blackaga,
        White
    }

    public enum BattlePointerChoice
    {
        Removed,
        Original,
        Custom,
        PSX
    }

    public enum BattleFrameOption
    {
        Removed,
        Vignette,
        Original
    }

    public enum UnitHighlightRingOption
    {
        Original,
        White,
        Red,
        Purple,
        Removed
    }

    public class Config : Configurable<Config>
    {
        [DisplayName("Sprites")]
        [Description("Select which sprite style to use (Mobile or Original).")]
        [DefaultValue(SpriteOption.Mobile)]
        public SpriteOption SpritesOption { get; set; } = SpriteOption.Mobile;

        [DisplayName("Portraits")]
        [Description("Select which portrait style to use (Upscaled or Original).")]
        [DefaultValue(PortraitOption.Original)]
        public PortraitOption PortraitsOption { get; set; } = PortraitOption.Original;

        [DisplayName("Party Menu Color")]
        [Description("Select the background color for the party/formation menu.")]
        [DefaultValue(PartyMenuColorOption.Original)]
        public PartyMenuColorOption PartyMenuColorOption { get; set; } = PartyMenuColorOption.Original;

        // --- MOVED PROPERTY BLOCK ---
        [DisplayName("Unit Highlight Ring")]
        [Description("Choose the color of the unit's highlight ring in battle.")]
        [DefaultValue(UnitHighlightRingOption.Original)]
        public UnitHighlightRingOption UnitHighlightRingOption { get; set; } = UnitHighlightRingOption.Original;
        // --- END OF MOVED BLOCK ---

        [DisplayName("Battle Pointer")]
        [Description("Choose which battle pointer to use.")]
        [DefaultValue(BattlePointerChoice.Removed)]
        public BattlePointerChoice BattlePointerOption { get; set; } = BattlePointerChoice.Removed;

        [DisplayName("Battle Frame")]
        [Description("Choose which battle frame to use.")]
        [DefaultValue(BattleFrameOption.Removed)]
        public BattleFrameOption BattleFrameOption { get; set; } = BattleFrameOption.Removed;

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