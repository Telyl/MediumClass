using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using MediumClass.Medium.NewComponents;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Marshal
{
    class InspiringCall
    {
        private static readonly string FeatName = "MarshalInspiringCall";
        private static readonly string DisplayName = "MarshalInspiringCall.Name";
        private static readonly string DisplayNameSaves = "MarshalInspiringCallSaves.Name";
        private static readonly string DisplayNameAttack = "MarshalInspiringCallAttack.Name";
        private static readonly string Description = "MarshalInspiringCall.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(InspiringCall));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Marshal Inspiring Call");

            var savingthrowsbuff = BuffConfigurator.New(FeatName + "SavingThrowBuff", Guids.MarshalInspiringCallSavingThrowBuff)
                .SetDisplayName(DisplayNameSaves)
                .SetDescription(Description)
                .SetIcon("assets/icons/inspiringcallsaves.png")
                .SetFxOnStart(fxOnStart: "a68e191c519cae741b6c4177b4d13ef6")
                .AddContextStatBonus(descriptor: ModifierDescriptor.Competence, stat: StatType.SaveFortitude, value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank,
                    Value = 0,
                    ValueRank = AbilityRankType.Default,
                })
                .AddContextStatBonus(descriptor: ModifierDescriptor.Competence, stat: StatType.SaveReflex, value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank,
                    Value = 0,
                    ValueRank = AbilityRankType.Default,
                })
                .AddContextStatBonus(descriptor: ModifierDescriptor.Competence, stat: StatType.SaveWill, value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank,
                    Value = 0,
                    ValueRank = AbilityRankType.Default,
                })
                .AddContextRankConfig(new ContextRankConfig
                {
                    m_Type = AbilityRankType.Default,
                    m_BaseValueType = ContextRankBaseValueType.FeatureRank,
                    m_Feature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MediumSpiritBonus),
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

            var attackbuff = BuffConfigurator.New(FeatName +"AttackBuff", Guids.MarshalInspiringCallAttackBuff)
                .SetDisplayName(DisplayNameAttack)
                .SetDescription(Description)
                .SetIcon("assets/icons/inspiringcallattack.png")
                .SetFxOnStart(fxOnStart: "a68e191c519cae741b6c4177b4d13ef6")
                .AddContextStatBonus(descriptor: ModifierDescriptor.Competence, stat: StatType.AdditionalAttackBonus, value: new ContextValue() { 
                    ValueType = ContextValueType.Rank,
                    Value = 0,
                    ValueRank = AbilityRankType.Default,
                })
                .AddContextStatBonus(descriptor: ModifierDescriptor.Competence, stat: StatType.AdditionalDamage, value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank,
                    Value = 0,
                    ValueRank = AbilityRankType.Default,
                })
                .AddContextRankConfig(new ContextRankConfig
                {
                    m_Type = AbilityRankType.Default,
                    m_BaseValueType = ContextRankBaseValueType.FeatureRank,
                    m_Feature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MediumSpiritBonus),
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

            var savingthrowabilityswift = AbilityConfigurator.New(FeatName + "SavingThrowAbilitySwift", Guids.MarshalInspiringCallSavingThrowAbilitySwift)
                .SetDisplayName(DisplayNameSaves)
                .SetDescription(Description)
                .SetIcon("assets/icons/inspiringcallsaves.png")
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuff(savingthrowsbuff, durationValue: ContextDuration.Fixed(1, Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)))
                .AddAbilityTargetsAround(radius: FeetExtension.Feet(50), spreadSpeed: FeetExtension.Feet(0), targetType: TargetType.Ally)
                .SetActionType(UnitCommand.CommandType.Swift)
                .Configure();

            var attackabilityswift = AbilityConfigurator.New(FeatName + "AttackAbilitySwift", Guids.MarshalInspiringCallAttackAbilitySwift)
                .SetDisplayName(DisplayNameAttack)
                .SetDescription(Description)
                .SetIcon("assets/icons/inspiringcallattack.png")
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuff(attackbuff, durationValue: ContextDuration.Fixed(1, Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)))
                .AddAbilityTargetsAround(radius: FeetExtension.Feet(50), spreadSpeed: FeetExtension.Feet(0), targetType: TargetType.Ally)
                .SetActionType(UnitCommand.CommandType.Swift)
                .Configure();

            var savingthrowabilitymove = AbilityConfigurator.New(FeatName + "SavingThrowAbilityMove", Guids.MarshalInspiringCallSavingThrowAbilityMove)
                .SetDisplayName(DisplayNameSaves)
                .SetDescription(Description)
                .SetIcon("assets/icons/inspiringcallsaves.png")
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuff(savingthrowsbuff, durationValue: ContextDuration.Fixed(1, Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)))
                .AddAbilityTargetsAround(radius: FeetExtension.Feet(50), spreadSpeed: FeetExtension.Feet(0), targetType: TargetType.Ally)
                .SetActionType(UnitCommand.CommandType.Move)
                .Configure();

            var attackabilitymove = AbilityConfigurator.New(FeatName + "AttackAbilityMove", Guids.MarshalInspiringCallAttackAbilityMove)
                .SetDisplayName(DisplayNameAttack)
                .SetDescription(Description)
                .SetIcon("assets/icons/inspiringcallattack.png")
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuff(attackbuff, durationValue: ContextDuration.Fixed(1, Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)))
                .AddAbilityTargetsAround(radius: FeetExtension.Feet(50), spreadSpeed: FeetExtension.Feet(0), targetType: TargetType.Ally)
                .SetActionType(UnitCommand.CommandType.Move)
                .Configure();

            var savingthrowabilitystandard = AbilityConfigurator.New(FeatName + "SavingThrowAbilityStandard", Guids.MarshalInspiringCallSavingThrowAbilityStandard)
                .SetDisplayName(DisplayNameSaves)
                .SetDescription(Description)
                .SetIcon("assets/icons/inspiringcallsaves.png")
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuff(savingthrowsbuff, durationValue: ContextDuration.Fixed(1, Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)))
                .AddAbilityTargetsAround(radius: FeetExtension.Feet(50), spreadSpeed: FeetExtension.Feet(0), targetType: TargetType.Ally)
                .Configure();

            var attackabilitystandard = AbilityConfigurator.New(FeatName + "AttackAbilityStandard", Guids.MarshalInspiringCallAttackAbilityStandard)
                .SetDisplayName(DisplayNameAttack)
                .SetDescription(Description)
                .SetIcon("assets/icons/inspiringcallattack.png")
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuff(attackbuff, durationValue: ContextDuration.Fixed(1, Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)))
                .AddAbilityTargetsAround(radius: FeetExtension.Feet(50), spreadSpeed: FeetExtension.Feet(0), targetType: TargetType.Ally)
                .Configure();

            var abilityswift = AbilityConfigurator.New(FeatName + "AbilitySwift", Guids.MarshalInspiringCallAbilitySwift)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/marshalinspiringcall.png")
                .SetActionType(UnitCommand.CommandType.Free)
                .AddAbilityVariants(new() { savingthrowabilityswift, attackabilityswift })
                .Configure();

            var abilitymove = AbilityConfigurator.New(FeatName + "AbilityMove", Guids.MarshalInspiringCallAbilityMove)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/marshalinspiringcall.png")
                .SetActionType(UnitCommand.CommandType.Free)
                .AddAbilityVariants(new() { savingthrowabilitymove, attackabilitymove })
                .Configure();

            var ability = AbilityConfigurator.New(FeatName + "AbilityStandard", Guids.MarshalInspiringCallAbilityStandard)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/marshalinspiringcall.png")
                .SetActionType(UnitCommand.CommandType.Free)
                .AddAbilityVariants(new() { savingthrowabilitystandard, attackabilitystandard })
                .Configure();

            FeatureConfigurator.New(FeatName + "Swift", Guids.MarshalInspiringCallSwift)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(BuffRefs.InspireCompetenceEffectBuff.Reference.Get().Icon)
                .AddFacts(new() { abilityswift })
                .AddRemoveFeatureOnApply(Guids.MarshalInspiringCallMove)
                .Configure();

            FeatureConfigurator.New(FeatName + "Move", Guids.MarshalInspiringCallMove)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(BuffRefs.InspireCompetenceEffectBuff.Reference.Get().Icon)
                .AddFacts(new() { abilitymove })
                .AddRemoveFeatureOnApply(Guids.MarshalInspiringCallStandard)
                .Configure();

            FeatureConfigurator.New(FeatName + "Standard", Guids.MarshalInspiringCallStandard)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(BuffRefs.InspireCompetenceEffectBuff.Reference.Get().Icon)
                .AddFacts(new() { ability })
                .Configure();
        }
    }
}
