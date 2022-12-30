using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
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

namespace MediumClass.Medium
{
    class Propitation
    {
        private static readonly string FeatName = "MediumPropitation";
        private static readonly string DisplayName = "MediumPropitation.Name";
        private static readonly string Description = "MediumPropitation.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Propitation));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Medium Propitation");
            BlueprintFeature Propitation = FeatureConfigurator.New(FeatName, Guids.MediumPropitation)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetHideInUI(false)
                .SetHideNotAvailibleInUI(false)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .SetRanks(10)
                .SetAllowNonContextActions(false)
                .AddIncreaseResourceAmount(BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), value: 1)
                .Configure();
        }
    }
}
