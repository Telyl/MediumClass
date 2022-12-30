﻿using BlueprintCore.Utils;
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

namespace MediumClass.Medium.NewComponents
{
    [AllowedOn(typeof(BlueprintUnitFact))]
    [TypeId("bcb943b96151484c961b2a68db3b9349")]
    public class AddSpiritSurgeArchmage : UnitFactComponentDelegate,
        IInitiatorRulebookHandler<RuleCalculateAbilityParams>,
        IRulebookHandler<RuleCalculateAbilityParams>, IInitiatorRulebookHandler<RuleSkillCheck>,
        IRulebookHandler<RuleSkillCheck>, IInitiatorRulebookHandler<RuleCheckConcentration>,
        IRulebookHandler<RuleCheckConcentration>,
        ISubscriber,
        IInitiatorRulebookSubscriber
    {

        private static readonly ModLogger Logger = Logging.GetLogger("SpiritSurgeArchmage");

        private static DiceFormula? SurgeDice;
        private static TimeSpan reduce1min = new TimeSpan(0, 0, 1, 0, 0);
        private static BlueprintUnitFact _spiritSurge;
        private static BlueprintUnitFact SpiritSurge
        {
            get
            {
                _spiritSurge ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.SpiritSurge);
                return _spiritSurge;
            }
        }

        private static BlueprintUnitFact _archmageSpirit;
        private static BlueprintUnitFact ArchmageSpirit
        {
            get
            {
                _archmageSpirit ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.ArchmageSpiritBonus);
                return _archmageSpirit;
            }
        }

        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            UnitEntityData caster = evt.Reason.Caster;
            if (caster.Descriptor.HasFact(SpiritSurge) && caster.Descriptor.HasFact(ArchmageSpirit))
            {
                var spiritsurgeranks = caster.Descriptor.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.SpiritSurge));
                SurgeDice = spiritsurgeranks switch
                {
                    1 => new DiceFormula(1, DiceType.D6),
                    2 => new DiceFormula(1, DiceType.D8),
                    3 => new DiceFormula(1, DiceType.D10),
                    4 => new DiceFormula(2, DiceType.D8),
                    _ => null
                };
                evt.AddBonusConcentration(RulebookEvent.Dice.D(SurgeDice.GetValueOrDefault()));
            }
        }
        public void OnEventDidTrigger(RuleCalculateAbilityParams evt) {

        }
        
        // Do the skill check bonuses for Spirit Surge
        public void OnEventAboutToTrigger(RuleSkillCheck evt)
        {
            UnitEntityData caster = evt.Reason.Caster;
            if (!caster.Descriptor.HasFact(ArchmageSpirit)) { return; }
            if (evt.StatType != StatType.SkillKnowledgeArcana || evt.StatType != StatType.SkillKnowledgeWorld) { return; }
            if (SurgeDice == null) { return; }
            evt.Bonus.AddModifier(RulebookEvent.Dice.D(SurgeDice.GetValueOrDefault()),
                            evt.Reason.Caster.Facts.Get(SpiritSurge), ModifierDescriptor.UntypedStackable);
            evt.Reason.Caster.Buffs.GetBuff(BlueprintTool.Get<BlueprintBuff>(Guids.SpiritSurgeBuff)).ReduceDuration(reduce1min);
        }
        public void OnEventDidTrigger(RuleSkillCheck evt) { }
        
        // Do the Concentration Check Bonus for Spirit Surge
        public void OnEventAboutToTrigger(RuleCheckConcentration evt)
        {
            UnitEntityData caster = evt.Reason.Caster;
            // Because we already added bonus concentration at an early calculation, all we hav eto do is reduce the duration.
            if (SurgeDice == null) { return; }
            if (!caster.Descriptor.HasFact(ArchmageSpirit)) { return; }
            evt.Reason.Caster.Buffs.GetBuff(BlueprintTool.Get<BlueprintBuff>(Guids.SpiritSurgeBuff)).ReduceDuration(reduce1min);
        }
        public void OnEventDidTrigger(RuleCheckConcentration evt) { }
    }
}

