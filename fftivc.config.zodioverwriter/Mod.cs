using fftivc.config.zodioverwriter.Configuration;
using fftivc.config.zodioverwriter.Template.Configuration;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using System;
using System.Collections.Generic; // Required for Dictionary
using System.IO;
using System.Text.Json;

namespace fftivc.config.zodioverwriter
{
    public class Mod : IModV1
    {
        private Config? _configuration;
        private string? _modRoot;

        public void Start(IModLoader loader, IModConfig modConfig)
        {
            try
            {
                _modRoot = loader.GetDirectoryForModId(modConfig.ModId);

                // Path to user config inside Reloaded II\User\Mods\
                string userConfigDir = Path.Combine(_modRoot!, "..", "..", "User", "Mods", modConfig.ModId);
                string userConfigPath = Path.GetFullPath(Path.Combine(userConfigDir, "Config.json"));
                Directory.CreateDirectory(userConfigDir);

                // Create default config if missing
                if (!File.Exists(userConfigPath))
                {
                    var defaultConfig = new Config();
                    File.WriteAllText(userConfigPath,
                        JsonSerializer.Serialize(defaultConfig, Configurable<Config>.SerializerOptions));

                    Console.WriteLine($"[fftivc.config.zodioverwriter] Created default Config.json");
                }

                // Load user config
                _configuration = Configurable<Config>.FromFile(userConfigPath, "User Config");

                Console.WriteLine($"[fftivc.config.zodioverwriter] Loaded user config from {userConfigPath}");
                Console.WriteLine($"[fftivc.config.zodioverwriter] Applying configuration before Mod Loader launch...");

                ApplyAll();

                Console.WriteLine($"[fftivc.config.zodioverwriter] Configuration applied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] ERROR applying configuration: {ex}");
            }
        }

        private void ApplyAll()
        {
            if (_configuration == null || _modRoot == null)
                return;

            // Locate target mod directory
            string texturePackDir = Path.Combine(_modRoot!, "..", "fftivc.asset.zoditexturepack");
            texturePackDir = Path.GetFullPath(texturePackDir);

            if (!Directory.Exists(texturePackDir))
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Texture pack not found: {texturePackDir}");
                return;
            }

            // Apply options
            ApplyBattlePointer(texturePackDir);
            ApplyBattleFrame(texturePackDir);
            ApplyUnitSelectFrame(texturePackDir);
            ApplyDirectionalWaitArrow(texturePackDir);
            ApplyBleedOutHeartsAndTurnGlow(texturePackDir);
            ApplyWorldMapBlur(texturePackDir);
            ApplyWorldMap(texturePackDir);
            ApplyMenuFilter(texturePackDir);
            ApplyBattleFilter(texturePackDir);
            ApplySpriteOption(texturePackDir);
            ApplyMapOption(texturePackDir);
            ApplyPortraitsOption(texturePackDir);

            // Replaced old ApplyEquipment with new ApplyEquipmentIcons
            ApplyEquipmentIcons(texturePackDir);

            ApplyPartyMenuColor(texturePackDir);
            ApplyUnitHighlightRing(texturePackDir);
            ApplyUnitShiftArrow(texturePackDir);
            ApplyUnitFavoriteTileAndFlag(texturePackDir);
            ApplyUnitTile(texturePackDir);
            ApplyUnitStatusHUD(texturePackDir);
            ApplyStatusIcons(texturePackDir);
            ApplyZodiacIcons(texturePackDir);
            ApplyMinimalButtonPrompts(texturePackDir);
            ApplyRemoveTextOnPortraits(texturePackDir);
            ApplyMinimalWarnings(texturePackDir);
            ApplyCursorFinger(texturePackDir);
            ApplySpeechBubble(texturePackDir);
            ApplySpeechBubbleTypeface(texturePackDir);
            ApplyTurnAndHUDTypeface(texturePackDir);
            ApplyAttackRange(texturePackDir);
            ApplyAttackTarget(texturePackDir);
            ApplyMoveTiles(texturePackDir);
            ApplyMoveTilesEnemy(texturePackDir);
            ApplyCommandHUDAndInfoPanels(texturePackDir);
            ApplyUIGlowAndEquipDisplay(texturePackDir);
        }

