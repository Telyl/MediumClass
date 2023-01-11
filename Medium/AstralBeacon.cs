using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Medium.NewActions;
using MediumClass.Medium.NewComponents;
using MediumClass.NewComponents;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium
{
    class AstralBeacon
    {
        private static readonly string FeatName = "AstralBeacon";
        private static readonly string DisplayName = "AstralBeacon.Name";
        private static readonly string Description = "AstralBeacon.Description";
        private const string IconPrefix = "assets/icons/";
        private const string ArchmageIconName = "spiritarchmage2.png";
        private const string ChampionIconName = "spiritchampion2.png";
        private const string GuardianIconName = "spiritguardian2.png";
        private const string HierophantIconName = "spirithierophant2.png";
        private const string MarshalIconName = "spiritmarshal2.png";
        private const string TricksterIconName = "spirittrickster2.png";
        private const string TranceOfThreeIconName = "tranceofthree.png";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(AstralBeacon));

        public static void ConfigureEnabled()
        {
            AbilityResourceConfigurator.New(FeatName + "Champion", Guids.MediumInfluenceResourceChampion).SetMaxAmount(new BlueprintAbilityResource.Amount
            {
                BaseValue = 5,
                IncreasedByLevel = false,
                IncreasedByLevelStartPlusDivStep = false,
                StartingLevel = 0,
                StartingIncrease = 0,
                LevelStep = 0,
                PerStepIncrease = 0,
                MinClassLevelIncrease = 0,
                IncreasedByStat = false,
                ResourceBonusStat = StatType.Charisma
            }).Configure();

            AbilityResourceConfigurator.New(FeatName + "Archmage", Guids.MediumInfluenceResourceArchmage).SetMaxAmount(new BlueprintAbilityResource.Amount
            {
                BaseValue = 5,
                IncreasedByLevel = false,
                IncreasedByLevelStartPlusDivStep = false,
                StartingLevel = 0,
                StartingIncrease = 0,
                LevelStep = 0,
                PerStepIncrease = 0,
                MinClassLevelIncrease = 0,
                IncreasedByStat = false,
                ResourceBonusStat = StatType.Charisma
            }).Configure();

            

            AbilityResourceConfigurator.New(FeatName + "Guardian", Guids.MediumInfluenceResourceGuardian).SetMaxAmount(new BlueprintAbilityResource.Amount
            {
                BaseValue = 5,
                IncreasedByLevel = false,
                IncreasedByLevelStartPlusDivStep = false,
                StartingLevel = 0,
                StartingIncrease = 0,
                LevelStep = 0,
                PerStepIncrease = 0,
                MinClassLevelIncrease = 0,
                IncreasedByStat = false,
                ResourceBonusStat = StatType.Charisma
            }).Configure();

            AbilityResourceConfigurator.New(FeatName + "Hierophant", Guids.MediumInfluenceResourceHierophant).SetMaxAmount(new BlueprintAbilityResource.Amount
            {
                BaseValue = 5,
                IncreasedByLevel = false,
                IncreasedByLevelStartPlusDivStep = false,
                StartingLevel = 0,
                StartingIncrease = 0,
                LevelStep = 0,
                PerStepIncrease = 0,
                MinClassLevelIncrease = 0,
                IncreasedByStat = false,
                ResourceBonusStat = StatType.Charisma
            }).Configure();

            AbilityResourceConfigurator.New(FeatName + "Marshal", Guids.MediumInfluenceResourceMarshal).SetMaxAmount(new BlueprintAbilityResource.Amount
            {
                BaseValue = 5,
                IncreasedByLevel = false,
                IncreasedByLevelStartPlusDivStep = false,
                StartingLevel = 0,
                StartingIncrease = 0,
                LevelStep = 0,
                PerStepIncrease = 0,
                MinClassLevelIncrease = 0,
                IncreasedByStat = false,
                ResourceBonusStat = StatType.Charisma
            }).Configure();

            AbilityResourceConfigurator.New(FeatName + "Trickster", Guids.MediumInfluenceResourceTrickster).SetMaxAmount(new BlueprintAbilityResource.Amount
            {
                BaseValue = 5,
                IncreasedByLevel = false,
                IncreasedByLevelStartPlusDivStep = false,
                StartingLevel = 0,
                StartingIncrease = 0,
                LevelStep = 0,
                PerStepIncrease = 0,
                MinClassLevelIncrease = 0,
                IncreasedByStat = false,
                ResourceBonusStat = StatType.Charisma
            }).Configure();

            FeatureConfigurator.New(FeatName, Guids.AstralBeacon)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddAbilityResources(amount: 0, resource: Guids.MediumInfluenceResourceArchmage, useThisAsResource: false, restoreAmount: true)
                .AddAbilityResources(amount: 0, resource: Guids.MediumInfluenceResourceChampion, useThisAsResource: false, restoreAmount: true)
                .AddAbilityResources(amount: 0, resource: Guids.MediumInfluenceResourceGuardian, useThisAsResource: false, restoreAmount: true)
                .AddAbilityResources(amount: 0, resource: Guids.MediumInfluenceResourceHierophant, useThisAsResource: false, restoreAmount: true)
                .AddAbilityResources(amount: 0, resource: Guids.MediumInfluenceResourceMarshal, useThisAsResource: false, restoreAmount: true)
                .AddAbilityResources(amount: 0, resource: Guids.MediumInfluenceResourceTrickster, useThisAsResource: false, restoreAmount: true)
                .Configure();
        }
    }
}
