using Reloaded.Mod.Interfaces;
using System.IO;

namespace fftivc.asset.zoditexturepack.Configuration
{
    public class ConfiguratorMixinBase
    {
        public virtual IUpdatableConfigurable[] MakeConfigurations(string configFolder)
        {
            return new IUpdatableConfigurable[]
            {
                Configurable<Config>.FromFile(Path.Combine(configFolder, "Config.json"), "Default Config")
            };
        }

        public virtual bool TryRunCustomConfiguration(Configurator configurator)
        {
            return false;
        }

        public virtual void Migrate(string oldDirectory, string newDirectory)
        {
            void TryMoveFile(string fileName)
            {
                try
                {
                    File.Move(Path.Combine(oldDirectory, fileName), Path.Combine(newDirectory, fileName));
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}
