using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Feint
{
    class Feint
    {
        private static readonly string FeatName = "Feint";
        private static readonly string DisplayName = "Feint.Name";
        private static readonly string Description = "Feint.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Feint));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Marshal Decisive Strike");

            var buff = BuffConfigurator.New(FeatName + "Buff", Guids.FeintBuff)
                .Configure();

            AbilityConfigurator.New(FeatName + "Ability", Guids.FeintAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.FalseGrace.Reference.Get().Icon)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                .SetRange(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityRange.Close)
                .SetCanTargetEnemies(true)
                .SetCanTargetFriends(true)
                .SetCanTargetSelf(false)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().Add<ContextActionFeintCheck>(c => {
                    c.Success = ActionsBuilder.New().ApplyBuff(buff, ContextDuration.Fixed(1, DurationRate.Rounds)).Build();
                }))
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.Feint, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { Guids.FeintAbility })
                .Configure();
        }
    }
}
