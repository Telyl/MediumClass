using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Utility;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium
{
    class SharedSeance
    {
        private static readonly string FeatName = "MediumSharedSeance";
        internal const string DisplayName = "MediumSharedSeance.Name";
        private static readonly string Description = "MediumSharedSeance.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SharedSeance));
        

        public static void ConfigureEnabled()
        {
            BlueprintBuff buff = BuffConfigurator.New(FeatName + "Buff", Guids.MediumSharedSeanceBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.RemoveOnRest)
                .AddComponent<MediumContextSharedSeanceComponent>()
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.MediumSharedSeance)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFactsChangeTrigger(checkedFacts: new() { BlueprintTool.GetRef<BlueprintUnitFactReference>(Guids.MediumChannelSpiritPrimarySpiritBuff) },
                    onFactGainedActions: ActionsBuilder.New().ApplyBuffPermanent(buff),
                    onFactLostActions: ActionsBuilder.New().RemoveBuff(buff))
                .Configure();
        }
        
    }
}
