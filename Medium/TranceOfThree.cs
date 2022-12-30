﻿using BlueprintCore.Actions.Builder;
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
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(TranceOfThree));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Medium Trance of Three");

            #region Archmage
            BlueprintBuff archmagebuff = BuffConfigurator.New(FeatName + "ArchmageBuff", Guids.MediumTranceOfThreeArchmageBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                //.AddFacts(new() { Guids.ArchmageSuddenAttack })
                .Configure();

            BlueprintAbility archmageability = AbilityConfigurator.New(FeatName + "ArchmageAbility", Guids.MediumTranceOfThreeArchmageAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuff(archmagebuff, ContextDuration.Variable(ContextValues.Rank()), toCaster: true))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }))
                .Configure();
            #endregion

            #region Champion
            BlueprintBuff championbuff = BuffConfigurator.New(FeatName + "ChampionBuff", Guids.MediumTranceOfThreeChampionBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                .AddFacts(new() { Guids.ChampionSuddenAttack })
                .Configure();

            BlueprintAbility championability = AbilityConfigurator.New(FeatName + "ChampionAbility", Guids.MediumTranceOfThreeChampionAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuff(championbuff, ContextDuration.Variable(ContextValues.Rank()), toCaster: true))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }))
                .Configure();
            #endregion

            #region Guardian
            BlueprintBuff guardianbuff = BuffConfigurator.New(FeatName + "GuardianBuff", Guids.MediumTranceOfThreeGuardianBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                .AddFacts(new() { Guids.GuardianAbsorbBlow })
                .Configure();

            BlueprintAbility guardianability = AbilityConfigurator.New(FeatName + "GuardianAbility", Guids.MediumTranceOfThreeGuardianAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuff(guardianbuff, ContextDuration.Variable(ContextValues.Rank()), toCaster: true))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }))
                .Configure();
            #endregion

            #region Hierophant
            BlueprintBuff hierophantbuff = BuffConfigurator.New(FeatName + "HierophantBuff", Guids.MediumTranceOfThreeHierophantBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                .AddFacts(new() { Guids.HierophantEnergyFont })
                .Configure();

            BlueprintAbility hierophantability = AbilityConfigurator.New(FeatName + "HierophantAbility", Guids.MediumTranceOfThreeHierophantAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuff(hierophantbuff, ContextDuration.Variable(ContextValues.Rank()), toCaster: true))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }))
                .Configure();
            #endregion

            #region Marshal
            BlueprintBuff marshalbuff = BuffConfigurator.New(FeatName + "MarshalBuff", Guids.MediumTranceOfThreeMarshalBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                .AddFacts(new() { Guids.MarshalInspiringCallStandard })
                //TODO: add something here that lets me add marshal spirit bonus!
                .Configure();

            BlueprintAbility marshalability = AbilityConfigurator.New(FeatName + "MarshalAbility", Guids.MediumTranceOfThreeMarshalAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuff(marshalbuff, ContextDuration.Variable(ContextValues.Rank()), toCaster: true))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }))
                .Configure();
            #endregion

            #region Trickster
            BlueprintBuff tricksterbuff = BuffConfigurator.New(FeatName + "TricksterBuff", Guids.MediumTranceOfThreeTricksterBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                .AddFacts(new() { Guids.TricksterSurpriseStrike, Guids.TricksterSurpriseStrike, Guids.TricksterSurpriseStrike, Guids.TricksterSurpriseStrike, Guids.TricksterSurpriseStrike })
                // TODO: Add something that adds enough trickster surprise strikes to character level...
                .Configure();

            BlueprintAbility tricksterability = AbilityConfigurator.New(FeatName + "TricksterAbility", Guids.MediumTranceOfThreeTricksterAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuff(tricksterbuff, ContextDuration.Variable(ContextValues.Rank()), toCaster: true))
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }))
                .Configure();
            #endregion


            BlueprintAbility ability = AbilityConfigurator.New(FeatName + "Ability", Guids.MediumTranceOfThreeAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
                .AddAbilityVariants(new() { archmageability, championability, guardianability, hierophantability, marshalability, tricksterability })
                .Configure();
            
            BlueprintFeature feature = FeatureConfigurator.New(FeatName, Guids.MediumTranceOfThree)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EuphoricTranquility.Reference.Get().Icon)
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