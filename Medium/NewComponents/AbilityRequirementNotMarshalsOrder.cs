using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using MediumClass.Medium.Spirits;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents
{
    [TypeId("52167181465d42ed9478b5b9612a8f9c")]
    public class AbilityRequirementNotMarshalsOrder : BlueprintComponent, IAbilityRestriction
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(AbilityRequirementNotMarshalsOrder));
        public string GetAbilityRestrictionUIText()
        {
            return $"You have Marshal's Order available.";
        }

        public bool IsAbilityRestrictionPassed(AbilityData ability)
        {
            if(!ability.Caster.HasFact(BlueprintTool.Get<BlueprintUnitFact>(Guids.MarshalMarshalsOrders)))
            {
                return true;
            }
            return false;
        }
    }
}
