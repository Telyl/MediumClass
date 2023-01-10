using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using MediumClass.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediumClass.Medium.Spirits
{
    public static class SpiritHelper
    {
        public static class SpiritClasses
        {
            public static BlueprintCharacterClassReference ArchmageClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Archmage);
            public static BlueprintCharacterClassReference ChampionClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Champion);
            public static BlueprintCharacterClassReference GuardianClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Guardian);
            public static BlueprintCharacterClassReference HierophantClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Hierophant);
            public static BlueprintCharacterClassReference MarshalClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Marshal);
            public static BlueprintCharacterClassReference TricksterClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Trickster);

            public static List<BlueprintCharacterClassReference> AllSpirits = new List<BlueprintCharacterClassReference> {
                ArchmageClass,
                ChampionClass,
                GuardianClass,
                HierophantClass,
                MarshalClass,
                TricksterClass,
            };
        }
    }
}
