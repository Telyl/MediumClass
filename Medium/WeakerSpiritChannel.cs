using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
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
    class WeakerSpiritChannel
    {
        private static readonly string FeatName = "WeakerSpiritChannel";
        private static readonly string DisplayName = "WeakerSpiritChannel.Name";
        private static readonly string Description = "WeakerSpiritChannel.Description";
        private static readonly string DisplayName1 = "WeakerSpiritChannel1.Name";
        private static readonly string DisplayName2 = "WeakerSpiritChannel2.Name";
        private static readonly string DisplayName3 = "WeakerSpiritChannel3.Name";
        private static readonly string DisplayName4 = "WeakerSpiritChannel4.Name";

        private static readonly ModLogger Logger = Logging.GetLogger(nameof(WeakerSpiritChannel));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Medium WeakerSpiritChannel");

            BlueprintBuff buff = BuffConfigurator.New(FeatName + "Buff", Guids.WeakerSpiritChannelBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.SpiderSwarmDamageAbility.Reference.Get().Icon)
                .AddComponent<MediumWeakerSpiritComponent>()
                .SetFlags(BlueprintBuff.Flags.RemoveOnRest)
                .Configure();

            BlueprintAbility WeakerSpiritAbilitiesSupreme = AbilityConfigurator.New(FeatName + "FourAbility", Guids.WeakerSpiritChannelFourAbility)
                .SetDisplayName(DisplayName4)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.SpiderSwarmDamageAbility.Reference.Get().Icon)
                .AddComponent<AbilityRequirementSpiritPowerRank>()
                .AddComponent<AbilityRequirementHasNoSpirit>()
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuffPermanent(buff, asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();

            BlueprintAbility WeakerSpiritAbilitiesGreater = AbilityConfigurator.New(FeatName + "ThreeAbility", Guids.WeakerSpiritChannelThreeAbility)
                .SetDisplayName(DisplayName3)
                .SetDescription(Description)
                .AddComponent<AbilityRequirementSpiritPowerRank>()
                .AddComponent<AbilityRequirementHasNoSpirit>()
                .SetIcon(AbilityRefs.SpiderSwarmDamageAbility.Reference.Get().Icon)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuffPermanent(buff, asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();

            BlueprintAbility WeakerSpiritAbilitiesIntermediate = AbilityConfigurator.New(FeatName + "TwoAbility", Guids.WeakerSpiritChannelTwoAbility)
                .SetDisplayName(DisplayName2)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.SpiderSwarmDamageAbility.Reference.Get().Icon)
                .AddComponent<AbilityRequirementSpiritPowerRank>()
                .AddComponent<AbilityRequirementHasNoSpirit>()
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuffPermanent(buff, asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();

            BlueprintAbility WeakerSpiritAbilitiesLesser = AbilityConfigurator.New(FeatName + "OneAbility", Guids.WeakerSpiritChannelOneAbility)
                .SetDisplayName(DisplayName1)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.SpiderSwarmDamageAbility.Reference.Get().Icon)
                .AddComponent<AbilityRequirementSpiritPowerRank>()
                .AddComponent<AbilityRequirementHasNoSpirit>()
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuffPermanent(buff,asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();

            BlueprintAbility WeakerSpiritAbilities = AbilityConfigurator.New(FeatName + "Abilities", Guids.WeakerSpiritChannelAbilities)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.SpiderSwarmDamageAbility.Reference.Get().Icon)
                .AddComponent<AbilityRequirementHasNoSpirit>()
                .AddAbilityVariants(variants: new() { WeakerSpiritAbilitiesLesser, WeakerSpiritAbilitiesIntermediate, WeakerSpiritAbilitiesGreater, WeakerSpiritAbilitiesSupreme })
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.WeakerSpiritChannel)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetHideInUI(false)
                .SetHideNotAvailibleInUI(false)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .SetRanks(10)
                .AddFacts(new() { WeakerSpiritAbilities })
                .SetAllowNonContextActions(false)
                .Configure();
        }
    }
}
