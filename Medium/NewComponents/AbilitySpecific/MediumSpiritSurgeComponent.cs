using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
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
    [TypeId("96306dd2-f947-44d4-a6d8-1eafe7c937dc")]
    class MediumSpiritSurgeComponent : UnitFactComponentDelegate, IConcentrationBonusProvider
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(MediumSpiritSurgeComponent));
        public override void OnTurnOn()
        {
            UnitPartMedium unitPartMedium = base.Context.MaybeCaster.Get<UnitPartMedium>();
            if(unitPartMedium == null) { return; }

            Stats = unitPartMedium.Spirit.SpiritBonus.Stats;
            Concentration = unitPartMedium.Spirit.SpiritBonus.Concentration;
            CharacterLevel = base.Context.MaybeCaster.Progression.GetClassLevel(BlueprintTool.Get<BlueprintCharacterClass>(Guids.Medium));
            MarshalBonus = 0;

            if(unitPartMedium.Spirit.SpiritClass.Get() == BlueprintTool.Get<BlueprintCharacterClass>(Guids.Marshal))
            {
                if (base.Context.SourceAbility != BlueprintTool.Get<BlueprintAbility>(Guids.MarshalLegendaryMarshalAbility))
                    MarshalBonus = base.Owner.Progression.Features.GetRank(unitPartMedium.Spirit.SpiritBonusFeature.Get());
            }
            foreach (StatType statType in Stats)
                base.Context.MainTarget.Unit.Descriptor.Stats.GetStat(statType).AddModifier((GetBonus() + MarshalBonus), base.Runtime, ModifierDescriptor.UntypedStackable);
        }

        public override void OnTurnOff()
        {
            foreach (StatType statType in Stats)
                base.Context.MainTarget.Unit.Descriptor.Stats.GetStat(statType).RemoveModifiersFrom(base.Runtime);
        }

        public int GetStaticConcentrationBonus(EntityFactComponent runtime)
        {
            if (!Concentration) 
                return 0;
            using (runtime.RequestEventContext())
                return GetBonus() + MarshalBonus;
        }

        public int GetBonus()
        {
            if(base.Context.SourceAbility == BlueprintTool.Get<BlueprintAbility>(Guids.MarshalLegendaryMarshalAbility)) { return rnd.Next(1, 6); }
            switch (CharacterLevel)
            {
                case < 10:
                    return rnd.Next(1, 6);
                case < 20:
                    return rnd.Next(1, 8);
                case 20:
                    return rnd.Next(1, 10);
            }
            return 0;
        }

        // Our spirit could change while we have spirit surge on which causes some issues; keep track of the status modified.
        public StatType[] Stats;
        public bool Concentration = false;
        public Random rnd = new Random();
        public int CharacterLevel = 0;
        public int MarshalBonus = 0;
    }
}