        // ========================================================================================================
        // METHOD: UI GLOW AND EQUIP DISPLAY (UPDATED FOR SPARSE FOLDERS)
        // ========================================================================================================
        private void ApplyUIGlowAndEquipDisplay(string texturePackDir)
        {
            try
            {
                // Dictionary maps each filename to its specific subfolder destination inside ui\ffto
                var destinations = new Dictionary<string, string>
                {
                    { "ui_bar_parts_uitx.tex", Path.Combine("bar", "texture") },
                    { "ui_battle_continuous_map_uitx.tex", Path.Combine("battle", "texture") },
                    { "ui_battle_encount_parts_uitx.tex", Path.Combine("battle", "texture") },
                    { "ui_brave_story_top_uitx.tex", Path.Combine("bravestory", "texture") },
                    { "ui_bs_category_tab_uitx.tex", Path.Combine("bravestory", "texture") },
                    { "ui_bs_detail_window_uitx.tex", Path.Combine("bravestory", "texture") },
                    { "ui_shop_parts_uitx.tex", Path.Combine("shop", "texture") },
                    { "ui_system_saveload_uitx.tex", Path.Combine("system", "texture") },
                    { "ui_title_mode_select_uitx.tex", Path.Combine("title", "texture") },
                    { "ui_unit_status_01_uitx.tex", Path.Combine("unit", "texture") },
                    { "ui_wm_base_uitx.tex", Path.Combine("worldmap", "texture") }
                };

                string baseDestDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto");

                if (_configuration!.UIGlowAndEquipDisplay == UIGlowAndEquipDisplayOption.Original)
                {
                    foreach (var kvp in destinations)
                    {
                        TryDelete(Path.Combine(baseDestDir, kvp.Value, kvp.Key));
                    }
                }
                else
                {
                    string option = _configuration!.UIGlowAndEquipDisplay.ToString();
                    string sourceDir = Path.Combine(_modRoot!, "Resources", "UIGlowAndEquipDisplay", option);

                    foreach (var kvp in destinations)
                    {
                        string sourcePath = Path.Combine(sourceDir, kvp.Key);
                        string destPath = Path.Combine(baseDestDir, kvp.Value, kvp.Key);

                        if (File.Exists(sourcePath))
                        {
                            // If you included the file in your specific option folder, copy it over.
                            TryCopy(sourcePath, destPath);
                        }
                        else
                        {
                            // If you omitted the file, automatically delete any existing override
                            // so the game reverts to using the vanilla Original file.
                            TryDelete(destPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying UI Glow And Equip Display: {ex.Message}");
            }
        }

        // ========================================================================================================
        // METHOD: COMMAND HUD AND INFO PANELS
        // ========================================================================================================
        private void ApplyCommandHUDAndInfoPanels(string texturePackDir)
        {
            try
            {
                string destCommon = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "common", "texture", "ui_common_bg_uitx.tex");
                string destUnit = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "unit", "texture", "ui_tooltip_art_bg_uitx.tex");

                if (_configuration!.CommandHUDAndInfoPanels == CommandHUDAndInfoPanelsOption.Original)
                {
                    TryDelete(destCommon);
                    TryDelete(destUnit);
                }
                else
                {
                    string option = _configuration!.CommandHUDAndInfoPanels.ToString();
                    string sourceDir = Path.Combine(_modRoot!, "Resources", "CommandHUDAndInfoPanels", option);

                    string sourceCommon = Path.Combine(sourceDir, "ui_common_bg_uitx.tex");
                    TryCopy(sourceCommon, destCommon);

                    string sourceUnit = Path.Combine(sourceDir, "ui_tooltip_art_bg_uitx.tex");
                    TryCopy(sourceUnit, destUnit);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Command HUD And Info Panels: {ex.Message}");
            }
        }

        // ========================================================================================================
        // METHOD: ATTACK RANGE
        // ========================================================================================================
        private void ApplyAttackRange(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "bg", "ui", "panel", "attack_range.tga");

                if (_configuration!.AttackRange == AttackRangeOption.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.AttackRange.ToString();
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "AttackRange", option, "attack_range.tga");
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Attack Range: {ex.Message}");
            }
        }

        // ========================================================================================================
        // METHOD: ATTACK TARGET
        // ========================================================================================================
        private void ApplyAttackTarget(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "bg", "ui", "panel", "attack_target.tga");

                if (_configuration!.AttackTarget == AttackTargetOption.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.AttackTarget.ToString();
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "AttackTarget", option, "attack_target.tga");
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Attack Target: {ex.Message}");
            }
        }

        // ========================================================================================================
        // METHOD: ENEMY MOVE TILES
        // ========================================================================================================
        private void ApplyMoveTilesEnemy(string texturePackDir)
        {
            try
            {
                string destDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "bg", "ui", "panel");
                var files = new[] { "e_move_range.tga", "e_move_range_s.tga" };

                if (_configuration!.MoveTilesEnemy == MoveTilesEnemyOption.Original)
                {
                    foreach (var file in files)
                    {
                        TryDelete(Path.Combine(destDir, file));
                    }
                }
                else
                {
                    string option = _configuration!.MoveTilesEnemy.ToString();
                    string sourceDir = Path.Combine(_modRoot!, "Resources", "MoveTilesEnemy", option);

                    foreach (var file in files)
                    {
                        string sourcePath = Path.Combine(sourceDir, file);
                        string destPath = Path.Combine(destDir, file);
                        TryCopy(sourcePath, destPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Enemy Move Tiles: {ex.Message}");
            }
        }

        // ========================================================================================================
        // METHOD: MOVE TILES (PLAYER)
        // ========================================================================================================
        private void ApplyMoveTiles(string texturePackDir)
        {
            try
            {
                string destDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "bg", "ui", "panel");
                var files = new[] { "p_move_range.tga", "p_hole_move_range.tga", "p_move_range_s.tga" };

                if (_configuration!.MoveTiles == MoveTilesOption.Original)
                {
                    foreach (var file in files)
                    {
                        TryDelete(Path.Combine(destDir, file));
                    }
                }
                else
                {
                    string option = _configuration!.MoveTiles.ToString();
                    string sourceDir = Path.Combine(_modRoot!, "Resources", "MoveTiles", option);

                    foreach (var file in files)
                    {
                        string sourcePath = Path.Combine(sourceDir, file);
                        string destPath = Path.Combine(destDir, file);
                        TryCopy(sourcePath, destPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Move Tiles: {ex.Message}");
            }
        }

        private void ApplyTurnAndHUDTypeface(string texturePackDir)
        {
            try
            {
                string destDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "system", "graphics", "font2");
                string fileName = "jura-semibold_ffto_custom_font.tex";
                string destPath = Path.Combine(destDir, fileName);

                if (_configuration!.TurnAndHUDTypeface == TurnAndHUDTypefaceOption.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    // Looks inside "Old_English" subdirectory
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "TurnAndHUDTypeface", "Old_English", fileName);
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Turn & Secondary HUD Typeface: {ex.Message}");
            }
        }

        private void ApplyEquipmentIcons(string texturePackDir)
        {
            try
            {
                string targetDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "icon", "equip_item_s", "texture");

                if (_configuration!.EquipmentIcons == EquipmentOption.Original)
                {
                    // Use PSX folder as a reference for what files to delete to restore vanilla
                    string referenceDir = Path.Combine(_modRoot!, "Resources", "Equipment", "PSX");
                    DeleteManagedFiles(referenceDir, targetDir);
                }
                else
                {
                    string option = _configuration!.EquipmentIcons.ToString();
                    string sourceDir = Path.Combine(_modRoot!, "Resources", "Equipment", option);

                    if (!Directory.Exists(sourceDir))
                    {
                        Console.WriteLine($"[fftivc.config.zodioverwriter] Equipment folder not found: {option}");
                        return;
                    }
                    CopyDirectory(sourceDir, targetDir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Equipment Icons: {ex.Message}");
            }
        }

        private void ApplySpeechBubbleTypeface(string texturePackDir)
        {
            try
            {
                string destDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "system", "graphics", "font2");
                string fileName = "ffto_bouwsmatext-master_font.tex";
                string destPath = Path.Combine(destDir, fileName);

                if (_configuration!.SpeechBubbleTypeface == SpeechBubbleTypefaceOption.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "SpeechBubbleTypeface", "Old_English", fileName);
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Speech Bubble Typeface: {ex.Message}");
            }
        }

        private void ApplySpeechBubble(string texturePackDir)
        {
            try
            {
                string destDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "event", "texture");

                var files = new[] {
                    "ui_event_balloon_normal_uitx.tex",
                    "ui_event_balloon_round_uitx.tex",
                    "ui_event_balloon_spike_uitx.tex",
                    "ui_event_balloon_tail_uitx.tex",
                    "ui_event_balloon_mask_face_uitx.tex"
                };

                if (_configuration!.SpeechBubble == SpeechBubbleOption.Original)
                {
                    foreach (var file in files)
                    {
                        TryDelete(Path.Combine(destDir, file));
                    }
                }
                else
                {
                    string sourceDir = Path.Combine(_modRoot!, "Resources", "SpeechBubble", "PSX_Upscaled");

                    foreach (var file in files)
                    {
                        string sourcePath = Path.Combine(sourceDir, file);
                        string destPath = Path.Combine(destDir, file);
                        TryCopy(sourcePath, destPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Speech Bubble: {ex.Message}");
            }
        }

        private void ApplyCursorFinger(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "common", "texture", "ui_common_02_uitx.tex");

                if (_configuration!.CursorFinger == CursorFingerOption.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.CursorFinger.ToString();
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "CursorFinger", option, "ui_common_02_uitx.tex");
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Cursor Finger: {ex.Message}");
            }
        }

        private void ApplyUnitSelectFrame(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "battle", "texture", "ui_unit_select_frame_uitx.tex");

                if (_configuration!.UnitSelectFrame == UnitSelectFrameOption.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.UnitSelectFrame.ToString();
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "UnitSelectFrame", option, "ui_unit_select_frame_uitx.tex");
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Unit Select Frame: {ex.Message}");
            }
        }

        private void ApplyStatusIcons(string texturePackDir)
        {
            try
            {
                string targetDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "icon", "status", "texture");

                if (_configuration!.StatusIcons == StatusIconsOption.Original)
                {
                    // We use the PSX folder as a "reference list" of files to delete to restore vanilla.
                    string referenceDir = Path.Combine(_modRoot!, "Resources", "StatusIcons", "PSX");
                    DeleteManagedFiles(referenceDir, targetDir);
                }
                else
                {
                    string option = _configuration!.StatusIcons.ToString();
                    string sourceDir = Path.Combine(_modRoot!, "Resources", "StatusIcons", option);

                    if (!Directory.Exists(sourceDir))
                    {
                        Console.WriteLine($"[fftivc.config.zodioverwriter] Status Icons folder not found: {option}");
                        return;
                    }
                    CopyDirectory(sourceDir, targetDir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Status Icons: {ex.Message}");
            }
        }

        private void ApplyZodiacIcons(string texturePackDir)
        {
            try
            {
                string targetDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "icon", "zodiac_sign", "texture");
                string optionName = _configuration!.ZodiacIcons.ToString();

                if (_configuration.ZodiacIcons == ZodiacIconsOption.Original)
                {
                    string referenceDir = Path.Combine(_modRoot!, "Resources", "ZodiacIcons", "Gold");
                    DeleteManagedFiles(referenceDir, targetDir);
                }
                else
                {
                    string sourceDir = Path.Combine(_modRoot!, "Resources", "ZodiacIcons", optionName);

                    if (!Directory.Exists(sourceDir))
                    {
                        Console.WriteLine($"[fftivc.config.zodioverwriter] Zodiac Icons folder not found: {optionName}");
                        return;
                    }
                    CopyDirectory(sourceDir, targetDir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Zodiac Icons: {ex.Message}");
            }
        }

        private void ApplyUnitStatusHUD(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "common", "texture", "ui_unit_info_assets_uitx.tex");

                if (_configuration!.UnitStatusHUD == UnitStatusHUDOption.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.UnitStatusHUD.ToString();
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "UnitStatusHUD", option, "ui_unit_info_assets_uitx.tex");
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Unit Status HUD: {ex.Message}");
            }
        }

        private void ApplyDirectionalWaitArrow(string texturePackDir)
        {
            try
            {
                string destDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui");
                var files = new[] { "direction.tga", "direction_select.tga" };

                if (_configuration!.DirectionalWaitArrow == DirectionalWaitArrowOption.Original)
                {
                    foreach (var file in files)
                    {
                        TryDelete(Path.Combine(destDir, file));
                    }
                }
                else
                {
                    string option = _configuration!.DirectionalWaitArrow.ToString();
                    string sourceDir = Path.Combine(_modRoot!, "Resources", "DirectionalWaitArrow", option);

                    foreach (var file in files)
                    {
                        string sourcePath = Path.Combine(sourceDir, file);
                        string destPath = Path.Combine(destDir, file);
                        TryCopy(sourcePath, destPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying directional wait arrow: {ex.Message}");
            }
        }

        private void ApplyBleedOutHeartsAndTurnGlow(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "battle", "texture", "ui_battle_atb_uitx.tex");

                if (_configuration!.BleedOutHeartsAndTurnGlow == BleedOutHeartsAndTurnGlowOption.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.BleedOutHeartsAndTurnGlow.ToString();
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "BleedOutHeartsAndTurnGlow", option, "ui_battle_atb_uitx.tex");
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Bleed Out Hearts & Turn Glow: {ex.Message}");
            }
        }

        private void ApplyMinimalWarnings(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "nxd", "uiannounce.nxd");

                if (_configuration!.MinimalWarnings)
                {
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "MinimalWarnings", "uiannounce.nxd");
                    TryCopy(sourcePath, destPath);
                }
                else
                {
                    TryDelete(destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying minimal warnings: {ex.Message}");
            }
        }

        private void ApplyBattleFilter(string texturePackDir)
        {
            try
            {
                for (int i = 0; i <= 1; i++)
                {
                    string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "vfx", "post_process", $"ffto_screen_filter_{i}.tga");

                    if (_configuration!.BattleFilter == BattleFilterOption.Original)
                    {
                        TryDelete(destPath);
                    }
                    else
                    {
                        string option = _configuration!.BattleFilter.ToString();
                        string sourcePath = Path.Combine(_modRoot!, "Resources", "BattleFilters", option, $"ffto_screen_filter_{i}.tga");
                        TryCopy(sourcePath, destPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying battle filter: {ex.Message}");
            }
        }

        // ========================================================================================================
        // UPDATED METHOD: MAPS
        // ========================================================================================================
        private void ApplyMapOption(string texturePackDir)
        {
            try
            {
                // We target "textures" specifically so we don't accidentally rename the entire "bg" 
                // folder which now contains your move/attack tiles in "bg\ui\panel"
                string targetDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "bg", "textures");

                if (_configuration!.Maps == MapOption.Original)
                {
                    DisableDirectory(targetDir);
                }
                else
                {
                    EnableDirectory(targetDir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying maps: {ex.Message}");
            }
        }

        private void ApplyRemoveTextOnPortraits(string texturePackDir)
        {
            try
            {
                var filesToManage = new[] { "ui.de.nxd", "ui.en.nxd", "ui.fr.nxd", "ui.ja.nxd" };

                foreach (var fileName in filesToManage)
                {
                    string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "nxd", fileName);

                    if (_configuration!.RemoveTextOnPortraits)
                    {
                        string sourcePath = Path.Combine(_modRoot!, "Resources", "RemoveTextOnPortraits", fileName);
                        TryCopy(sourcePath, destPath);
                    }
                    else
                    {
                        TryDelete(destPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying 'Remove Text On Portraits': {ex.Message}");
            }
        }

        private void ApplyMinimalButtonPrompts(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "nxd", "uibuttonguide.nxd");

                if (_configuration!.MinimalButtonPrompts)
                {
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "MinimalButtonPrompts", "uibuttonguide.nxd");
                    TryCopy(sourcePath, destPath);
                }
                else
                {
                    TryDelete(destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying minimal button prompts: {ex.Message}");
            }
        }

        private void ApplyWorldMap(string texturePackDir)
        {
            try
            {
                string targetDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "worldmap", "texture");
                string optionName = _configuration!.WorldMap.ToString();
                string sourceDir = Path.Combine(_modRoot!, "Resources", "WorldMap", optionName);

                if (_configuration.WorldMap == WorldMapOption.Original)
                {
                    string customMapSourceDir = Path.Combine(_modRoot!, "Resources", "WorldMap", "Azure_and_Ivory");
                    DeleteManagedFiles(customMapSourceDir, targetDir);
                }
                else
                {
                    if (!Directory.Exists(sourceDir))
                    {
                        Console.WriteLine($"[fftivc.config.zodioverwriter] No world map folder found for: {optionName}");
                        return;
                    }
                    Console.WriteLine($"[fftivc.config.zodioverwriter] Applying {optionName} world map...");
                    CopyDirectory(sourceDir, targetDir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying world map: {ex.Message}");
            }
        }

        private void ApplyBattlePointer(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "sword.tga");

                if (_configuration!.BattlePointerOption == BattlePointerChoice.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.BattlePointerOption.ToString();
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "BattlePointers", option, "sword.tga");
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying battle pointer: {ex.Message}");
            }
        }

        private void ApplyBattleFrame(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "battle", "texture", "ui_battle_frame_uitx.tex");

                if (_configuration!.BattleFrameOption == BattleFrameOption.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.BattleFrameOption.ToString().ToLower();
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "BattleFrame", option, "ui_battle_frame_uitx.tex");
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying battle frame: {ex.Message}");
            }
        }

        private void ApplyWorldMapBlur(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "worldmap", "texture", "wm_edge_blur_uitx.tex");

                if (_configuration!.DisableWorldMapBlur)
                {
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "WorldMapBlur", "Removed", "wm_edge_blur_uitx.tex");
                    TryCopy(sourcePath, destPath);
                }
                else
                {
                    TryDelete(destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying world map blur: {ex.Message}");
            }
        }

        private void ApplyMenuFilter(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "common", "texture", "ffto_screen_filter_uitx.tex");

                if (_configuration!.RemovePartyMenuFilter)
                {
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "MenuFilter", "Disabled", "ffto_screen_filter_uitx.tex");
                    TryCopy(sourcePath, destPath);
                }
                else
                {
                    TryDelete(destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying menu filter: {ex.Message}");
            }
        }

        private void ApplyPartyMenuColor(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "common", "texture", "ui_bg_stone_uitx.tex");

                if (_configuration!.PartyMenuColorOption == PartyMenuColorOption.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.PartyMenuColorOption.ToString();
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "PartyMenuColor", option, "ui_bg_stone_uitx.tex");
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying party menu color: {ex.Message}");
            }
        }

        private void ApplyUnitHighlightRing(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "unit", "texture", "ui_unit_tex_uitx.tex");

                if (_configuration!.UnitHighlightRingOption == UnitHighlightRingOption.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.UnitHighlightRingOption.ToString();
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "UnitHighlightRing", option, "ui_unit_tex_uitx.tex");
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying unit highlight ring: {ex.Message}");
            }
        }

        private void ApplyUnitShiftArrow(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "unit", "texture", "ui_jobchange_uitx.tex");

                if (_configuration!.UnitShiftArrow == UnitShiftArrowOption.Original)
                {
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.UnitShiftArrow.ToString();
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "UnitShiftArrow", option, "ui_jobchange_uitx.tex");
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Unit Shift Arrow: {ex.Message}");
            }
        }

        private void ApplyUnitFavoriteTileAndFlag(string texturePackDir)
        {
            try
            {
                // Destination path for the favorite tile texture
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "unit", "texture", "ui_unit_starting_member_uitx.tex");

                if (_configuration!.UnitFavoriteTileAndFlag == UnitFavoriteTileAndFlagOption.Original)
                {
                    // Restore vanilla/default texture by deleting the override
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.UnitFavoriteTileAndFlag.ToString();
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "UnitFavoriteTileAndFlag", option, "ui_unit_starting_member_uitx.tex");

                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Favorite Unit Tile & Flag: {ex.Message}");
            }
        }

        private void ApplyUnitTile(string texturePackDir)
        {
            try
            {
                // Destination path for the unit tile texture
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "icon", "unit_stand", "texture", "us_001_uitx.tex");

                if (_configuration!.UnitTile == UnitTileOption.Original)
                {
                    // Restore vanilla/default texture by deleting the override
                    TryDelete(destPath);
                }
                else
                {
                    string option = _configuration!.UnitTile.ToString();
                    // Source path: Resources/UnitTile/{Option}/us_001_uitx.tex
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "UnitTile", option, "us_001_uitx.tex");

                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying Unit Tile: {ex.Message}");
            }
        }

        private void ApplySpriteOption(string texturePackDir)
        {
            try
            {
                string targetDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "system", "ffto", "g2d");

                if (_configuration!.SpritesOption == SpriteOption.Original)
                {
                    DisableDirectory(targetDir);
                }
                else
                {
                    EnableDirectory(targetDir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying sprites: {ex.Message}");
            }
        }

        private void ApplyPortraitsOption(string texturePackDir)
        {
            try
            {
                string targetDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "common", "face", "texture");

                if (_configuration!.PortraitsOption == PortraitOption.Original)
                {
                    DisableDirectory(targetDir);
                }
                else
                {
                    EnableDirectory(targetDir);
                    string sourceDir = Path.Combine(_modRoot!, "Resources", "Portraits", "Upscaled");

                    if (!Directory.Exists(sourceDir))
                    {
                        Console.WriteLine($"[fftivc.config.zodioverwriter] No portraits folder found for: Upscaled");
                        return;
                    }

                    Console.WriteLine($"[fftivc.config.zodioverwriter] Applying Upscaled portraits...");
                    CopyDirectory(sourceDir, targetDir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying portraits: {ex.Message}");
            }
        }

        // --- HELPER METHODS ---

        private void DeleteManagedFiles(string sourceDir, string targetDir)
        {
            if (!Directory.Exists(sourceDir))
            {
                return;
            }

            if (!Directory.Exists(targetDir))
            {
                return;
            }

            try
            {
                foreach (var file in Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories))
                {
                    string relativePath = file.Substring(sourceDir.Length + 1);
                    string targetFile = Path.Combine(targetDir, relativePath);

                    if (File.Exists(targetFile))
                    {
                        File.Delete(targetFile);
                        Console.WriteLine($"[fftivc.config.zodioverwriter] Removed: {Path.GetFileName(targetFile)}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error removing managed files: {ex.Message}");
            }
        }

        // ========================================================================================================
        // UPDATED: FAST DIRECTORY TOGGLING
        // ========================================================================================================
        private void DisableDirectory(string path)
        {
            try
            {
                string disabledPath = path + ".disabled_by_config";

                // If already disabled, do nothing
                if (Directory.Exists(disabledPath))
                    return;

                if (Directory.Exists(path))
                {
                    Directory.Move(path, disabledPath);
                    Console.WriteLine($"[fftivc.config.zodioverwriter] Disabled: {Path.GetFileName(path)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error disabling {Path.GetFileName(path)}: {ex.Message}");
            }
        }

        private void EnableDirectory(string path)
        {
            try
            {
                string disabledPath = path + ".disabled_by_config";

                // If already enabled, do nothing
                if (Directory.Exists(path))
                    return;

                if (Directory.Exists(disabledPath))
                {
                    Directory.Move(disabledPath, path);
                    Console.WriteLine($"[fftivc.config.zodioverwriter] Enabled: {Path.GetFileName(path)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error enabling {Path.GetFileName(path)}: {ex.Message}");
            }
        }

        private void CopyDirectory(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories))
            {
                string relativePath = file.Substring(sourceDir.Length + 1);
                string targetFile = Path.Combine(targetDir, relativePath);
                Directory.CreateDirectory(Path.GetDirectoryName(targetFile)!);
                File.Copy(file, targetFile, true);
            }
        }

        private void TryCopy(string source, string destination)
        {
            try
            {
                if (File.Exists(source))
                {
                    // This handles creating the folder if it doesn't exist,
                    // which is safe even if UnitHighlightRing already created it.
                    Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
                    File.Copy(source, destination, true);
                    Console.WriteLine($"[fftivc.config.zodioverwriter] Copied: {Path.GetFileName(source)}");
                }
                else
                {
                    Console.WriteLine($"[fftivc.config.zodioverwriter] Missing source: {source}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Copy failed for {Path.GetFileName(source)}: {ex.Message}");
            }
        }

        private void TryDelete(string destination)
        {
            try
            {
                if (File.Exists(destination))
                {
                    File.Delete(destination);
                    Console.WriteLine($"[fftivc.config.zodioverwriter] Removed: {Path.GetFileName(destination)} (Restoring texture pack default)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Failed to delete {Path.GetFileName(destination)}: {ex.Message}");
            }
        }

        // ========================================================================================================
        // INTERFACE METHODS (Required by IModV1)
        // ========================================================================================================
        public void Suspend() { }
        public void Resume() { }
        public void Unload() { }
        public bool CanUnload() => true;
        public bool CanSuspend() => true;
        public Action? Disposing => null;
    }
}