using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Guardian
{
    class LegendaryGuardian
    {
        private static readonly string FeatName = "LegendaryGuardian";
        private static readonly string DisplayName = "LegendaryGuardian.Name";
        private static readonly string Description = "LegendaryGuardian.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(LegendaryGuardian));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Legendary Guardian");

            var buff = BuffConfigurator.New(FeatName + "Buff", Guids.LegendaryGuardianBuff)
                .CopyFrom(BuffRefs.WalkingDeadImmortalityBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.LegendaryGuardian)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddKeepAlliesAlive(maxAttacksCount: new ContextValue()
                {
                    m_AbilityParameter = AbilityParameterType.Level,
                    Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.StatCharisma,
                    ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Damage,
                    ValueRank = Kingmaker.Enums.AbilityRankType.Default,
                    ValueType = ContextValueType.CasterProperty,
                    Value = 0
                },walkingDeadBuff: buff)
                
                .Configure();
        }
    }
}
