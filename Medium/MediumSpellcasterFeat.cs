using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediumClass.Medium
{
    class MediumSpellcasterFeat
    {
        private static readonly string FeatName = "MediumSpellcasterFeat";
        internal const string DisplayName = "MediumSpellcasterFeat.Name";
        private static readonly string Description = "MediumSpellcasterFeat.Description";

        public static void ConfigureEnabled()
        {
            FeatureConfigurator.New(FeatName, Guids.MediumSpellcasterFeat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddSpellbook(ContextValues.Rank(), spellbook: Guids.ArchmageSpellbook)
                .AddSpellbook(ContextValues.Rank(), spellbook: Guids.HierophantSpellbook)
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { BlueprintTool.Get<BlueprintCharacterClass>(Guids.Medium).ToString() }, false, Kingmaker.Enums.AbilityRankType.Default, 20, 1))
                .SetHideInUI(true)
                .SetHideNotAvailibleInUI(true)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .SetAllowNonContextActions(false)
                .Configure();

            FeatureConfigurator.New(FeatName + "ProhibitA", Guids.MediumSpellcasterFeatProhibitArchmage)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddComponent<ForbidSpellbook>(c =>
                {
                    c.m_Spellbook = BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.ArchmageSpellbook);
                })
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { BlueprintTool.Get<BlueprintCharacterClass>(Guids.Medium).ToString() }, false, Kingmaker.Enums.AbilityRankType.Default, 20, 1))
                .SetHideInUI(true)
                .SetHideNotAvailibleInUI(true)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .SetAllowNonContextActions(false)
                .Configure();

            FeatureConfigurator.New(FeatName + "ProhibitH", Guids.MediumSpellcasterFeatProhibitHierophant)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddComponent<ForbidSpellbook>(c =>
                {
                    c.m_Spellbook = BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.HierophantSpellbook);
                })
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { BlueprintTool.Get<BlueprintCharacterClass>(Guids.Medium).ToString() }, false, Kingmaker.Enums.AbilityRankType.Default, 20, 1))
                .SetHideInUI(true)
                .SetHideNotAvailibleInUI(true)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .SetAllowNonContextActions(false)
                .Configure();
        }
    }
}
