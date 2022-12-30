﻿using BlueprintCore.Blueprints.References;
using static UnityModManagerNet.UnityModManager.ModEntry;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.Configurators.Classes;
using Kingmaker.RuleSystem;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Root;
using Kingmaker.Blueprints.Classes.Spells;
using MediumClass.Utils;
using MediumClass.Utilities;

namespace MediumClass.Medium
{
    /// <summary>
    /// Adds the Strong Jaw spell that only effects creatures of AnimalType.
    /// </summary>
    public class MediumClass
    {
        private static readonly string ClassName = "MediumClass";
        internal const string DisplayName = "MediumClass.Name";
        private static readonly string Description = "MediumClass.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Medium));

        public static void ConfigureEnabled()
        {
            BlueprintCharacterClass mediumclass = CharacterClassConfigurator.New(ClassName, Guids.Medium).Configure();
            BlueprintProgression mediumprogression = MediumProgression.Configure();
            BlueprintSpellbook mediumspellbook = MediumSpellbook.ConfigureEnabled();
            Spirits.Archmage.Archmage.ConfigureEnabled();
            Spirits.Champion.Champion.ConfigureEnabled();
            Spirits.Guardian.Guardian.ConfigureEnabled();
            Spirits.Hierophant.Hierophant.ConfigureEnabled();
            Spirits.Marshal.Marshal.ConfigureEnabled();
            Spirits.Trickster.Trickster.ConfigureEnabled();

            CharacterClassConfigurator.For(Guids.Medium)
                .SetLocalizedName(DisplayName)
                .SetLocalizedDescription(Description)
                .SetSkillPoints(3) // Medium is 4 + INT in TT
                .SetHitDie(DiceType.D8)
                .SetPrestigeClass(false)
                .SetIsMythic(false)
                .SetHideIfRestricted(false)
                .SetBaseAttackBonus(StatProgressionRefs.BABMedium.Reference.Get())
                .SetFortitudeSave(StatProgressionRefs.SavesLow.Reference.Get())
                .SetReflexSave(StatProgressionRefs.SavesLow.Reference.Get())
                .SetWillSave(StatProgressionRefs.SavesHigh.Reference.Get())
                .SetProgression(mediumprogression)
                .SetSpellbook(mediumspellbook)
                .SetIsDivineCaster(false)
                .SetIsArcaneCaster(false)
                .SetStartingGold(411)
                .SetStartingItems(
                ItemWeaponRefs.ColdIronShortsword.Reference.Get(),
                ItemArmorRefs.ScalemailStandard.Reference.Get(),
                ItemEquipmentUsableRefs.PotionOfCureLightWounds.Reference.Get(),
                ItemEquipmentUsableRefs.ScrollOfMageArmor.Reference.Get(),
                ItemEquipmentUsableRefs.ScrollOfMageShield.Reference.Get())
                .SetPrimaryColor(11)
                .SetSecondaryColor(47)
                .SetDifficulty(3)
                .AddToMaleEquipmentEntities("f26d20fbaedf1374388c75d7ab1d9ecc", "69b184d9e882f204f99c2cff2d471a13")
                .AddToFemaleEquipmentEntities("2072db411b232024daf6fbfac1001065", "cec1fa08b14c22647834f2168336e16f")
                .AddToClassSkills(
                StatType.SkillPersuasion,
                StatType.SkillLoreReligion,
                StatType.SkillKnowledgeArcana,
                StatType.SkillPerception,
                StatType.SkillUseMagicDevice)
                .AddToRecommendedAttributes(StatType.Charisma)
                .Configure();

            BlueprintCharacterClassReference classref = mediumclass.ToReference<BlueprintCharacterClassReference>();
            BlueprintRoot root = BlueprintTool.Get<BlueprintRoot>("2d77316c72b9ed44f888ceefc2a131f6");
            root.Progression.m_CharacterClasses = CommonTool.Append(root.Progression.m_CharacterClasses, classref);
            
                


        }
    }
}

