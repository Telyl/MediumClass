using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.View.Animation;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Champion
{
    class FleetCharge
    {
        private static readonly string FeatName = "ChampionFleetCharge";
        private static readonly string DisplayName = "ChampionFleetCharge.Name";
        private static readonly string Description = "ChampionFleetCharge.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(FleetCharge));

        public static ActionList CreateActionList(params GameAction[] actions)
        {
            if (actions == null || actions.Length == 1 && actions[0] == null) actions = Array.Empty<GameAction>();
            return new ActionList() { Actions = actions };
        }

        public static void ConfigureEnabled()
        {
            var ability = AbilityConfigurator.New(FeatName + "Ability", Guids.ChampionFleetChargeAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetActionType(UnitCommand.CommandType.Standard)
                .SetCanTargetEnemies()
                .SetCanTargetSelf(false)
                .SetCanTargetPoint(false)
                .SetCanTargetFriends(false)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Special)
                .SetIcon("assets/icons/fleetcharge.png")
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .MeleeAttack(autoCritConfirmation: false, autoCritThreat: false, autoHit: false, extraAttack: false, fullAttack: true, ignoreStatBonus: false, selectNewTarget: false))
                .Configure();

            Logger.Log("Generating Champion Sudden Attack");
            FeatureConfigurator.New(FeatName, Guids.ChampionFleetCharge)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { ability })
                .Configure();
        }
    }
}
