using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.UnitLogic.Abilities.Components;
using MediumClass.Medium.NewComponents;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Champion
{
    class LegendaryChampion
    {
        private static readonly string FeatName = "LegendaryChampion";
        private static readonly string DisplayName = "LegendaryChampion.Name";
        private static readonly string Description = "LegendaryChampion.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(LegendaryChampion));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Champion Legendary Champion");
            
            BlueprintAbilityResource LegendaryChampionResource = AbilityResourceConfigurator.New(FeatName + "Resource", Guids.LegendaryChampionResource)
                .SetMaxAmount(ResourceAmountBuilder.New(2).Build())
                .Configure();

            var abil = AbilityConfigurator.New(FeatName + "Ability", Guids.LegendaryChampionAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.LegendaryChampion)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .Configure();
        }
    }
}
