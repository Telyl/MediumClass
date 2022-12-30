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
using MediumClass.Utilities;
using MediumClass.Utils;

namespace MediumClass.Medium.NewComponents
{
    [AllowedOn(typeof(BlueprintUnitFact))]
    [TypeId("7e01e0ea011746a7add09d95fce54ea5")]
    public class AddSpiritSurgeHierophant : UnitFactComponentDelegate,
        IInitiatorRulebookHandler<RuleCalculateAbilityParams>,
        IRulebookHandler<RuleCalculateAbilityParams>,
        IInitiatorRulebookHandler<RuleSkillCheck>, IRulebookHandler<RuleSkillCheck>,
        IInitiatorRulebookHandler<RuleSavingThrow>, IRulebookHandler<RuleSavingThrow>,
        ISubscriber,
        IInitiatorRulebookSubscriber
    {

        private static readonly ModLogger Logger = Logging.GetLogger(nameof(AddSpiritSurgeHierophant));

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
        private static BlueprintUnitFact _hierophantSpirit;
        private static BlueprintUnitFact HierophantSpirit
        {
            get
            {
                _hierophantSpirit ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.HierophantSpiritBonus);
                return _hierophantSpirit;
            }
        }

        /* We're about to roll our dice! We need to modify this! */
        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            UnitEntityData caster = evt.Reason.Caster;
            if (caster.Descriptor.HasFact(SpiritSurge) && caster.Descriptor.HasFact(HierophantSpirit))
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
            }
        }
        public void OnEventDidTrigger(RuleCalculateAbilityParams evt)
        {

        }

        public void OnEventAboutToTrigger(RuleSkillCheck evt)
        {
            UnitEntityData caster = evt.Reason.Caster;
            if (!caster.Descriptor.HasFact(HierophantSpirit)) { return; }
            if (evt.StatType != StatType.SkillLoreNature || evt.StatType != StatType.SkillLoreReligion || evt.StatType != StatType.SkillPerception) { return; }
            if (SurgeDice == null) { return; }
            evt.Bonus.AddModifier(RulebookEvent.Dice.D(SurgeDice.GetValueOrDefault()),
                            evt.Reason.Caster.Facts.Get(SpiritSurge), ModifierDescriptor.UntypedStackable);
            evt.Reason.Caster.Buffs.GetBuff(BlueprintTool.Get<BlueprintBuff>(Guids.SpiritSurgeBuff)).ReduceDuration(reduce1min);
        }

        public void OnEventDidTrigger(RuleSkillCheck evt) { }

        public void OnEventAboutToTrigger(RuleSavingThrow evt)
        {
            UnitEntityData caster = evt.Reason.Caster;
            if (!caster.Descriptor.HasFact(HierophantSpirit)) { return; }
            if (evt.StatType != StatType.SaveWill) { return; }
            if (SurgeDice == null) { return; }
            evt.AddModifier(bonus: RulebookEvent.Dice.D(SurgeDice.GetValueOrDefault()),
                            descriptor: ModifierDescriptor.UntypedStackable,
                            source: evt.Reason.Caster.Facts.Get(SpiritSurge));
            evt.Reason.Caster.Buffs.GetBuff(BlueprintTool.Get<BlueprintBuff>(Guids.SpiritSurgeBuff)).ReduceDuration(reduce1min);
        }

        public void OnEventDidTrigger(RuleSavingThrow evt) { }
    }
}

