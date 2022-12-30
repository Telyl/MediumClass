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
    [TypeId("13d948daff964e66ad8b8600d8bd00a1")]
    public class AddSpiritSurgeMarshal : UnitFactComponentDelegate,
        IInitiatorRulebookHandler<RuleCalculateAbilityParams>,
        IRulebookHandler<RuleCalculateAbilityParams>,
        IInitiatorRulebookHandler<RuleSkillCheck>, IRulebookHandler<RuleSkillCheck>,
        ISubscriber,
        IInitiatorRulebookSubscriber
    {

        private static readonly ModLogger Logger = Logging.GetLogger("AddSpiritSurgeMarshal");

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
        private static BlueprintUnitFact _marshalSpirit;
        private static BlueprintUnitFact MarshalSpirit
        {
            get
            {
                _marshalSpirit ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.MarshalSpiritBonus);
                return _marshalSpirit;
            }
        }
        private static BlueprintUnitFact _spiritBonus;
        private static BlueprintUnitFact SpiritBonus
        {
            get
            {
                _spiritBonus ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.MarshalSpiritBonus);
                return _spiritBonus;
            }
        }

        /* We're about to roll our dice! We need to modify this! */
        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            UnitEntityData caster = evt.Reason.Caster;
            if (caster.Descriptor.HasFact(SpiritSurge) && caster.Descriptor.HasFact(MarshalSpirit))
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
            if (!caster.Descriptor.HasFact(MarshalSpirit)) { return; }
                if (evt.StatType != StatType.SkillPersuasion || evt.StatType != StatType.SkillUseMagicDevice) { return; }
            if (SurgeDice == null) { return; }
            var spiritbonus = evt.Reason.Caster.Descriptor.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.MarshalSpiritBonus));
            evt.Bonus.AddModifier(RulebookEvent.Dice.D(SurgeDice.GetValueOrDefault()),
                            evt.Reason.Caster.Facts.Get(SpiritSurge), ModifierDescriptor.UntypedStackable);
            evt.Bonus.AddModifier(spiritbonus,
                            evt.Reason.Caster.Facts.Get(SpiritBonus), ModifierDescriptor.UntypedStackable);
            evt.Reason.Caster.Buffs.GetBuff(BlueprintTool.Get<BlueprintBuff>(Guids.SpiritSurgeBuff)).ReduceDuration(reduce1min);
        }

        public void OnEventDidTrigger(RuleSkillCheck evt) { }
    }
}

