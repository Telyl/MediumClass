using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Trickster
{
    class LegendaryTrickster
    {
        private static readonly string FeatName = "TricksterLegendaryTrickster";
        private static readonly string DisplayName = "TricksterLegendaryTrickster.Name";
        private static readonly string Description = "TricksterLegendaryTrickster.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(LegendaryTrickster));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Trickster Legendary Trickster");

            BlueprintAbility polymorphability = AbilityConfigurator.New(FeatName + "Polymorph", Guids.TricksterLegendaryTricksterPolymorph)
                .CopyFrom(AbilityRefs.PolymorphGreaterBase, c => c is not (SpellListComponent or RecommendationNoFeatFromGroup))
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetType(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural)
                .Configure();

            BlueprintBuff d20buff = BuffConfigurator.New(FeatName + "D20Buff", Guids.TricksterLegendaryTricksterD20Buff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(BuffRefs.NaturalCharmerBuff.Reference.Get().Icon)
                .AddModifyD20(rule: RuleType.SkillCheck, withChance: false, rollResult: new ContextValue() { Value = 20, }, rollsAmount: 0, takeBest: false, dispellOn20: true, specificSkill: false)
                .Configure();
            
            BlueprintAbilityResource resource = AbilityResourceConfigurator.New(FeatName + "Resource", Guids.TricksterLegendaryTricksterResource)
                .SetMaxAmount(ResourceAmountBuilder.New(1).Build())
                .Configure();
            
            BlueprintAbility d20ability = AbilityConfigurator.New(FeatName + "D20Ability", Guids.TricksterLegendaryTricksterD20Ability)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.NaturalCharmerAbility.Reference.Get().Icon)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuffPermanent(d20buff))
                .AddAbilityResourceLogic(1, isSpendResource:true, requiredResource: resource)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.TricksterLegendaryTrickster)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddAbilityResources(amount: 1, resource: resource, restoreAmount: true)
                .AddFacts(new() { polymorphability, d20ability })
                .Configure();
        }
    }
}
