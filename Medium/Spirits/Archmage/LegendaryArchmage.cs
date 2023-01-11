using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using MediumClass.Medium.NewComponents;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.NewComponents.AbilitySpecific;
using TabletopTweaks.Core.Utilities;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Archmage
{
    class LegendaryArchmage
    {
        private static readonly string FeatName = "LegendaryArchmage";
        private static readonly string DisplayName = "LegendaryArchmage.Name";
        private static readonly string Description = "LegendaryArchmage.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(LegendaryArchmage));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Archmage Supreme Power");

            AbilityConfigurator.New(FeatName + "Ability9", Guids.ArchmageSupremeAbility9)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/legendaryarchmage9.png")
                .Configure();

            AbilityConfigurator.New(FeatName + "Ability8", Guids.ArchmageSupremeAbility8)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/legendaryarchmage8.png")
                .Configure();

            AbilityConfigurator.New(FeatName + "Ability7", Guids.ArchmageSupremeAbility7)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/legendaryarchmage7.png")
                .Configure();

            AbilityResourceConfigurator.New(FeatName + "Resource", Guids.ArchmageSupremeResource)
                .SetMaxAmount(ResourceAmountBuilder.New(1).Build())
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.ArchmageSupreme)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { Guids.ArchmageSupremeAbility7, Guids.ArchmageSupremeAbility8, Guids.ArchmageSupremeAbility9 })
                .AddAbilityResources(amount: 0, resource: Guids.ArchmageSupremeResource, restoreAmount: true, false, false)
                .SetRanks(1)
                .Configure();
        }
    }
}
