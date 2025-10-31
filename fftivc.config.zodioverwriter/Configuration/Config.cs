using fftivc.config.zodioverwriter.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System;
using System.ComponentModel;

namespace fftivc.config.zodioverwriter.Configuration
{
    // --- Enums ---
    public enum SpriteOption { Mobile, Original }
    public enum PortraitOption { Upscaled, Original }
    public enum PartyMenuColorOption { Original, Black, Blackara, Blackaga, White }

    // --- ENUM UPDATED HERE (ORDER CHANGED) ---
    public enum BattlePointerChoice
    {
        Removed,
        Original,
        PSX,
        Pink, // Moved here
        Purple,
        Red,
        Green,
        Cyan,
        Blue,
        White,
        Black
    }

    public enum BattleFrameOption { Removed, Vignette, Original }
    public enum UnitHighlightRingOption { Original, White, Red, Purple, Removed }
    public enum WorldMapOption { Original, Azure_and_Ivory }


    public class Config : Configurable<Config>
    {
        // --- Category: Asset Swaps ---

        [Category("Asset Swaps")]
        [DisplayName("Sprites")]
        [Description("Select which sprite style to use (Mobile or Original).")]
        [DefaultValue(SpriteOption.Mobile)]
        public SpriteOption SpritesOption { get; set; } = SpriteOption.Mobile;

        [Category("Asset Swaps")]
        [DisplayName("Portraits")]
        [Description("Select which portrait style to use (Upscaled or Original).")]
        [DefaultValue(PortraitOption.Original)]
        public PortraitOption PortraitsOption { get; set; } = PortraitOption.Original;

        // --- Category: UI & Colors ---

        [Category("UI & Colors")]
        [DisplayName("Party Menu Color")]
        [Description("Select the background color for the party menu.")]
        [DefaultValue(PartyMenuColorOption.Original)]
        public PartyMenuColorOption PartyMenuColorOption { get; set; } = PartyMenuColorOption.Original;

        [Category("UI & Colors")]
        [DisplayName("Unit Highlight Ring")]
        [Description("Choose the color of the unit's highlight ring for the party menu.")]
        [DefaultValue(UnitHighlightRingOption.Original)]
        public UnitHighlightRingOption UnitHighlightRingOption { get; set; } = UnitHighlightRingOption.Original;

        [Category("UI & Colors")]
        [DisplayName("Battle Pointer")]
        [Description("Choose which battle pointer to use.")]
        [DefaultValue(BattlePointerChoice.Removed)]
        public BattlePointerChoice BattlePointerOption { get; set; } = BattlePointerChoice.Removed;

        [Category("UI & Colors")]
        [DisplayName("Battle Frame")]
        [Description("Choose which battle frame to use.")]
        [DefaultValue(BattleFrameOption.Removed)]
        public BattleFrameOption BattleFrameOption { get; set; } = BattleFrameOption.Removed;

        // --- Category: World Map ---

        [Category("World Map")]
        [DisplayName("World Map")]
        [Description("Select the world map to use.")]
        [DefaultValue(WorldMapOption.Original)]
        public WorldMapOption WorldMap { get; set; } = WorldMapOption.Original;

        [Category("World Map")]
        [DisplayName("Disable World Map Blur")]
        [Description("Disables the blur effect on the world map.")]
        [DefaultValue(false)]
        public bool DisableWorldMapBlur { get; set; } = false;

        // --- Category: Filters ---

        [Category("Filters")]
        [DisplayName("Disable Menu Filter")]
        [Description("Disables the menu screen filter.")]
        [DefaultValue(true)]
        public bool DisableMenuFilter { get; set; } = true;

        [Category("Filters")]
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