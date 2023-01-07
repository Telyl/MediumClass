using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using MediumClass.Utils;
using TabletopTweaks.Core.NewUnitParts;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
    [TypeId("74bb36c4-55e3-4f86-bbcd-bc41c7ec8634")]
    public class WildArcanaComponent : UnitFactComponentDelegate
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(MediumSpiritSurgeComponent));
        public override void OnTurnOn()
        {
            base.Owner.Ensure<UnitPartSpellcaster>().AddSpellList(base.Fact, m_SpellLists, m_CharacterClass, m_Spellbook, m_Resource);
        }
        public override void OnTurnOff()
        {
            Owner.Get<UnitPartSpellcaster>().RemoveEntry(this.Fact);
        }

        public BlueprintCharacterClassReference m_CharacterClass;
        public BlueprintSpellbookReference m_Spellbook;
        public BlueprintSpellListReference m_SpellLists;
        public BlueprintAbilityResourceReference m_Resource;
    }
}
