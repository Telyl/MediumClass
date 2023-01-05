using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Guardian
{
    class SeanceBoon
    {
        private static readonly string FeatName = "GuardianSeanceBoon";
        private static readonly string DisplayName = "GuardianSeanceBoon.Name";
        private static readonly string Description = "GuardianSeanceBoon.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SeanceBoon));

        public static void ConfigureEnabled()
        {
            FeatureConfigurator.New(FeatName, Guids.GuardianSeanceBoon)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/spiritguardian.png")
                .AddStatBonus(descriptor: ModifierDescriptor.Other, stat: StatType.AdditionalCMD, value: 1)
                .Configure();
        }
    }
}
