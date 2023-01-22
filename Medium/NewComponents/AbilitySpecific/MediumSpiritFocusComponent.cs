using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using MediumClass.Medium.NewUnitParts;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
    [TypeId("923eef91-ff39-4733-87ba-e466f6848ee5")]
    class MediumSpiritFocusComponent : UnitFactComponentDelegate
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(MediumSpiritFocusComponent));
        public override void OnTurnOn()
        {
            Owner.Ensure<UnitPartMedium>().AddSpiritFocus(Spirit);
        }

        public override void OnTurnOff()
        {

            UnitPartMedium unitPartMedium = base.Owner.Ensure<UnitPartMedium>();
            unitPartMedium.RemoveSpiritFocus(Spirit);
        }

        public BlueprintCharacterClassReference Spirit;
    }
}
