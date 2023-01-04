using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Hierophant
{
    class OverflowingGrace
    {
        private static readonly string FeatName = "HierophantOverflowingGrace";
        private static readonly string DisplayName = "HierophantOverflowingGrace.Name";
        private static readonly string Description = "HierophantOverflowingGrace.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(OverflowingGrace));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Hierophant Overflowing Grace");
            BlueprintBuff overflowingbuff = BuffConfigurator.New(FeatName + "SacredBuff", Guids.HierophantOverflowingGraceSacred)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.AdditionalAttackBonus, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SkillAthletics, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SkillKnowledgeArcana, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SkillKnowledgeWorld, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SkillLoreNature, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SkillLoreReligion, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SkillMobility, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SkillPerception, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SkillPersuasion, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SkillStealth, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SkillThievery, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SkillUseMagicDevice, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SaveFortitude, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SaveReflex, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Sacred, stat: StatType.SaveWill, value: 1)
                .Configure();

            BlueprintBuff overflowingprofane = BuffConfigurator.New(FeatName + "ProfaneBuff", Guids.HierophantOverflowingGraceProfane)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.AdditionalAttackBonus, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SkillAthletics, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SkillKnowledgeArcana, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SkillKnowledgeWorld, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SkillLoreNature, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SkillLoreReligion, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SkillMobility, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SkillPerception, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SkillPersuasion, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SkillStealth, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SkillThievery, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SkillUseMagicDevice, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SaveFortitude, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SaveReflex, value: 1)
                .AddStatBonus(descriptor: ModifierDescriptor.Profane, stat: StatType.SaveWill, value: 1)
                .Configure();

            FeatureConfigurator.New(FeatName + "OverflowingGrace", Guids.HierophantOverflowingGrace)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddOverHealTrigger(maxValue: new ContextValue()
                {
                    Value = 1,
                }, limitMaximum: true, actionOnTarget: ActionsBuilder.New().Conditional(ConditionsBuilder.New().IsAlly().HasFact(FeatureRefs.NegativeEnergyAffinity.Reference.Get(), true),
                    ifTrue: ActionsBuilder.New().ApplyBuff(buff: overflowingbuff, asChild: true, isFromSpell: false, isNotDispelable: true, toCaster: false, durationValue: ContextDuration.Fixed(1, DurationRate.Rounds))))
                .AddOverHealTrigger(maxValue: new ContextValue()
                {
                    Value = 1,
                }, limitMaximum: true, actionOnTarget: ActionsBuilder.New().Conditional(ConditionsBuilder.New().IsAlly().HasFact(FeatureRefs.LifeDominantSoul.Reference.Get()),
                    ifTrue: ActionsBuilder.New().ApplyBuff(buff: overflowingbuff, asChild: true, isFromSpell: false, isNotDispelable: true, toCaster: false, durationValue: ContextDuration.Fixed(1, DurationRate.Rounds))))
                .AddOverHealTrigger(maxValue: new ContextValue()
                {
                    Value = 1,
                }, limitMaximum: true, actionOnTarget: ActionsBuilder.New().Conditional(ConditionsBuilder.New().IsAlly().HasFact(FeatureRefs.NegativeEnergyAffinity.Reference.Get()),
                    ifTrue: ActionsBuilder.New().ApplyBuff(buff: overflowingprofane, asChild: true, isFromSpell: false, isNotDispelable: true, toCaster: false, durationValue: ContextDuration.Fixed(1, DurationRate.Rounds))))
                .Configure();
        }
    }
}
