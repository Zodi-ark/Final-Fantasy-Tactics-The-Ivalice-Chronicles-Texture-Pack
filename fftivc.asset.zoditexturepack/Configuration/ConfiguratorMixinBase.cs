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
            try
            {
                if (!Directory.Exists(oldDirectory))
                    return;

                Directory.CreateDirectory(newDirectory);
                foreach (var file in Directory.GetFiles(oldDirectory))
                {
                    var dest = Path.Combine(newDirectory, Path.GetFileName(file));
                    if (!File.Exists(dest))
                        File.Move(file, dest);
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
