using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
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
    [TypeId("c94f2d6c4f2e42b79d068633a89be55f")]
    class MediumSpiritComponent : UnitFactComponentDelegate
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(MediumSpiritComponent));
        public override void OnTurnOn()
        {
            Owner.Ensure<UnitPartMedium>().AddSpiritEntry(SpiritClass, SpiritInfluencePenalty, MediumInfluence, SpiritBonusFeature, base.Fact, Concentration, Stats, Penalties);
            Owner.Get<UnitPartMedium>().HandleInfluencePenalty();
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
        public StatType[] Stats;
        public StatType[] Penalties;
        public bool Concentration = false;
    }
}
