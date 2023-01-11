using System;
using System.Collections.Generic;
using System.Linq;
using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using MediumClass.Medium.NewUnitParts;
using MediumClass.Utilities;
using MediumClass.Utils;
using Owlcat.QA.Validation;
using Owlcat.Runtime.Core.Utils;
using TabletopTweaks.Core.NewUnitParts;
using UnityEngine;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.NewComponents
{
	// Token: 0x02001BB6 RID: 7094
	[TypeId("72326d37-dfb6-42a3-bd9c-24ee2201539b")]
	public class ApplySpirits : UnitFactComponentDelegate
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(ApplySpirits));
		private UnitPartMedium medium;

		public override void OnActivate()
		{
			medium = base.Owner.Ensure<UnitPartMedium>();
			this.TryApplySpirit();
		}

		public override void OnDeactivate()
		{
			foreach (var spirit in medium.Spirits.Keys)
			{
				if (spirit.Get() != medium.PrimarySpirit.Get()) {
					RemoveSecondarySpirits(spirit); }
			}
			this.Revert();
			
		}

		private void ApplySpiritSpellbook()
        {
			int SpiritPowerRank = base.Owner.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.SpiritPower)) - medium.ForgonePowers;
			if ((medium.PrimarySpirit.Get() == BlueprintTool.Get<BlueprintCharacterClass>(Guids.Hierophant) || medium.PrimarySpirit.Get() == BlueprintTool.Get<BlueprintCharacterClass>(Guids.Archmage)) && SpiritPowerRank > 0)
			{
				base.Owner.Progression.Features.RemoveFact(medium.Spirits[medium.PrimarySpirit].SpiritLesserPower.Get());
			}
		}
		private void CheckWeakerSpiritAndApply()
        {			 
			int SpiritPowerRank = base.Owner.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.SpiritPower)) - medium.ForgonePowers;
			if ((SpiritPowerRank >= 1))
				base.Owner.AddFact(medium.Spirits[medium.PrimarySpirit].SpiritLesserPower.Get());
			if ((SpiritPowerRank >= 2))
				base.Owner.AddFact(medium.Spirits[medium.PrimarySpirit].SpiritIntermediatePower.Get());
			if ((SpiritPowerRank >= 2))
				base.Owner.AddFact(medium.Spirits[medium.PrimarySpirit].SpiritIntermediatePowerMove.Get());
			if ((SpiritPowerRank >= 2))
				base.Owner.AddFact(medium.Spirits[medium.PrimarySpirit].SpiritIntermediatePowerSwift.Get());
			if ((SpiritPowerRank >= 3))
				base.Owner.AddFact(medium.Spirits[medium.PrimarySpirit].SpiritGreaterPower.Get());
			if ((SpiritPowerRank >= 4))
				base.Owner.AddFact(medium.Spirits[medium.PrimarySpirit].SpiritSupremePower.Get());
		}

		private void TryApplySpirit()
		{
			CheckWeakerSpiritAndApply();
			ApplySpiritSpellbook();


			if (base.Owner.Progression.Features.HasFact(BlueprintTool.Get<BlueprintFeature>(Guids.AstralBeacon)))
            {
				foreach (var spirit in medium.Spirits.Keys)
				{
					if (spirit.Get() != medium.PrimarySpirit.Get())
						ApplySecondarySpirits(spirit);
				}
			}
			
		}

		private void ApplySecondarySpirits(BlueprintCharacterClassReference spirit)
		{
			base.Owner.AddFact(medium.Spirits[spirit].SpiritIntermediatePower);
			base.Owner.AddFact(medium.Spirits[spirit].SpiritIntermediatePowerMove);
			base.Owner.AddFact(medium.Spirits[spirit].SpiritIntermediatePowerSwift);
			base.Owner.AddFact(medium.Spirits[spirit].OverwriteIntermediatePower);
			base.Owner.AddFact(medium.Spirits[spirit].SpiritGreaterPower);
			base.Owner.AddFact(medium.Spirits[spirit].OverwriteGreaterPower);
			base.Owner.AddFact(medium.Spirits[spirit].SpiritSupremePower);
		}

		private void RemoveSecondarySpirits(BlueprintCharacterClassReference spirit)
		{
			base.Owner.RemoveFact(medium.Spirits[spirit].SpiritIntermediatePower);
			base.Owner.RemoveFact(medium.Spirits[spirit].SpiritIntermediatePowerMove);
			base.Owner.RemoveFact(medium.Spirits[spirit].SpiritIntermediatePowerSwift);
			base.Owner.RemoveFact(medium.Spirits[spirit].OverwriteIntermediatePower);
			base.Owner.RemoveFact(medium.Spirits[spirit].SpiritGreaterPower);
			base.Owner.RemoveFact(medium.Spirits[spirit].OverwriteGreaterPower);
			base.Owner.RemoveFact(medium.Spirits[spirit].SpiritSupremePower);
		}

		// Token: 0x0600BDCF RID: 48591 RVA: 0x00318090 File Offset: 0x00316290
		private void Revert()
		{
			List<Buff> list = base.Owner.Buffs.Enumerable.ToTempList<Buff>();
			foreach (Buff buff in list)
			{
				if (buff.Blueprint.Name.Contains("Trickster's Edge")){
					base.Owner.Buffs.RemoveFact(buff);
				}
				if (buff.Blueprint.Name.Contains("Seance Boon"))
				{
					base.Owner.Buffs.RemoveFact(buff);
				}
			}

			base.Owner.RemoveFact(medium.Spirits[medium.PrimarySpirit].SpiritLesserPower);
			base.Owner.RemoveFact(medium.Spirits[medium.PrimarySpirit].SpiritIntermediatePower);
			base.Owner.RemoveFact(medium.Spirits[medium.PrimarySpirit].SpiritIntermediatePowerMove);
			base.Owner.RemoveFact(medium.Spirits[medium.PrimarySpirit].SpiritIntermediatePowerSwift);
			base.Owner.RemoveFact(medium.Spirits[medium.PrimarySpirit].SpiritGreaterPower);
			base.Owner.RemoveFact(medium.Spirits[medium.PrimarySpirit].SpiritSupremePower);

			base.Owner.Progression.Features.AddFact(BlueprintTool.Get<BlueprintFeature>(Guids.MediumSpellcasterFeatProhibitArchmage), Context);
			base.Owner.Progression.Features.AddFact(BlueprintTool.Get<BlueprintFeature>(Guids.MediumSpellcasterFeatProhibitHierophant), Context);

			medium.PrimarySpirit = new BlueprintCharacterClassReference();
			base.ClearData();
		}
	}
}
