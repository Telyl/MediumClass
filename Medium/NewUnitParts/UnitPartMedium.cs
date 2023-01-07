using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using MediumClass.Utilities;
using MediumClass.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnBased.Controllers;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewUnitParts
{
    public class UnitPartMedium : UnitPart
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(UnitPartMedium));
		public void AddSpiritEntry(BlueprintCharacterClassReference spiritClass, BlueprintBuffReference spiritInfluencePenalty, BlueprintAbilityResourceReference influence, BlueprintFeatureReference feature, EntityFact source, bool concentration, StatType[] stats, StatType[] penalty_stats)
        {
			Logger.Log("Adding Spirit Entry");
			SpiritBonuses = new SpiritStatEntry()
			{
				Stats = stats,
				Concentration = concentration
			};

			SpiritPenalties = new SpiritStatEntry()
			{
				Stats = penalty_stats,
				Concentration = false
			};

			Spirit = new SpiritEntry()
			{
				SpiritClass = spiritClass,
				SpiritInfluencePenalty = spiritInfluencePenalty,
				InfluenceResource = influence,
				SpiritBonusFeature = feature,
				SpiritBonus = SpiritBonuses,
				SpiritPenalty = SpiritPenalties,
				Source = source
			};
        }

		public void RemoveSpiritEntry(EntityFact source)
		{
			Spirit = new SpiritEntry();
			SpiritBonuses = new SpiritStatEntry();
			//TryRemove();
		}

		private void TryRemove()
		{
			//this.RemoveSelf();
		}

		public bool IsInfluencePenalty()
        {
			Logger.Log($"IsInfluencePenalty");
			if (base.Owner.Descriptor.Resources.GetResourceAmount(Spirit.InfluenceResource.Get()) <= 2) { return true; }
			return false;
        }

		public void HandleInfluencePenalty()
		{
			if (IsInfluencePenalty()) { base.Owner.Buffs.AddBuff(Spirit.SpiritInfluencePenalty.Get(), base.Owner, new TimeSpan(24, 0, 0)); }
        }

		public class SpiritStatEntry
		{
			public StatType[] Stats;
			public bool Concentration = false;
		}
		public class SpiritEntry
		{
			public BlueprintCharacterClassReference SpiritClass;
			public BlueprintBuffReference SpiritInfluencePenalty;
			public BlueprintAbilityResourceReference InfluenceResource;
			public BlueprintFeatureReference SpiritBonusFeature;
			public SpiritStatEntry SpiritBonus;
			public SpiritStatEntry SpiritPenalty;
			public EntityFact Source;
		}
		public SpiritStatEntry SpiritBonuses = new SpiritStatEntry();
		public SpiritStatEntry SpiritPenalties = new SpiritStatEntry();
		public SpiritEntry Spirit = new SpiritEntry();

	}
}
