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

            ApplyBattlePointer(texturePackDir);
            ApplyBattleFrame(texturePackDir); // --- NEW METHOD CALLED HERE ---
            ApplyMenuFilter(texturePackDir);
            ApplyBattleFilter(texturePackDir);
            ApplySpriteOption(texturePackDir);
            ApplyPortraitsOption(texturePackDir);
        }

        private void ApplyBattlePointer(string texturePackDir)
        {
            try
            {
                string option = _configuration!.BattlePointerOption.ToString();
                string sourcePath = Path.Combine(_modRoot!, "Resources", "BattlePointers", option, "sword.tga");
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "sword.tga");
                TryCopy(sourcePath, destPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying battle pointer: {ex.Message}");
            }
        }

        // --- NEW METHOD ADDED HERE ---
        private void ApplyBattleFrame(string texturePackDir)
        {
            try
            {
                // Converts "Removed" to "removed", "Vignette" to "vignette", etc.
                // to match your folder names.
                string option = _configuration!.BattleFrameOption.ToString().ToLower();

                string sourcePath = Path.Combine(_modRoot!, "Resources", "BattleFrame", option, "ui_battle_frame_uitx.tex");
                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "battle", "texture", "ui_battle_frame_uitx.tex");
                TryCopy(sourcePath, destPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying battle frame: {ex.Message}");
            }
        }
        // --- END OF NEW METHOD ---

        private void ApplyMenuFilter(string texturePackDir)
        {
            try
            {
                string sourcePath = Path.Combine(_modRoot!, "Resources", "MenuFilter",
                    _configuration!.DisableMenuFilter ? "Disabled" : "Original", "ffto_screen_filter_uitx.tex");

                string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "common", "texture", "ffto_screen_filter_uitx.tex");
                TryCopy(sourcePath, destPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying menu filter: {ex.Message}");
            }
        }

        private void ApplyBattleFilter(string texturePackDir)
        {
            try
            {
                for (int i = 0; i <= 1; i++)
                {
                    string sourcePath = Path.Combine(_modRoot!, "Resources", "BattleFilters",
                        _configuration!.DisableBattleFilter ? "Disabled" : "Original", $"ffto_screen_filter_{i}.tga");

                    string destPath = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "vfx", "post_process", $"ffto_screen_filter_{i}.tga");
                    TryCopy(sourcePath, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying battle filter: {ex.Message}");
            }
        }

        private void ApplySpriteOption(string texturePackDir)
        {
            try
            {
                string targetDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "system", "ffto", "g2d");

                if (_configuration!.SpritesOption == SpriteOption.Original)
                {
                    // User wants Original (base game): DISABLE the texture pack's "Mobile" sprite folder.
                    DisableDirectory(targetDir);
                }
                else // SpriteOption.Mobile
                {
                    // User wants Mobile (texture pack): ENABLE the texture pack's folder.
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
                    // User wants Original (base game): DISABLE the texture pack's portrait folder.
                    DisableDirectory(targetDir);
                }
                else // PortraitOption.Upscaled
                {
                    // User wants Upscaled:
                    // 1. ENABLE the folder (in case we disabled it last time).
                    EnableDirectory(targetDir);

                    // 2. Copy the upscaled files from this mod's resources.
                    string sourceDir = Path.Combine(_modRoot!, "Resources", "Portraits", "Upscaled");

                    if (!Directory.Exists(sourceDir))
                    {
                        Console.WriteLine($"[fftivc.config.zodioverwriter] No portraits folder found for: Upscaled");
                        return;
                    }

                    Console.WriteLine($"[fftivc.config.zodioverwriter] Applying Upscaled portraits...");
                    CopyDirectory(sourceDir, targetDir); // Uses your existing CopyDirectory method
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying portraits: {ex.Message}");
            }
        }

        /// <summary>
        /// Renames a directory to "disable" it (e.g., "g2d" -> "g2d.disabled_by_config").
        /// </summary>
        private void DisableDirectory(string path)
        {
            try
            {
                string disabledPath = path + ".disabled_by_config";

                // If it's already disabled, we're done.
                if (Directory.Exists(disabledPath))
                    return;

                // If the active path exists, rename it.
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

        /// <summary>
        /// Restores a "disabled" directory (e.g., "g2d.disabled_by_config" -> "g2d").
        /// </summary>
        private void EnableDirectory(string path)
        {
            try
            {
                string disabledPath = path + ".disabled_by_config";

                // If it's already enabled, we're done.
                if (Directory.Exists(path))
                    return;

                // If the disabled path exists, rename it back.
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

        public void Suspend() { }
        public void Resume() { }
        public void Unload() { }
        public bool CanUnload() => true;
        public bool CanSuspend() => true;
        public Action? Disposing => null;
    }
}