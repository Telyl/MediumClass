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
using TabletopTweaks.Core.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewUnitParts
{
	//I pulled this directly from SpellKenning implemented by Vek17 and switched it up. All thanks go to him and TabletopTweaks-Core.
	[ComponentName("Spontaneous Spell Conversion")]
	[AllowMultipleComponents]
	[AllowedOn(typeof(BlueprintUnit), false)]
	[AllowedOn(typeof(BlueprintUnitFact), false)]
	[TypeId("18df8977af254951be0e49854a471953")]
	public class UnitPartArchmage : UnitPart, ISpontaneousConversionHandler
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(UnitPartArchmage));
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
		}

		public void AddSpellList(EntityFact fact, BlueprintSpellListReference spellList, BlueprintCharacterClassReference characterClass, BlueprintSpellbookReference spellbook, BlueprintAbilityResourceReference resource)
		{
			m_CharacterClass = characterClass;
			m_Spellbook = spellbook;
			m_Resource = resource;
			SpellLists.Add(new InfluenceSpellLists(spellList, fact));
		}

		public void RemoveEntry(EntityFact source)
		{
			SpellLists.RemoveAll((list) => list.Source == source);
			m_Spellbook = new BlueprintSpellbookReference();
			m_Resource = new BlueprintAbilityResourceReference();
			m_CharacterClass = new BlueprintCharacterClassReference();
			this.RemoveSelf();
		}

		public void HandleGetConversions(AbilityData ability, ref IEnumerable<AbilityData> conversions)
		{
			var conversionList = conversions.ToList();
            #region 1&2 spells
            if (ability.Blueprint == BlueprintTool.Get<BlueprintAbility>(Guids.ArchmageGreaterAbility1))
			{
				for (int i = 1; i <= 2; i++)
				{
					foreach (var abilityData in SpellTools.SpellList.WizardSpellList.GetSpells(i))
					{
						AbilityVariants variantComponent = abilityData.GetComponent<AbilityVariants>();
						if (variantComponent != null)
						{
							foreach (var variant in variantComponent.Variants.AsEnumerable())
							{
								AbilityData.AddAbilityUnique(ref conversionList, new SpiritAbilityData(ability, variant)
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
				}
			}
            #endregion
            #region 3&4 spells
            if (ability.Blueprint == BlueprintTool.Get<BlueprintAbility>(Guids.ArchmageGreaterAbility2))
			{

				for (int i = 3; i <= 4; i++)
				{
					foreach (var abilityData in SpellTools.SpellList.WizardSpellList.GetSpells(i))
					{
						AbilityVariants variantComponent = abilityData.GetComponent<AbilityVariants>();
						if (variantComponent != null)
						{
							foreach (var variant in variantComponent.Variants.AsEnumerable())
							{
								AbilityData.AddAbilityUnique(ref conversionList, new SpiritAbilityData(ability, variant)
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
				}
			}
            #endregion
            #region 5&6 spells
            int j = 0;
			if (base.Owner.Progression.GetClassLevel(BlueprintTool.Get<BlueprintCharacterClass>(Guids.Medium)) >= 13)
			{
				j = 5;
			}
			else if(base.Owner.Progression.GetClassLevel(BlueprintTool.Get<BlueprintCharacterClass>(Guids.Medium)) >= 16)
			{
				j = 6;
            }

			if (ability.Blueprint == BlueprintTool.Get<BlueprintAbility>(Guids.ArchmageGreaterAbility3) && j != 0)
			{

				for (int i = 5; i <= j; i++)
				{
					foreach (var abilityData in SpellTools.SpellList.WizardSpellList.GetSpells(i))
					{
						AbilityVariants variantComponent = abilityData.GetComponent<AbilityVariants>();
						if (variantComponent != null)
						{
							foreach (var variant in variantComponent.Variants.AsEnumerable())
							{
								AbilityData.AddAbilityUnique(ref conversionList, new SpiritAbilityData(ability, variant)
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
				}
			}
            #endregion
            if (ability.Blueprint == BlueprintTool.Get<BlueprintAbility>(Guids.ArchmageSupremeAbility7))
			{

				for (int i = 7; i <= 7; i++)
				{
					foreach (var abilityData in SpellTools.SpellList.WizardSpellList.GetSpells(i))
					{
						AbilityVariants variantComponent = abilityData.GetComponent<AbilityVariants>();
						if (variantComponent != null)
						{
							foreach (var variant in variantComponent.Variants.AsEnumerable())
							{
								AbilityData.AddAbilityUnique(ref conversionList, new SpiritAbilityData(ability, variant)
								{
									OverridenResourceLogic = new InfluenceResourceOverride()
									{
										m_RequiredResource = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.ArchmageSupremeResource),
										cost = 1
									}
								});
							}
						}
						else
						{
							AbilityData.AddAbilityUnique(ref conversionList, new SpiritAbilityData(ability, abilityData)
							{
								OverridenResourceLogic = new InfluenceResourceOverride()
								{
									m_RequiredResource = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.ArchmageSupremeResource),
									cost = 1
								}
							});
						}
					}
				}
			}
			if (ability.Blueprint == BlueprintTool.Get<BlueprintAbility>(Guids.ArchmageSupremeAbility8))
			{

				for (int i = 8; i <= 8; i++)
				{
					foreach (var abilityData in SpellTools.SpellList.WizardSpellList.GetSpells(i))
					{
						AbilityVariants variantComponent = abilityData.GetComponent<AbilityVariants>();
						if (variantComponent != null)
						{
							foreach (var variant in variantComponent.Variants.AsEnumerable())
							{
								AbilityData.AddAbilityUnique(ref conversionList, new SpiritAbilityData(ability, variant)
								{
									OverridenResourceLogic = new InfluenceResourceOverride()
									{
										m_RequiredResource = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.ArchmageSupremeResource),
										cost = 1
									}
								});
							}
						}
						else
						{
							AbilityData.AddAbilityUnique(ref conversionList, new SpiritAbilityData(ability, abilityData)
							{
								OverridenResourceLogic = new InfluenceResourceOverride()
								{
									m_RequiredResource = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.ArchmageSupremeResource),
									cost = 1
								}
							});
						}
					}
				}
			}
			if (ability.Blueprint == BlueprintTool.Get<BlueprintAbility>(Guids.ArchmageSupremeAbility9))
			{

				for (int i = 9; i <= 9; i++)
				{
					foreach (var abilityData in SpellTools.SpellList.WizardSpellList.GetSpells(i))
					{
						AbilityVariants variantComponent = abilityData.GetComponent<AbilityVariants>();
						if (variantComponent != null)
						{
							foreach (var variant in variantComponent.Variants.AsEnumerable())
							{
								AbilityData.AddAbilityUnique(ref conversionList, new SpiritAbilityData(ability, variant)
								{
									OverridenResourceLogic = new InfluenceResourceOverride()
									{
										m_RequiredResource = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.ArchmageSupremeResource),
										cost = 1
									}
								});
							}
						}
						else
						{
							AbilityData.AddAbilityUnique(ref conversionList, new SpiritAbilityData(ability, abilityData)
							{
								OverridenResourceLogic = new InfluenceResourceOverride()
								{
									m_RequiredResource = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.ArchmageSupremeResource),
									cost = 1
								}
							});
						}
					}
				}
			}
			conversions = conversionList;
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
				unit.Descriptor.Buffs.AddBuff(BlueprintTool.Get<BlueprintBuff>(Guids.MediumInfluenceDebuff), unit, new TimeSpan(24, 0, 0));

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