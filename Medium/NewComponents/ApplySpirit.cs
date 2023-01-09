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
	[TypeId("454ad9c029fc4e598c347f383ea5b8a4")]
	public class ApplySpirit : UnitFactComponentDelegate<ApplyClassProgressionData>
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(ApplySpirit));

		private static BlueprintUnitFact _archmageSeance;
		private static BlueprintUnitFact ArchmageSeance
		{
			get
			{
				_archmageSeance ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.ArchmageSeanceBoon);
				return _archmageSeance;
			}
		}

		private static BlueprintUnitFact _championSeance;
		private static BlueprintUnitFact ChampionSeance
		{
			get
			{
				_championSeance ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.ChampionSeanceBoon);
				return _championSeance;
			}
		}

		private static BlueprintUnitFact _guardianSeance;
		private static BlueprintUnitFact GuardianSeance
		{
			get
			{
				_guardianSeance ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.GuardianSeanceBoon);
				return _guardianSeance;
			}
		}

		private static BlueprintUnitFact _hierophantSeance;
		private static BlueprintUnitFact HierophantSeance
		{
			get
			{
				_hierophantSeance ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.HierophantSeanceBoon);
				return _hierophantSeance;
			}
		}

		private static BlueprintUnitFact _marshalSeance;
		private static BlueprintUnitFact MarshalSeance
		{
			get
			{
				_marshalSeance ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.MarshalSeanceBoon);
				return _marshalSeance;
			}
		}

		private static BlueprintUnitFact _tricksterSeance;
		private static BlueprintUnitFact TricksterSeance
		{
			get
			{
				_tricksterSeance ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.TricksterSeanceBoon);
				return _tricksterSeance;
			}
		}

		// Token: 0x17001CF5 RID: 7413
		// (get) Token: 0x0600BDC6 RID: 48582 RVA: 0x00317B1B File Offset: 0x00315D1B
		public BlueprintCharacterClass Class
		{
			get
			{
				return this.m_Class;
			}
		}

		// Token: 0x17001CF6 RID: 7414
		// (get) Token: 0x0600BDC7 RID: 48583 RVA: 0x00317B28 File Offset: 0x00315D28
		public ReferenceArrayProxy<BlueprintAbility, BlueprintAbilityReference> SelectSpells
		{
			get
			{
				return this.m_SelectSpells;
			}
		}

		// Token: 0x17001CF7 RID: 7415
		// (get) Token: 0x0600BDC8 RID: 48584 RVA: 0x00317B35 File Offset: 0x00315D35
		public ReferenceArrayProxy<BlueprintAbility, BlueprintAbilityReference> MemorizeSpells
		{
			get
			{
				return this.m_MemorizeSpells;
			}
		}

		// Token: 0x17001CF8 RID: 7416
		// (get) Token: 0x0600BDC9 RID: 48585 RVA: 0x00317B42 File Offset: 0x00315D42
		public ReferenceArrayProxy<BlueprintFeature, BlueprintFeatureReference> Features
		{
			get
			{
				return this.m_Features;
			}
		}

		// Token: 0x0600BDCA RID: 48586 RVA: 0x00317B4F File Offset: 0x00315D4F
		public override void OnActivate()
		{
			this.Level = Owner.Descriptor.Progression.GetClassLevel(BlueprintTool.Get<BlueprintCharacterClass>(Guids.Medium));
			this.TryApplyClassLevels();
		}

		// Token: 0x0600BDCB RID: 48587 RVA: 0x00317B57 File Offset: 0x00315D57
		public override void OnDeactivate()
		{
			this.Revert();
		}

		// Token: 0x0600BDCC RID: 48588 RVA: 0x00317B60 File Offset: 0x00315D60
		private void TryApplyClassLevels()
		{
			ClassData classData = base.Owner.Progression.GetClassData(this.Class);
			int num = ((classData != null) ? classData.Level : 0);
			int num2 = Math.Max(num, Math.Min(20, this.Level));
			//base.Owner.Progression.AddFakeClassLevels(this.Class, num2); //We don't want this. In fact, this is bad and breaks level-ups!
			base.Data.Class = this.Class;
			base.Data.Levels = num2 - num;
			Game.Instance.AdvanceGameTime(new TimeSpan(1, 0, 0), false);

			foreach (BlueprintFeature blueprintFeature in this.Features)
			{
				EntityFact entityFact = base.Owner.AddFact(blueprintFeature, null, null);
				base.Data.Features.Add(entityFact.UniqueId);
			}
			foreach (ParameterizedFeatureEntry parameterizedFeatureEntry in this.ParameterizedFeatures)
			{
				Feature feature = (Feature)base.Owner.AddFact(parameterizedFeatureEntry.Feature, null, parameterizedFeatureEntry.Param);
				base.Data.Features.Add(feature.UniqueId);
			}
			//Spellbook spellbook = ((this.Class.Spellbook != null) ? base.Owner.Descriptor.DemandSpellbook(this.Class.Spellbook) : null);
			Spellbook spellbook = null;
			base.Data.Spellbook = this.Class.Spellbook;
			base.Data.Progressions.InsertRange(0, this.CollectProgressions(this.Level));
			for (int j = 1; j <= num2; j++)
			{
				Spellbook spellbook2 = spellbook;
				if (spellbook2 != null)
				{
					spellbook2.AddLevelFromClass(this.Class);
				}
				foreach (BlueprintProgression blueprintProgression in base.Data.Progressions)
				{
					this.ApplyProgressionLevel(blueprintProgression, j);
				}
			}
			Func<SpellListComponent, bool> bspelllistcomp = null;
			foreach (BlueprintAbility blueprintAbility in this.SelectSpells)
			{
				SpellListComponent spellListComponent;
				if (spellbook == null)
				{
					spellListComponent = null;
				}
				else
				{
					IEnumerable<SpellListComponent> components = blueprintAbility.GetComponents<SpellListComponent>();
					Func<SpellListComponent, bool> func;
					if ((func = bspelllistcomp) == null)
					{
						func = (bspelllistcomp = (SpellListComponent sl) => sl.SpellList == spellbook.Blueprint.SpellList);
					}
					spellListComponent = components.FirstOrDefault(func);
				}
				SpellListComponent spellListComponent2 = spellListComponent;
				if (spellbook != null && spellListComponent2 != null)
				{
					spellbook.AddKnown(spellListComponent2.SpellLevel, blueprintAbility, true);
					base.Data.Spells.Add(blueprintAbility);
				}
				else
				{
					Ability ability = (Ability)base.Owner.AddFact(blueprintAbility, null, null);
					base.Data.Abilities.Add(ability.UniqueId);
				}
			}
			if (spellbook != null)
			{
				spellbook.UpdateAllSlotsSize(true);
				foreach (BlueprintAbility blueprintAbility2 in this.MemorizeSpells)
				{
					int minSpellLevel = spellbook.GetMinSpellLevel(blueprintAbility2);
					AbilityData abilityData = new AbilityData(blueprintAbility2, spellbook, minSpellLevel);
					spellbook.Memorize(abilityData, null);
				}
			}
			UnitPartMedium medium = base.Owner.Ensure<UnitPartMedium>();
			int SpiritPowerRank = base.Owner.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.SpiritPower)) - medium.ForgonePowers;
			if (this.Class == BlueprintTool.Get<BlueprintCharacterClass>(Guids.Archmage) && SpiritPowerRank > 0)
            {
				base.Owner.Progression.Features.RemoveFact(BlueprintTool.Get<BlueprintFeature>(Guids.MediumSpellcasterFeatProhibitArchmage));
			}
			else if (this.Class == BlueprintTool.Get<BlueprintCharacterClass>(Guids.Hierophant) && SpiritPowerRank > 0)
			{
				base.Owner.Progression.Features.RemoveFact(BlueprintTool.Get<BlueprintFeature>(Guids.MediumSpellcasterFeatProhibitHierophant));
			}
			if(base.Owner.Progression.Features.HasFact(BlueprintTool.Get<BlueprintFeature>(Guids.MediumSpiritMastery)) && medium.FreeSurgeAmount == 0)
            {
				medium.FreeSurgeAmount += 2;
            }
		}

		// Token: 0x0600BDCD RID: 48589 RVA: 0x00317EDC File Offset: 0x003160DC
		private List<BlueprintProgression> CollectProgressions(int level)
		{
			BlueprintProgression progression = base.Owner.GetProgression(this.Class);
			List<BlueprintProgression> list = new List<BlueprintProgression> { progression };
			foreach (LevelEntry levelEntry in progression.LevelEntries)
			{
				if (levelEntry.Level <= level)
				{
					foreach (BlueprintFeatureBase blueprintFeatureBase in levelEntry.Features)
					{
						BlueprintProgression blueprintProgression = blueprintFeatureBase as BlueprintProgression;
						if (blueprintProgression != null)
						{
							list.Add(blueprintProgression);
						}
					}
				}
			}
			foreach (BlueprintFeature blueprintFeature in this.Features)
			{
				BlueprintProgression blueprintProgression2 = blueprintFeature as BlueprintProgression;
				if (blueprintProgression2 != null)
				{
					list.Add(blueprintProgression2);
				}
			}
			return list;
		}

		// Token: 0x0600BDCE RID: 48590 RVA: 0x00317FD0 File Offset: 0x003161D0
		private void ApplyProgressionLevel(BlueprintProgression progression, int level)
		{
			LevelEntry levelEntry = progression.LevelEntries.FirstItem((LevelEntry e) => e.Level == level);
			if (levelEntry == null)
			{
				return;
			}
			//Get the spirit powers from unit part and exnay them!
			UnitPartMedium medium = base.Owner.Ensure<UnitPartMedium>();
			int SpiritPowerRank = base.Owner.Progression.Features.GetRank(BlueprintTool.Get<BlueprintFeature>(Guids.SpiritPower)) - medium.ForgonePowers;
			foreach (BlueprintFeatureBase blueprintFeatureBase in levelEntry.Features)
			{
				if (!(blueprintFeatureBase is IFeatureSelection))
				{
					BlueprintFeature blueprintFeature = blueprintFeatureBase as BlueprintFeature;
					if (blueprintFeature == null || blueprintFeature.MeetsPrerequisites(null, base.Owner, null, true))
					{
						if (SpiritLesserPower.Get() == blueprintFeature)
                        {
							if (SpiritPowerRank == 0) { continue; }
						}
						else if (SpiritIntermediatePower.Get() == blueprintFeature)
                        {
							if (SpiritPowerRank <= 1) { continue; }
						}
						else if (SpiritIntermediatePowerMove.Get() == blueprintFeature)
						{
							if (SpiritPowerRank <= 1) { continue; }
						}
						else if (SpiritIntermediatePowerSwift.Get() == blueprintFeature)
						{
							if (SpiritPowerRank <= 1) { continue; }
						}
						else if (SpiritGreaterPower.Get() == blueprintFeature)
                        {
							if (SpiritPowerRank <= 2) { continue; }
						}	
						else if (SpiritSupremePower.Get() == blueprintFeature)
                        {
							if (SpiritPowerRank <= 3) { continue; }
						}
						EntityFact entityFact = base.Owner.AddFact(blueprintFeatureBase, null, null);
						base.Data.Features.Add(entityFact.UniqueId);
					}
				}
			}
			//TODO: Add resource for each spirit power removed. Add a pre-created feat X amount of times! Remove it later! (or maybe this += 2 trick works?)
			//base.Owner.Resources.GetResource(BlueprintTool.Get<BlueprintAbilityResource>(Guids.MediumInfluenceResource))
		}

		// Token: 0x0600BDCF RID: 48591 RVA: 0x00318090 File Offset: 0x00316290
		private void Revert()
		{
			Logger.Log($"Reverting. Removing Class Levels of Data.Class and Data.Levels {base.Data.Class} and {base.Data.Levels}");
			base.Owner.Progression.RemoveClassLevels(base.Data.Class, base.Data.Levels);
			foreach (string text in base.Data.Abilities)
			{
				base.Owner.Facts.Remove(base.Owner.Facts.FindById(text), true);
			}

			foreach (string text2 in base.Data.Features)
			{
				base.Owner.Facts.Remove(base.Owner.Facts.FindById(text2), true);
			}
			
			//TODO: This is bad. Please. This is a hack job that isn't even a hack job. Clean up later.
			List<Buff> list = base.Owner.Buffs.Enumerable.ToTempList<Buff>();
			foreach (Buff buff in list)
			{
				if (buff.Blueprint.Name.Contains("Trickster's Edge")){
					Logger.Log("It contained Trickster's Edge.");
					base.Owner.Buffs.RemoveFact(buff);
				}
				if (buff.Blueprint.Name.Contains("Seance Boon"))
				{
					Logger.Log("It contained Seance Boon");
					base.Owner.Buffs.RemoveFact(buff);
				}
			}


			// No one else around will have the medium class, so I'm going to brute force it.
			UnitEntityData medium = base.Owner;
			foreach (UnitEntityData unitEntityData in GameHelper.GetTargetsAround(base.Owner.Position, FeetExtension.Feet(1000), false, true))
			{
				unitEntityData.RemoveFact(ArchmageSeance);
				unitEntityData.RemoveFact(ChampionSeance);
				unitEntityData.RemoveFact(GuardianSeance);
				unitEntityData.RemoveFact(HierophantSeance);
				unitEntityData.RemoveFact(MarshalSeance);
				unitEntityData.RemoveFact(TricksterSeance);
			}
			base.Owner.Progression.Features.AddFact(BlueprintTool.Get<BlueprintFeature>(Guids.MediumSpellcasterFeatProhibitArchmage), Context);
			base.Owner.Progression.Features.AddFact(BlueprintTool.Get<BlueprintFeature>(Guids.MediumSpellcasterFeatProhibitHierophant), Context);
			base.ClearData();
		}

		// Token: 0x0400802C RID: 32812
		[Range(1f, 20f)]
		public int Level = 1;

		// Token: 0x0400802D RID: 32813
		[SerializeField]
		[ValidateNotNull]
		public BlueprintCharacterClassReference m_Class;

		public BlueprintFeatureReference SpiritLesserPower = new BlueprintFeatureReference();
		public BlueprintFeatureReference SpiritIntermediatePower = new BlueprintFeatureReference();
		public BlueprintFeatureReference SpiritIntermediatePowerMove = new BlueprintFeatureReference();
		public BlueprintFeatureReference SpiritIntermediatePowerSwift = new BlueprintFeatureReference();
		public BlueprintFeatureReference SpiritGreaterPower = new BlueprintFeatureReference();
		public BlueprintFeatureReference SpiritSupremePower = new BlueprintFeatureReference();

		// Token: 0x0400802E RID: 32814
		[SerializeField]
		[ValidateNoNullEntries]
		public BlueprintAbilityReference[] m_SelectSpells = new BlueprintAbilityReference[0];

		// Token: 0x0400802F RID: 32815
		[SerializeField]
		[ValidateNoNullEntries]
		public BlueprintAbilityReference[] m_MemorizeSpells = new BlueprintAbilityReference[0];

		// Token: 0x04008030 RID: 32816
		[SerializeField]
		[ValidateNoNullEntries]
		public BlueprintFeatureReference[] m_Features = new BlueprintFeatureReference[0];

		// Token: 0x04008031 RID: 32817
		[ValidateNoNullEntries]
		public ParameterizedFeatureEntry[] ParameterizedFeatures = new ParameterizedFeatureEntry[0];
	}
}
