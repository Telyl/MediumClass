using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Guardian
{
    class AbsorbBlow
    {
        private static readonly string FeatName = "GuardianAbsorbBlow";
        private static readonly string DisplayName = "GuardianAbsorbBlow.Name";
        private static readonly string Description = "GuardianAbsorbBlow.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(AbsorbBlow));

        public static void ConfigureEnabled()
        {
            FeatureConfigurator.New(FeatName, Guids.GuardianAbsorbBlow)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddSpellKnownTemporary(characterClass: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Medium), spell: AbilityRefs.BestowGrace.Reference.Get(), level: 2, onlySpontaneous: true)
                .AddDamageResistancePhysical(value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                })
                .AddDamageResistanceEnergy(type: DamageEnergyType.Acid, value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                })
                .AddDamageResistanceEnergy(type: DamageEnergyType.Cold, value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                })
                .AddDamageResistanceEnergy(type: DamageEnergyType.Electricity, value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                })
                .AddDamageResistanceEnergy(type: DamageEnergyType.Fire, value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                })
                .AddDamageResistanceEnergy(type: DamageEnergyType.Sonic, value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                })
                .AddContextRankConfig(
                    ContextRankConfigs.ClassLevel(new string[] { BlueprintTool.Get<BlueprintCharacterClass>(Guids.Medium).ToString() }).WithDiv2Progression())
                .Configure();
        }
    }
}
