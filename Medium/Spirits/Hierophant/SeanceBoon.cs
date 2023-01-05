using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Hierophant
{
    class SeanceBoon
    {
        private static readonly string FeatName = "HierophantSeanceBoon";
        private static readonly string DisplayName = "HierophantSeanceBoon.Name";
        private static readonly string Description = "HierophantSeanceBoon.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SeanceBoon));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Hierophant Seance Boon");
            FeatureConfigurator.New(FeatName, Guids.HierophantSeanceBoon)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/spirithierophant.png")
                .AddIncreaseSpellHealing(2)
                .Configure();
        }
    }
}
