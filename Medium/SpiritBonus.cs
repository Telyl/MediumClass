using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
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
    class SpiritBonus
    {
        private static readonly string FeatName = "MediumSpiritBonus";
        private static readonly string DisplayName = "MediumSpiritBonus.Name";
        private static readonly string Description = "MediumSpiritBonus.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SpiritBonus));
        /// <summary>
        /// This feat is only used for the purposes of displaying to the character. The Spirit Bonuses
        /// are generated for each spirit individually.
        /// </summary>
        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Medium Spirit Bonus");

            BlueprintBuff buff = BuffConfigurator.New(FeatName + "Buff", Guids.MediumSpiritBonusBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/tranceofthree.png")
                .AddComponent<MediumContextStatBonusComponent>()
                .Configure();

            BlueprintFeature SpiritBonus = FeatureConfigurator.New(FeatName, Guids.MediumSpiritBonus)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetHideInUI(false)
                .SetHideNotAvailibleInUI(false)
                .SetIsClassFeature(true)
                .SetRanks(10)
                .SetAllowNonContextActions(false)
                .AddFactsChangeTrigger(checkedFacts: new() { BlueprintTool.GetRef<BlueprintUnitFactReference>(Guids.MediumChannelSpiritPrimarySpiritBuff) }, 
                    onFactGainedActions: ActionsBuilder.New().ApplyBuffPermanent(buff),
                    onFactLostActions: ActionsBuilder.New().RemoveBuff(buff))
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
        }
    }
}
