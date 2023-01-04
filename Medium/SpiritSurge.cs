using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using MediumClass.Medium.NewActions;
using MediumClass.Medium.NewComponents;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Activatable embedded menu. Investigator class by SIGURD
namespace MediumClass.Medium
{
    class SpiritSurge
    {
        private static readonly string FeatName = "SpiritSurge";
        private static readonly string DisplayName = "SpiritSurge.Name";
        private static readonly string Description = "SpiritSurge.Description";
       
        public static void ConfigureEnabled()
        {
            var feat = FeatureConfigurator.New(FeatName, Guids.SpiritSurge)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetHideInUI(false)
                .SetHideNotAvailibleInUI(false)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .SetRanks(10)
                .SetAllowNonContextActions(false)
                .Configure();

            var buff = BuffConfigurator.New(FeatName + "Buff", Guids.SpiritSurgeBuff)
            .SetIcon(AbilityRefs.EldritchFontGreaterSurgeAbility.Reference.Get().Icon)
            .AddComponent<MediumSpiritSurgeComponent>()
            .AddContextRankConfig(
                    ContextRankConfigs.FeatureRank(Guids.SpiritSurge, max: 20, min: 1))
            .Configure();

            var ability = AbilityConfigurator.New(FeatName + "Ability", Guids.SpiritSurgeAbility)
                .SetIcon(AbilityRefs.EldritchFontGreaterSurgeAbility.Reference.Get().Icon)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddComponent<AbilityRequirementHasSpirit>()
                .AddComponent<AbilityRequirementNotMarshalsOrder>()
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .AddAbilityResourceLogic(amount: 1, requiredResource: Guids.MediumInfluenceResource, isSpendResource: true, costIsCustom: false)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .Add<ContextActionSpiritInfluence>()
                        .ApplyBuff(buff, ContextDuration.Fixed(1, DurationRate.Minutes),
                                    asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();

            FeatureConfigurator.For(feat)
                .AddFacts(new() { ability })
                .AddContextRankConfig(new ContextRankConfig
                {
                    m_Type = AbilityRankType.Default,
                    m_BaseValueType = ContextRankBaseValueType.FeatureRank,
                    m_Feature = feat.ToReference<BlueprintFeatureReference>(),
                    m_Stat = StatType.Unknown,
                    m_Buff = null,
                    m_Progression = ContextRankProgression.AsIs,
                    m_StartLevel = 0,
                    m_StepLevel = 0,
                    m_UseMin = false,
                    m_Min = 0,
                    m_UseMax = false,
                    m_Max = 20,
                    m_ExceptClasses = false,
                    Archetype = null
                })
                .Configure();
        }
    }
}
