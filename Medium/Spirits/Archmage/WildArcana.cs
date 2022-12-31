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

            var buff = BuffConfigurator.New(FeatName + "Buff", Guids.ArchmageGreaterBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.ShamanItemBondAbility.Reference.Get().Icon)
                .AddAbilityUseTrigger(action: ActionsBuilder.New().RemoveSelf(),
                spellbooks: new() { BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.ArchmageSpellbook),
                    BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.MediumSpellbook) },
                fromSpellbook: true, afterCast: true, forMultipleSpells: false, checkAbilityType: false, minSpellLevel: false, exactSpellLevel: false, spellDescriptor: SpellDescriptor.None, range: Kingmaker.UnitLogic.Abilities.Blueprints.AbilityRange.Touch)
                .SetFlags(BlueprintBuff.Flags.RemoveOnRest)
                .Configure();

            var ability = AbilityConfigurator.New(FeatName + "Ability", Guids.ArchmageGreaterAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.ShamanItemBondAbility.Reference.Get().Icon)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New().ApplyBuffPermanent(buff))
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: Guids.MediumInfluenceResource)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.ArchmageGreater)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.ShamanItemBondAbility.Reference.Get().Icon)
                .AddComponent<SpontaneousConversion>(c => {
                    c.m_CharacterClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Archmage);
                })
                .Configure();
        }
    }
}
