using fftivc.config.zodioverwriter.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System.ComponentModel;

namespace fftivc.config.zodioverwriter.Configuration
{
    // --- Enums ---
    public enum SpriteOption { Original, Mobile }
    public enum PortraitOption { Original, Upscaled }

    public enum EquipmentOption
    {
        Original,
        Original_With_Drop_Shadow,
        PSX,
        PSX_With_Drop_Shadow
    }

    public enum PartyMenuColorOption
    {
        Original,
        Black,
        Blackara,
        Blackaga,
        Teal,
        Red,
        Purple,
        White,
        White_HDR
    }

    public enum BattlePointerChoice
    {
        Original,
        Removed,
        PSX,
        Pink,
        Purple,
        Purple_HDR,
        Red,
        Green,
        Cyan,
        Blue,
        White,
        Black,
        Tactics_Ogre,
        Tactics_Ogre_Reborn,
        Tactics_Ogre_Reborn_Alt,
        Tactics_Ogre_Reborn_Quil,
        Tactics_Ogre_Reborn_Quil_Alt
    }

    public enum BattleFrameOption { Original, Removed, Vignette }
    public enum UnitSelectFrameOption { Original, Removed }

    public enum CursorFingerOption
    {
        Original,
        PSX,
        Black,
        Crisis_Core,
        Crisis_Core_Upscaled,
        Dissidia,
        Dissidia_Upscaled,
        Tactics_Ogre,
        Tactics_Ogre_Upscaled,
        Tactics_Ogre_Reborn
    }

    public enum UnitHighlightRingOption
    {
        Original,
        Minimal_Original,
        Minimal_Removed,
        Minimal_Purple,
        Minimal_White,
        Minimal_Red
    }

    public enum UnitShiftArrowOption { Original, Removed, Greyscale }

