using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Champion
{
    class Champion
    {
        private static readonly string ClassName = "Champion";
        internal const string ChampionName = "Champion.Name";
        private static readonly string ChampionDescription = "Champion.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Champion));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Champion Spirit");
            CharacterClassConfigurator.New(ClassName, Guids.Champion).Configure();
            SpiritBonus.ConfigureEnabled();
            SeanceBoon.ConfigureEnabled();
            SuddenAttack.ConfigureEnabled();
            FleetCharge.ConfigureEnabled();
            LegendaryChampion.ConfigureEnabled();
            ChampionLesser.ConfigureEnabled();
            BlueprintProgression Progression = ChampionProgression();

            CharacterClassConfigurator.For(Guids.Champion)
                .SetLocalizedName(ChampionName)
                .SetLocalizedDescription(ChampionDescription)
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
        private static BlueprintProgression ChampionProgression()
        {
            var entries = LevelEntryBuilder.New()
                .AddEntry(1, Guids.ChampionSpiritBonus, Guids.ChampionSeanceBoon, Guids.ChampionLesser)
                .AddEntry(4, Guids.ChampionSpiritBonus)
                .AddEntry(6, Guids.ChampionSuddenAttack)
                .AddEntry(8, Guids.ChampionSpiritBonus)
                .AddEntry(11, Guids.ChampionFleetCharge)
                .AddEntry(12, Guids.ChampionSpiritBonus)
                .AddEntry(16, Guids.ChampionSpiritBonus)
                .AddEntry(17, Guids.LegendaryChampion)
                .AddEntry(20, Guids.ChampionSpiritBonus);

            return ProgressionConfigurator.New(ClassName +"Progression", Guids.ChampionProgression)
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(false)
                .SetForAllOtherClasses(false)
                .SetLevelEntries(entries)
                .AddToClasses(Guids.Champion)
                .Configure();
        }
    }
}
