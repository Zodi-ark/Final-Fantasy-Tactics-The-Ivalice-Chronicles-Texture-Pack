using fftivc.config.zodioverwriter.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations; // Required for [Display]

namespace fftivc.config.zodioverwriter.Configuration
{
    // --- Enums ---
    public enum SpriteOption
    {
        Original,
        Mobile
    }

    public enum PortraitOption
    {
        Original,
        Upscaled
    }

    public enum EquipmentOption
    {
        Original,

        [Display(Name = "Original — Drop Shadow")]
        Original_With_Drop_Shadow,

        PSX,

        [Display(Name = "PSX — Drop Shadow")]
        PSX_With_Drop_Shadow
    }

    public enum PartyMenuColorOption
    {
        Original,

        [Display(Name = "Black — 2x (High Detail)")]
        blackhd,

        Black,
        Blackara,
        Blackaga,
        Teal,
        Red,
        Purple,
        Blue,
        White,

        [Display(Name = "Marble")]
        White_HDR
    }

    public enum BattlePointerChoice
    {
        Original,
        Removed,
        PSX,
        Pink,
        Purple,

        [Display(Name = "Purple — HDR")]
        Purple_HDR,

        Red,
        Green,
        Cyan,
        Blue,
        White,
        Black,

        [Display(Name = "Tactics Ogre")]
        Tactics_Ogre,

        [Display(Name = "Tactics Ogre Reborn")]
        Tactics_Ogre_Reborn,

        [Display(Name = "Tactics Ogre Reborn Alt")]
        Tactics_Ogre_Reborn_Alt,

        [Display(Name = "Tactics Ogre Reborn Quil")]
        Tactics_Ogre_Reborn_Quil,

        [Display(Name = "Tactics Ogre Reborn Quil Alt")]
        Tactics_Ogre_Reborn_Quil_Alt
    }

    public enum BattleFrameOption { Original, Removed, Vignette, Letterbox }
    public enum UnitSelectFrameOption { Original, Removed }

    public enum CursorFingerOption
    {
        Original,
        PSX,
        Greyscale,

        [Display(Name = "Final Fantasy VIII")]
        Final_Fantasy_VIII,

        [Display(Name = "Crisis Core")]
        Crisis_Core,

        [Display(Name = "Crisis Core — Upscaled")]
        Crisis_Core_Upscaled,

        Dissidia,

        [Display(Name = "Dissidia — Upscaled")]
        Dissidia_Upscaled,

        [Display(Name = "Tactics Ogre")]
        Tactics_Ogre,

        [Display(Name = "Tactics Ogre — Upscaled")]
        Tactics_Ogre_Upscaled,

        [Display(Name = "Tactics Ogre Reborn")]
        Tactics_Ogre_Reborn,

        [Display(Name = "Octopath Traveler")]
        Octopath_Traveler
    }

    public enum UnitHighlightRingOption
    {
        Original,

        [Display(Name = "Minimal Style & Original Ring")]
        Minimal_Original,

        [Display(Name = "Minimal Style & Removed Ring")]
        Minimal_Removed,

        [Display(Name = "Minimal Style & Purple Ring")]
        Minimal_Purple,

        [Display(Name = "Minimal Style & White Ring")]
        Minimal_White,

        [Display(Name = "Minimal Style & Red Ring")]
        Minimal_Red
    }

    public enum UnitShiftArrowOption
    {
        Original,

        [Display(Name = "No Arrows & Ring")]
        Removed_Arrow_Removed_Job_Circle,

        [Display(Name = "No Arrows & Ring (White Job Highlight)")]
        Removed_Arrow_Removed_Job_Circle_White_Job_Highlight,

        [Display(Name = "No Arrows & Ring (Purple Job Highlight)")]
        Removed_Arrow_Removed_Job_Circle_Purple_Job_Highlight,

        [Display(Name = "No Arrows & Ring (Red Job Highlight)")]
        Removed_Arrow_Removed_Job_Circle_Red_Job_Highlight,

        [Display(Name = "Original Arrows & Removed Job Ring")]
        Original_Arrow_Removed_Job_Circle,

        [Display(Name = "Removed Arrows & Original Job Ring")]
        Removed,

        [Display(Name = "Greyscale Arrows & Original Job Ring")]
        Greyscale,

        [Display(Name = "Greyscale Arrows & Removed Job Ring")]
        Greyscale_Arrow_Removed_Job_Circle,
    }

