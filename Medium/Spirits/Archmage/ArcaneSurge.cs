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
using Kingmaker.UnitLogic.Buffs.Blueprints;
using MediumClass.Medium.NewActions;
using MediumClass.Medium.NewComponents;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Archmage
{
    class ArcaneSurge
    {
        private static readonly string FeatName = "ArchmageIntermediate";
        private static readonly string DisplayName = "ArchmageIntermediate.Name";
        private static readonly string Description = "ArchmageIntermediate.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(ArcaneSurge));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Archmage Intermediate Power");

            var buff = BuffConfigurator.New(FeatName + "Buff", Guids.ArchmageIntermediateBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EcclesitheurgeBondAbility.Reference.Get().Icon)
                .AddAbilityUseTrigger(action: ActionsBuilder.New().RemoveSelf(),
                spellbooks: new() { BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.ArchmageSpellbook),
                    BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.MediumSpellbook) },
                fromSpellbook: true, afterCast: true, forMultipleSpells: false, checkAbilityType: false, minSpellLevel: false, exactSpellLevel: false, spellDescriptor: SpellDescriptor.None, range: Kingmaker.UnitLogic.Abilities.Blueprints.AbilityRange.Touch)
                .AddIncreaseSpellSpellbookDC(1, descriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable, spellbooks: new()
                {
                    BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.ArchmageSpellbook),
                    BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.MediumSpellbook),
                    BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.HierophantSpellbook)
                })
                .AddCasterLevelForSpellbook(1, descriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable, spellbooks: new()
                {
                    BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.ArchmageSpellbook),
                    BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.MediumSpellbook),
                    BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.HierophantSpellbook)
                })
                .AddComponent<AddResourcelessSpell>()
                .SetFlags(BlueprintBuff.Flags.RemoveOnRest)
                .Configure();

            var ability = AbilityConfigurator.New(FeatName + "Ability", Guids.ArchmageIntermediateAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EcclesitheurgeBondAbility.Reference.Get().Icon)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().Add<ContextActionSpiritInfluence>().ApplyBuffPermanent(buff))
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.ArchmageIntermediate)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EcclesitheurgeBondAbility.Reference.Get().Icon)
                .AddFacts(new() { ability })
                .Configure();
        }
    }
}
