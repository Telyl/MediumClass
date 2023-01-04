using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
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
    [TypeId("4c5cd1ca-7eed-4a21-aa7d-2e79d5953009")]
    class MediumInfluencePenaltyComponent : UnitFactComponentDelegate
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(MediumInfluencePenaltyComponent));
        public override void OnTurnOn()
        {
            UnitPartMedium unitPartMedium = base.Context.MaybeCaster.Get<UnitPartMedium>();
            if (unitPartMedium == null) { return; }

            Penalties = unitPartMedium.Spirit.SpiritPenalty.Stats;
            SpiritBonus = base.Context.MaybeCaster.Progression.Features.GetRank(unitPartMedium.Spirit.SpiritBonusFeature);


            foreach (StatType statType in Penalties)
            {
                base.Context.MainTarget.Unit.Descriptor.Stats.GetStat(statType).AddModifier((SpiritBonus * -1), base.Runtime, ModifierDescriptor.Penalty);
            }
            if(unitPartMedium.Spirit.SpiritClass.Get() == BlueprintTool.Get<BlueprintCharacterClass>(Guids.Marshal))
            {
                fightDefensively = true;
                base.Context.MainTarget.Unit.Buffs.AddBuff(BuffRefs.FightDefensivelyBuff.Reference.Get(), base.Context, new TimeSpan(24, 0, 0));
            }
        }

        public override void OnTurnOff()
        {
            foreach (StatType statType in Penalties)
            {
                base.Context.MainTarget.Unit.Descriptor.Stats.GetStat(statType).RemoveModifiersFrom(base.Runtime);
            }
            if(fightDefensively)
            {
                base.Owner.Buffs.RemoveFact(BuffRefs.FightDefensivelyBuff.Reference.Get());
            }
            
        }

        private StatType[] Penalties;
        private int SpiritBonus = 0;
        private bool fightDefensively = false;
    }
}
