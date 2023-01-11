using System;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
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
	[TypeId("47229d52-5129-4439-a657-5e3d10acc5c4")]
	public class MediumContextSharedSeanceComponent : UnitFactComponentDelegate
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(MediumContextSharedSeanceComponent));
		public override void OnTurnOn()
		{
			UnitPartMedium medium = base.Owner.Ensure<UnitPartMedium>();
			if (!medium.Spirits.ContainsKey(medium.PrimarySpirit))
			{

				return;
			}
			base.Owner.AddFact(medium.Spirits[medium.PrimarySpirit].SpiritSeanceBoon);
			foreach (UnitEntityData unitEntityData in Game.Instance.Player.ActiveCompanions)
			{
				unitEntityData.AddFact(medium.Spirits[medium.PrimarySpirit].SpiritSeanceBoon.Get());
			}
			spirit = medium.PrimarySpirit;
		}

		// Token: 0x0600BC6A RID: 48234 RVA: 0x00313508 File Offset: 0x00311708
		public override void OnTurnOff()
		{
			UnitPartMedium medium = base.Owner.Ensure<UnitPartMedium>();
			foreach (UnitEntityData unitEntityData in Game.Instance.Player.ActiveCompanions)
			{
				foreach(var spirit in medium.Spirits.Keys)
                {
					base.Owner.RemoveFact(medium.Spirits[spirit].SpiritSeanceBoon);
					unitEntityData.RemoveFact(medium.Spirits[spirit].SpiritSeanceBoon);
				}
			}
		}
		private BlueprintCharacterClassReference spirit;
	}
}
