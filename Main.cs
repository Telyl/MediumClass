using BlueprintCore.Blueprints.Configurators.Root;
using BlueprintCore.Utils;
using HarmonyLib;
using System;
using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager.ModEntry;
using Kingmaker.PubSubSystem;
using MediumClass.Utils;
using MediumClass.Features;
using MediumClass.Features.MediumSpecific;

namespace MediumClass
{
    [EnableReloading]
    public static class Main
    {
        public static bool Enabled;
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Main));
        //        private static readonly LogWrapper Logger = LogWrapper.Get("AddedFeats");

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                modEntry.OnUnload = Unload;
                var harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll();

                EventBus.Subscribe(new BlueprintCacheInitHandler());

                Logger.Log("Finished patching.");
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to patch", e);
            }
            return true;
        }

        static bool Unload(UnityModManager.ModEntry modEntry)
        {
            try
            {
                var harmony = new Harmony(modEntry.Info.Id);
                harmony.UnpatchAll();
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to unload.", e);
            }
            return true;
        }

        class BlueprintCacheInitHandler : IBlueprintCacheInitHandler
        {
            private static bool Initialized = false;
            private static bool InitializeDelayed = false;

            public void AfterBlueprintCachePatches()
            {
                try
                {
                    if (InitializeDelayed)
                    {
                        Logger.Log("Already initialized blueprints cache.");
                        return;
                    }
                    InitializeDelayed = true;

                    ConfigureFeatsDelayed();

                    RootConfigurator.ConfigureDelayedBlueprints();
                }
                catch (Exception e)
                {
                    Logger.LogException("Delayed blueprint configuration failed.", e);
                }
            }

            public void BeforeBlueprintCachePatches()
            {

            }

            public void BeforeBlueprintCacheInit()
            {

            }

            public void AfterBlueprintCacheInit()
            {
                try
                {
                    if (Initialized)
                    {
                        Logger.Log("Already initialized blueprints cache.");
                        return;
                    }
                    Initialized = true;
                    LogWrapper.EnableInternalVerboseLogs();
                    // First strings
                    LocalizationTool.LoadEmbeddedLocalizationPacks(
                      "MediumClass.Strings.Archmage.json",
                      "MediumClass.Strings.Champion.json",
                      "MediumClass.Strings.Guardian.json",
                      "MediumClass.Strings.Hierophant.json",
                      "MediumClass.Strings.Marshal.json",
                      "MediumClass.Strings.Medium.json",
                      "MediumClass.Strings.Settings.json",
                      "MediumClass.Strings.Feats.json",
                      "MediumClass.Strings.Trickster.json");

                    // Then settings

                    Settings.Init();
                    ConfigureClasses();
                    ConfigureHomebrew();
                    ConfigureFeats();
                    ConfigureSpells();
                }
                catch (Exception e)
                {
                    Logger.LogException("Failed to initialize.", e);
                }
            }
            private static void ConfigureHomebrew()
            {
                Logger.Log("Configuring homebrew.");
            }
            private static void ConfigureClasses()
            {
                Logger.Log("Configuring Classes.");
                Medium.MediumClass.ConfigureEnabled();
            }
            private static void ConfigureClassFeats()
            {
                Logger.Log("Configuring class features.");
            }
            private static void ConfigureFeats()
            {   
                Logger.Log("Configuring features.");
                //Feint.Feint.ConfigureEnabled();
                DesnaDivineFightingTechnique.ConfigureEnabled();
                BackgroundMedium.ConfigureEnabled();
            }
            private static void ConfigureSpells()
            {
                Logger.Log("Configuring spells.");
            }
            private static void ConfigureFeatsDelayed()
            {
                Logger.Log("Configuring delayed.");
            }
        }
    }
}

