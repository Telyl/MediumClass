using BlueprintCore.Utils;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.ResourceLinks;
using System;
using Kingmaker.Utility;
using Kingmaker.Enums;
using Kingmaker.Blueprints.Facts;
using static UnityModManagerNet.UnityModManager.ModEntry;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.Blueprints;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Parts;
using UnityEngine;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using MediumClass.Utils;
using MediumClass.Utilities;
using BlueprintCore.Blueprints.References;
using Kingmaker.UnitLogic.FactLogic;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
    [AllowedOn(typeof(BlueprintUnitFact))]
    [TypeId("842af9e297aa42619768f8fb5bbaf8ee")]
    public class ContextSpiritInfluence : AddContextStatBonus,
        ISubscriber,
        IInitiatorRulebookSubscriber
    {

        private static readonly ModLogger Logger = Logging.GetLogger(nameof(CheckInfluence));

        public override void OnTurnOn()
        {

            ModifiableValue init_stat = base.Owner.Stats.GetStat(StatType.Initiative);
            ModifiableValue will_stat= base.Owner.Stats.GetStat(StatType.SaveWill);
            init_stat.AddModifier(-2, base.Runtime, ModifierDescriptor.Penalty);
            will_stat.AddModifier(2, base.Runtime, ModifierDescriptor.UntypedStackable);

            if (base.Owner.Descriptor.HasFact(BlueprintTool.Get<BlueprintFeature>(Guids.ArchmageSpiritBonus)))
            {
                var archmage_bonus = base.Owner.Descriptor.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.ArchmageSpiritBonus));
                ModifiableValue ab_stat = base.Owner.Stats.GetStat(StatType.AdditionalAttackBonus);
                ModifiableValue dmg_stat = base.Owner.Stats.GetStat(StatType.AdditionalDamage);
                ModifiableValue athletics_stat = base.Owner.Stats.GetStat(StatType.SkillAthletics);
                ModifiableValue fortitude_stat = base.Owner.Stats.GetStat(StatType.SaveFortitude);
                ab_stat.AddModifier(archmage_bonus * -1, base.Runtime, ModifierDescriptor.Penalty);
                dmg_stat.AddModifier(archmage_bonus * -1, base.Runtime, ModifierDescriptor.Penalty);
                athletics_stat.AddModifier(archmage_bonus * -1, base.Runtime, ModifierDescriptor.Penalty);
                fortitude_stat.AddModifier(archmage_bonus * -1, base.Runtime, ModifierDescriptor.Penalty);
            }
            else if(base.Owner.Descriptor.HasFact(BlueprintTool.Get<BlueprintFeature>(Guids.ChampionSpiritBonus)))
            {
                var champion_bonus = base.Owner.Descriptor.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.ChampionSpiritBonus));
                ModifiableValue casterlevel_stat = base.Owner.Stats.GetStat(StatType.BonusCasterLevel);
                ModifiableValue arcana_stat = base.Owner.Stats.GetStat(StatType.SkillKnowledgeArcana);
                ModifiableValue world_stat = base.Owner.Stats.GetStat(StatType.SkillKnowledgeWorld);
                casterlevel_stat.AddModifier(champion_bonus * -1, base.Runtime, ModifierDescriptor.Penalty);
                arcana_stat.AddModifier(champion_bonus * -1, base.Runtime, ModifierDescriptor.Penalty);
                world_stat.AddModifier(champion_bonus * -1, base.Runtime, ModifierDescriptor.Penalty);
            }
            else if (base.Owner.Descriptor.HasFact(BlueprintTool.Get<BlueprintFeature>(Guids.GuardianSpiritBonus)))
            {
                var guardian_bonus = base.Owner.Descriptor.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.GuardianSpiritBonus));
                base.Owner.AddBuff(BuffRefs.FightDefensivelyBuff.Reference.Get(), base.Context, new TimeSpan(24, 0, 0));
            }
            else if (base.Owner.Descriptor.HasFact(BlueprintTool.Get<BlueprintFeature>(Guids.HierophantSpiritBonus)))
            {
                var hierophant_bonus = base.Owner.Descriptor.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.HierophantSpiritBonus));
                ModifiableValue persuasion_stat = base.Owner.Stats.GetStat(StatType.SkillPersuasion);
                ModifiableValue umd_stat = base.Owner.Stats.GetStat(StatType.SkillUseMagicDevice);
                persuasion_stat.AddModifier(hierophant_bonus * -1, base.Runtime, ModifierDescriptor.Penalty);
                umd_stat.AddModifier(hierophant_bonus * -1, base.Runtime, ModifierDescriptor.Penalty);
            }
            else if (base.Owner.Descriptor.HasFact(BlueprintTool.Get<BlueprintFeature>(Guids.MarshalSpiritBonus)))
            {
                var marshal_bonus = base.Owner.Descriptor.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.MarshalSpiritBonus));
                ModifiableValue lore_stat = base.Owner.Stats.GetStat(StatType.SkillLoreNature);
                ModifiableValue religion_stat = base.Owner.Stats.GetStat(StatType.SkillLoreReligion);
                lore_stat.AddModifier(marshal_bonus * -1, base.Runtime, ModifierDescriptor.Penalty);
                religion_stat.AddModifier(marshal_bonus * -1, base.Runtime, ModifierDescriptor.Penalty);
            }
            else if (base.Owner.Descriptor.HasFact(BlueprintTool.Get<BlueprintFeature>(Guids.TricksterSpiritBonus)))
            {
                var trickster_bonus = base.Owner.Descriptor.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.TricksterSpiritBonus));
                // TBD
            }

            base.OnTurnOn();
        }

        public override void OnTurnOff()
        {
            ModifiableValue ab_stat = base.Owner.Stats.GetStat(StatType.AdditionalAttackBonus);
            ModifiableValue dmg_stat = base.Owner.Stats.GetStat(StatType.AdditionalDamage);
            ModifiableValue athletics_stat = base.Owner.Stats.GetStat(StatType.SkillAthletics);
            ModifiableValue fortitude_stat = base.Owner.Stats.GetStat(StatType.SaveFortitude);
            ModifiableValue init_stat = base.Owner.Stats.GetStat(StatType.Initiative);
            ModifiableValue will_stat = base.Owner.Stats.GetStat(StatType.SaveWill);
            ModifiableValue casterlevel_stat = base.Owner.Stats.GetStat(StatType.BonusCasterLevel);
            ModifiableValue arcana_stat = base.Owner.Stats.GetStat(StatType.SkillKnowledgeArcana);
            ModifiableValue world_stat = base.Owner.Stats.GetStat(StatType.SkillKnowledgeWorld);
            ModifiableValue persuasion_stat = base.Owner.Stats.GetStat(StatType.SkillPersuasion);
            ModifiableValue umd_stat = base.Owner.Stats.GetStat(StatType.SkillUseMagicDevice);
            ModifiableValue lore_stat = base.Owner.Stats.GetStat(StatType.SkillLoreNature);
            ModifiableValue religion_stat = base.Owner.Stats.GetStat(StatType.SkillLoreReligion);

            //Archmage
            ab_stat.RemoveModifiersFrom(base.Runtime);
            dmg_stat.RemoveModifiersFrom(base.Runtime);
            athletics_stat.RemoveModifiersFrom(base.Runtime);
            fortitude_stat.RemoveModifiersFrom(base.Runtime);
            //All
            init_stat.RemoveModifiersFrom(base.Runtime);
            will_stat.RemoveModifiersFrom(base.Runtime);
            //Champion
            casterlevel_stat.RemoveModifiersFrom(base.Runtime);
            arcana_stat.RemoveModifiersFrom(base.Runtime);
            world_stat.RemoveModifiersFrom(base.Runtime);
            //Guardian
            base.Owner.Buffs.RemoveFact(BuffRefs.FightDefensivelyBuff.Reference.Get());
            //Hierophant
            persuasion_stat.RemoveModifiersFrom(base.Runtime);
            umd_stat.RemoveModifiersFrom(base.Runtime);
            //Marshal
            lore_stat.RemoveModifiersFrom(base.Runtime);
            religion_stat.RemoveModifiersFrom(base.Runtime);
            //Trickster


            base.OnTurnOff();
        }
    }
}

