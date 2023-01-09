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
    [TypeId("46bdb129-b66b-4fd5-9b38-6d56ad0ab9aa")]
    class MediumSpiritComponent : UnitFactComponentDelegate
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(MediumSpiritComponent));
        public override void OnTurnOn()
        {
            Owner.Ensure<UnitPartMedium>().AddSpiritEntry(SpiritClass, SpiritInfluencePenalty, MediumInfluence, SpiritBonusFeature, base.Fact, Concentration, Stats, Penalties, 
                SpiritLesserPower, SpiritIntermediatePower, SpiritGreaterPower, SpiritSupremePower, SpiritIntermediatePowerMove, SpiritIntermediatePowerSwift);
        }

        public override void OnTurnOff()
        {
            UnitPartMedium unitPartMedium = base.Owner.Ensure<UnitPartMedium>();
            unitPartMedium.RemoveSpiritEntry(base.Fact);
        }

        public BlueprintCharacterClassReference SpiritClass;
        public BlueprintBuffReference SpiritInfluencePenalty;
        public BlueprintAbilityResourceReference MediumInfluence;
        public BlueprintFeatureReference SpiritBonusFeature;
        public BlueprintFeatureReference SpiritLesserPower;
        public BlueprintFeatureReference SpiritIntermediatePower;
        public BlueprintFeatureReference SpiritIntermediatePowerMove = new BlueprintFeatureReference();
        public BlueprintFeatureReference SpiritIntermediatePowerSwift = new BlueprintFeatureReference();
        public BlueprintFeatureReference SpiritGreaterPower;
        public BlueprintFeatureReference SpiritSupremePower;
        public StatType[] Stats;
        public StatType[] Penalties;
        public bool Concentration = false;
    }
}
