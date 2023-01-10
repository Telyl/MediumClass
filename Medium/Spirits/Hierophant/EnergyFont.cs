using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Components;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Hierophant
{
    class EnergyFont
    {
        private static readonly string FeatName = "HierophantEnergyFont";
        private static readonly string DisplayName = "HierophantEnergyFont.Name";
        private static readonly string Description = "HierophantEnergyFont.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(EnergyFont));

        public static void ConfigureEnabled()
        { 
        // The resource for our channel energy, because we're fancy.
            var resource = AbilityResourceConfigurator.New(FeatName + "Resource", Guids.HierophantEnergyFontResource)
                .SetMaxAmount(new BlueprintAbilityResource.Amount
                {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma
                })
                .SetMax(10)
                .SetUseMax(false)
                .Configure();


        var posability = AbilityConfigurator.New(FeatName + "PositiveHeal", Guids.HierophantEnergyFontPositiveHeal)
            .SetDisplayName(DisplayName)
            .SetDescription(Description)
            .AddAbilityResourceLogic(amount: 1, requiredResource: Guids.HierophantEnergyFontResource, isSpendResource: true, costIsCustom: false)
            .CopyFrom(AbilityRefs.ChannelEnergy, c => c is not (ContextRankConfig or AbilityResourceLogic))
            .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }, type: default, max: 20, min: 0).WithDiv2Progression())
            .AddContextRankConfig(ContextRankConfigs.CustomProperty(type: AbilityRankType.DamageBonus, property: UnitPropertyRefs.MythicChannelProperty.ToString(), max: 20, min: 0))
            .Configure();

        var posharmability = AbilityConfigurator.New(FeatName + "PositiveHarm", Guids.HierophantEnergyFontPositiveHarm)
            .SetDisplayName(DisplayName)
            .SetDescription(Description)
            .AddAbilityResourceLogic(amount: 1, requiredResource: Guids.HierophantEnergyFontResource, isSpendResource: true, costIsCustom: false)
            .CopyFrom(AbilityRefs.ChannelPositiveHarm, c => c is not (ContextRankConfig or AbilityResourceLogic))
            .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }, type: default, max: 20, min: 0).WithDiv2Progression())
            .AddContextRankConfig(ContextRankConfigs.CustomProperty(type: AbilityRankType.DamageBonus, property: UnitPropertyRefs.MythicChannelProperty.ToString(), max: 20, min: 0))
            .Configure();

        var negability = AbilityConfigurator.New(FeatName + "NegativeHarm", Guids.HierophantEnergyFontNegativeHarm)
            .SetDisplayName(DisplayName)
            .SetDescription(Description)
            .AddAbilityResourceLogic(amount: 1, requiredResource: Guids.HierophantEnergyFontResource, isSpendResource: true, costIsCustom: false)
            .CopyFrom(AbilityRefs.ChannelNegativeEnergy, c => c is not (ContextRankConfig or AbilityResourceLogic))
            .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }, type: default, max: 20, min: 0).WithDiv2Progression())
            .AddContextRankConfig(ContextRankConfigs.CustomProperty(type: AbilityRankType.DamageBonus, property: UnitPropertyRefs.MythicChannelProperty.ToString(), max: 20, min: 0))
            .Configure();

        var neghealability = AbilityConfigurator.New(FeatName + "NegativeHeal", Guids.HierophantEnergyFontNegativeHeal)
            .SetDisplayName(DisplayName)
            .SetDescription(Description)
            .AddAbilityResourceLogic(amount: 1, requiredResource: Guids.HierophantEnergyFontResource, isSpendResource: true, costIsCustom: false)
            .CopyFrom(AbilityRefs.ChannelNegativeHeal, c => c is not (ContextRankConfig or AbilityResourceLogic))
            .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { Guids.Medium }, type: default, max: 20, min: 0).WithDiv2Progression())
            .AddContextRankConfig(ContextRankConfigs.CustomProperty(type: AbilityRankType.DamageBonus, property: UnitPropertyRefs.MythicChannelProperty.ToString(), max: 20, min: 0))
            .Configure();

        FeatureConfigurator.New(FeatName, Guids.HierophantEnergyFont)
            .SetDisplayName(DisplayName)
            .SetDescription(Description)
            .AddToIsPrerequisiteFor(FeatureRefs.SelectiveChannel.Reference.Get(), FeatureRefs.MythicChannelMythicFeat.Reference.Get())
            .AddSpellKnownTemporary(Guids.Medium, 1, true, AbilityRefs.CureLightWounds.Reference.Get())
            .AddSpellKnownTemporary(Guids.Medium, 1, true, AbilityRefs.InflictLightWounds.Reference.Get())
            .AddSpellKnownTemporary(Guids.Medium, 2, true, AbilityRefs.CureModerateWounds.Reference.Get())
            .AddSpellKnownTemporary(Guids.Medium, 2, true, AbilityRefs.InflictModerateWounds.Reference.Get())
            .AddSpellKnownTemporary(Guids.Medium, 3, true, AbilityRefs.CureSeriousWounds.Reference.Get())
            .AddSpellKnownTemporary(Guids.Medium, 3, true, AbilityRefs.InflictSeriousWounds.Reference.Get())
            .AddSpellKnownTemporary(Guids.Medium, 4, true, AbilityRefs.CureCriticalWounds.Reference.Get())
            .AddSpellKnownTemporary(Guids.Medium, 4, true, AbilityRefs.InflictCriticalWounds.Reference.Get())
            .AddFacts(new() { posability, posharmability, negability, neghealability })
            .Configure();


        // We want the player to be able to select this regardless of if they have the Hierophant channeled or not.
        FeatureConfigurator.For(FeatureRefs.SelectiveChannel)
            .AddPrerequisiteFeature(Guids.MediumChannelSpirit, group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any)
            .Configure();
        }
    }
}
