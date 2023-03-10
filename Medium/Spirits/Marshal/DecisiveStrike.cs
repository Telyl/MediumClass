using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Medium.NewActions;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Marshal
{
    class DecisiveStrike
    {
        private static readonly string FeatName = "MarshalGreater";
        private static readonly string DisplayNameFeat = "MarshalGreater.Name";
        private static readonly string DescriptionFeat = "MarshalGreater.Description";
        private static readonly string DisplayName = "MarshalGreaterStandard.Name";
        private static readonly string Description = "MarshalGreaterStandard.Description";
        private static readonly string DisplayNameSwift = "MarshalGreaterSwift.Name";
        private static readonly string DescriptionSwift = "MarshalGreaterSwift.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(DecisiveStrike));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Marshal Decisive Strike");

            var standardb = BuffConfigurator.New(FeatName + "StandardBuff", Guids.MarshalDecisiveStrikeStandardBuff)
                .SetIcon("assets/icons/decisivestrikestandard.png")
                .AddComponent<DecisiveStrikeStandardComponent>()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .Configure();

            var swiftability = AbilityConfigurator.New(FeatName + "SwiftAbility", Guids.MarshalDecisiveStrikeSwiftAbility)
                .SetIcon("assets/icons/decisivestrikeattack.png")
                .SetDisplayName(DisplayNameSwift)
                .SetDescription(DescriptionSwift)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .SetRange(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityRange.Medium)
                .SetType(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural)
                .SetCanTargetEnemies(false)
                .SetCanTargetFriends(true)
                .SetCanTargetSelf(false)
                .SetNotOffensive(true)
                .SetHasFastAnimation()
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Directional)
                .SetSpellResistance(false)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .Add<ContextActionSpiritInfluence>()
                        .Add<ContextActionDecisiveStrikeSwift>())
                .Configure();

            var standardability = AbilityConfigurator.New(FeatName + "StandardAbility", Guids.MarshalDecisiveStrikeStandardAbility)
                .SetIcon("assets/icons/decisivestrikestandard.png")
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                .SetRange(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityRange.Medium)
                .SetType(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural)
                .SetCanTargetEnemies(false)
                .SetCanTargetFriends(true)
                .SetCanTargetSelf(false)
                .SetNotOffensive(true)
                .SetHasFastAnimation()
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Directional)
                .SetSpellResistance(false)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .Add<ContextActionSpiritInfluence>()
                        .ApplyBuff(standardb, ContextDuration.Fixed(1, rate: DurationRate.Rounds),
                                    asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();

            var ability = AbilityConfigurator.New(FeatName + "Ability", Guids.MarshalDecisiveStrikeAbility)
                .SetIcon("assets/icons/decisivestrike.png")
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddAbilityVariants(new() { standardability, swiftability })
                .Configure();


            FeatureConfigurator.New(FeatName, Guids.MarshalDecisiveStrikeFeature)
                .SetDisplayName(DisplayNameFeat)
                .SetDescription(DescriptionFeat)
                .AddFacts(new() { ability })
                .Configure();

            var secondaryswiftability = AbilityConfigurator.New(FeatName + "SecondarySwiftAbility", Guids.SecondaryDecisiveStrikeSwiftAbility)
                .CopyFrom(swiftability, c => c is not AbilityResourceLogic)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResourceMarshal)
                .Configure();

            var secondarystandardability = AbilityConfigurator.New(FeatName + "SecondaryStandardAbility", Guids.SecondaryDecisiveStrikeStandardAbility)
                .CopyFrom(standardability, c => c is not AbilityResourceLogic)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResourceMarshal)
                .Configure();

            var secondaryability = AbilityConfigurator.New(FeatName + "SecondaryAbility", Guids.SecondaryDecisiveStrikeAbility)
                .SetIcon("assets/icons/decisivestrike.png")
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddAbilityVariants(new() { secondarystandardability, secondaryswiftability })
                .Configure();

            FeatureConfigurator.New(FeatName + "Secondary", Guids.SecondaryDecisiveStrikeFeature)
                .SetDisplayName(DisplayNameFeat)
                .SetDescription(DescriptionFeat)
                .AddFacts(new() { secondaryability })
                .AddRemoveFeatureOnApply(Guids.MarshalDecisiveStrikeFeature)
                .Configure();
        }
    }
}
