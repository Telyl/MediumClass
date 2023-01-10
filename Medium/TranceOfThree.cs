using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using MediumClass.Medium.NewActions;
using MediumClass.Medium.NewComponents;
using MediumClass.Medium.NewComponents.AbilitySpecific;
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
    class TranceOfThree
    {
        private static readonly string FeatName = "MediumTranceOfThree";
        private static readonly string DisplayName = "MediumTranceOfThree.Name";
        private static readonly string Description = "MediumTranceOfThree.Description";
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
            Logger.Log("Generating Medium Trance of Three");

            #region Archmage
            BlueprintBuff archmagebuff = BuffConfigurator.New(FeatName + "ArchmageBuff", Guids.MediumTranceOfThreeArchmageBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + ArchmageIconName)
                .AddFacts(new() { Guids.ArchmageIntermediate})
                .Configure();

            BlueprintAbility archmageability = AbilityConfigurator.New(FeatName + "ArchmageAbility", Guids.MediumTranceOfThreeArchmageAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + ArchmageIconName)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .AddComponent<AbilityRequirementHasSpirit>()
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().Add<ContextActionSpiritInfluence>().ApplyBuff(archmagebuff, ContextDuration.Variable(ContextValues.Rank()), toCaster: true))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }))
                .Configure();
            #endregion

            #region Champion
            BlueprintBuff championbuff = BuffConfigurator.New(FeatName + "ChampionBuff", Guids.MediumTranceOfThreeChampionBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + ChampionIconName)
                .AddFacts(new() { Guids.ChampionSuddenAttack })
                .Configure();

            BlueprintAbility championability = AbilityConfigurator.New(FeatName + "ChampionAbility", Guids.MediumTranceOfThreeChampionAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + ChampionIconName)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .AddComponent<AbilityRequirementHasSpirit>()
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().Add<ContextActionSpiritInfluence>().ApplyBuff(championbuff, ContextDuration.Variable(ContextValues.Rank()), toCaster: true))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }))
                .Configure();
            #endregion

            #region Guardian
            BlueprintBuff guardianbuff = BuffConfigurator.New(FeatName + "GuardianBuff", Guids.MediumTranceOfThreeGuardianBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + GuardianIconName)
                .AddFacts(new() { Guids.GuardianAbsorbBlow })
                .Configure();

            BlueprintAbility guardianability = AbilityConfigurator.New(FeatName + "GuardianAbility", Guids.MediumTranceOfThreeGuardianAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + GuardianIconName)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .AddComponent<AbilityRequirementHasSpirit>()
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().Add<ContextActionSpiritInfluence>().ApplyBuff(guardianbuff, ContextDuration.Variable(ContextValues.Rank()), toCaster: true))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }))
                .Configure();
            #endregion

            #region Hierophant
            BlueprintBuff hierophantbuff = BuffConfigurator.New(FeatName + "HierophantBuff", Guids.MediumTranceOfThreeHierophantBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + HierophantIconName)
                .AddFacts(new() { Guids.HierophantEnergyFont })
                .Configure();

            BlueprintAbility hierophantability = AbilityConfigurator.New(FeatName + "HierophantAbility", Guids.MediumTranceOfThreeHierophantAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + HierophantIconName)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .AddComponent<AbilityRequirementHasSpirit>()
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().Add<ContextActionSpiritInfluence>().ApplyBuff(hierophantbuff, ContextDuration.Variable(ContextValues.Rank()), toCaster: true))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }))
                .Configure();
            #endregion

            #region Marshal
            BlueprintBuff marshalbuff = BuffConfigurator.New(FeatName + "MarshalBuff", Guids.MediumTranceOfThreeMarshalBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + MarshalIconName)
                .AddFacts(new() { Guids.MarshalInspiringCallStandard })
                .AddComponent<MediumTranceOfThreeComponent>(c =>
                {
                    c.BP = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MediumSpiritBonus);
                })
                .Configure();

            BlueprintAbility marshalability = AbilityConfigurator.New(FeatName + "MarshalAbility", Guids.MediumTranceOfThreeMarshalAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + MarshalIconName)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .AddComponent<AbilityRequirementHasSpirit>()
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().Add<ContextActionSpiritInfluence>().ApplyBuff(marshalbuff, ContextDuration.Variable(ContextValues.Rank()), toCaster: true))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }))
                .Configure();
            #endregion

            #region Trickster
            BlueprintBuff tricksterbuff = BuffConfigurator.New(FeatName + "TricksterBuff", Guids.MediumTranceOfThreeTricksterBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + TricksterIconName)
                .AddComponent<MediumTranceOfThreeComponent>(c =>
                {
                    c.BP = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterSurpriseStrike);
                })
                .Configure();

            BlueprintAbility tricksterability = AbilityConfigurator.New(FeatName + "TricksterAbility", Guids.MediumTranceOfThreeTricksterAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + TricksterIconName)
                .AddComponent<AbilityRequirementHasSpirit>()
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().Add<ContextActionSpiritInfluence>().ApplyBuff(tricksterbuff, ContextDuration.Variable(ContextValues.Rank()), toCaster: true))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }))
                .Configure();
            #endregion


            BlueprintAbility ability = AbilityConfigurator.New(FeatName + "Ability", Guids.MediumTranceOfThreeAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + TranceOfThreeIconName)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .AddComponent<AbilityRequirementHasSpirit>()
                .AddAbilityVariants(new() { archmageability, championability, guardianability, hierophantability, marshalability, tricksterability })
                .Configure();
            
            BlueprintFeature feature = FeatureConfigurator.New(FeatName, Guids.MediumTranceOfThree)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(IconPrefix + TranceOfThreeIconName)
                .SetHideInUI(false)
                .SetHideNotAvailibleInUI(false)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .SetRanks(10)
                .AddFacts(new() { ability })
                .SetAllowNonContextActions(false)
                .Configure();
        }
    }
}
