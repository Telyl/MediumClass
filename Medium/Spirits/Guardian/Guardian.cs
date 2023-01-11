using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Spells;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Guardian
{
    class Guardian
    {
        private static readonly string ClassName = "Guardian";
        internal const string GuardianName = "Guardian.Name";
        private static readonly string GuardianDescription = "Guardian.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Guardian));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Guardian Spirit");
            CharacterClassConfigurator.New(ClassName, Guids.Guardian).Configure();
            SeanceBoon.ConfigureEnabled();
            GuardianLesser.ConfigureEnabled();
            GuardianSmite.ConfigureEnabled();
            LegendaryGuardian.ConfigureEnabled();
            AbsorbBlow.ConfigureEnabled();
            BlueprintProgression Progression = GuardianProgression();

            CharacterClassConfigurator.For(Guids.Guardian)
                .SetLocalizedName(GuardianName)
                .SetLocalizedDescription(GuardianDescription)
                .SetSkillPoints(0)
                .SetHitDie(DiceType.D8)
                .SetPrestigeClass(false)
                .SetIsMythic(false)
                .SetHideIfRestricted(false)
                .SetBaseAttackBonus(StatProgressionRefs.BABMedium.Reference.Get())
                .SetFortitudeSave(StatProgressionRefs.SavesLow.Reference.Get())
                .SetReflexSave(StatProgressionRefs.SavesLow.Reference.Get())
                .SetWillSave(StatProgressionRefs.SavesHigh.Reference.Get())
                .SetProgression(Progression)
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
        }
        private static BlueprintProgression GuardianProgression()
        {
            var entries = LevelEntryBuilder.New()
                .AddEntry(1, Guids.GuardianLesser)
                .AddEntry(6, Guids.GuardianAbsorbBlow)
                .AddEntry(11, Guids.GuardianGreater)
                .AddEntry(17, Guids.LegendaryGuardian);

            return ProgressionConfigurator.New(ClassName +"Progression", Guids.GuardianProgression)
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(false)
                .SetForAllOtherClasses(false)
                .AddToClasses(Guids.Guardian)
                .SetLevelEntries(entries)
                .Configure();
        }
    }
}
