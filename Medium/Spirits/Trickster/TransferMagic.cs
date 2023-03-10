using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.View.Animation;
using MediumClass.Medium.NewActions;
using MediumClass.Medium.NewComponents;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Trickster
{
    class TransferMagic
    {
        private static readonly string FeatName = "TricksterTransferMagic";
        private static readonly string DisplayName = "TricksterTransferMagic.Name";
        private static readonly string Description = "TricksterTransferMagic.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(TransferMagic));

        public static ActionList CreateActionList(params GameAction[] actions)
        {
            if (actions == null || actions.Length == 1 && actions[0] == null) actions = Array.Empty<GameAction>();
            return new ActionList() { Actions = actions };
        }

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Trickster Transfer Magic");
            var effect = AbilityConfigurator.New(FeatName + "Effect", Guids.TricksterTransferMagicEffect)
                .CopyFrom(AbilityRefs.ShockingGraspEffect, c => c is not (AbilityEffectRunAction or ContextRankConfig))
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().Add<ContextActionTransferMagic>())
                .SetIcon("assets/icons/transfermagic.png")
                .Configure();

            var ability = AbilityConfigurator.New(FeatName + "Ability", Guids.TricksterTransferMagicAbility)
                .CopyFrom(AbilityRefs.ShockingGraspCast, c => c is not (SpellListComponent or AbilityEffectStickyTouch or CraftInfoComponent))
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/transfermagic.png")
                .SetActionType(UnitCommand.CommandType.Standard)
                .SetCanTargetEnemies()
                .SetCanTargetSelf(false)
                .SetCanTargetPoint(false)
                .SetCanTargetFriends()
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().Add<ContextActionSpiritInfluence>())
                .AddAbilityEffectStickyTouch(touchDeliveryAbility: effect)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch)
                .Configure();
           
            FeatureConfigurator.New(FeatName, Guids.TricksterTransferMagic)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { ability })
                .Configure();

            var secondability = AbilityConfigurator.New(FeatName + "AbilitySecondary", Guids.SecondaryTricksterTransferMagicAbility)
                .CopyFrom(ability, c => c is not AbilityResourceLogic)
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResourceTrickster)
                .Configure();

            FeatureConfigurator.New(FeatName + "Secondary", Guids.SecondaryTricksterTransferMagic)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { secondability })
                .AddRemoveFeatureOnApply(Guids.TricksterTransferMagic)
                .Configure();
        }
    }
}
