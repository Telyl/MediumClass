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
    [TypeId("a1a054a6-1218-449c-a97a-c8b96be3eedf")]
    class MediumWeakerSpiritComponent : UnitFactComponentDelegate
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(MediumWeakerSpiritComponent));
        public override void OnTurnOn()
        {
            Logger.Log("Turning on WeakerSpiritComponent");
            Owner.Ensure<UnitPartMedium>().AddWeakerSpiritChannel(base.Context.SourceAbility);
        }

        public override void OnTurnOff()
        {
            Logger.Log("Turning off WeakerSpiritComponent");
            UnitPartMedium unitPartMedium = base.Owner.Ensure<UnitPartMedium>();
            unitPartMedium.RemoveWeakerSpiritChannel();
        }
    }
}
