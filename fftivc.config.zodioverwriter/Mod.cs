using fftivc.config.zodioverwriter.Configuration;
using fftivc.config.zodioverwriter.Template.Configuration;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using System;
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
            ApplyDirectionalWaitArrow(texturePackDir);
            ApplyWorldMapBlur(texturePackDir);
            ApplyWorldMap(texturePackDir);
            ApplyMenuFilter(texturePackDir);
            ApplyBattleFilter(texturePackDir);
            ApplySpriteOption(texturePackDir);
            ApplyMapOption(texturePackDir);
            ApplyPortraitsOption(texturePackDir);
            ApplyPartyMenuColor(texturePackDir);
            ApplyUnitHighlightRing(texturePackDir);
            ApplyUnitStatusHUD(texturePackDir); // --- NEW METHOD CALLED HERE ---
            ApplyMinimalButtonPrompts(texturePackDir);
            ApplyRemoveTextOnPortraits(texturePackDir);
            ApplyMinimalWarnings(texturePackDir);
        }

        // --- NEW METHOD ADDED HERE ---
        private void ApplyUnitStatusHUD(string texturePackDir)
        {
            try
            {
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "common", "texture", "ui_unit_info_assets_uitx.tex");

                if (_configuration!.UnitStatusHUD == UnitStatusHUDOption.Original)
                {
                    // User wants Original: Delete our file.
                    TryDelete(destPath);
                }
                else
                {
                    // User wants Minimal or Minimal_Blue_HP_Bar.
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
        // --- END OF NEW METHOD ---


        // --- ALL YOUR OTHER METHODS (UNCHANGED) ---

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

        private void ApplyMapOption(string texturePackDir)
        {
            try
            {
                string targetDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "bg");

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
                Console.WriteLine($"[fftivc.config.zodioverwriter] Missing source directory, cannot delete files: {sourceDir}");
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

        private void DisableDirectory(string path)
        {
            try
            {
                string disabledPath = path + ".disabled_by_config";

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


        public void Suspend() { }
        public void Resume() { }
        public void Unload() { }
        public bool CanUnload() => true;
        public bool CanSuspend() => true;
        public Action? Disposing => null;
    }
}