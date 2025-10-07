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
        private readonly string ConfigAssetsFolder;

        public Mod(IMod mod)
        {
            // Load configuration
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                             "fftivc.asset.zoditexturepack",
                                             "Configuration",
                                             "Config.json");
            _configuration = Configurable<Config>.FromFile(configPath, "Default Config");

            // Path to config assets
            ConfigAssetsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                              "fftivc.asset.zoditexturepack",
                                              "ConfigAssets");

            ApplyConfigFilters();
        }

        private void ApplyConfigFilters()
        {
            try
            {
                // Menu filters
                string menuSourceFolder = _configuration.DisableMenuFilters
                    ? Path.Combine(ConfigAssetsFolder, "MenuFilters", "Disabled")
                    : Path.Combine(ConfigAssetsFolder, "MenuFilters", "Original");

                File.Copy(Path.Combine(menuSourceFolder, "ffto_screen_filter_0.tga"),
                          Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FFTIVC", "data", "enhanced", "vfx", "post_process", "ffto_screen_filter_0.tga"),
                          true);

                File.Copy(Path.Combine(menuSourceFolder, "ffto_screen_filter_1.tga"),
                          Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FFTIVC", "data", "enhanced", "vfx", "post_process", "ffto_screen_filter_1.tga"),
                          true);

                // Battle filter
                string battleSourceFolder = _configuration.DisableBattleFilter
                    ? Path.Combine(ConfigAssetsFolder, "BattleFilter", "Disabled")
                    : Path.Combine(ConfigAssetsFolder, "BattleFilter", "Original");

                File.Copy(Path.Combine(battleSourceFolder, "ffto_screen_filter_uitx.tex"),
                          Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FFTIVC", "data", "enhanced", "ui", "ffto", "common", "texture", "ffto_screen_filter_uitx.tex"),
                          true);

                Console.WriteLine("[Mod] Filters applied successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] Failed to apply filters: " + ex);
            }
        }

        #region IModV1 Implementation
        public void Suspend() { }
        public void Resume() { }
        public void Unload() { }
        public bool CanUnload() => true;
        public bool CanSuspend() => true;
        public Action Disposing => null!;
        #endregion
    }
}
