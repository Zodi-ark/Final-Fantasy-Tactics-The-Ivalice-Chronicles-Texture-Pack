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
                string selected = _configuration!.SpritesOption.ToString();
                string sourceDir = Path.Combine(_modRoot!, "Resources", "Sprites", selected);
                string targetDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "system", "ffto", "g2d");

                if (!Directory.Exists(sourceDir))
                {
                    Console.WriteLine($"[fftivc.config.zodioverwriter] No sprite folder found for: {selected}");
                    return;
                }

                Console.WriteLine($"[fftivc.config.zodioverwriter] Applying {selected} sprites...");
                CopyDirectory(sourceDir, targetDir);
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
                string selected = _configuration!.PortraitsOption.ToString();
                string sourceDir = Path.Combine(_modRoot!, "Resources", "Portraits", selected);
                string targetDir = Path.Combine(texturePackDir, "FFTIVC", "data", "enhanced", "ui", "ffto", "common", "face", "texture");

                if (!Directory.Exists(sourceDir))
                {
                    Console.WriteLine($"[fftivc.config.zodioverwriter] No portraits folder found for: {selected}");
                    return;
                }

                Console.WriteLine($"[fftivc.config.zodioverwriter] Applying {selected} portraits...");
                CopyDirectory(sourceDir, targetDir);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.config.zodioverwriter] Error applying portraits: {ex.Message}");
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
