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

namespace MediumClass.Medium.Spirits.Trickster
{
    class Trickster
    {
        private static readonly string ClassName = "Trickster";
        internal const string TricksterName = "Trickster.Name";
        private static readonly string TricksterDescription = "Trickster.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Trickster));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Trickster Spirit");
            CharacterClassConfigurator.New(ClassName, Guids.Trickster).Configure();
            SpiritBonus.ConfigureEnabled();
            SeanceBoon.ConfigureEnabled();
            TrickstersEdge.ConfigureEnabled();
            SurpriseStrike.ConfigureEnabled();
            TransferMagic.ConfigureEnabled();
            LegendaryTrickster.ConfigureEnabled();
            BlueprintProgression Progression = TricksterProgression();

            CharacterClassConfigurator.For(Guids.Trickster)
                .SetLocalizedName(TricksterName)
                .SetLocalizedDescription(TricksterDescription)
                .SetSkillPoints(4)
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
        private static BlueprintProgression TricksterProgression()
        {
            //FeatureRefs.SneakAttack.Reference.Get()
            var entries = LevelEntryBuilder.New()
                .AddEntry(1, Guids.TricksterSpiritBonus, Guids.TricksterSeanceBoon, Guids.TricksterEdge)
                .AddEntry(4, Guids.TricksterSpiritBonus)
                .AddEntry(6, Guids.TricksterSurpriseStrike, Guids.TricksterSurpriseStrike)
                .AddEntry(8, Guids.TricksterSpiritBonus)
                .AddEntry(9, Guids.TricksterSurpriseStrike)
                .AddEntry(11, Guids.TricksterTransferMagic)
                .AddEntry(12, Guids.TricksterSpiritBonus, Guids.TricksterSurpriseStrike)
                .AddEntry(15, Guids.TricksterSurpriseStrike)
                .AddEntry(16, Guids.TricksterSpiritBonus)
                .AddEntry(17, Guids.TricksterLegendaryTrickster)
                .AddEntry(18, Guids.TricksterSurpriseStrike)
                .AddEntry(20, Guids.TricksterSpiritBonus);

            return ProgressionConfigurator.New(ClassName +"Progression", Guids.TricksterProgression)
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(false)
                .SetForAllOtherClasses(false)
                .AddToClasses(Guids.Trickster)
                .SetLevelEntries(entries)
                .AddSkillPointPerCharacterLevel()
                .Configure();
        }
    }
}
