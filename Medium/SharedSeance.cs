using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Utility;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium
{
    class SharedSeance
    {
        private static readonly string FeatName = "MediumSharedSeance";
        internal const string DisplayName = "MediumSharedSeance.Name";
        private static readonly string Description = "MediumSharedSeance.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SharedSeance));

        public static void ConfigureEnabled()
        {
            FeatureConfigurator.New(FeatName, Guids.MediumSharedSeance)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .Configure();
        }
        
    }
}
