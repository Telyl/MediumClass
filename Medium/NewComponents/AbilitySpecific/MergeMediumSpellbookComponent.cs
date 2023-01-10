using System;
using System.Collections.Generic;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.QA;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using MediumClass.Medium.NewUnitParts;
using MediumClass.Utilities;
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
	public class MergeMediumSpellbookComponent : UnitFactComponentDelegate
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(MergeMediumSpellbookComponent));
		public override void OnTurnOn()
		{
			UnitPartMedium medium = base.Owner.Ensure<UnitPartMedium>();
			if (!medium.Spirits.ContainsKey(medium.PrimarySpirit))
			{
				return;
			}
			Logger.Log("Spellbooks!");
			Spellbook spiritSpellbook = base.Owner.DemandSpellbook(medium.PrimarySpirit.Get());
			Spellbook mediumSpellbook = base.Owner.DemandSpellbook(BlueprintTool.Get<BlueprintCharacterClass>(Guids.Medium));
			Logger.Log($"spirit: {spiritSpellbook.Blueprint} medium: {mediumSpellbook.Blueprint}");

			foreach(AbilityData ability in spiritSpellbook.GetAllKnownSpells())
            {
				if(ability.SpellLevel == 0) { continue; }
				mediumSpellbook.AddKnownTemporary(ability.SpellLevel, ability.Blueprint);
				abilities.Add(ability);
			}
			spirit = medium.PrimarySpirit;
		}

		// Token: 0x0600BC6A RID: 48234 RVA: 0x00313508 File Offset: 0x00311708
		public override void OnTurnOff()
		{
			Spellbook mediumSpellbook = base.Owner.DemandSpellbook(BlueprintTool.Get<BlueprintCharacterClass>(Guids.Medium));
			foreach(AbilityData ability in abilities)
            {
				mediumSpellbook.RemoveTemporarySpell(ability);
			}
		}
		private BlueprintCharacterClassReference spirit;
		private List<AbilityData> abilities = new List<AbilityData>();
	}
}
