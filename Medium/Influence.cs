using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium
{
    class Influence
    {
        private static readonly string FeatName = "MediumInfluence";
        internal const string DisplayName = "MediumInfluence.Name";
        private static readonly string Description = "MediumInfluence.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Influence));

        public static void ConfigureEnabled()
        {

            var debuff = BuffConfigurator.New(FeatName + "Debuff", Guids.MediumInfluenceDebuff).Configure();
            BuffConfigurator.For(debuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(debuff))
                .SetIcon(BuffRefs.Fatigued.Reference.Get().Icon)
                .AddComponent<MediumInfluencePenaltyComponent>()
                .Configure();

            var resource = AbilityResourceConfigurator.New(FeatName + "Resource", Guids.MediumInfluenceResource)
                .SetMaxAmount(new BlueprintAbilityResource.Amount
                {
                    BaseValue = 5,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Charisma
                })
                .SetMax(10)
                .SetUseMax(false)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.MediumInfluence)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIsClassFeature()
                .SetHideInCharacterSheetAndLevelUp()
                .SetHideInUI()
                .AddAbilityResources(amount: 0, resource: resource, restoreAmount: true, restoreOnLevelUp: false, useThisAsResource: false)
                .AddAbilityResources(amount: 0, resource: Guids.MediumInfluenceResourceArchmage, useThisAsResource: false, restoreAmount: true)
                .AddAbilityResources(amount: 0, resource: Guids.MediumInfluenceResourceChampion, useThisAsResource: false, restoreAmount: true)
                .AddAbilityResources(amount: 0, resource: Guids.MediumInfluenceResourceGuardian, useThisAsResource: false, restoreAmount: true)
                .AddAbilityResources(amount: 0, resource: Guids.MediumInfluenceResourceHierophant, useThisAsResource: false, restoreAmount: true)
                .AddAbilityResources(amount: 0, resource: Guids.MediumInfluenceResourceMarshal, useThisAsResource: false, restoreAmount: true)
                .AddAbilityResources(amount: 0, resource: Guids.MediumInfluenceResourceTrickster, useThisAsResource: false, restoreAmount: true)
                .AddAbilityResources(amount: 0, resource: Guids.HierophantEnergyFontResource, restoreAmount: true, restoreOnLevelUp: false, useThisAsResource: false)
                .AddAbilityResources(amount: 1, resource: Guids.TricksterLegendaryTricksterResource, restoreAmount: true)
                .AddAbilityResources(amount: 1, restoreAmount: true, resource: Guids.TricksterEdgeResource)
                .Configure();
        }
        
    }
}
