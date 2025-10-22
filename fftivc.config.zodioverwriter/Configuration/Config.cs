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

    // --- ENUM UPDATED WITH NEW COLORS ---
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

    public class Config : Configurable<Config>
    {
        [DisplayName("Sprites Option")]
        [Description("Select which sprite style to use (Mobile or Original).")]
        [DefaultValue(SpriteOption.Mobile)]
        public SpriteOption SpritesOption { get; set; } = SpriteOption.Mobile;

        [DisplayName("Portraits Option")]
        [Description("Select which portrait style to use (Upscaled or Original).")]
        [DefaultValue(PortraitOption.Original)]
        public PortraitOption PortraitsOption { get; set; } = PortraitOption.Original;

        // --- CONFIG PROPERTY UPDATED WITH NEW DEFAULT ---
        [DisplayName("Party Menu Color")]
        [Description("Select the background color for the party/formation menu.")]
        [DefaultValue(PartyMenuColorOption.Original)] // Default changed to Original
        public PartyMenuColorOption PartyMenuColorOption { get; set; } = PartyMenuColorOption.Original; // Default changed to Original

        [DisplayName("Battle Pointer Option")]
        [Description("Choose which battle pointer to use.")]
        [DefaultValue(BattlePointerChoice.Removed)]
        public BattlePointerChoice BattlePointerOption { get; set; } = BattlePointerChoice.Removed;

        [DisplayName("Battle Frame Option")]
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