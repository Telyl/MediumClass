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
    class LegendaryGuardian
    {
        private static readonly string FeatName = "LegendaryGuardian";
        private static readonly string DisplayName = "LegendaryGuardian.Name";
        private static readonly string Description = "LegendaryGuardian.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(LegendaryGuardian));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Legendary Guardian");
            var resource = AbilityResourceConfigurator.New(FeatName + "Resource", Guids.LegendaryGuardianResource)
                .SetMaxAmount(new BlueprintAbilityResource.Amount
                {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma
                })
                .SetMax(10)
                .SetUseMax(false)
                .Configure();

            var buff = BuffConfigurator.New(FeatName + "Buff", Guids.LegendaryGuardianBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(BuffRefs.DefensiveStanceBuff.Reference.Get().Icon)
                .AddIgnoreIncommingDamage()
                .AddRemoveBuffIfPartyNotInCombat()
                .Configure();

            var ability = AbilityConfigurator.New(FeatName + "Ability", Guids.LegendaryGuardianAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuffPermanent(buff))
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .AddAbilityResourceLogic(amount: 1, requiredResource: Guids.LegendaryGuardianResource, isSpendResource: true, costIsCustom: false)
                .SetIcon(BuffRefs.DefensiveStanceBuff.Reference.Get().Icon)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.LegendaryGuardian)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { ability })
                .AddAbilityResources(amount: 0, resource: resource, restoreAmount: true, restoreOnLevelUp: false, useThisAsResource: false)
                .Configure();
        }
    }
}
