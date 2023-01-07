using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using MediumClass.Utils;
using TabletopTweaks.Core.NewUnitParts;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
    [TypeId("c7a2b2de-1cc8-4f98-946f-852abef20879")]
    public class LegendaryHierophantComponent : UnitFactComponentDelegate
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(LegendaryHierophantComponent));
        public override void OnTurnOn()
        {
            base.Owner.Ensure<UnitPartHierophant>().AddSpellList(base.Fact, m_SpellLists, m_CharacterClass, m_Spellbook, m_Resource);
        }
        public override void OnTurnOff()
        {
            Owner.Get<UnitPartHierophant>().RemoveEntry(this.Fact);
        }

        public BlueprintCharacterClassReference m_CharacterClass;
        public BlueprintSpellbookReference m_Spellbook;
        public BlueprintSpellListReference m_SpellLists;
        public BlueprintAbilityResourceReference m_Resource;
    }
}
