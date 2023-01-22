using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediumClass.Feint
{
    public class ContextActionSkillCheckWithFailures : ContextAction
    {
        public StatType Stat;
        public bool use_custom_dc;
        public ContextValue custom_dc;
        public ActionList Success = Helpers.CreateActionList();
        public ActionList Failure5 = Helpers.CreateActionList();
        public ActionList Failure10 = Helpers.CreateActionList();
        public ActionList Failure = Helpers.CreateActionList();
        public ActionList Bypass5 = Helpers.CreateActionList();
        public ActionList Bypass10 = Helpers.CreateActionList();
        public bool on_caster = false;

        public override void RunAction()
        {
            if (this.Target.Unit == null || this.Context.MaybeCaster == null)
            {
                UberDebug.LogError((object)"Target unit is missing", (object[])Array.Empty<object>());
            }
            else
            {
                var dc = !this.use_custom_dc ? this.Context.Params.DC : custom_dc.Calculate(this.Context);
                var skill_check = this.Context.TriggerRule<RuleSkillCheck>(new RuleSkillCheck(on_caster ? this.Context.MaybeCaster : this.Target.Unit, this.Stat, dc) { ShowAnyway = true });

                if (skill_check.IsPassed)
                {
                    this.Success.Run();
                    if (skill_check.IsSuccessRoll(skill_check.D20, -10))
                    {
                        this.Bypass10.Run();
                    }
                    else if (skill_check.IsSuccessRoll(skill_check.D20, -5))
                    {
                        this.Bypass5.Run();
                    }
                }
                else if (!skill_check.IsSuccessRoll(skill_check.D20, 9))
                {
                    this.Failure10.Run();
                }
                else if (!skill_check.IsSuccessRoll(skill_check.D20, 4))
                {
                    this.Failure5.Run();
                }
                else
                {
                    this.Failure.Run();
                }
            }
        }

        public override string GetCaption()
        {
            return string.Format("Skill check {0} {1}", (object)this.Stat, !this.use_custom_dc ? (object)string.Empty : (object)string.Format("(DC: {0})", (object)this.custom_dc));
        }

        [Serializable]
        private struct ConditionalDCIncrease
        {
            public ConditionsChecker Condition;
            public ContextValue Value;
        }
    }


    public class ContextFeintSkillCheck : ContextAction
    {
        public ActionList Success;
        public ActionList Failure;
        static BlueprintFeature[] single_penalty_facts = new BlueprintFeature[] {Main.library.Get<BlueprintFeature>("455ac88e22f55804ab87c2467deff1d6"), //dragons
                                                                                 Main.library.Get<BlueprintFeature>("625827490ea69d84d8e599a33929fdc6"), //magical beasts
                                                                                };

        static BlueprintFeature[] double_penalty_facts = new BlueprintFeature[] {Main.library.Get<BlueprintFeature>("a95311b3dc996964cbaa30ff9965aaf6"), //animals
                                                                                };


        public override string GetCaption()
        {
            return "Feint check";
        }

        public override void RunAction()
        {
            if (this.Target.Unit == null)
            {
                UberDebug.LogError((object)"Target unit is missing", (object[])Array.Empty<object>());
            }
            else if (this.Context.MaybeCaster == null)
            {
                UberDebug.LogError((object)"Caster is missing", (object[])Array.Empty<object>());
            }
            else
            {
                int dc_bab = this.Target.Unit.Descriptor.Stats.BaseAttackBonus.ModifiedValue + this.Target.Unit.Descriptor.Stats.Wisdom.Bonus;
                int dc_sense_motive = (this.Target.Unit.Descriptor.Stats.SkillPerception.BaseValue > 0) ? this.Target.Unit.Descriptor.Stats.SkillPerception.ModifiedValue : 0;

                //int dc = 10 + Math.Max(dc_bab, dc_sense_motive);
                int dc = 10 + dc_bab;

                if (targetHasFactFromList(double_penalty_facts))
                {
                    dc += 8;
                }
                else if (targetHasFactFromList(single_penalty_facts))
                {
                    dc += 4;
                }

                if (this.Context.TriggerRule<RuleSkillCheck>(new RuleSkillCheck(this.Context.MaybeCaster, StatType.CheckBluff, dc)
                {
                    ShowAnyway = true
                }).IsPassed)
                    this.Success.Run();
                else
                    this.Failure.Run();
            }
        }

        private bool targetHasFactFromList(params BlueprintFeature[] facts)
        {
            foreach (var f in facts)
            {
                if (this.Target.Unit.Descriptor.HasFact(f))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
