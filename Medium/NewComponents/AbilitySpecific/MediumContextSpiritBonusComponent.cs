using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.QA;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using MediumClass.Medium.NewUnitParts;
using MediumClass.Utils;
using Newtonsoft.Json;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
	// Token: 0x02001B4E RID: 6990
	[ComponentName("Add stat bonus")]
	[AllowedOn(typeof(BlueprintFeature), false)]
	[AllowedOn(typeof(BlueprintBuff), false)]
	[AllowMultipleComponents]
	[TypeId("995fb9e0-f2f5-4dc2-a281-b7959ea95cda")]
	public class MediumContextSpiritBonusComponent : UnitFactComponentDelegate<AddContextStatBonus.ComponentData>
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(MediumContextSpiritBonusComponent));
		public override void OnTurnOn()
		{
			UnitPartMedium medium = base.Owner.Ensure<UnitPartMedium>();
			if (!medium.Spirits.ContainsKey(medium.PrimarySpirit))
			{
				return;
			}
			int ranks = base.Owner.Progression.Features.GetRank(medium.Spirits[medium.PrimarySpirit].SpiritBonus.SpiritBonusFeature) + medium.Spirits[medium.PrimarySpirit].SpiritFocus;
			foreach (StatType stat in medium.Spirits[medium.PrimarySpirit].SpiritBonus.Stats)
			{
				base.Owner.Stats.GetStat(stat).AddModifier(ranks, base.Runtime, ModifierDescriptor.UntypedStackable);
			}
			spirit = medium.PrimarySpirit;
		}

		// Token: 0x0600BC6A RID: 48234 RVA: 0x00313508 File Offset: 0x00311708
		public override void OnTurnOff()
		{
			UnitPartMedium medium = base.Owner.Ensure<UnitPartMedium>();
			foreach(StatType stat in medium.Spirits[spirit].SpiritBonus.Stats)
            {
				base.Owner.Stats.GetStat(stat).RemoveModifiersFrom(base.Runtime);
			}
		}
		private BlueprintCharacterClassReference spirit;
	}
}
