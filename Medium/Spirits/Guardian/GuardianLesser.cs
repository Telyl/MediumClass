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
    class GuardianLesser
    {
        private static readonly string FeatName = "GuardianLesser";
        private static readonly string DisplayName = "GuardianLesser.Name";
        private static readonly string Description = "GuardianLesser.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(GuardianLesser));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Guardian Lesser Spirit Power");

            FeatureConfigurator.New(FeatName, Guids.GuardianLesser)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() {
                    FeatureRefs.HeavyArmorProficiency.Reference.Get(),
                    FeatureRefs.ShieldsProficiency.Reference.Get(),
                    FeatureRefs.TowerShieldProficiency.Reference.Get()
                })
                .Configure();
        }
    }
}
