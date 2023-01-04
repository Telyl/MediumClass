using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
    public class ContextActionDecisiveStrikeSwift : ContextAction
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(ContextActionDecisiveStrikeSwift));

        public override string GetCaption()
        {
            return "Allows ally to attack with a melee or ranged attack.";
        }

        public override void RunAction()
        {
            TargetWrapper ally = base.Context.MainTarget;
            var threatHand = ally.Unit.GetThreatHand();
            if(threatHand == null)
            {
                threatHand = ally.Unit.GetThreatHandRanged();
            }
            if(threatHand == null) { Logger.Log("Ally unable to attack."); return; }
            foreach (UnitGroupMemory.UnitInfo unitInfo in ally.Unit.Memory.Enemies)
            {
                UnitEntityData unit = unitInfo.Unit;
                if (unit.Descriptor.State.IsConscious && ally.Unit.IsReach(unit, threatHand))
                {
                    base.Context.TriggerRule(new RuleAttackWithWeapon(ally.Unit, unit, threatHand.Weapon, 0));
                    return;
                }
            }
        }
    }
}
