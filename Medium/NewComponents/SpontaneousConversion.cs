using System;
using System.Collections.Generic;
using System.Linq;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using JetBrains.Annotations;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Utility;
using MediumClass.Medium.Spirits;
using MediumClass.Utilities;
using MediumClass.Utils;
using Newtonsoft.Json;
using Owlcat.QA.Validation;
using TabletopTweaks.Core.NewEvents;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents
{
	//I pulled this directly from SpellKenning implemented by Vek17 and switched it up. All thanks go to him and TabletopTweaks-Core.
	[ComponentName("Spontaneous Spell Conversion")]
	[AllowMultipleComponents]
	[AllowedOn(typeof(BlueprintUnit), false)]
	[AllowedOn(typeof(BlueprintUnitFact), false)]
	[TypeId("18df8977af254951be0e49854a471953")]
	public class SpontaneousConversion : UnitFactComponentDelegate, ISpontaneousConversionHandler
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(SpontaneousConversion));
		public BlueprintCharacterClass CharacterClass
		{
			get
			{
				return this.m_CharacterClass.Get();
			}
		}
		public BlueprintSpellList SpellList
		{
			get
			{
				return this.m_SpellList.Get();
			}
		}
		public BlueprintSpellbook Spellbook
		{
			get
			{
				return this.m_Spellbook.Get();
			}
		}
		public BlueprintAbilityResource Resource
		{
			get
			{
				return this.m_Resource.Get();
			}
		}

		public override void OnPostLoad()
		{
			base.OnPostLoad();
			UpdateConversions();
		}

		public override void OnTurnOn()
		{
			SpellLists.Add(new InfluenceSpellLists(SpellList.ToReference<BlueprintSpellListReference>(), this.Fact));
			UpdateConversions();
		}
		public override void OnTurnOff() { }

		public void HandleGetConversions(AbilityData ability, ref IEnumerable<AbilityData> conversions)
		{
			var conversionList = conversions.ToList();
			BlueprintSpellbookReference SpellBook = this.Spellbook.ToReference<BlueprintSpellbookReference>();
			if (!(SpellBook.deserializedGuid == ability.SpellbookBlueprint?.AssetGuid)) { return; }
			if(GetConversionSpells(ability.SpellLevel).Count() == 0) {
				UpdateConversions();
            }
			if(this.CharacterClass == BlueprintTool.Get<BlueprintCharacterClass>(Guids.Archmage))
            {
				foreach (var abilityData in GetConversionSpells(ability.SpellLevel))
				{
					AbilityData.AddAbilityUnique(ref conversionList, new SpiritAbilityData(ability, abilityData)
					{
						OverridenResourceLogic = new InfluenceResourceOverride()
						{
							m_RequiredResource = this.Resource.ToReference<BlueprintAbilityResourceReference>(),
							cost = 1
						}
					});
				}
			}
            else
            {
				foreach (var abilityData in GetConversionSpells(ability.SpellLevel))
				{
					AbilityData.AddAbilityUnique(ref conversionList, new SpiritAbilityData(ability, abilityData)
					{
						ExtraSpellSlotCost = -1,
						OverridenResourceLogic = new InfluenceResourceOverride()
						{
							m_RequiredResource = this.Resource.ToReference<BlueprintAbilityResourceReference>(),
							m_Spirit = this.CharacterClass.ToReference<BlueprintCharacterClassReference>(),
							cost = 1
						}
					});
				}
			}
			
			conversions = conversionList;
		}

		public void UpdateConversions()
		{
			for (int level = 0; level < cachedConversions.Length; level++)
			{
				cachedConversions[level] = SpellLists
					.Select(list => list.SpellList.Get())
					.SelectMany(list => list.SpellsByLevel)
					.Where(spellList => spellList.SpellLevel != 0)
					.Where(spellList => spellList.SpellLevel == level)
					.SelectMany(level => level.Spells)
					.Where(spell => this.Owner.DemandSpellbook(this.CharacterClass).IsKnownOnLevel(spell, level))
					.SelectMany(spell => {
						AbilityVariants variantComponent = spell.GetComponent<AbilityVariants>();
						if (variantComponent != null)
						{
							return variantComponent.Variants.AsEnumerable();
						}
						return new BlueprintAbility[] { spell };
					})
					.Distinct()
					.Where(spell => !spell.GetComponent<AbilityShadowSpell>())
					.Select(spell => spell.ToReference<BlueprintAbilityReference>())
					.ToList();
			}
		}

		public IEnumerable<BlueprintAbility> GetConversionSpells(int level)
		{
			return cachedConversions[Math.Max(0, Math.Min(cachedConversions.Length - 1, level))].Select(spell => spell.Get());
		}

        private readonly List<BlueprintAbilityReference>[] cachedConversions = new List<BlueprintAbilityReference>[10];
		private readonly List<InfluenceSpellLists> SpellLists = new();
		

		internal class InfluenceSpellLists
		{
			[JsonProperty]
			public BlueprintSpellListReference SpellList;
			[JsonProperty]
			public EntityFactRef Source;

			public InfluenceSpellLists() { }
			public InfluenceSpellLists(BlueprintSpellListReference spellList, EntityFact source)
			{
				SpellList = spellList;
				Source = source;
			}
		}

		internal class SpiritAbilityData : AbilityData
		{
			public SpiritAbilityData() : base() { }
			public SpiritAbilityData(
				BlueprintAbility blueprint,
				UnitDescriptor caster,
				[CanBeNull] Ability fact,
				[CanBeNull] BlueprintSpellbook spellbookBlueprint) : base(blueprint, caster, fact, spellbookBlueprint)
			{
			}

			public SpiritAbilityData(AbilityData other, BlueprintAbility replaceBlueprint) : this(replaceBlueprint ?? other.Blueprint, other.Caster, other.Fact, other.SpellbookBlueprint)
			{
				this.MetamagicData = null;
				this.m_ConvertedFrom = other;
			}
		}
		internal class InfluenceResourceOverride : IAbilityResourceLogic
		{
			public InfluenceResourceOverride() : base() { }
			public BlueprintAbilityResource RequiredResource => m_RequiredResource;
			public bool IsSpendResource => true;

            public BlueprintCharacterClass Spirit => m_Spirit;

            public int CalculateCost(AbilityData ability)
			{
				return cost;
			}
			public void Spend(AbilityData ability)
			{
				UnitEntityData unit = ability.Caster.Unit;
				if (unit == null)
				{
					PFLog.Default.Error("Caster is missing", Array.Empty<object>());
					return;
				}
				if (unit.Blueprint.IsCheater)
				{
					return;
				}
				unit.Descriptor.Resources.Spend(RequiredResource, cost);
				HandleSpiritInfluence(ability);
			}

            public void HandleSpiritInfluence(AbilityData ability)
            {
				UnitEntityData unit = ability.Caster.Unit;
				if (unit.Descriptor.Resources.GetResourceAmount(RequiredResource) > 2) { return; }	
				if (this.Spirit == BlueprintTool.Get<BlueprintCharacterClass>(Guids.Archmage)) { 
					unit.Descriptor.Buffs.AddBuff(BlueprintTool.Get<BlueprintBuff>(Guids.MediumInfluenceDebuff), unit, new TimeSpan(24, 0, 0)); 
				}		
			}

            [JsonProperty]
			public BlueprintAbilityResourceReference m_RequiredResource;
			[JsonProperty]
			public BlueprintCharacterClassReference m_Spirit;
			[JsonProperty]
			public int cost = 1;
		}
		[NotNull]
		[ValidateNotNull]
		[SerializeField]
		[FormerlySerializedAs("CharacterClass")]
		public BlueprintCharacterClassReference m_CharacterClass;
		
		[NotNull]
		[ValidateNotNull]
		[SerializeField]
		public BlueprintSpellListReference m_SpellList;

		[NotNull]
		[ValidateNotNull]
		[SerializeField]
		public BlueprintSpellbookReference m_Spellbook;

		[NotNull]
		[ValidateNotNull]
		[SerializeField]
		public BlueprintAbilityResourceReference m_Resource;
	}
}

