using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using MediumClass.Medium.Spirits;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents
{
    [TypeId("60be564b13e54e3cb0ca08e50bbcf2e3")]
    public class AbilityRequirementHasSpirit : BlueprintComponent, IAbilityRestriction
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(AbilityRequirementHasSpirit));
        public string GetAbilityRestrictionUIText()
        {
            return $"You don't have a spirit channeled";
        }

        public bool IsAbilityRestrictionPassed(AbilityData ability)
        {
            foreach (BlueprintBuff clazzbuff in SpiritHelper.SpiritClasses.AllSpirits){
                try
                {
                    if(ability.Caster.Buffs.GetBuff(clazzbuff) != null)
                    {
                        return true;
                    }
                }
                catch(Exception e)
                {
                    Logger.LogException(e);
                }
            }
            return false;
        }
    }
}
