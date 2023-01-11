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
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Mechanics.Components;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Guardian
{
    class GuardianSmite
    {
        private static readonly string FeatName = "GuardianGreater";
        private static readonly string DisplayName = "GuardianGreater.Name";
        private static readonly string Description = "GuardianGreater.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(GuardianSmite));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Guardian Greater Ability");

            var ability = AbilityConfigurator.New(FeatName + "Ability", Guids.GuardianGreaterAbility)
                .CopyFrom(AbilityRefs.SmiteEvilAbility, c => c is not (ContextRankConfig or AbilityResourceLogic or AbilityCasterAlignment))
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/guardiansmite.png")
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddContextRankConfig(ContextRankConfigs.StatBonus(stat: StatType.Charisma, min: 0, max: 20))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }, type: AbilityRankType.DamageBonus, max: 20, min: 0))
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.GuardianGreater)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { ability })
                .Configure();

            var secondaryability = AbilityConfigurator.New(FeatName + "SecondaryAbility", Guids.SecondaryGuardianGreaterAbility)
                .CopyFrom(ability, c => c is not AbilityResourceLogic)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResourceGuardian)
                .Configure();

            FeatureConfigurator.New(FeatName + "Secondary", Guids.SecondaryGuardianGreater)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { secondaryability })
                .AddRemoveFeatureOnApply(Guids.GuardianGreater)
                .Configure();
        }
    }
}
