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

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
    [AllowedOn(typeof(BlueprintUnitFact))]
    [TypeId("e6f1c2c497fb477f8cb36c913036dad3")]
    public class CheckInfluence : UnitFactComponentDelegate,
        IInitiatorRulebookHandler<RuleCastSpell>,
        IRulebookHandler<RuleCastSpell>,
        ISubscriber,
        IInitiatorRulebookSubscriber
    {

        private static readonly ModLogger Logger = Logging.GetLogger(nameof(CheckInfluence));
        private static BlueprintUnitFact _archmageSpirit;
        private static BlueprintUnitFact ArchmageSpirit
        {
            get
            {
                _archmageSpirit ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.ArchmageSpiritBonus);
                return _archmageSpirit;
            }
        }
        private static BlueprintUnitFact _championSpirit;
        private static BlueprintUnitFact ChampionSpirit
        {
            get
            {
                _championSpirit ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.ChampionSpiritBonus);
                return _championSpirit;
            }
        }
        private static BlueprintUnitFact _guardianSpirit;
        private static BlueprintUnitFact GuardianSpirit
        {
            get
            {
                _guardianSpirit ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.GuardianSpiritBonus);
                return _guardianSpirit;
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
        private static BlueprintUnitFact _marshalSpirit;
        private static BlueprintUnitFact MarshalSpirit
        {
            get
            {
                _marshalSpirit ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.MarshalSpiritBonus);
                return _marshalSpirit;
            }
        }
        private static BlueprintUnitFact _tricksterSpirit;
        private static BlueprintUnitFact TricksterSpirit
        {
            get
            {
                _tricksterSpirit ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.TricksterSpiritBonus);
                return _tricksterSpirit;
            }
        }

        public void OnEventAboutToTrigger(RuleCastSpell evt)
        {

        }
        public void OnEventDidTrigger(RuleCastSpell evt)
        {
            try
            {
                UnitEntityData caster = base.Owner;
                bool isMemorizedSpell = false;
                if (caster.Descriptor.HasFact(ArchmageSpirit))
                {
                    if (evt.Context.Ability.Blueprint.IsSpell)
                    {
                        Spellbook spellbook = caster.DemandSpellbook(BlueprintTool.Get<BlueprintCharacterClass>(Guids.Archmage));
                        var memorizedspells = spellbook.GetAllMemorizedSpells();
                        foreach (SpellSlot ss in memorizedspells)
                        {
                            if (evt.Context.Ability.Blueprint == ss.Spell.Blueprint)
                            {
                                isMemorizedSpell = true;
                            }
                        }

                        if (!isMemorizedSpell)
                        {
                            Logger.Log("I shouldn't be here more than once...");
                            base.Owner.Resources.Spend(BlueprintTool.Get<BlueprintAbilityResource>(Guids.MediumInfluenceResource), 1);
                        }
                        Logger.Log($"{evt.Context.Ability.ConvertedFrom }");
                    }
                }
            }
            catch { }

            try
            {
                UnitEntityData caster = base.Owner;
                var influenceResource = BlueprintTool.Get<BlueprintAbilityResource>(Guids.MediumInfluenceResource);
                int resource_amount = base.Owner.Resources.GetResourceAmount(BlueprintTool.Get<BlueprintAbilityResource>(Guids.MediumInfluenceResource));
                if (evt.Reason.Ability.AbilityResourceLogic.IsSpendResource)
                {
                    Logger.Log($"{evt.Reason.Ability.AbilityResourceLogic.IsSpendResource}");
                    var requiredResource = evt.Reason.Ability.AbilityResourceLogic.RequiredResource;
                    if (requiredResource == influenceResource)
                    {
                        Logger.Log("I am required resource!");
                        if ((resource_amount - 1) <= 2)
                        {
                            Logger.Log("We'll have to add that fact of -2 initiative and some nasties.");

                            //Strength-based skill checks, Constitution checks, attack rolls, and non-spell damage rolls.
                            if (caster.Descriptor.HasFact(ArchmageSpirit)) { Logger.Log("I should be a debuffed Archmage!"); caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(Guids.MediumInfluenceArchmage), caster, new TimeSpan(24,0,0)); }
                            else if (caster.Descriptor.HasFact(ChampionSpirit)) { Logger.Log("I should be a debuffed Champion!"); caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(Guids.MediumInfluenceChampion), caster, new TimeSpan(24, 0, 0)); }
                            else if (caster.Descriptor.HasFact(GuardianSpirit)) { Logger.Log("I should be a debuffed Guardian!"); caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(Guids.MediumInfluenceGuardian), caster, new TimeSpan(24, 0, 0)); }
                            else if (caster.Descriptor.HasFact(HierophantSpirit)) { Logger.Log("I should be a debuffed Hierophant!"); caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(Guids.MediumInfluenceHierophant), caster, new TimeSpan(24, 0, 0)); }
                            else if (caster.Descriptor.HasFact(MarshalSpirit)) { Logger.Log("I should be a debuffed Marshal!"); caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(Guids.MediumInfluenceMarshal), caster, new TimeSpan(24, 0, 0)); }
                            else if (caster.Descriptor.HasFact(TricksterSpirit)) { Logger.Log("I should be a debuffed Trickster!"); caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(Guids.MediumInfluenceTrickster), caster, new TimeSpan(24, 0, 0)); }
                        }

                    }
                }
            }
            catch { }
            
        }

    }
}

