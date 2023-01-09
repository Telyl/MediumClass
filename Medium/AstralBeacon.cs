using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Medium.NewActions;
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
            AbilityResourceConfigurator.New(FeatName + "Archmage", Guids.MediumInfluenceResourceArchmage).Configure();
            AbilityResourceConfigurator.New(FeatName + "Champion", Guids.MediumInfluenceResourceChampion).Configure();
            AbilityResourceConfigurator.New(FeatName + "Guardian", Guids.MediumInfluenceResourceGuardian).Configure();
            AbilityResourceConfigurator.New(FeatName + "Hierophant", Guids.MediumInfluenceResourceHierophant).Configure();
            AbilityResourceConfigurator.New(FeatName + "Marshal", Guids.MediumInfluenceResourceMarshal).Configure();
            AbilityResourceConfigurator.New(FeatName + "Trickster", Guids.MediumInfluenceResourceTrickster).Configure();

            var buff = BuffConfigurator.New(FeatName + "Buff", Guids.AstralBeaconBuff)
                .Configure();

            var a = AbilityConfigurator.New(FeatName + "AbilityArchmage", Guids.AstralBeaconAbilityArchmage)
                .SetIcon(IconPrefix + ArchmageIconName)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResourceArchmage)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(buff, ContextDuration.Fixed(1, DurationRate.Rounds),
                                    asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();
            var c = AbilityConfigurator.New(FeatName + "AbilityChampion", Guids.AstralBeaconAbilityChampion)
                .SetIcon(IconPrefix + ChampionIconName)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResourceChampion)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(buff, ContextDuration.Fixed(1, DurationRate.Rounds),
                                    asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();
            var g = AbilityConfigurator.New(FeatName + "AbilityGuardian", Guids.AstralBeaconAbilityGuardian)
                .SetIcon(IconPrefix + GuardianIconName)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResourceGuardian)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(buff, ContextDuration.Fixed(1, DurationRate.Rounds),
                                    asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();
            var h = AbilityConfigurator.New(FeatName + "AbilityHierophant", Guids.AstralBeaconAbilityHierophant)
                .SetIcon(IconPrefix + HierophantIconName)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResourceHierophant)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(buff, ContextDuration.Fixed(1, DurationRate.Rounds),
                                    asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();
            var m = AbilityConfigurator.New(FeatName + "AbilityMarshal", Guids.AstralBeaconAbilityMarshal)
                .SetIcon(IconPrefix + MarshalIconName)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResourceMarshal)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(buff, ContextDuration.Fixed(1, DurationRate.Rounds),
                                    asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();
            var t = AbilityConfigurator.New(FeatName + "AbilityTrickster", Guids.AstralBeaconAbilityTrickster)
                .SetIcon(IconPrefix + TricksterIconName)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResourceTrickster)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(buff, ContextDuration.Fixed(1, DurationRate.Rounds),
                                    asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();

            AbilityConfigurator.New(FeatName + "Ability", Guids.AstralBeaconAbility)
                .AddAbilityVariants(new() { a,c,g,h,m,t })
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.AstralBeacon)
                .Configure();
        }
    }
}