    public enum WorldMapOption { Original, Azure_and_Ivory }
    public enum MapOption { Original, Vibrant }
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
        Black,
        Tactics_Ogre,
        Tactics_Ogre_Greyscale,
        Tactics_Ogre_Alt
    }

    public enum UnitStatusHUDOption { Original, Minimal, Minimal_Blue_HP_Bar, Minimal_PSX }
    public enum StatusIconsOption { Original, Greyscale, Minimal, Minimal_Greyscale, PSX, PSX_Dark }

    public enum ZodiacIconsOption
    {
        Original,
        Original_Alt,
        Original_Alt_Vibrant,
        Original_Alt_Greyscale,
        Auracite,
        Auracite_Pixelated,
        Auracite_Pixelated_Extra,
        Auracite_Glowing,
        Auracite_Glowing_Pixelated,
        Auracite_Glowing_Pixelated_Extra,
        Gold,
        Gold_Pixelated,
        Gold_Pixelated_Extra
    }

    public enum SpeechBubbleOption { Original, PSX_Upscaled }
    public enum SpeechBubbleTypefaceOption { Original, Old_English }


    public class Config : Configurable<Config>
    {
        // ========================================================================
        // ASSET SWAPS
        // ========================================================================

        [Category("Asset Swaps")]
        [DisplayName("Maps")]
        [Description("Select which map textures to use.")]
        [DefaultValue(MapOption.Vibrant)]
        public MapOption Maps { get; set; } = MapOption.Vibrant;

        [Category("Asset Swaps")]
        [DisplayName("Portraits")]
        [Description("Select which portrait style to use.")]
        [DefaultValue(PortraitOption.Original)]
        public PortraitOption PortraitsOption { get; set; } = PortraitOption.Original;

        [Category("Asset Swaps")]
        [DisplayName("Sprites")]
        [Description("Select which sprite style to use.")]
        [DefaultValue(SpriteOption.Mobile)]
        public SpriteOption SpritesOption { get; set; } = SpriteOption.Mobile;

        // ========================================================================
        // WORLD MAP
        // ========================================================================

        [Category("World Map")]
        [DisplayName("Disable World Map Blur")]
        [Description("Disables the blur effect on the world map.")]
        [DefaultValue(false)]
        public bool DisableWorldMapBlur { get; set; } = false;

        [Category("World Map")]
        [DisplayName("World Map")]
        [Description("Select the world map to use.")]
        [DefaultValue(WorldMapOption.Original)]
        public WorldMapOption WorldMap { get; set; } = WorldMapOption.Original;

        // ========================================================================
        // DIALOG
        // ========================================================================

        [Category("Dialog")]
        [DisplayName("Speech Bubble Style")]
        [Description("Select the style for speech bubbles.")]
        [DefaultValue(SpeechBubbleOption.Original)]
        public SpeechBubbleOption SpeechBubble { get; set; } = SpeechBubbleOption.Original;

        [Category("Dialog")]
        [DisplayName("Speech Bubble Typeface")]
        [Description("Select the typeface style for speech bubbles.")]
        [DefaultValue(SpeechBubbleTypefaceOption.Original)]
        public SpeechBubbleTypefaceOption SpeechBubbleTypeface { get; set; } = SpeechBubbleTypefaceOption.Original;

        // ========================================================================
        // UI & COLORS
        // ========================================================================

        [Category("UI & Colors")]
        [DisplayName("Directional Wait Arrows")]
        [Description("Choose the color or style of the directional selection arrows.")]
        [DefaultValue(DirectionalWaitArrowOption.Original)]
        public DirectionalWaitArrowOption DirectionalWaitArrow { get; set; } = DirectionalWaitArrowOption.Original;

        [Category("UI & Colors")]
        [DisplayName("Battle Frame")]
        [Description("Choose which battle frame to use.")]
        [DefaultValue(BattleFrameOption.Removed)]
        public BattleFrameOption BattleFrameOption { get; set; } = BattleFrameOption.Removed;

        [Category("UI & Colors")]
        [DisplayName("Battle Pointer")]
        [Description("Choose which battle pointer to use.")]
        [DefaultValue(BattlePointerChoice.Removed)]
        public BattlePointerChoice BattlePointerOption { get; set; } = BattlePointerChoice.Removed;

        [Category("UI & Colors")]
        [DisplayName("Unit Select Frame")]
        [Description("Select the option for the frame surrounding the unit when selecting them for battle.")]
        [DefaultValue(UnitSelectFrameOption.Removed)]
        public UnitSelectFrameOption UnitSelectFrame { get; set; } = UnitSelectFrameOption.Removed;

        [Category("UI & Colors")]
        [DisplayName("Unit Shift Arrows")]
        [Description("Select the style for the unit shift arrow on the unit status and job page.")]
        [DefaultValue(UnitShiftArrowOption.Original)]
        public UnitShiftArrowOption UnitShiftArrow { get; set; } = UnitShiftArrowOption.Original;

        [Category("UI & Colors")]
        [DisplayName("Unit Status HUD")]
        [Description("Select the style for the unit status HUD (HP/MP bars).")]
        [DefaultValue(UnitStatusHUDOption.Minimal)]
        public UnitStatusHUDOption UnitStatusHUD { get; set; } = UnitStatusHUDOption.Minimal;

        [Category("UI & Colors")]
        [DisplayName("Party Menu Style & Ring")]
        [Description("This setting changes the style of the party menu and also changes the color of the unit highlight ring.")]
        [DefaultValue(UnitHighlightRingOption.Original)]
        public UnitHighlightRingOption UnitHighlightRingOption { get; set; } = UnitHighlightRingOption.Original;

        [Category("UI & Colors")]
        [DisplayName("Party Menu Color")]
        [Description("Select the background color for the party menu.")]
        [DefaultValue(PartyMenuColorOption.Original)]
        public PartyMenuColorOption PartyMenuColorOption { get; set; } = PartyMenuColorOption.Original;

        [Category("UI & Colors")]
        [DisplayName("Cursor Finger")]
        [Description("Select the style for the cursor finger.")]
        [DefaultValue(CursorFingerOption.Original)]
        public CursorFingerOption CursorFinger { get; set; } = CursorFingerOption.Original;

        // ========================================================================
        // INTERFACE TOGGLES
        // ========================================================================

        [Category("Interface Toggles")]
        [DisplayName("Minimal Warnings")]
        [Description("Removes many warnings such as 'That tile cannot be targeted' and range warnings.")]
        [DefaultValue(false)]
        public bool MinimalWarnings { get; set; } = false;

        [Category("Interface Toggles")]
        [DisplayName("Minimal Button Prompts")]
        [Description("Removes many UI button tooltip prompts.")]
        [DefaultValue(false)]
        public bool MinimalButtonPrompts { get; set; } = false;

        [Category("Interface Toggles")]
        [DisplayName("Remove Text On Portraits")]
        [Description("Removes \"Enemy,\" \"Guest,\" \"Special,\"  \"Auto,\" and \"Objective\" text from portraits and sprites.")]
        [DefaultValue(false)]
        public bool RemoveTextOnPortraits { get; set; } = false;

        // ========================================================================
        // ICONS
        // ========================================================================

        [Category("Icons")]
        [DisplayName("Status Icons")]
        [Description("Select the style for status effect icons.")]
        [DefaultValue(StatusIconsOption.Original)]
        public StatusIconsOption StatusIcons { get; set; } = StatusIconsOption.Original;

        [Category("Icons")]
        [DisplayName("Zodiac Icons")]
        [Description("Select the style for zodiac sign icons.")]
        [DefaultValue(ZodiacIconsOption.Original)]
        public ZodiacIconsOption ZodiacIcons { get; set; } = ZodiacIconsOption.Original;

        [Category("Icons")]
        [DisplayName("Equipment Icons")]
        [Description("Select the style for equipment icons.")]
        [DefaultValue(EquipmentOption.Original)]
        public EquipmentOption EquipmentIcons { get; set; } = EquipmentOption.Original;

        // ========================================================================
        // FILTERS
        // ========================================================================

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