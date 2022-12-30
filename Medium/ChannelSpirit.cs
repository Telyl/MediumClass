using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.NewComponents;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium
{
    class ChannelSpirit
    {
        private static readonly string FeatName = "MediumChannelSpirit";
        private static readonly string DisplayName = "MediumChannelSpirit.Name";
        private static readonly string Description = "MediumChannelSpirit.Description";

        internal const string ArchmageName = "Archmage.Name";
        private static readonly string ArchmageDescription = "Archmage.Description";

        internal const string ChampionName = "Champion.Name";
        private static readonly string ChampionDescription = "Champion.Description";

        internal const string GuardianName = "Guardian.Name";
        private static readonly string GuardianDescription = "Guardian.Description";

        internal const string HierophantName = "Hierophant.Name";
        private static readonly string HierophantDescription = "Hierophant.Description";

        internal const string MarshalName = "Marshal.Name";
        private static readonly string MarshalDescription = "Marshal.Description";

        internal const string TricksterName = "Trickster.Name";
        private static readonly string TricksterDescription = "Trickster.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(ChannelSpirit));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Channel Spirit");

            #region Archmage
            var ab = BuffConfigurator.New(FeatName + "AbilityArchmageBuff", Guids.MediumChannelSpiritAbilityArchmageBuff)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Archmage);
                })
                .AddComponent<AddSharedSeance>()
                .Configure();
            var a = ActivatableAbilityConfigurator.New(FeatName + "AbilityArchmage", Guids.MediumChannelSpiritAbilityArchmage)
                .SetDisplayName(ArchmageName)
                .SetDescription(ArchmageDescription)
                .AddComponent<CheckInfluence>()
                .SetIcon(AbilityRefs.DismissAreaEffect.Reference.Get().Icon)
                //.SetHiddenInUI(true)
                .SetBuff(ab)
                .SetGroup((ActivatableAbilityGroup)239480)
                .AddActivatableAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.TurnOn)
                .AddTurnOffImmediatelyWithUnitCommand()
                .Configure();
            #endregion
            #region Champion
            var cb = BuffConfigurator.New(FeatName + "AbilityChampionBuff", Guids.MediumChannelSpiritAbilityChampionBuff)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Champion);
                })
                .AddComponent<AddSharedSeance>()
                .Configure();
            var c = ActivatableAbilityConfigurator.New(FeatName + "AbilityChampion", Guids.MediumChannelSpiritAbilityChampion)
                .SetDisplayName(ChampionName)
                .SetDescription(ChampionDescription)
                .SetIcon(AbilityRefs.CavalierKnightsChallengeAbility.Reference.Get().Icon)
                //.SetHiddenInUI(true)
                .AddComponent<CheckInfluence>()
                .SetBuff(cb)
                .SetGroup((ActivatableAbilityGroup)239480)
                .AddActivatableAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.TurnOn)
                .AddTurnOffImmediatelyWithUnitCommand()
                .Configure();
            #endregion
            #region Guardian
            var gb = BuffConfigurator.New(FeatName + "AbilityGuardianBuff", Guids.MediumChannelSpiritAbilityGuardianBuff)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Guardian);
                })
                .AddComponent<AddSharedSeance>()
                .Configure();
            var g = ActivatableAbilityConfigurator.New(FeatName + "AbilityGuardian", Guids.MediumChannelSpiritAbilityGuardian)
                .SetDisplayName(GuardianName)
                .SetDescription(GuardianDescription)
                .AddComponent<CheckInfluence>()
                .SetIcon(FeatureRefs.Bravery.Reference.Get().Icon)
                //.SetHiddenInUI(true)
                .SetBuff(gb)
                .SetGroup((ActivatableAbilityGroup)239480)
                .AddActivatableAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.TurnOn)
                .AddTurnOffImmediatelyWithUnitCommand()
                .Configure();
            #endregion
            #region Hierophant
            var hb = BuffConfigurator.New(FeatName + "AbilityHierophantBuff", Guids.MediumChannelSpiritAbilityHierophantBuff)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Hierophant);
                })
                .AddComponent<AddSharedSeance>()
                .Configure();
            var h = ActivatableAbilityConfigurator.New(FeatName + "AbilityHierophant", Guids.MediumChannelSpiritAbilityHierophant)
                .SetDisplayName(HierophantName)
                .SetDescription(HierophantDescription)
                .AddComponent<CheckInfluence>()
                .SetIcon(AbilityRefs.CavalierForTheFaithAbility.Reference.Get().Icon)
                //.SetHiddenInUI(true)
                .SetBuff(hb)
                .SetGroup((ActivatableAbilityGroup)239480)
                .AddActivatableAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.TurnOn)
                .AddTurnOffImmediatelyWithUnitCommand()
                .Configure();
            #endregion
            #region Marshal
            var mb = BuffConfigurator.New(FeatName + "AbilityMarshalBuff", Guids.MediumChannelSpiritAbilityMarshalBuff)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Marshal);
                })
                .AddComponent<AddSharedSeance>()
                .Configure();

            var m = ActivatableAbilityConfigurator.New(FeatName + "AbilityMarshal", Guids.MediumChannelSpiritAbilityMarshal)
                .SetDisplayName(MarshalName)
                .SetDescription(MarshalDescription)
                .AddComponent<CheckInfluence>()
                .SetIcon(AbilityRefs.CavalierForTheKingAbility.Reference.Get().Icon)
                //.SetHiddenInUI(true)
                .SetBuff(mb)
                .SetGroup((ActivatableAbilityGroup)239480)
                .AddActivatableAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.TurnOn)
                .AddTurnOffImmediatelyWithUnitCommand()
                .Configure();
            #endregion
            #region Trickster
            var tb = BuffConfigurator.New(FeatName + "AbilityTricksterBuff", Guids.MediumChannelSpiritAbilityTricksterBuff)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Trickster);
                })
                .AddComponent<AddSharedSeance>()
                .Configure();
            
            var t = ActivatableAbilityConfigurator.New(FeatName + "AbilityTrickster", Guids.MediumChannelSpiritAbilityTrickster)
                .SetDisplayName(TricksterName)
                .SetDescription(TricksterDescription)
                .AddComponent<CheckInfluence>()
                .SetIcon(FeatureRefs.SneakAttack.Reference.Get().Icon)
                //.SetHiddenInUI(true)
                .SetBuff(tb)
                .SetGroup((ActivatableAbilityGroup)239480)
                .AddActivatableAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.TurnOn)
                .AddTurnOffImmediatelyWithUnitCommand()
                .Configure();
            #endregion

            // If OwlCat ever makes this look nicer, I'll re-enable it, otherwise it's cleaner and more elegant and less clunky to just have them all show up.
            /*var ability = ActivatableAbilityConfigurator.New(FeatName + "Ability", Guids.MediumChannelSpiritAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.ShamanWanderingHexAbility.Reference.Get().Icon)
                .AddActivatableAbilityVariants(variants: new() { a, c, g, h, m, t })
                .AddActivationDisable()
                .Configure();*/

            FeatureConfigurator.New(FeatName, Guids.MediumChannelSpirit)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.ShamanWanderingHexAbility.Reference.Get().Icon)
                //.AddFacts(new() { ability })
                .AddFacts(new() { a, c, g, h, m, t })
                .Configure();
        }
    }
}
