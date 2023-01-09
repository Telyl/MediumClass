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

namespace MediumClass.Medium.Spirits.Marshal
{
    class Marshal
    {
        private static readonly string ClassName = "Marshal";
        internal const string MarshalName = "Marshal.Name";
        private static readonly string MarshalDescription = "Marshal.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Marshal));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Marshal Spirit");
            CharacterClassConfigurator.New(ClassName, Guids.Marshal).Configure();
            SpiritBonus.ConfigureEnabled();
            SeanceBoon.ConfigureEnabled();
            InspiringCall.ConfigureEnabled();
            MarshalsOrders.ConfigureEnabled();
            DecisiveStrike.ConfigureEnabled();
            LegendaryMarshal.ConfigureEnabled();
            BlueprintProgression Progression = MarshalProgression();

            CharacterClassConfigurator.For(Guids.Marshal)
                .SetLocalizedName(MarshalName)
                .SetLocalizedDescription(MarshalDescription)
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
        private static BlueprintProgression MarshalProgression()
        {
            var entries = LevelEntryBuilder.New()
                .AddEntry(1,
                    Guids.MarshalSeanceBoon,
                    Guids.MarshalSpiritBonus,
                    Guids.MarshalMarshalsOrders)
                .AddEntry(4, Guids.MarshalSpiritBonus)
                .AddEntry(6, Guids.MarshalInspiringCallStandard)
                .AddEntry(8, Guids.MarshalSpiritBonus)
                .AddEntry(11, Guids.MarshalInspiringCallMove, Guids.MarshalDecisiveStrikeFeature)
                .AddEntry(12, Guids.MarshalSpiritBonus)
                .AddEntry(16, Guids.MarshalSpiritBonus)
                .AddEntry(17, Guids.MarshalInspiringCallSwift, Guids.MarshalLegendaryMarshal)
                .AddEntry(20, Guids.MarshalSpiritBonus);

            return ProgressionConfigurator.New(ClassName +"Progression", Guids.MarshalProgression)
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(false)
                .SetForAllOtherClasses(false)
                .AddToClasses(Guids.Marshal)
                .SetLevelEntries(entries)
                .Configure();
        }
    }
}
