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
    [TypeId("8deea97d-ea0c-441f-904d-30b3f1f078b2")]
    public class AbilityRequirementHasNoSpirit : BlueprintComponent, IAbilityRestriction
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(AbilityRequirementHasNoSpirit));
        public string GetAbilityRestrictionUIText()
        {
            return $"You cannot have a spirit channeled";
        }

        public bool IsAbilityRestrictionPassed(AbilityData ability)
        {
            foreach (BlueprintBuff clazzbuff in SpiritHelper.SpiritClasses.AllSpirits){
                try
                {
                    if(ability.Caster.Buffs.GetBuff(clazzbuff) != null)
                    {
                        return false;
                    }
                }
                catch(Exception e)
                {
                    Logger.LogException(e);
                }
            }
            return true;
        }
    }
}
