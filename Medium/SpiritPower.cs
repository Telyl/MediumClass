    using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Components;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

// This will be a buff.
namespace MediumClass.Medium
{
    class SpiritPower
    {
        private static readonly string FeatName = "SpiritPower";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SpiritPower));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Medium Spirit Powers.");
            BlueprintFeature SpiritPowerLesser = FeatureConfigurator.New(FeatName + "Lesser", Guids.SpiritPowerLesser)
                .SetDisplayName("SpiritPowerLesser.Name")
                .SetDescription("SpiritPowerLesser.Description")
                .Configure();
            BlueprintFeature SpiritPowerIntermediate = FeatureConfigurator.New(FeatName + "Intermediate", Guids.SpiritPowerIntermediate)
                .SetDisplayName("SpiritPowerIntermediate.Name")
                .SetDescription("SpiritPowerIntermediate.Description")
                .Configure();
            BlueprintFeature SpiritPowerGreater = FeatureConfigurator.New(FeatName + "Greater", Guids.SpiritPowerGreater)
                .SetDisplayName("SpiritPowerGreater.Name")
                .SetDescription("SpiritPowerGreater.Description")
                .Configure();
            BlueprintFeature SpiritPowerSupreme = FeatureConfigurator.New(FeatName + "Supreme", Guids.SpiritPowerSupreme)
                .SetDisplayName("SpiritPowerSupreme.Name")
                .SetDescription("SpiritPowerSupreme.Description")
                .Configure();
        }
    }
}
