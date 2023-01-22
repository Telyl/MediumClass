using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using MediumClass.Utils;
using Owlcat.Runtime.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Feint
{
    class ContextActionFeintCheck : ContextAction
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Feint));
        public ActionList Success;
        public ActionList Failure;
        static BlueprintFeature[] single_penalty_facts = new BlueprintFeature[] {FeatureRefs.DragonType.Reference.Get(), //dragons
                                                                                 FeatureRefs.MagicalBeastType.Reference.Get(), //magical beasts
                                                                                };
        static BlueprintFeature[] double_penalty_facts = new BlueprintFeature[] {FeatureRefs.AnimalType.Reference.Get(), //animals
                                                                                };
        public override string GetCaption()
        {
            return "Feint check";
        }

        public override void RunAction()
        {
            if (this.Target.Unit == null)
            {
                LogChannel.Default.Error(this, "ContextActionFeintCheck: target is null", Array.Empty<object>());
                return;
            }
            else if (this.Context.MaybeCaster == null)
            {
                LogChannel.Default.Error(this, "ContextActionFeintCheck: caster is null", Array.Empty<object>());
                return;
            }
            int dc_bab = this.Target.Unit.Descriptor.Stats.BaseAttackBonus.ModifiedValue + this.Target.Unit.Descriptor.Stats.Wisdom.Bonus;
            int dc_sense_motive = (this.Target.Unit.Descriptor.Stats.SkillPerception.BaseValue > 0) ? this.Target.Unit.Descriptor.Stats.SkillPerception.ModifiedValue : 0;
            int dc = 10;
            if (dc_bab >= dc_sense_motive)
                dc += dc_bab;
            else
                dc += dc_sense_motive;
            
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
            }).Success)
            {
                Logger.Log("Success!");
                //this.Success.Run();
            }
            else
            {
                Logger.Log("Failure!");
                //this.Failure.Run();
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
