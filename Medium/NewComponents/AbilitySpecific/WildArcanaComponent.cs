using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using MediumClass.Medium.NewUnitParts;
using MediumClass.Utilities;
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
            var medium = base.Owner.Ensure<UnitPartMedium>();
            if(medium.PrimarySpirit.Get() != BlueprintTool.Get<BlueprintCharacterClass>(Guids.Archmage))
            {
                m_Resource = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResourceArchmage);
            }
            base.Owner.Ensure<UnitPartArchmage>().AddSpellList(base.Fact, m_SpellLists, m_CharacterClass, m_Spellbook, m_Resource);
        }
        public override void OnTurnOff()
        {
            Owner.Get<UnitPartArchmage>().RemoveEntry(this.Fact);
        }

        public BlueprintCharacterClassReference m_CharacterClass;
        public BlueprintSpellbookReference m_Spellbook;
        public BlueprintSpellListReference m_SpellLists;
        public BlueprintAbilityResourceReference m_Resource;
    }
}
