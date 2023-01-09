using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
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
    [TypeId("b039219b-c848-4b6a-aa1d-71dac52c7470")]
    public class AbilityRequirementSpiritPowerRank : BlueprintComponent, IAbilityRestriction
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(AbilityRequirementSpiritPowerRank));
        public string GetAbilityRestrictionUIText()
        {
            return $"You have Marshal's Order available.";
        }

        public bool IsAbilityRestrictionPassed(AbilityData ability)
        {
            int rank = ability.Caster.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.SpiritPower));
            if(ability.Blueprint == BlueprintTool.Get<BlueprintAbility>(Guids.WeakerSpiritChannelOneAbility) && rank >= 1)
            {
                return true;
            }
            else if (ability.Blueprint == BlueprintTool.Get<BlueprintAbility>(Guids.WeakerSpiritChannelTwoAbility) && rank >= 2)
            {
                return true;
            }
            else if (ability.Blueprint == BlueprintTool.Get<BlueprintAbility>(Guids.WeakerSpiritChannelThreeAbility) && rank >= 3)
            {
                return true;
            }
            else if (ability.Blueprint == BlueprintTool.Get<BlueprintAbility>(Guids.WeakerSpiritChannelFourAbility) && rank >= 4)
            {
                return true;
            }
            return false;
        }
    }
}
