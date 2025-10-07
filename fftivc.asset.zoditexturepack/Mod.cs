using fftivc.asset.zoditexturepack.Configuration;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using System;
using System.IO;

namespace fftivc.asset.zoditexturepack
{
    public class Mod : IModV1
    {
        private readonly Config _configuration;
        private readonly string _configAssetsFolder;

        public Mod()
        {
            // Robust path to config assets folder
            _configAssetsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fftivc.asset.zoditexturepack", "ConfigAssets");

            // Load configuration from file
            _configuration = Configurable<Config>.FromFile(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fftivc.asset.zoditexturepack", "Configuration", "Config.json"),
                "Default Config"
            );

            ApplyConfigFilters();
        }

        public Action? Disposing => null;

        // IModV1 interface members
        public void Suspend() { }
        public void Resume() { }
        public void Unload() { }
        public bool CanUnload() => true;
        public bool CanSuspend() => true;

        private void ApplyConfigFilters()
        {
            // Menu filter
            string menuFilterPath = Path.Combine(_configAssetsFolder, "MenuFilter", _configuration.DisableMenuFilter ? "Disabled" : "Original", "ffto_screen_filter_uitx.tex");
            string menuTargetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FFTIVC", "data", "enhanced", "ui", "ffto", "common", "texture", "ffto_screen_filter_uitx.tex");
            TryCopy(menuFilterPath, menuTargetPath);

            // Battle filters
            for (int i = 0; i <= 1; i++)
            {
                string battleFilterPath = Path.Combine(_configAssetsFolder, "BattleFilters", _configuration.DisableBattleFilter ? "Disabled" : "Original", $"ffto_screen_filter_{i}.tga");
                string battleTargetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FFTIVC", "data", "enhanced", "vfx", "post_process", $"ffto_screen_filter_{i}.tga");
                TryCopy(battleFilterPath, battleTargetPath);
            }
        }

        private void TryCopy(string source, string destination)
        {
            try
            {
                File.Copy(source, destination, true);
            }
            catch
            {
                // ignored
            }
        }
    }
}
