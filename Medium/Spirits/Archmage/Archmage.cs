using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Spells;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Archmage
{
    class Archmage
    {
        private static readonly string ClassName = "Archmage";
        internal const string ArchmageName = "Archmage.Name";
        private static readonly string ArchmageDescription = "Archmage.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Archmage));

        public static void ConfigureEnabled()
        {

            Logger.Log("Generating Archmage Spirit.");
            CharacterClassConfigurator.New(ClassName, Guids.Archmage).Configure();
            SeanceBoon.ConfigureEnabled();
            ArcaneSurge.ConfigureEnabled();
            WildArcana.ConfigureEnabled();
            BlueprintSpellbook Spellbook = ArchmageSpellbook();
            BlueprintProgression Progression = ArchmageProgression();

            CharacterClassConfigurator.For(Guids.Archmage)
                .SetLocalizedName(ArchmageName)
                .SetLocalizedDescription(ArchmageDescription)
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
                .SetSpellbook(Spellbook)
                .SetIsDivineCaster(false)
                .SetIsArcaneCaster(true)
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
        private static BlueprintProgression ArchmageProgression()
        {
            var entries = LevelEntryBuilder.New()
                .AddEntry(1, Guids.ArchmageSeanceBoon, Guids.ArchmageLesser)
                .AddEntry(6, Guids.ArchmageIntermediate)
                .AddEntry(11, Guids.ArchmageGreater)
                .AddEntry(17);

            return ProgressionConfigurator.New(ClassName +"Progression", Guids.ArchmageProgression)
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(true)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(false)
                .SetForAllOtherClasses(false)
                .AddToClasses(Guids.Archmage)
                .SetLevelEntries(entries)
                .Configure();
        }
        private static BlueprintSpellbook ArchmageSpellbook()
        {
            var SpellSlotsTable = SpellsTableConfigurator.New(ClassName + "SpellSlotsTable", Guids.ArchmageSpellSlotsTable)
                .SetLevels(new SpellsLevelEntry[] {
                    new SpellsLevelEntry{ Count = new int[] { 0 } },//0
                    new SpellsLevelEntry{ Count = new int[] { 0, 1 } },//1
                    new SpellsLevelEntry{ Count = new int[] { 0, 1 } },//2
                    new SpellsLevelEntry{ Count = new int[] { 0, 1 } },//3
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1 } },//4
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1 } },//5
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1 } },//6
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1 } },//7
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1 } },//8
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1 } },//9
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1, 1 } },//10
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1, 1 } },//11
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1, 1 } },//12
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1, 1, 1 } },//13
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1, 1, 1 } },//14
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1, 1, 1 } },//15
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1, 1, 1, 1 } },//16
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1, 1, 1, 1 } },//17
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1, 1, 1, 1 } },//18
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1, 1, 1, 1 } },//19
                    new SpellsLevelEntry{ Count = new int[] { 0, 1, 1, 1, 1, 1, 1 } },//20
                    })
                .Configure();

            var SpellsPerDayTable = SpellsTableConfigurator.New(ClassName + "SpellPerDayTable", Guids.ArchmageSpellsPerDayTable)
                .SetLevels(new SpellsLevelEntry[] {
                    new SpellsLevelEntry{ Count = new int[] { 0 } },//0
                    new SpellsLevelEntry{ Count = new int[] { 0, 1 } },//1
                    new SpellsLevelEntry{ Count = new int[] { 0, 2 } },//2
                    new SpellsLevelEntry{ Count = new int[] { 0, 3 } },//3
                    new SpellsLevelEntry{ Count = new int[] { 0, 3, 1 } },//4
                    new SpellsLevelEntry{ Count = new int[] { 0, 4, 2 } },//5
                    new SpellsLevelEntry{ Count = new int[] { 0, 4, 3 } },//6
                    new SpellsLevelEntry{ Count = new int[] { 0, 4, 3, 1 } },//7
                    new SpellsLevelEntry{ Count = new int[] { 0, 4, 4, 2 } },//8
                    new SpellsLevelEntry{ Count = new int[] { 0, 5, 4, 3 } },//9
                    new SpellsLevelEntry{ Count = new int[] { 0, 5, 4, 3, 1 } },//10
                    new SpellsLevelEntry{ Count = new int[] { 0, 5, 4, 4, 2 } },//11
                    new SpellsLevelEntry{ Count = new int[] { 0, 5, 5, 4, 3 } },//12
                    new SpellsLevelEntry{ Count = new int[] { 0, 5, 5, 4, 3, 1 } },//13
                    new SpellsLevelEntry{ Count = new int[] { 0, 5, 5, 4, 4, 2 } },//14
                    new SpellsLevelEntry{ Count = new int[] { 0, 5, 5, 5, 4, 3 } },//15
                    new SpellsLevelEntry{ Count = new int[] { 0, 5, 5, 5, 4, 3, 1 } },//16
                    new SpellsLevelEntry{ Count = new int[] { 0, 5, 5, 5, 4, 4, 2, 1, 1, 1 } },//17
                    new SpellsLevelEntry{ Count = new int[] { 0, 5, 5, 5, 5, 4, 3, 1, 1, 1 } },//18
                    new SpellsLevelEntry{ Count = new int[] { 0, 5, 5, 5, 5, 5, 4, 1, 1, 1 } },//19
                    new SpellsLevelEntry{ Count = new int[] { 0, 5, 5, 5, 5, 5, 5, 1, 1, 1 } },//20
                    })
                .Configure();

            return SpellbookConfigurator.New(ClassName + "Spellbook", Guids.ArchmageSpellbook)
                .SetName(ArchmageName)
                .SetCharacterClass(Guids.Archmage)
                .SetSpellsPerDay(SpellsPerDayTable)
                .SetSpellSlots(SpellSlotsTable)
                .SetSpellList(SpellListRefs.WizardSpellList.Reference.Get())
                .SetCastingAttribute(StatType.Charisma)
                .SetAllSpellsKnown(true)
                .SetIsMythic(false)
                .SetSpontaneous(true)
                .SetCantripsType(CantripsType.Cantrips)
                .SetIsArcane(true)
                .SetIsArcanist(true)
                .SetCanCopyScrolls(false)
                .SetCasterLevelModifier(0)
                .Configure();
        }
    }
}
