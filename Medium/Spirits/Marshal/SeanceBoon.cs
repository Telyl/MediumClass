using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Marshal
{
    class SeanceBoon
    {
        private static readonly string FeatName = "MarshalSeanceBoon";
        private static readonly string DisplayName = "MarshalSeanceBoon.Name";
        private static readonly string Description = "MarshalSeanceBoon.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SeanceBoon));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Marshal Seance Boon");
            var MarshalSeanceBoonResource = AbilityResourceConfigurator.New(FeatName + "Resource", Guids.MarshalSeanceBoonResource)
                .SetMaxAmount(ResourceAmountBuilder.New(1).Build())
                .Configure();

            var ability = AbilityConfigurator.New(FeatName + "Ability", Guids.MarshalSeanceBoonAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/spiritmarshal.png")
                .AddAbilityApplyFact(hasDuration: false, restriction: AbilityApplyFact.FactRestriction.CasterHasNoFact, facts: new() { Guids.ArchmageSeanceBoon, Guids.ChampionSeanceBoon, Guids.GuardianSeanceBoon, Guids.HierophantSeanceBoon, Guids.TricksterSeanceBoon })
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: MarshalSeanceBoonResource)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.MarshalSeanceBoon)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { ability })
                .AddAbilityResources(amount: 1, restoreAmount: true, resource: MarshalSeanceBoonResource)
                .Configure();
        }
    }
}
