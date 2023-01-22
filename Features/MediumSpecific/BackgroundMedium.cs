using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using MediumClass.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediumClass.Features.MediumSpecific
{
    class BackgroundMedium
    {
        private static readonly string FeatName = "BackgroundMedium";
        internal const string DisplayName = "BackgroundMedium.Name";
        private static readonly string Description = "BackgroundMedium.Description";

        public static void ConfigureEnabled()
        {
            FeatureConfigurator.New(FeatName, Guids.BackgroundMedium, FeatureGroup.Trait, FeatureGroup.BackgroundSelection)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureOnClassLevel(clazz: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Medium), level: 6, feature: BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MediumSpiritBonus), beforeThisLevel: false)
                .AddFeatureOnClassLevel(clazz: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Medium), level: 12, feature: BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MediumSpiritBonus), beforeThisLevel: false)
                .AddFeatureOnClassLevel(clazz: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Medium), level: 18, feature: BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MediumSpiritBonus), beforeThisLevel: false)
                .Configure();
        }
        
    }
}
