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

            var magedebuff = BuffConfigurator.New(FeatName + "Archmage", Guids.MediumInfluenceArchmage).Configure();
            BuffConfigurator.For(magedebuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(magedebuff))
                .SetIcon(BuffRefs.Asmodeus_Debuff.Reference.Get().Icon)
                .AddComponent<ContextSpiritInfluence>()
                .Configure();

            var champdebuff = BuffConfigurator.New(FeatName + "Champion", Guids.MediumInfluenceChampion).Configure();
            BuffConfigurator.For(champdebuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(champdebuff))
                .SetIcon(BuffRefs.Asmodeus_Debuff.Reference.Get().Icon)
                .AddComponent<ContextSpiritInfluence>()
                .Configure();

            var guarddebuff = BuffConfigurator.New(FeatName + "Guardian", Guids.MediumInfluenceGuardian).Configure();
            BuffConfigurator.For(guarddebuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(guarddebuff))
                .SetIcon(BuffRefs.FightDefensivelyBuff.Reference.Get().Icon)
                .AddFacts(new() { BuffRefs.FightDefensivelyBuff.Reference.Get() })
                .Configure();
            var clericdebuff = BuffConfigurator.New(FeatName + "Hierophant", Guids.MediumInfluenceHierophant).Configure();
            BuffConfigurator.For(clericdebuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(clericdebuff))
                .SetIcon(BuffRefs.Asmodeus_Debuff.Reference.Get().Icon)
                .AddComponent<ContextSpiritInfluence>()
                .Configure();

            var barddebuff = BuffConfigurator.New(FeatName + "Marshal", Guids.MediumInfluenceMarshal).Configure();
            BuffConfigurator.For(barddebuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(barddebuff))
                .SetIcon(BuffRefs.Asmodeus_Debuff.Reference.Get().Icon)
                .AddComponent<ContextSpiritInfluence>()
                .Configure();

            var roguedebuff = BuffConfigurator.New(FeatName + "Trickster", Guids.MediumInfluenceTrickster).Configure();
            BuffConfigurator.For(roguedebuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(roguedebuff))
                .SetIcon(BuffRefs.Asmodeus_Debuff.Reference.Get().Icon)
                .AddComponent<ContextSpiritInfluence>()
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
                .Configure();
        }
        
    }
}
