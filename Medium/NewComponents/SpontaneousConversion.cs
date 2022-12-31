using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using MediumClass.Utils;
using Owlcat.QA.Validation;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents
{
	// Token: 0x02001C2B RID: 7211
	[ComponentName("Spontaneous Spell Conversion")]
	[AllowMultipleComponents]
	[AllowedOn(typeof(BlueprintUnit), false)]
	[AllowedOn(typeof(BlueprintUnitFact), false)]
	[TypeId("18df8977af254951be0e49854a471953")]
	public class SpontaneousConversion : UnitFactComponentDelegate
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(SpontaneousConversion));
		public BlueprintCharacterClass CharacterClass
		{
			get
			{
				return this.m_CharacterClass.Get();
			}
		}

		// Token: 0x0600BFFD RID: 49149 RVA: 0x0031FB39 File Offset: 0x0031DD39
		public override void OnTurnOn()
		{
			Spellbook spellbook = base.Owner.DemandSpellbook(this.CharacterClass);
			IEnumerable<AbilityData> allspells = spellbook.GetAllKnownSpells();

			List<BlueprintAbility> level1 = new List<BlueprintAbility>();
			List<BlueprintAbility> level2 = new List<BlueprintAbility>();
			List<BlueprintAbility> level3 = new List<BlueprintAbility>();
			List<BlueprintAbility> level4 = new List<BlueprintAbility>();
			List<BlueprintAbility> level5 = new List<BlueprintAbility>();
			List<BlueprintAbility> level6 = new List<BlueprintAbility>();

			foreach (AbilityData ability in allspells)
            {
				switch(ability.SpellLevel)
                {
					case 1:
						level1.Add(ability.Blueprint);
						break;
					case 2:
						level2.Add(ability.Blueprint);
						break;
					case 3:
						level3.Add(ability.Blueprint);
						break;
					case 4:
						level4.Add(ability.Blueprint);
						break;
					case 5:
						level5.Add(ability.Blueprint);
						break;
					case 6:
						level6.Add(ability.Blueprint);
						break;
				}
            }

			foreach (BlueprintAbility ability in level1)
            {
				List<BlueprintAbility> SpellsByLevel = new List<BlueprintAbility>();
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(ability);
				base.Owner.DemandSpellbook(this.CharacterClass).AddSpellConversionList(string.Format("{0}#{1}", base.Fact.Blueprint, ability.name), SpellsByLevel.ToArray<BlueprintAbility>());
			}

			foreach (BlueprintAbility ability in level2)
			{
				List<BlueprintAbility> SpellsByLevel = new List<BlueprintAbility>();
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(ability);
				base.Owner.DemandSpellbook(this.CharacterClass).AddSpellConversionList(string.Format("{0}#{1}", base.Fact.Blueprint, ability.name), SpellsByLevel.ToArray<BlueprintAbility>());
			}

			foreach (BlueprintAbility ability in level3)
			{
				List<BlueprintAbility> SpellsByLevel = new List<BlueprintAbility>();
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(ability);
				base.Owner.DemandSpellbook(this.CharacterClass).AddSpellConversionList(string.Format("{0}#{1}", base.Fact.Blueprint, ability.name), SpellsByLevel.ToArray<BlueprintAbility>());
			}

			foreach (BlueprintAbility ability in level4)
			{
				List<BlueprintAbility> SpellsByLevel = new List<BlueprintAbility>();
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(ability);
				base.Owner.DemandSpellbook(this.CharacterClass).AddSpellConversionList(string.Format("{0}#{1}", base.Fact.Blueprint, ability.name), SpellsByLevel.ToArray<BlueprintAbility>());
			}
			
			foreach (BlueprintAbility ability in level5)
			{
				List<BlueprintAbility> SpellsByLevel = new List<BlueprintAbility>();
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(ability);
				base.Owner.DemandSpellbook(this.CharacterClass).AddSpellConversionList(string.Format("{0}#{1}", base.Fact.Blueprint, ability.name), SpellsByLevel.ToArray<BlueprintAbility>());
			}

			foreach (BlueprintAbility ability in level6)
			{
				List<BlueprintAbility> SpellsByLevel = new List<BlueprintAbility>();
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(null);
				SpellsByLevel.Add(ability);
				base.Owner.DemandSpellbook(this.CharacterClass).AddSpellConversionList(string.Format("{0}#{1}", base.Fact.Blueprint, ability.name), SpellsByLevel.ToArray<BlueprintAbility>());
			}

		}

		// Token: 0x0600BFFE RID: 49150 RVA: 0x0031FB67 File Offset: 0x0031DD67
		public override void OnTurnOff()
		{
			Spellbook spellbook = base.Owner.DemandSpellbook(this.CharacterClass);
			IEnumerable<AbilityData> allspells = spellbook.GetAllKnownSpells();

			List<BlueprintAbility> level1 = new List<BlueprintAbility>();
			List<BlueprintAbility> level2 = new List<BlueprintAbility>();
			List<BlueprintAbility> level3 = new List<BlueprintAbility>();
			List<BlueprintAbility> level4 = new List<BlueprintAbility>();
			List<BlueprintAbility> level5 = new List<BlueprintAbility>();
			List<BlueprintAbility> level6 = new List<BlueprintAbility>();

			foreach (AbilityData ability in allspells)
			{
				switch (ability.SpellLevel)
				{
					case 1:
						level1.Add(ability.Blueprint);
						break;
					case 2:
						level2.Add(ability.Blueprint);
						break;
					case 3:
						level3.Add(ability.Blueprint);
						break;
					case 4:
						level4.Add(ability.Blueprint);
						break;
					case 5:
						level5.Add(ability.Blueprint);
						break;
					case 6:
						level6.Add(ability.Blueprint);
						break;
				}
			}

			foreach (BlueprintAbility ability in level1)
			{
				base.Owner.DemandSpellbook(this.CharacterClass).RemoveSpellConversionList(string.Format("{0}#{1}", base.Fact.Blueprint, ability.name));
			}
			foreach (BlueprintAbility ability in level2)
			{
				base.Owner.DemandSpellbook(this.CharacterClass).RemoveSpellConversionList(string.Format("{0}#{1}", base.Fact.Blueprint, ability.name));
			}
			foreach (BlueprintAbility ability in level3)
			{
				base.Owner.DemandSpellbook(this.CharacterClass).RemoveSpellConversionList(string.Format("{0}#{1}", base.Fact.Blueprint, ability.name));
			}
			foreach (BlueprintAbility ability in level4)
			{
				base.Owner.DemandSpellbook(this.CharacterClass).RemoveSpellConversionList(string.Format("{0}#{1}", base.Fact.Blueprint, ability.name));
			}
			foreach (BlueprintAbility ability in level5)
			{
				base.Owner.DemandSpellbook(this.CharacterClass).RemoveSpellConversionList(string.Format("{0}#{1}", base.Fact.Blueprint, ability.name));
			}
			foreach (BlueprintAbility ability in level6)
			{
				base.Owner.DemandSpellbook(this.CharacterClass).RemoveSpellConversionList(string.Format("{0}#{1}", base.Fact.Blueprint, ability.name));
			}
		}

        // Token: 0x04008105 RID: 33029
        [NotNull]
		[ValidateNotNull]
		[SerializeField]
		[FormerlySerializedAs("CharacterClass")]
		public BlueprintCharacterClassReference m_CharacterClass;

	}
}
