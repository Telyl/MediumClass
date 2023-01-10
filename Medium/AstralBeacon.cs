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
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(TranceOfThree));

        public static void ConfigureEnabled()
        {
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

            var ab = BuffConfigurator.New(FeatName + "BuffArchmage", Guids.MediumChannelSpiritSecondarySpiritBuffArchmage)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.isSecondarySpirit = true;
                    c.SecondarySpirit = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Archmage);
                })
                .SetFlags(BlueprintBuff.Flags.RemoveOnRest)
                .Configure();

            var cb = BuffConfigurator.New(FeatName + "BuffChampion", Guids.MediumChannelSpiritSecondarySpiritBuffChampion)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.isSecondarySpirit = true;
                    c.SecondarySpirit = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Champion);
                })
                .SetFlags(BlueprintBuff.Flags.RemoveOnRest)
                .Configure();

            var gb = BuffConfigurator.New(FeatName + "BuffGuardian", Guids.MediumChannelSpiritSecondarySpiritBuffGuardian)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.isSecondarySpirit = true;
                    c.SecondarySpirit = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Guardian);
                })
                .SetFlags(BlueprintBuff.Flags.RemoveOnRest)
                .Configure();

            var hb = BuffConfigurator.New(FeatName + "BuffHierophant", Guids.MediumChannelSpiritSecondarySpiritBuffHierophant)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.isSecondarySpirit = true;
                    c.SecondarySpirit = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Hierophant);
                })
                .SetFlags(BlueprintBuff.Flags.RemoveOnRest)
                .Configure();

            var mb = BuffConfigurator.New(FeatName + "BuffMarshal", Guids.MediumChannelSpiritSecondarySpiritBuffMarshal)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.isSecondarySpirit = true;
                    c.SecondarySpirit = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Marshal);
                })
                .SetFlags(BlueprintBuff.Flags.RemoveOnRest)
                .Configure();

            var tb = BuffConfigurator.New(FeatName + "BuffTrickster", Guids.MediumChannelSpiritSecondarySpiritBuffTrickster)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.isSecondarySpirit = true;
                    c.SecondarySpirit = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Trickster);
                })
                .SetFlags(BlueprintBuff.Flags.RemoveOnRest)
                .Configure();




            var a = ActivatableAbilityConfigurator.New(FeatName + "AbilityArchmage", Guids.AstralBeaconAbilityArchmage)
                .SetIcon(IconPrefix + ArchmageIconName)
                .AddComponent<AbilityRequirementHasSpirit>()
                .SetBuff(ab)
                .Configure();
            var c = ActivatableAbilityConfigurator.New(FeatName + "AbilityChampion", Guids.AstralBeaconAbilityChampion)
                .SetIcon(IconPrefix + ChampionIconName)
                .AddComponent<AbilityRequirementHasSpirit>()
                .SetBuff(cb)
                .Configure();
            var g = ActivatableAbilityConfigurator.New(FeatName + "AbilityGuardian", Guids.AstralBeaconAbilityGuardian)
                .SetIcon(IconPrefix + GuardianIconName)
                .AddComponent<AbilityRequirementHasSpirit>()
                .SetBuff(gb)
                .Configure();
            var h = ActivatableAbilityConfigurator.New(FeatName + "AbilityHierophant", Guids.AstralBeaconAbilityHierophant)
                .SetIcon(IconPrefix + HierophantIconName)
                .AddComponent<AbilityRequirementHasSpirit>()
                .SetBuff(hb)
                .Configure();
            var m = ActivatableAbilityConfigurator.New(FeatName + "AbilityMarshal", Guids.AstralBeaconAbilityMarshal)
                .SetIcon(IconPrefix + MarshalIconName)
                .AddComponent<AbilityRequirementHasSpirit>()
                .SetBuff(mb)
                .Configure();
            var t = ActivatableAbilityConfigurator.New(FeatName + "AbilityTrickster", Guids.AstralBeaconAbilityTrickster)
                .SetIcon(IconPrefix + TricksterIconName)
                .AddComponent<AbilityRequirementHasSpirit>()
                .SetBuff(tb)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.AstralBeacon)
                .AddFacts(new() { a, c, g, h, m, t })
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
