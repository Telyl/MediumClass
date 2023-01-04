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
            public static BlueprintBuff ArchmageClass = BlueprintTool.Get<BlueprintBuff>(Guids.MediumChannelSpiritAbilityArchmageBuff);
            public static BlueprintBuff ChampionClass = BlueprintTool.Get<BlueprintBuff>(Guids.MediumChannelSpiritAbilityChampionBuff);
            public static BlueprintBuff GuardianClass = BlueprintTool.Get<BlueprintBuff>(Guids.MediumChannelSpiritAbilityGuardianBuff);
            public static BlueprintBuff HierophantClass = BlueprintTool.Get<BlueprintBuff>(Guids.MediumChannelSpiritAbilityHierophantBuff);
            public static BlueprintBuff MarshalClass = BlueprintTool.Get<BlueprintBuff>(Guids.MediumChannelSpiritAbilityMarshalBuff);
            public static BlueprintBuff TricksterClass = BlueprintTool.Get<BlueprintBuff>(Guids.MediumChannelSpiritAbilityTricksterBuff);

            public static List<BlueprintBuff> AllSpirits = new List<BlueprintBuff> {
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
