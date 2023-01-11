using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using MediumClass.Medium.NewComponents;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using TabletopTweaks.Core.NewComponents.AbilitySpecific;
using TabletopTweaks.Core.Utilities;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Hierophant
{
    class LegendaryHierophant
    {
        private static readonly string FeatName = "HierophantSupreme";
        private static readonly string DisplayName = "HierophantSupreme.Name";
        private static readonly string Description = "HierophantSupreme.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(LegendaryHierophant));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Hierophant Supreme Power");

            var resource = AbilityResourceConfigurator.New(FeatName + "Resource", Guids.HierophantSupremeResource)
                .SetMaxAmount(new BlueprintAbilityResource.Amount
                {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Charisma
                })
                .SetMax(10)
                .SetUseMax(false)
                .Configure();

            AbilityConfigurator.New(FeatName + "Ability4", Guids.HierophantSupremeAbility4)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.AngelArmyOfHeaven.Reference.Get().Icon)
                .Configure();

            AbilityConfigurator.New(FeatName + "Ability3", Guids.HierophantSupremeAbility3)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/legendaryhierophant.png")
                .Configure();

            AbilityConfigurator.New(FeatName + "Ability2", Guids.HierophantSupremeAbility2)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.Guidance.Reference.Get().Icon)
                .Configure();

            AbilityConfigurator.New(FeatName + "Ability1", Guids.HierophantSupremeAbility1)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/legendaryhierophant1.png")
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.HierophantSupreme)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/legendaryhierophant.png")
                .AddAbilityResources(amount: 0, resource: resource, restoreAmount: true, restoreOnLevelUp: false, useThisAsResource: false)
                .AddComponent<LegendaryHierophantComponent>(c =>
                {
                    c.m_CharacterClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Hierophant);
                    c.m_Spellbook = BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.HierophantSpellbook);
                    c.m_SpellLists = SpellTools.SpellList.ClericSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Resource = resource.ToReference<BlueprintAbilityResourceReference>();
                })
                .AddFacts(new() { Guids.HierophantSupremeAbility1, Guids.HierophantSupremeAbility2, Guids.HierophantSupremeAbility3, Guids.HierophantSupremeAbility4})
                .SetRanks(1)
                .Configure();
        }
    }
}