    public enum WorldMapOption
    {
        Original,

        [Display(Name = "Azure and Ivory")]
        Azure_and_Ivory
    }

    public enum MapOption { Original, Vibrant }

    public enum BattleFilterOption
    {
        Original,
        Removed,

        [Display(Name = "Removed — Bright")]
        Removed_Bright
    }

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

        [Display(Name = "Tactics Ogre")]
        Tactics_Ogre,

        [Display(Name = "Tactics Ogre — Greyscale")]
        Tactics_Ogre_Greyscale,

        [Display(Name = "Tactics Ogre Alt")]
        Tactics_Ogre_Alt
    }

    public enum UnitStatusHUDOption
    {
        Original,
        Minimal,

        [Display(Name = "Minimal — Smooth Gradient")]
        Minimal_Gradient,

        [Display(Name = "Minimal — Blue HP Bar")]
        Minimal_Blue_HP_Bar,

        [Display(Name = "Minimal — PSX")]
        Minimal_PSX
    }

    public enum StatusIconsOption
    {
        Original,
        Greyscale,
        Minimal,

        [Display(Name = "Minimal — Greyscale")]
        Minimal_Greyscale,

        PSX,

        [Display(Name = "PSX — Dark")]
        PSX_Dark
    }

    public enum ZodiacIconsOption
    {
        Original,

        Removed,

        [Display(Name = "Minimal")]
        Original_Alt,

        [Display(Name = "Minimal — Vibrant")]
        Original_Alt_Vibrant,

        [Display(Name = "Minimal — Greyscale")]
        Original_Alt_Greyscale,

        Auracite,

        [Display(Name = "Auracite — Pixelated")]
        Auracite_Pixelated,

        [Display(Name = "Auracite — Pixelated (Extra)")]
        Auracite_Pixelated_Extra,

        [Display(Name = "Auracite Glowing")]
        Auracite_Glowing,

        [Display(Name = "Auracite Glowing — Pixelated")]
        Auracite_Glowing_Pixelated,

        [Display(Name = "Auracite Glowing — Pixelated (Extra)")]
        Auracite_Glowing_Pixelated_Extra,

        Gold,

        [Display(Name = "Gold — Pixelated")]
        Gold_Pixelated,

        [Display(Name = "Gold — Pixelated (Extra)")]
        Gold_Pixelated_Extra
    }

    public enum SpeechBubbleOption
    {
        Original,

        [Display(Name = "PSX Upscaled")]
        PSX_Upscaled
    }

    public enum SpeechBubbleTypefaceOption
    {
        Original,

        [Display(Name = "Old English")]
        Old_English
    }

    public enum BleedOutHeartsAndTurnGlowOption
    {
        Original,
        Minimal,

        [Display(Name = "Minimal — Greyscale")]
        Minimal_Greyscale
    }

    public enum UnitFavoriteTileAndFlagOption
    {
        Original,
        Teal,
        Purple,
        Green,
        Pink,
        Blue,
        Greyscale,

        [Display(Name = "Greyscale — Bright")]
        Greyscale_Bright,

        [Display(Name = "Greyscale — Dark")]
        Greyscale_Dark,

        [Display(Name = "Original — Vibrant")]
        Original_Vibrant,

        Gold,

        [Display(Name = "PSX Red")]
        PSX_Red,

        [Display(Name = "PSX Green")]
        PSX_Green,

        [Display(Name = "PSX Pink")]
        PSX_Pink,

        [Display(Name = "PSX Gold")]
        PSX_Gold,

        [Display(Name = "Gold Slate")]
        Gold_Slate,

        [Display(Name = "Gold Brick")]
        Gold_Brick
    }

    public enum UnitTileOption
    {
        Original,
        Teal,
        Purple,
        Red,
        Green,
        Pink,
        Blue,
        Azure,
        White,
        Greyscale,

        [Display(Name = "Greyscale — Bright")]
        Greyscale_Bright,

        [Display(Name = "Greyscale — Dark")]
        Greyscale_Dark,

        [Display(Name = "Greyscale — Smooth")]
        Greyscale_Smooth,

        Gold,

        PSX,

        [Display(Name = "PSX Bright")]
        PSX_Bright
    }

    public enum TurnAndHUDTypefaceOption
    {
        Original,

        [Display(Name = "Old English")]
        Old_English
    }

    public enum MoveTilesOption
    {
        Original,
        Cyanobacteria,
        Green,

        [Display(Name = "Green — Vibrant")]
        Green_Vibrant,

        Teal,
        Purple,
        Pink,
        Gold,

        [Display(Name = "Gold — Alt")]
        Gold_Alt,

        Red,
        Orange,
        White,
        Black
    }

    public enum MoveTilesEnemyOption
    {
        Original,
        Cyanobacteria,
        Green,

        [Display(Name = "Green — Vibrant")]
        Green_Vibrant,

        Teal,
        Purple,
        Pink,
        Gold,

        [Display(Name = "Gold — Alt")]
        Gold_Alt,

        Red,
        Orange,
        White,
        Black
    }

    public enum AttackTargetOption
    {
        Original,
        Green,
        Teal,
        Purple,
        Pink,
        Gold,
        Red,
        White,
        Black
    }

    public enum AttackRangeOption
    {
        Original,
        Green,
        Teal,
        Purple,

        [Display(Name = "Purple — Vibrant")]
        Purple_Vibrant,

        Pink,
        Gold,
        Red,
        White,
        Black
    }

    public enum CommandHUDAndInfoPanelsOption
    {
        Original,
        Silver,
        Pink
    }

    public enum UIGlowAndEquipDisplayOption
    {
        Original,

        [Display(Name = "Silver Display Only")]
        Silver_Display_Only,

        [Display(Name = "Silver Display + Nearby Glow")]
        Silver_Display_Nearby_Glow,

        [Display(Name = "Silver Display + Full UI Glow")]
        Silver_Display_All_Glow,

        [Display(Name = "Pink Display Only")]
        Pink_Display_Only,

        [Display(Name = "Pink Display + Nearby Glow")]
        Pink_Display_Nearby_Glow,

        [Display(Name = "Pink Display + Full UI Glow")]
        Pink_Display_All_Glow
    }

    public enum MenuHighlightColorOption
    {
        Original,
        Red
    }

    public enum IntroLogoOneOption
    {
        Original,

        [Display(Name = "Job Outline")]
        Job_Outline
    }

    public enum IntroLogoTwoOption
    {
        Original,

        [Display(Name = "TIC Key Art")]
        TIC_Key_Art,

        [Display(Name = "WotL Love Art")]
        WotL_Love_Art,

        [Display(Name = "Zodiac Grey")]
        Zodiac_Grey,

        [Display(Name = "Zodiac Yellow")]
        Zodiac_Yellow
    }

    // Fixed: Renamed to match the pattern
    public enum IntroLogoThreeOption
    {
        Original,

        [Display(Name = "FFT Japan")]
        FFT_Japan
    }

    public class Config : Configurable<Config>
    {
        // ========================================================================
        // GLOBAL
        // ========================================================================

        [Category("Global")]
        [DisplayName("Cursor Finger")]
        [Description("Select the style for the cursor finger.")]
        [DefaultValue(CursorFingerOption.Original)]
        public CursorFingerOption CursorFinger { get; set; } = CursorFingerOption.Original;

        [Category("Global")]
        [DisplayName("Sprites")]
        [Description("Select which sprite style to use.")]
        [DefaultValue(SpriteOption.Original)]
        public SpriteOption SpritesOption { get; set; } = SpriteOption.Original;

        [Category("Global")]
        [DisplayName("Portraits")]
        [Description("Select which portrait style to use.")]
        [DefaultValue(PortraitOption.Original)]
        public PortraitOption PortraitsOption { get; set; } = PortraitOption.Original;

        [Category("Global")]
        [DisplayName("Unit Status HUD")]
        [Description("Select the style for the unit status HUD (HP/MP bars).")]
        [DefaultValue(UnitStatusHUDOption.Original)]
        public UnitStatusHUDOption UnitStatusHUD { get; set; } = UnitStatusHUDOption.Original;

        [Category("Global")]
        [DisplayName("HUD Secondary Typeface")]
        [Description("Select the typeface for the turn counter, detailed statistics & secondary elements on the HUD: EXP, LV, Next, and JP including their respective number value.")]
        [DefaultValue(TurnAndHUDTypefaceOption.Original)]
        public TurnAndHUDTypefaceOption TurnAndHUDTypeface { get; set; } = TurnAndHUDTypefaceOption.Original;

        [Category("Global")]
        [DisplayName("Command HUD & Info Panels")]
        [Description("Select the Command HUD (Move, Abilities, Wait, Status) and Info Panels used for equipment and ability descriptions, Poach alerts, and Treasure Hunt alerts.")]
        [DefaultValue(CommandHUDAndInfoPanelsOption.Original)]
        public CommandHUDAndInfoPanelsOption CommandHUDAndInfoPanels { get; set; } = CommandHUDAndInfoPanelsOption.Original;

        [Category("Global")]
        [DisplayName("UI Glow & Equip Display")]
        [Description("Controls glowing UI accents throughout the interface, along with the background texture for equipped equipment and abilities.")]
        [DefaultValue(UIGlowAndEquipDisplayOption.Original)]
        public UIGlowAndEquipDisplayOption UIGlowAndEquipDisplay { get; set; } = UIGlowAndEquipDisplayOption.Original;

        [Category("Global")]
        [DisplayName("Menu Highlight Color")]
        [Description("Select the color of the highlight shown on the selected menu item at the command HUD, equipment/ability screens, and other menus.")]
        [DefaultValue(MenuHighlightColorOption.Original)]
        public MenuHighlightColorOption MenuHighlightColor { get; set; } = MenuHighlightColorOption.Original;

        [Category("Global")]
        [DisplayName("Minimal Button Prompts")]
        [Description("Removes many UI button tooltip prompts.")]
        [DefaultValue(false)]
        public bool MinimalButtonPrompts { get; set; } = false;

        [Category("Global")]
        [DisplayName("Minimal Warnings")]
        [Description("Removes many warnings such as 'That tile cannot be targeted' and range warnings.")]
        [DefaultValue(false)]
        public bool MinimalWarnings { get; set; } = false;


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
        // WORLD MAP
        // ========================================================================

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


        // ========================================================================
        // FILTERS
        // ========================================================================

        [Category("Filters")]
        [DisplayName("Battle Filter")]
        [Description("Select the battle screen filter style.")]
        [DefaultValue(BattleFilterOption.Original)]
        public BattleFilterOption BattleFilter { get; set; } = BattleFilterOption.Original;

        [Category("Filters")]
        [DisplayName("Remove Party Menu Filter")]
        [Description("Removes the party menu screen filter.")]
        [DefaultValue(false)]
        public bool RemovePartyMenuFilter { get; set; } = false;


        // ========================================================================
        // BATTLE
        // ========================================================================

        [Category("Battle")]
        [DisplayName("Remove Text On Portraits")]
        [Description("Removes \"Enemy,\" \"Guest,\" \"Special,\"  \"Auto,\" and \"Objective\" text from portraits and sprites.")]
        [DefaultValue(false)]
        public bool RemoveTextOnPortraits { get; set; } = false;

        [Category("Battle")]
        [DisplayName("Bleed Out Hearts & Turn Glow")]
        [Description("Select the style of the heart counter over KO'ed units and the gold glow over the turn counter.")]
        [DefaultValue(BleedOutHeartsAndTurnGlowOption.Original)]
        public BleedOutHeartsAndTurnGlowOption BleedOutHeartsAndTurnGlow { get; set; } = BleedOutHeartsAndTurnGlowOption.Original;

        [Category("Battle")]
        [DisplayName("Directional Wait Arrows")]
        [Description("Choose the color or style of the directional selection arrows.")]
        [DefaultValue(DirectionalWaitArrowOption.Original)]
        public DirectionalWaitArrowOption DirectionalWaitArrow { get; set; } = DirectionalWaitArrowOption.Original;

        [Category("Battle")]
        [DisplayName("Attack Target Tiles")]
        [Description("Select the color style for the attack target tiles.")]
        [DefaultValue(AttackTargetOption.Original)]
        public AttackTargetOption AttackTarget { get; set; } = AttackTargetOption.Original;

        [Category("Battle")]
        [DisplayName("Attack Range Tiles")]
        [Description("Select the color style for the attack range tiles.")]
        [DefaultValue(AttackRangeOption.Original)]
        public AttackRangeOption AttackRange { get; set; } = AttackRangeOption.Original;

        [Category("Battle")]
        [DisplayName("Enemy Move Tiles")]
        [Description("Select the color style for the enemy movement range tiles in battle.")]
        [DefaultValue(MoveTilesEnemyOption.Original)]
        public MoveTilesEnemyOption MoveTilesEnemy { get; set; } = MoveTilesEnemyOption.Original;

        [Category("Battle")]
        [DisplayName("Move Tiles")]
        [Description("Select the color style for the movement range tiles in battle.")]
        [DefaultValue(MoveTilesOption.Original)]
        public MoveTilesOption MoveTiles { get; set; } = MoveTilesOption.Original;

        [Category("Battle")]
        [DisplayName("Battle Frame")]
        [Description("Choose which frame to use that appears at the top and bottom of the screen during battle.")]
        [DefaultValue(BattleFrameOption.Original)]
        public BattleFrameOption BattleFrameOption { get; set; } = BattleFrameOption.Original;

        [Category("Battle")]
        [DisplayName("Battle Pointer")]
        [Description("Choose which battle pointer to use.")]
        [DefaultValue(BattlePointerChoice.Original)]
        public BattlePointerChoice BattlePointerOption { get; set; } = BattlePointerChoice.Original;

        [Category("Battle")]
        [DisplayName("Unit Select Frame")]
        [Description("Select the option for the frame surrounding the unit when selecting them for battle.")]
        [DefaultValue(UnitSelectFrameOption.Original)]
        public UnitSelectFrameOption UnitSelectFrame { get; set; } = UnitSelectFrameOption.Original;

        [Category("Battle")]
        [DisplayName("Maps")]
        [Description("Select which map textures to use.")]
        [DefaultValue(MapOption.Original)]
        public MapOption Maps { get; set; } = MapOption.Original;

        // ========================================================================
        // PARTY MENU
        // ========================================================================

        [Category("Party Menu")]
        [DisplayName("Shift Arrows & Job Highlights")]
        [Description("Select the style for the unit shift arrows on the unit status and jobs page, as well as the yellow highlight ring below units and the job highlight color on the jobs page. ")]
        [DefaultValue(UnitShiftArrowOption.Original)]
        public UnitShiftArrowOption UnitShiftArrow { get; set; } = UnitShiftArrowOption.Original;

        [Category("Party Menu")]
        [DisplayName("Unit Tile")]
        [Description("Select the style for the tile units stand on.")]
        [DefaultValue(UnitTileOption.Original)]
        public UnitTileOption UnitTile { get; set; } = UnitTileOption.Original;

        [Category("Party Menu")]
        [DisplayName("Favorite Unit Tile & Flag")]
        [Description("Select the style for the tile favorited units stand on and the flag over them.")]
        [DefaultValue(UnitFavoriteTileAndFlagOption.Original)]
        public UnitFavoriteTileAndFlagOption UnitFavoriteTileAndFlag { get; set; } = UnitFavoriteTileAndFlagOption.Original;

        [Category("Party Menu")]
        [DisplayName("Party Menu Style & Ring Color")]
        [Description("This setting changes the style of the party menu and also changes the color of the unit highlight ring.")]
        [DefaultValue(UnitHighlightRingOption.Original)]
        public UnitHighlightRingOption UnitHighlightRingOption { get; set; } = UnitHighlightRingOption.Original;

        [Category("Party Menu")]
        [DisplayName("Party Menu Color")]
        [Description("Select the background color for the party menu.")]
        [DefaultValue(PartyMenuColorOption.Original)]
        public PartyMenuColorOption PartyMenuColorOption { get; set; } = PartyMenuColorOption.Original;

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
        // INTRO LOGOS
        // ========================================================================

        [Category("Intro Logos")]
        [DisplayName("Intro Logo 1")]
        [Description("Select the screen that appears first on the intro sequence.")]
        [DefaultValue(IntroLogoOneOption.Original)]
        public IntroLogoOneOption IntroLogoOne { get; set; } = IntroLogoOneOption.Original;

        [Category("Intro Logos")]
        [DisplayName("Intro Logo 2")]
        [Description("Select the screen that appears second on the intro sequence.")]
        [DefaultValue(IntroLogoTwoOption.Original)]
        public IntroLogoTwoOption IntroLogoTwo { get; set; } = IntroLogoTwoOption.Original;

        [Category("Intro Logos")]
        [DisplayName("Intro Logo 3")]
        [Description("Select the screen that appears third on the intro sequence.")]
        [DefaultValue(IntroLogoThreeOption.Original)]
        public IntroLogoThreeOption IntroLogoThree { get; set; } = IntroLogoThreeOption.Original; // Fixed: Renamed property
    }

    public class ConfiguratorMixin : ConfiguratorMixinBase
    {
        // Reserved for future UI hooks
    }
}