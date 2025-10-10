using System;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using fftivc.config.zodioverwriter;

namespace fftivc.config.zodioverwriter.Template
{
    // Simple bootstrapper that uses your existing Mod class (which implements Start(IModLoader, IModConfig)).
    // This avoids needing to inherit Template.ModBase or add a Mod(ModContext) constructor.
    public class Startup : IMod
    {
        private IModLoader _modLoader = null!;
        private IModConfig _modConfig = null!;
        private ILogger _logger = null!;
        private IReloadedHooks? _hooks;
        private fftivc.config.zodioverwriter.Mod _mod = null!;

        // Entry called by Reloaded-II (v1 entrypoint)
        public void StartEx(IModLoaderV1 loaderApi, IModConfigV1 modConfig)
        {
            // Cast the v1 APIs to the older interfaces your Mod uses
            _modLoader = (IModLoader)loaderApi;
            _modConfig = (IModConfig)modConfig;

            // Get logger & hooks if available (safe casts)
            _logger = (ILogger)_modLoader.GetLogger();
            _modLoader.GetController<IReloadedHooks>()?.TryGetTarget(out _hooks);

            // Create your Mod and call its Start method (the one that expects IModLoader + IModConfig)
            _mod = new fftivc.config.zodioverwriter.Mod();
            _mod.Start(_modLoader, _modConfig);
        }

        /* Pass lifecycle calls through to your Mod implementation */
        public void Suspend() => _mod?.Suspend();
        public void Resume() => _mod?.Resume();
        public void Unload() => _mod?.Unload();

        public bool CanUnload() => _mod?.CanUnload() ?? true;
        public bool CanSuspend() => _mod?.CanSuspend() ?? true;

        public Action? Disposing => _mod?.Disposing;
    }
}
