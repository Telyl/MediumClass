using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Parts;
using MediumClass.Medium.NewUnitParts;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
    [TypeId("f2933242-a725-4fe5-a584-5d5a8de84b9d")]
    class MediumSpiritMasteryComponent : UnitFactComponentDelegate
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(MediumSpiritMasteryComponent));
        public override void OnTurnOn()
        {
            Owner.Ensure<UnitPartMedium>().HandleSpiritMastery();
        }

        public override void OnTurnOff()
        {
        
            UnitPartMedium unitPartMedium = base.Owner.Ensure<UnitPartMedium>();
            unitPartMedium.RemoveSpiritMastery();
        }
    }
}
