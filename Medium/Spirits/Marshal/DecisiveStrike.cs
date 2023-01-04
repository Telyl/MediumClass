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
                .SetIcon(AbilityRefs.AssassinCreatePoisonAbility.Reference.Get().Icon)
                .AddComponent<DecisiveStrikeStandardComponent>()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .Configure();

            var swiftability = AbilityConfigurator.New(FeatName + "SwiftAbility", Guids.MarshalDecisiveStrikeSwiftAbility)
                .SetIcon(AbilityRefs.AssassinCreatePoisonAbilitySwift.Reference.Get().Icon)
                .SetDisplayName(DisplayNameSwift)
                .SetDescription(DescriptionSwift)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .SetRange(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityRange.Medium)
                .SetType(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural)
                .SetCanTargetEnemies(false)
                .SetCanTargetFriends(true)
                .SetCanTargetSelf(true)
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
                .SetIcon(AbilityRefs.AssassinCreatePoisonAbility.Reference.Get().Icon)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                .SetRange(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityRange.Medium)
                .SetType(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural)
                .SetCanTargetEnemies(false)
                .SetCanTargetFriends(true)
                .SetCanTargetSelf(true)
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

            FeatureConfigurator.New(FeatName, Guids.MarshalDecisiveStrikeFeature)
                .SetDisplayName(DisplayNameFeat)
                .SetDescription(DescriptionFeat)
                .AddFacts(new() { swiftability, standardability })
                .Configure();
        }
    }
}
