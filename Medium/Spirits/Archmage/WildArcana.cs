using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
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
    class WildArcana
    {
        private static readonly string FeatName = "ArchmageGreater";
        private static readonly string DisplayName = "ArchmageGreater.Name";
        private static readonly string Description = "ArchmageGreater.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(WildArcana));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Archmage Greater Power");

            BlueprintBuff WildArcanaBuff = BuffConfigurator.New(FeatName + "Buff", Guids.ArchmageGreaterBuff)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddComponent<CheckWildArcana>()
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.ArchmageGreater)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddComponent<WildArcanaComponent>(c =>
                {
                    c.m_CharacterClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Archmage);
                    c.m_Spellbook = BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.ArchmageSpellbook);
                    c.m_SpellLists = SpellTools.SpellList.WizardSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Resource = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource);
                })
                .AddFacts(new() { Guids.ArchmageGreaterBuff })
                .SetRanks(1)
                .Configure();
        }
    }
}
