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
    public enum BattlePointerChoice
    {
        Removed,
        Original,
        PSX,
        Pink,
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
    public enum MapOption { Vibrant, Original }
    public enum BattleFilterOption { Original, Removed, Removed_Bright }
    public enum DirectionalWaitArrowOption
    {
        Original,
        Pink,
        Purple,
        Red,
        Green,
        Cyan,
        Blue,
        White,
        Black
    }

    // --- NEW ENUM ADDED HERE ---
    public enum UnitStatusHUDOption { Original, Minimal, Minimal_Blue_HP_Bar }


    public class Config : Configurable<Config>
    {
        // --- Category: Asset Swaps ---

        [Category("Asset Swaps")]
        [DisplayName("Sprites")]
        [Description("Select which sprite style to use.")]
        [DefaultValue(SpriteOption.Mobile)]
        public SpriteOption SpritesOption { get; set; } = SpriteOption.Mobile;

        [Category("Asset Swaps")]
        [DisplayName("Portraits")]
        [Description("Select which portrait style to use.")]
        [DefaultValue(PortraitOption.Original)]
        public PortraitOption PortraitsOption { get; set; } = PortraitOption.Original;

        [Category("Asset Swaps")]
        [DisplayName("Maps")]
        [Description("Select which map textures to use.")]
        [DefaultValue(MapOption.Vibrant)]
        public MapOption Maps { get; set; } = MapOption.Vibrant;

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

        // --- NEW PROPERTY ADDED HERE ---
        [Category("UI & Colors")]
        [DisplayName("Unit Status HUD")]
        [Description("Select the style for the unit status HUD (HP/MP bars).")]
        [DefaultValue(UnitStatusHUDOption.Minimal)]
        public UnitStatusHUDOption UnitStatusHUD { get; set; } = UnitStatusHUDOption.Minimal;

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

        [Category("UI & Colors")]
        [DisplayName("Directional Wait Arrow")]
        [Description("Choose the color of the directional selection arrows.")]
        [DefaultValue(DirectionalWaitArrowOption.Original)]
        public DirectionalWaitArrowOption DirectionalWaitArrow { get; set; } = DirectionalWaitArrowOption.Original;

        [Category("UI & Colors")]
        [DisplayName("Minimal Button Prompts")]
        [Description("Removes many UI button tooltip prompts.")]
        [DefaultValue(false)]
        public bool MinimalButtonPrompts { get; set; } = false;

        [Category("UI & Colors")]
        [DisplayName("Minimal Warnings")]
        [Description("Removes many warnings such as 'That tile cannot be targeted' and range warnings.")]
        [DefaultValue(false)]
        public bool MinimalWarnings { get; set; } = false;

        [Category("UI & Colors")]
        [DisplayName("Remove Text On Portraits")]
        [Description("Removes \"Enemy,\" \"Guest,\" \"Special,\" and \"Objective\" text from portraits.")]
        [DefaultValue(false)]
        public bool RemoveTextOnPortraits { get; set; } = false;

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
        [DisplayName("Battle Filter")]
        [Description("Select the battle screen filter style.")]
        [DefaultValue(BattleFilterOption.Removed)]
        public BattleFilterOption BattleFilter { get; set; } = BattleFilterOption.Removed;

        [Category("Filters")]
        [DisplayName("Remove Party Menu Filter")]
        [Description("Removes the party menu screen filter.")]
        [DefaultValue(true)]
        public bool RemovePartyMenuFilter { get; set; } = true;
    }

    public class ConfiguratorMixin : ConfiguratorMixinBase
    {
        // Reserved for future UI hooks
    }
}