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
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Medium.NewActions;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Marshal
{
    class MarshalsOrders
    {
        private static readonly string FeatName = "MarshalLesser";
        private static readonly string DisplayName = "MarshalLesser.Name";
        private static readonly string Description = "MarshalLesser.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(MarshalsOrders));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Marshal Marshal's Order");

            var buff = BuffConfigurator.For(Guids.SpiritSurgeBuff).Configure();

            var ability = AbilityConfigurator.New(FeatName + "Ability", Guids.MarshalMarshalsOrdersAbility)
                .SetIcon("assets/icons/marshalsorder.png")
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
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
                        .ApplyBuff(buff, ContextDuration.Fixed(1, rate: DurationRate.Minutes),
                                    asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.MarshalMarshalsOrders)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { ability })
                .Configure();
        }
    }
}
