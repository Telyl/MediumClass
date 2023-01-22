using BlueprintCore.Blueprints.Components.Replacements;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kingmaker.UnitLogic.Mechanics.Components.OutcomingDamageAndHealingModifier;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Features
{
    class DesnaDivineFightingTechnique
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(DesnaDivineFightingTechnique));
        private static readonly string FeatName = "DesnaDivineFightingTechnique";
        private static readonly string DisplayName = "DesnaDivineFightingTechnique.Name";
        private static readonly string Description = "DesnaDivineFightingTechnique.Description";

        public static void ConfigureEnabled()
        {
            FeatureConfigurator.New(FeatName, Guids.DesnaDivineFightingTechnique, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddPrerequisitePlayerHasFeature(FeatureRefs.DesnaFeature.Reference.Get())
                .AddAttackStatReplacementFixed(new AttackStatReplacementFixed(Kingmaker.EntitySystem.Stats.StatType.Charisma, weaponTypes: BlueprintTool.GetRef<BlueprintWeaponTypeReference>("5a939137fc039084580725b2b0845c3f")))
                .AddWeaponTypeDamageStatReplacement(Kingmaker.Enums.WeaponCategory.Starknife, false, Kingmaker.EntitySystem.Stats.StatType.Charisma, false)
                .Configure();
        }
    }
}
