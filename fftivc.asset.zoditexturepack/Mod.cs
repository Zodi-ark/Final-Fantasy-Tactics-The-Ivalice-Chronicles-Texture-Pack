using fftivc.asset.zoditexturepack.Configuration;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using System;
using System.IO;
using System.Text.Json;

namespace fftivc.asset.zoditexturepack
{
    public class Mod : IModV1
    {
        private Config? _configuration;
        private string? _modRoot;
        private string? _configAssetsFolder;

        public void Start(IModLoader loader, IModConfig modConfig)
        {
            try
            {
                _modRoot = loader.GetDirectoryForModId(modConfig.ModId);
                _configAssetsFolder = Path.Combine(_modRoot!, "ConfigAssets");

                // Path to user-specific config in Reloaded II\User\Mods\
                string userConfigDir = Path.Combine(_modRoot!, "..", "..", "User", "Mods", modConfig.ModId);
                string userConfigPath = Path.GetFullPath(Path.Combine(userConfigDir, "Config.json"));
                Directory.CreateDirectory(userConfigDir);

                // Create default if missing
                if (!File.Exists(userConfigPath))
                {
                    var defaultConfig = new Config();
                    File.WriteAllText(userConfigPath,
                        JsonSerializer.Serialize(defaultConfig, Configurable<Config>.SerializerOptions));
                }

                // Load config
                _configuration = Configurable<Config>.FromFile(userConfigPath, "User Config");

                Console.WriteLine($"[fftivc.asset.zoditexturepack] Loaded user config from {userConfigPath}");
                Console.WriteLine($"[fftivc.asset.zoditexturepack] Applying user selections before launch...");

                ApplyAll();

                Console.WriteLine($"[fftivc.asset.zoditexturepack] Custom texture configuration applied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.asset.zoditexturepack] ERROR applying configuration: {ex}");
            }
        }

        private void ApplyAll()
        {
            if (_configuration == null || _modRoot == null || _configAssetsFolder == null)
                return;

            ApplyConfigFilters();
            ApplyBattlePointers();
        }

        private void ApplyConfigFilters()
        {
            try
            {
                string menuFilterPath = Path.Combine(
                    _configAssetsFolder!, "MenuFilter",
                    _configuration!.DisableMenuFilter ? "Disabled" : "Original",
                    "ffto_screen_filter_uitx.tex");

                string menuTargetPath = Path.Combine(
                    _modRoot!, "FFTIVC", "data", "enhanced", "ui", "ffto", "common", "texture",
                    "ffto_screen_filter_uitx.tex");

                TryCopy(menuFilterPath, menuTargetPath);

                for (int i = 0; i <= 1; i++)
                {
                    string battleFilterPath = Path.Combine(
                        _configAssetsFolder!, "BattleFilters",
                        _configuration.DisableBattleFilter ? "Disabled" : "Original",
                        $"ffto_screen_filter_{i}.tga");

                    string battleTargetPath = Path.Combine(
                        _modRoot!, "FFTIVC", "data", "enhanced", "vfx", "post_process",
                        $"ffto_screen_filter_{i}.tga");

                    TryCopy(battleFilterPath, battleTargetPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.asset.zoditexturepack] Error applying filters: {ex.Message}");
            }
        }

        private void ApplyBattlePointers()
        {
            try
            {
                string option = _configuration!.BattlePointerOption.ToString();

                string pointerSourcePath = Path.Combine(
                    _configAssetsFolder!, "BattlePointers", option, "sword.tga");

                string pointerTargetPath = Path.Combine(
                    _modRoot!, "FFTIVC", "data", "enhanced", "ui", "sword.tga");

                TryCopy(pointerSourcePath, pointerTargetPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.asset.zoditexturepack] Error applying battle pointer: {ex.Message}");
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
                    Console.WriteLine($"[fftivc.asset.zoditexturepack] Copied: {Path.GetFileName(source)}");
                }
                else
                {
                    Console.WriteLine($"[fftivc.asset.zoditexturepack] Missing source file: {source}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[fftivc.asset.zoditexturepack] Copy failed for {Path.GetFileName(source)}: {ex.Message}");
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
