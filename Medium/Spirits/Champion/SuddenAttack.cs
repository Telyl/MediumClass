using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using static Kingmaker.UnitLogic.FactLogic.AddMechanicsFeature;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Champion
{
    class SuddenAttack
    {
        private static readonly string FeatName = "ChampionSuddenAttach";
        private static readonly string DisplayName = "ChampionSuddenAttack.Name";
        private static readonly string Description = "ChampionSuddenAttack.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SuddenAttack));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Champion Sudden Attack");
            FeatureConfigurator.New(FeatName, Guids.ChampionSuddenAttack)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddComponent<AddSuddenAttack>(c =>
                {
                    c.Number = 1;
                    c.Haste = true;
                    c.Penalized = false;
                })
                .AddMechanicsFeature(MechanicsFeatureType.SuppressedManyshot)
                .Configure();
        }
    }
}
