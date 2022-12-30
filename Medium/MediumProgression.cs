using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using MediumClass.Medium;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium
{
    class MediumProgression
    {
        private static readonly string ProgressionName = "MediumProgression";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(MediumProgression));

        public static BlueprintProgression Configure()
        {
            Logger.Log("Generating Medium Progression");
            MediumProficiencies.ConfigureEnabled();
            SpiritPower.ConfigureEnabled();
            SpiritSurge.ConfigureEnabled();
            SpiritBonus.ConfigureEnabled();
            ChannelSpirit.ConfigureEnabled();
            Influence.ConfigureEnabled();
            SharedSeance.ConfigureEnabled();
            Propitation.ConfigureEnabled();
            TranceOfThree.ConfigureEnabled();
            SpiritMastery.ConfigureEnabled();

            var entries = LevelEntryBuilder.New()
                .AddEntry(1, Guids.MediumSpiritBonus, Guids.MediumProficiencies, Guids.SpiritPowerLesser, Guids.SpiritSurge, Guids.MediumChannelSpirit, Guids.MediumInfluence)
                .AddEntry(2, Guids.MediumSharedSeance)
                .AddEntry(4, Guids.MediumSpiritBonus)
                .AddEntry(6, Guids.SpiritPowerIntermediate)
                .AddEntry(8, Guids.MediumSpiritBonus)
                .AddEntry(9, Guids.MediumPropitation)
                .AddEntry(10, Guids.SpiritSurge) 
                .AddEntry(11, Guids.SpiritPowerGreater)
                .AddEntry(12, Guids.MediumSpiritBonus)
                .AddEntry(15, Guids.MediumTranceOfThree)
                .AddEntry(16, Guids.MediumSpiritBonus)
                .AddEntry(17, Guids.SpiritPowerSupreme)
                .AddEntry(19, Guids.MediumSpiritMastery)
                .AddEntry(20, Guids.MediumSpiritBonus, Guids.SpiritSurge);

            return ProgressionConfigurator.New(ProgressionName, Guids.MediumProgression)
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(false)
                .SetForAllOtherClasses(false)
                .SetLevelEntries(entries)
                .Configure();
        }
    }
}
