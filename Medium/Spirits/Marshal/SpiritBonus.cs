
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Marshal
{
    class SpiritBonus
    {
        private static readonly string FeatName = "MarshalSpiritBonus";
        private static readonly string DisplayName = "MarshalSpiritBonus.Name";
        private static readonly string Description = "MarshalSpiritBonus.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SpiritBonus));

        public static BlueprintFeature ConfigureEnabled()
        {
            Logger.Log("Generating Marshal Spirit Bonus");
            BlueprintFeature SpiritBonus = FeatureConfigurator.New(FeatName, Guids.MarshalSpiritBonus)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetHideInUI(false)
                .SetHideNotAvailibleInUI(false)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .SetRanks(10)
                .AddContextStatBonus(StatType.SkillUseMagicDevice, value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                }, ModifierDescriptor.UntypedStackable)
                .AddContextStatBonus(StatType.SkillPersuasion, value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                }, ModifierDescriptor.UntypedStackable)
                .SetAllowNonContextActions(false)
                .Configure();

            FeatureConfigurator.For(SpiritBonus)
                .AddContextRankConfig(new ContextRankConfig
                {
                    m_Type = AbilityRankType.Default,
                    m_BaseValueType = ContextRankBaseValueType.FeatureRank,
                    m_Feature = SpiritBonus.ToReference<BlueprintFeatureReference>(),
                    m_Stat = StatType.Unknown,
                    m_Buff = null,
                    m_Progression = ContextRankProgression.AsIs,
                    m_StartLevel = 0,
                    m_StepLevel = 0,
                    m_UseMin = false,
                    m_Min = 0,
                    m_UseMax = false,
                    m_Max = 20,
                    m_ExceptClasses = false,
                    Archetype = null
                })
                .Configure();

            return SpiritBonus;
        }

        internal static T ToReference<T>()
        {
            throw new NotImplementedException();
        }
    }
}
