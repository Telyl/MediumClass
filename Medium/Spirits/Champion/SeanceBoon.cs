using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Champion
{
    class SeanceBoon
    {
        private static readonly string FeatName = "ChampionSeanceBoon";
        private static readonly string DisplayName = "ChampionSeanceBoon.Name";
        private static readonly string Description = "ChampionSeanceBoon.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SeanceBoon));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Champion Seance Boon");
            FeatureConfigurator.New(FeatName, Guids.ChampionSeanceBoon)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.CavalierKnightsChallengeAbility.Reference.Get().Icon)
                .AddStatBonus(descriptor: ModifierDescriptor.UntypedStackable, stat: StatType.AdditionalDamage, value: 2)
                .Configure();
        }
    }
}
