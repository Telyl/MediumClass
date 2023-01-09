using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
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
		public void AddSpiritEntry(BlueprintCharacterClassReference spiritClass, BlueprintBuffReference spiritInfluencePenalty, BlueprintAbilityResourceReference influence, BlueprintFeatureReference feature, BlueprintFeatureReference boon,
			EntityFact source, bool concentration, StatType[] stats, StatType[] penalty_stats, 
			BlueprintFeatureReference LesserPower, BlueprintFeatureReference IntermediatePower, BlueprintFeatureReference GreaterPower, BlueprintFeatureReference SupremePower, BlueprintFeatureReference IntermediatePowerMove, BlueprintFeatureReference IntermediatePowerSwift)
        {
			Logger.Log("Adding Spirit Entry");
			Spirits.Add(spiritClass, new SpiritEntry()
			{
				SpiritInfluencePenalty = spiritInfluencePenalty,
				InfluenceResource = influence,
				SpiritBonusFeature = feature,
				SpiritSeanceBoon = boon,
				SpiritLesserPower = LesserPower,
				SpiritIntermediatePower = IntermediatePower,
				SpiritIntermediatePowerMove = IntermediatePowerMove,
				SpiritIntermediatePowerSwift = IntermediatePowerSwift,
				SpiritGreaterPower = GreaterPower,
				SpiritSupremePower = SupremePower,
				SpiritBonus = new SpiritStatEntry()
				{
					Stats = stats,
					Concentration = concentration
				},
				SpiritPenalty = new SpiritStatEntry()
				{
					Stats = penalty_stats,
					Concentration = false
				},
				Source = source
			});
        }

		public void RemoveSpiritEntry(EntityFact source)
		{
			Spirits.Clear();
			TryRemove();
		}

		private void TryRemove()
		{
			if(!Spirits.Any()) { this.RemoveSelf(); }
		}

		public void AddWeakerSpiritChannel(BlueprintAbility SourceAbility)
        {
			if (SourceAbility == BlueprintTool.Get<BlueprintAbility>(Guids.WeakerSpiritChannelOneAbility))
			{
				ForgonePowers = 1;
				FreeSurgeAmount = 2;
			}
			else if (SourceAbility == BlueprintTool.Get<BlueprintAbility>(Guids.WeakerSpiritChannelTwoAbility))
			{
				ForgonePowers = 2;
				FreeSurgeAmount = 4;
			}
			else if (SourceAbility == BlueprintTool.Get<BlueprintAbility>(Guids.WeakerSpiritChannelThreeAbility))
			{
				ForgonePowers = 3;
				FreeSurgeAmount = 6;
			}
			else if (SourceAbility == BlueprintTool.Get<BlueprintAbility>(Guids.WeakerSpiritChannelFourAbility))
			{
				ForgonePowers = 4;
				FreeSurgeAmount = 8;
			}
			if(base.Owner.HasFact(BlueprintTool.Get<BlueprintUnitFact>(Guids.MediumSpiritMastery)))
            {
				FreeSurgeAmount *= 2;
				FreeSurgeAmount += 2;
			}
				
		}

		public void RemoveWeakerSpiritChannel()
		{
			FreeSurgeAmount = 0;
			ForgonePowers = 0;
			if (base.Owner.HasFact(BlueprintTool.Get<BlueprintUnitFact>(Guids.MediumSpiritMastery)))
				FreeSurgeAmount = 2;
		}

		public void HandleSpiritMastery()
        {
			FreeSurgeAmount = 2;
        }

		public void RemoveSpiritMastery()
        {
			FreeSurgeAmount = 0;
        }

		public bool IsInfluencePenalty(bool isSecondaryCheck = false)
        {
			if(Spirits.ContainsKey(SecondarySpirit) && isSecondaryCheck)
            {
				if (base.Owner.Descriptor.Resources.GetResourceAmount(Spirits[SecondarySpirit].InfluenceResource.Get()) <= 2) { return true; }
			}
			else if (Spirits.ContainsKey(PrimarySpirit))
			{
				if (base.Owner.Descriptor.Resources.GetResourceAmount(BlueprintTool.Get<BlueprintAbilityResource>(Guids.MediumInfluenceResource)) <= 2) { return true; }
			}
			return false;
        }

		public void HandleInfluencePenalty()
		{
			if (IsInfluencePenalty()) { base.Owner.Buffs.AddBuff(Spirits[PrimarySpirit].SpiritInfluencePenalty.Get(), base.Owner, new TimeSpan(24, 0, 0)); }
			if (IsInfluencePenalty(true)) { base.Owner.Buffs.AddBuff(Spirits[SecondarySpirit].SpiritInfluencePenalty.Get(), base.Owner, new TimeSpan(24, 0, 0)); }
		}

		public class SpiritStatEntry
		{
			public StatType[] Stats;
			public bool Concentration = false;
		}
		public class SpiritEntry
		{
			public BlueprintBuffReference SpiritInfluencePenalty;
			public BlueprintAbilityResourceReference InfluenceResource;
			public BlueprintFeatureReference SpiritBonusFeature;
			public BlueprintFeatureReference SpiritSeanceBoon;
			public BlueprintFeatureReference SpiritLesserPower;
			public BlueprintFeatureReference SpiritIntermediatePower;
			public BlueprintFeatureReference SpiritIntermediatePowerMove;
			public BlueprintFeatureReference SpiritIntermediatePowerSwift;
			public BlueprintFeatureReference SpiritGreaterPower;
			public BlueprintFeatureReference SpiritSupremePower;
			public SpiritStatEntry SpiritBonus;
			public SpiritStatEntry SpiritPenalty;
			public EntityFact Source;
		}
		public BlueprintCharacterClassReference PrimarySpirit = new BlueprintCharacterClassReference();
		public BlueprintCharacterClassReference SecondarySpirit = new BlueprintCharacterClassReference();
		public IDictionary<BlueprintCharacterClassReference, SpiritEntry> Spirits = new Dictionary<BlueprintCharacterClassReference, SpiritEntry>();
		public int ForgonePowers = 0;
		public int FreeSurgeAmount = 0;

	}
}
