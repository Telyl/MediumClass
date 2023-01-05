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
    class LegendaryMarshal
    {
        private static readonly string FeatName = "MarshalSupreme";
        private static readonly string DisplayName = "MarshalSupreme.Name";
        private static readonly string Description = "MarshalSupreme.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(LegendaryMarshal));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Marshal Legendary Marshal");

            var buff = BuffConfigurator.For(Guids.SpiritSurgeBuff).Configure();

            var ability = AbilityConfigurator.New(FeatName + "Ability", Guids.MarshalLegendaryMarshalAbility)
                .SetIcon("assets/icons/legendarymarshal.png")
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
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(buff, ContextDuration.Fixed(1, rate: DurationRate.Minutes),
                                    asChild: false, isFromSpell: false, isNotDispelable: true))
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.MarshalLegendaryMarshal)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { ability })
                .Configure();
        }
    }
}
