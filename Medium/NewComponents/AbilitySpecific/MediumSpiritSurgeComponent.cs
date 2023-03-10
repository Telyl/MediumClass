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
            Logger.Log("I am in OnTurnOn of SpiritSurgeComponent");
            UnitPartMedium unitPartMedium = base.Context.MaybeCaster.Get<UnitPartMedium>();
            if(unitPartMedium == null) { return; }

            Stats = unitPartMedium.Spirits[unitPartMedium.PrimarySpirit].SpiritBonus.Stats;
            Concentration = unitPartMedium.Spirits[unitPartMedium.PrimarySpirit].SpiritBonus.Concentration;
            CharacterLevel = base.Context.MaybeCaster.Progression.GetClassLevel(BlueprintTool.Get<BlueprintCharacterClass>(Guids.Medium));
            MarshalBonus = 0;

            Logger.Log("Right before If statement of SpiritSurgeComponent");
            if (unitPartMedium.PrimarySpirit.Get() == BlueprintTool.Get<BlueprintCharacterClass>(Guids.Marshal))
            {
                if (base.Context.SourceAbility != BlueprintTool.Get<BlueprintAbility>(Guids.MarshalLegendaryMarshalAbility))
                    MarshalBonus = base.Owner.Progression.Features.GetRank(unitPartMedium.Spirits[unitPartMedium.PrimarySpirit].SpiritBonus.SpiritBonusFeature.Get()) + + unitPartMedium.Spirits[unitPartMedium.PrimarySpirit].SpiritFocus;
            }
            foreach (StatType statType in Stats)
                base.Context.MainTarget.Unit.Descriptor.Stats.GetStat(statType).AddModifier((GetBonus() + MarshalBonus), base.Runtime, ModifierDescriptor.UntypedStackable);
            if(unitPartMedium.FreeSurgeAmount > 0)
            {
                var resource = base.Owner.Resources.GetResource(BlueprintTool.Get<BlueprintAbilityResource>(Guids.MediumInfluenceResource));
                resource.Amount += 1;
                unitPartMedium.FreeSurgeAmount -= 1;
            }
            
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

        public StatType[] Stats;
        public bool Concentration = false;
        public Random rnd = new Random();
        public int CharacterLevel = 0;
        public int MarshalBonus = 0;
    }
}
