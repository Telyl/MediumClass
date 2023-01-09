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
    [TypeId("ed6eeb9a-df92-40f0-b6ca-ddd8f5c28afe")]
    class MediumAstralBeaconComponent : UnitFactComponentDelegate
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(MediumAstralBeaconComponent));
        public override void OnTurnOn()
        {
            ranks = Owner.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.MediumSpiritBonus));
            for (int i = 1; i <= ranks; i++)
            {
                Owner.AddFact(BP.Get());
            }
        }

        public override void OnTurnOff()
        {
            for (int i = 1; i <= ranks; i++)
            {
                Owner.RemoveFact(BP.Get());
            }
        }

        private int ranks = 0;
        public BlueprintFeatureReference BP;
    }
}
