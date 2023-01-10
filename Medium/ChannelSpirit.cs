using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Medium.NewActions;
using MediumClass.Medium.NewComponents;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.NewComponents;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.NewComponents;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium
{
    class ChannelSpirit
    {
        private static readonly string FeatName = "MediumChannelSpirit";
        private static readonly string DisplayName = "MediumChannelSpirit.Name";
        private static readonly string Description = "MediumChannelSpirit.Description";


        private const string IconPrefix = "assets/icons/";
        internal const string ArchmageName = "Archmage.Name";
        private static readonly string ArchmageDescription = "Archmage.Description";
        private const string ArchmageIconName = IconPrefix + "spiritarchmage2.png";

        internal const string ChampionName = "Champion.Name";
        private static readonly string ChampionDescription = "Champion.Description";
        private const string ChampionIconName = IconPrefix + "spiritchampion2.png";

        internal const string GuardianName = "Guardian.Name";
        private static readonly string GuardianDescription = "Guardian.Description";
        private const string GuardianIconName = IconPrefix + "spiritguardian2.png";

        internal const string HierophantName = "Hierophant.Name";
        private static readonly string HierophantDescription = "Hierophant.Description";
        private const string HierophantIconName = IconPrefix + "spirithierophant2.png";

        internal const string MarshalName = "Marshal.Name";
        private static readonly string MarshalDescription = "Marshal.Description";
        private const string MarshalIconName = IconPrefix + "spiritmarshal2.png";

        internal const string TricksterName = "Trickster.Name";
        private static readonly string TricksterDescription = "Trickster.Description";
        private const string TricksterIconName = IconPrefix + "spirittrickster2.png";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(ChannelSpirit));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Channel Spirit");

            
            var buff = BuffConfigurator.New(FeatName + "PrimarySpiritBuff", Guids.MediumChannelSpiritPrimarySpiritBuff)
                .AddComponent<ApplySpirit>()
                .SetFlags(BlueprintBuff.Flags.RemoveOnRest)
                .Configure();

            #region Archmage
            var a = AbilityConfigurator.New(FeatName + "AbilityArchmage", Guids.MediumChannelSpiritAbilityArchmage)
                .SetDisplayName(ArchmageName)
                .SetDescription(ArchmageDescription)
                .SetIcon(ArchmageIconName)
                .AddComponent<AbilityRequirementHasSpirit>(c => {
                    c.Not = true;
                })
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .Add<ContextActionSpiritInfluence>()
                        .Add<ContextActionApplySpirit>(c =>
                        {
                            c.Spirit = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Archmage);
                        }))
                .AddAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), amount: 1, isSpendResource: true)
                .Configure();
            #endregion
            #region Champion
            var c = AbilityConfigurator.New(FeatName + "AbilityChampion", Guids.MediumChannelSpiritAbilityChampion)
                .SetDisplayName(ChampionName)
                .SetDescription(ChampionDescription)
                .SetIcon(ChampionIconName)
                .AddComponent<AbilityRequirementHasSpirit>(c => {
                    c.Not = true;
                })
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .Add<ContextActionSpiritInfluence>()
                        .Add<ContextActionApplySpirit>(c =>
                        {
                            c.Spirit = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Champion);
                        }))
                .AddAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), amount: 1, isSpendResource: true)
                .Configure();
            #endregion
            #region Guardian
            var g = AbilityConfigurator.New(FeatName + "AbilityGuardian", Guids.MediumChannelSpiritAbilityGuardian)
                .SetDisplayName(GuardianName)
                .SetDescription(GuardianDescription)
                .SetIcon(GuardianIconName)
                .AddComponent<AbilityRequirementHasSpirit>(c => {
                    c.Not = true;
                })
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .Add<ContextActionSpiritInfluence>()
                        .Add<ContextActionApplySpirit>(c =>
                        {
                            c.Spirit = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Guardian);
                        }))
                .AddAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), amount: 1, isSpendResource: true)
                .Configure();
            #endregion
            #region Hierophant
            var h = AbilityConfigurator.New(FeatName + "AbilityHierophant", Guids.MediumChannelSpiritAbilityHierophant)
                .SetDisplayName(HierophantName)
                .SetDescription(HierophantDescription)
                .SetIcon(HierophantIconName)
                .AddComponent<AbilityRequirementHasSpirit>(c => {
                    c.Not = true;
                })
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .Add<ContextActionSpiritInfluence>()
                        .Add<ContextActionApplySpirit>(c =>
                        {
                            c.Spirit = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Hierophant);
                        }))
                .AddAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), amount: 1, isSpendResource: true)
                .Configure();
            #endregion
            #region Marshal
            var m = AbilityConfigurator.New(FeatName + "AbilityMarshal", Guids.MediumChannelSpiritAbilityMarshal)
                .SetDisplayName(MarshalName)
                .SetDescription(MarshalDescription)
                .SetIcon(MarshalIconName)
                .AddComponent<AbilityRequirementHasSpirit>(c => {
                    c.Not = true;
                })
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .Add<ContextActionSpiritInfluence>()
                        .Add<ContextActionApplySpirit>(c =>
                        {
                            c.Spirit = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Marshal);
                        }))
                .AddAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), amount: 1, isSpendResource: true)
                .Configure();
            #endregion
            #region Trickster
            var t = AbilityConfigurator.New(FeatName + "AbilityTrickster", Guids.MediumChannelSpiritAbilityTrickster)
                .SetDisplayName(TricksterName)
                .SetDescription(TricksterDescription)
                .SetIcon(TricksterIconName)
                .AddComponent<AbilityRequirementHasSpirit>(c => {
                    c.Not = true;
                })
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .Add<ContextActionSpiritInfluence>()
                        .Add<ContextActionApplySpirit>(c =>
                        {
                            c.Spirit = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Trickster);
                        }))
                .AddAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), amount: 1, isSpendResource: true)
                .Configure();
            #endregion

            FeatureConfigurator.New(FeatName, Guids.MediumChannelSpirit)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.ShamanWanderingHexAbility.Reference.Get().Icon)
                .AddComponent<MediumSpiritComponent>(c => {
                    c.SpiritClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Archmage);
                    c.SpiritInfluencePenalty = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.MediumInfluenceDebuff);
                    c.SpiritInfluence = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResourceArchmage);
                    c.SpiritBonusFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MediumSpiritBonus);
                    c.SpiritSeanceBoon = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ArchmageSeanceBoon);
                    c.SpiritLesserPower = new BlueprintFeatureReference();
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ArchmageIntermediate);
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ArchmageGreater);
                    c.SpiritSupremePower = new BlueprintFeatureReference();
                    c.Concentration = true;
                    c.Stats = new StatType[] { StatType.SkillKnowledgeArcana, StatType.SkillKnowledgeWorld };
                    c.Penalties = new StatType[] { StatType.AdditionalAttackBonus, StatType.AdditionalDamage, StatType.SaveFortitude, StatType.SkillAthletics };
                })
                .AddComponent<MediumSpiritComponent>(c => {
                    c.SpiritClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Champion);
                    c.SpiritInfluencePenalty = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.MediumInfluenceDebuff);
                    c.SpiritInfluence = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResourceChampion);
                    c.SpiritBonusFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MediumSpiritBonus);
                    c.SpiritSeanceBoon = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ChampionSeanceBoon);
                    c.SpiritLesserPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ChampionLesser);
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ChampionSuddenAttack);
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ChampionFleetCharge);
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.LegendaryChampion);
                    c.Stats = new StatType[] { StatType.SkillAthletics, StatType.SaveFortitude, StatType.AdditionalDamage, StatType.AdditionalAttackBonus };
                    c.Penalties = new StatType[] { StatType.SkillKnowledgeArcana, StatType.SkillKnowledgeWorld, StatType.BonusCasterLevel };
                })
                .AddComponent<MediumSpiritComponent>(c => {
                    c.SpiritClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Guardian);
                    c.SpiritInfluencePenalty = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.MediumInfluenceDebuff);
                    c.SpiritInfluence = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResourceGuardian);
                    c.SpiritBonusFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MediumSpiritBonus);
                    c.SpiritSeanceBoon = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.GuardianSeanceBoon);
                    c.SpiritLesserPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.GuardianLesser);
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.GuardianAbsorbBlow);
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.GuardianGreater);
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.LegendaryGuardian);
                    c.Stats = new StatType[] { StatType.AC, StatType.SaveReflex, StatType.SaveFortitude };
                    c.Penalties = new StatType[] { StatType.AdditionalDamage };
                })
                .AddComponent<MediumSpiritComponent>(c => {
                    c.SpiritClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Hierophant);
                    c.SpiritInfluencePenalty = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.MediumInfluenceDebuff);
                    c.SpiritInfluence = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResourceHierophant);
                    c.SpiritBonusFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MediumSpiritBonus);
                    c.SpiritSeanceBoon = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.HierophantSeanceBoon);
                    c.SpiritLesserPower = new BlueprintFeatureReference();
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.HierophantEnergyFont);
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.HierophantOverflowingGrace);
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.HierophantSupreme);
                    c.Stats = new StatType[] { StatType.SkillPerception, StatType.SkillLoreNature, StatType.SkillLoreReligion, StatType.SaveWill };
                    c.Penalties = new StatType[] { StatType.SkillUseMagicDevice, StatType.SkillPersuasion };
                })
                .AddComponent<MediumSpiritComponent>(c => {
                    c.SpiritClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Marshal);
                    c.SpiritInfluencePenalty = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.MediumInfluenceDebuff);
                    c.SpiritInfluence = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResourceMarshal);
                    c.SpiritBonusFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MediumSpiritBonus);
                    c.SpiritSeanceBoon = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalSeanceBoon);
                    c.SpiritLesserPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalMarshalsOrders);
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalInspiringCallStandard);
                    c.SpiritIntermediatePowerMove = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalInspiringCallMove);
                    c.SpiritIntermediatePowerSwift = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalInspiringCallSwift);
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalDecisiveStrikeFeature);
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalLegendaryMarshal);
                    c.Stats = new StatType[] { StatType.AdditionalAttackBonus }.Concat(StatTypeHelper.Saves).Concat(StatTypeHelper.Skills).ToArray();
                    c.Penalties = new StatType[] { StatType.SkillPerception, StatType.SkillLoreNature, StatType.SkillLoreReligion, StatType.SaveWill };
                    c.Concentration = true;
                })
                .AddComponent<MediumSpiritComponent>(c => {
                    c.SpiritClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Trickster);
                    c.SpiritInfluencePenalty = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.MediumInfluenceDebuff);
                    c.SpiritInfluence = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResourceTrickster);
                    c.SpiritBonusFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MediumSpiritBonus);
                    c.SpiritSeanceBoon = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterSeanceBoon);
                    c.SpiritLesserPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterEdge);
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterSurpriseStrike);
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterTransferMagic);
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterLegendaryTrickster);
                    c.Penalties = new StatType[] { StatType.AC, StatType.AdditionalCMB, StatType.AdditionalCMD };
                    c.Stats = new StatType[] { StatType.SkillAthletics, StatType.SkillKnowledgeArcana, StatType.SkillKnowledgeWorld, StatType.SkillLoreNature, StatType.SkillLoreReligion,
                        StatType.SkillMobility, StatType.SkillPerception, StatType.SkillPersuasion, StatType.SkillStealth, StatType.SkillThievery, StatType.SkillUseMagicDevice, StatType.SaveReflex, StatType.Initiative };
                })
                .AddFacts(new() { a, c, g, h, m, t })
                .Configure();
        }
    }
}
