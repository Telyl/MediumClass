using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Features.MediumSpecific
{
    class SpiritFocus
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SpiritFocus));
        private static readonly string FeatName = "SpiritFocus";
        private static readonly string DisplayName = "SpiritFocus.Name";
        private static readonly string Description = "SpiritFocus.Description";

        public static void ConfigureEnabled()
        {
            FeatureConfigurator.New(FeatName + "Archmage", Guids.SpiritFocusArchmage)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddComponent<MediumSpiritFocusComponent>()
                .Configure();

            FeatureConfigurator.New(FeatName + "Champion", Guids.SpiritFocusChampion)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddComponent<MediumSpiritFocusComponent>()
                .Configure();

            FeatureConfigurator.New(FeatName + "Guardian", Guids.SpiritFocusGuardian)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddComponent<MediumSpiritFocusComponent>()
                .Configure();

            FeatureConfigurator.New(FeatName + "Hierophant", Guids.SpiritFocusHierophant)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddComponent<MediumSpiritFocusComponent>()
                .Configure();

            FeatureConfigurator.New(FeatName + "Marshal", Guids.SpiritFocusMarshal)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddComponent<MediumSpiritFocusComponent>()
                .Configure();

            FeatureConfigurator.New(FeatName + "Trickster", Guids.SpiritFocusTrickster)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddComponent<MediumSpiritFocusComponent>()
                .Configure();


            FeatureSelectionConfigurator.New(FeatName + "Selection", Guids.SpiritFocusSelection)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetHideInUI(false)
                .SetHideNotAvailibleInUI(false)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .AddToAllFeatures(Guids.SpiritFocusArchmage, Guids.SpiritFocusChampion, Guids.SpiritFocusGuardian, Guids.SpiritFocusHierophant, Guids.SpiritFocusMarshal, Guids.SpiritFocusTrickster)
                .SetAllowNonContextActions(false)
                .Configure();
        }
    }
}
