using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.UnitLogic.Abilities.Components;
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

namespace MediumClass.Medium.Spirits.Champion
{
    class ChampionLesser
    {
        private static readonly string FeatName = "ChampionLesser";
        private static readonly string DisplayName = "ChampionLesser.Name";
        private static readonly string Description = "ChampionLesser.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(ChampionLesser));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Champion Lesser Spirit Power");
            
            FeatureConfigurator.New(FeatName, Guids.ChampionLesser)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() {
                    FeatureRefs.MartialWeaponProficiency.Reference.Get(),
                    FeatureRefs.BastardSwordProficiency.Reference.Get(),
                    FeatureRefs.DuelingSwordProficiency.Reference.Get(),
                    FeatureRefs.DwarvenWeaponFamiliarity.Reference.Get(),
                    FeatureRefs.ElvenCurvedBladeProficiency.Reference.Get(),
                    FeatureRefs.EstocProficiency.Reference.Get(),
                    FeatureRefs.FalcataProficiency.Reference.Get(),
                    FeatureRefs.FauchardProficiency.Reference.Get(),
                    FeatureRefs.HookedHammerProficiency.Reference.Get(),
                    FeatureRefs.KamaProficiency.Reference.Get(),
                    FeatureRefs.NunchakuProficiency.Reference.Get(),
                    FeatureRefs.DoubleAxeProficiency.Reference.Get(),
                    FeatureRefs.SaiProficiency.Reference.Get(),
                    FeatureRefs.SlingStaffProficiency.Reference.Get(),
                    FeatureRefs.StarknifeProficiency.Reference.Get(),
                    FeatureRefs.TongiProficiency.Reference.Get(),
                    FeatureRefs.UrgroshProficiency.Reference.Get(),
                    FeatureRefs.DoubleSwordProficiency.Reference.Get()
                })
                .Configure();
        }
    }
}
