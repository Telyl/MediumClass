using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
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
    class SpiritMastery
    {
        private static readonly string FeatName = "MediumSpiritMastery";
        private static readonly string DisplayName = "MediumSpiritMastery.Name";
        private static readonly string Description = "MediumSpiritMastery.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SpiritMastery));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Medium Propitation");
            BlueprintFeature SpiritMastery = FeatureConfigurator.New(FeatName, Guids.MediumSpiritMastery)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetHideInUI(false)
                .SetHideNotAvailibleInUI(false)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .SetRanks(10)
                .SetAllowNonContextActions(false)
                .AddComponent<MediumSpiritMasteryComponent>()
                .Configure();
        }
    }
}
