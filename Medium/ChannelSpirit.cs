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
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Medium.NewActions;
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


        private const string IconPrefix = "assets/icons/";
        internal const string ArchmageName = "Archmage.Name";
        private static readonly string ArchmageDescription = "Archmage.Description";
        private const string ArchmageIconName = IconPrefix + "spiritarchmage2.png";

        internal const string ChampionName = "Champion.Name";
        private static readonly string ChampionDescription = "Champion.Description";
        private const string ChampionIconName = IconPrefix + "spiritchampion2.png";

        internal const string GuardianName = "Guardian.Name";
        private static readonly string GuardianDescription = "Guardian.Description";
        private const string GuardianIconName = IconPrefix + "spiritguardian2.png";

        internal const string HierophantName = "Hierophant.Name";
        private static readonly string HierophantDescription = "Hierophant.Description";
        private const string HierophantIconName = IconPrefix + "spirithierophant2.png";

        internal const string MarshalName = "Marshal.Name";
        private static readonly string MarshalDescription = "Marshal.Description";
        private const string MarshalIconName = IconPrefix + "spiritmarshal2.png";

        internal const string TricksterName = "Trickster.Name";
        private static readonly string TricksterDescription = "Trickster.Description";
        private const string TricksterIconName = IconPrefix + "spirittrickster2.png";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(ChannelSpirit));

        public static void ConfigureEnabled()
        {
            Logger.Log("Generating Channel Spirit");

            #region Archmage
            var ab = BuffConfigurator.New(FeatName + "AbilityArchmageBuff", Guids.MediumChannelSpiritAbilityArchmageBuff)
                .AddComponent<MediumSpiritComponent>(c => {
                    c.SpiritClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Archmage);
                    c.SpiritInfluencePenalty = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.MediumInfluenceDebuff);
                    c.MediumInfluence = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource);
                    c.SpiritBonusFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ArchmageSpiritBonus);
                    c.SpiritLesserPower = new BlueprintFeatureReference();
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ArchmageIntermediate);
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ArchmageGreater);
                    c.SpiritSupremePower = new BlueprintFeatureReference();
                    c.Concentration = true;
                    c.Stats = new StatType[] { StatType.SkillKnowledgeArcana, StatType.SkillKnowledgeWorld };
                    c.Penalties = new StatType[] { StatType.AdditionalAttackBonus, StatType.AdditionalDamage, StatType.SaveFortitude, StatType.SkillAthletics };
                })
                .AddComponent<AddSharedSeance>()
                .AddComponent<ApplySpirit>(c =>
                {
                    c.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Archmage);
                    c.SpiritLesserPower = new BlueprintFeatureReference();
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ArchmageIntermediate);
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ArchmageGreater);
                    c.SpiritSupremePower = new BlueprintFeatureReference();
                })
                .Configure();
            var a = ActivatableAbilityConfigurator.New(FeatName + "AbilityArchmage", Guids.MediumChannelSpiritAbilityArchmage)
                .SetDisplayName(ArchmageName)
                .SetDescription(ArchmageDescription)
                .SetIcon(ArchmageIconName)
                .SetBuff(ab)
                .SetGroup((ActivatableAbilityGroup)239480)
                .AddActivatableAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.TurnOn)
                .SetDeactivateImmediately(true)
                .Configure();
            #endregion
            #region Champion
            var cb = BuffConfigurator.New(FeatName + "AbilityChampionBuff", Guids.MediumChannelSpiritAbilityChampionBuff)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Champion);
                    c.SpiritLesserPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ChampionLesser);
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ChampionSuddenAttack);
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ChampionFleetCharge);
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.LegendaryChampion);
                })
                .AddComponent<AddSharedSeance>()
                .AddComponent<MediumSpiritComponent>(c => {
                    c.SpiritClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Champion);
                    c.SpiritInfluencePenalty = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.MediumInfluenceDebuff);
                    c.MediumInfluence = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource);
                    c.SpiritBonusFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ChampionSpiritBonus);
                    c.SpiritLesserPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ChampionLesser); 
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ChampionSuddenAttack); 
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.ChampionFleetCharge); 
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.LegendaryChampion); 
                    c.Stats = new StatType[] { StatType.SkillAthletics, StatType.SaveFortitude, StatType.AdditionalDamage, StatType.AdditionalAttackBonus };
                    c.Penalties = new StatType[] { StatType.SkillKnowledgeArcana, StatType.SkillKnowledgeWorld, StatType.BonusCasterLevel };
                })
                .Configure();
            var c = ActivatableAbilityConfigurator.New(FeatName + "AbilityChampion", Guids.MediumChannelSpiritAbilityChampion)
                .SetDisplayName(ChampionName)
                .SetDescription(ChampionDescription)
                .SetIcon(ChampionIconName)
                //.SetHiddenInUI(true)
                .SetBuff(cb)
                .SetGroup((ActivatableAbilityGroup)239480)
                .AddActivatableAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.TurnOn)
                .SetDeactivateImmediately(true)
                .Configure();
            #endregion
            #region Guardian
            var gb = BuffConfigurator.New(FeatName + "AbilityGuardianBuff", Guids.MediumChannelSpiritAbilityGuardianBuff)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Guardian);
                    c.SpiritLesserPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.GuardianLesser);
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.GuardianAbsorbBlow);
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.GuardianGreater);
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.LegendaryGuardian);
                })
                .AddComponent<AddSharedSeance>()
                .AddComponent<MediumSpiritComponent>(c => {
                    c.SpiritClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Guardian);
                    c.SpiritInfluencePenalty = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.MediumInfluenceDebuff);
                    c.MediumInfluence = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource);
                    c.SpiritBonusFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.GuardianSpiritBonus);
                    c.SpiritLesserPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.GuardianLesser); 
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.GuardianAbsorbBlow); 
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.GuardianGreater); 
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.LegendaryGuardian); 
                    c.Stats = new StatType[] { StatType.AC, StatType.SaveReflex, StatType.SaveFortitude };
                    c.Penalties = new StatType[] { StatType.AdditionalDamage };
                })
                .Configure();
            var g = ActivatableAbilityConfigurator.New(FeatName + "AbilityGuardian", Guids.MediumChannelSpiritAbilityGuardian)
                .SetDisplayName(GuardianName)
                .SetDescription(GuardianDescription)
                .SetIcon(GuardianIconName)
                //.SetHiddenInUI(true)
                .SetBuff(gb)
                .SetGroup((ActivatableAbilityGroup)239480)
                .AddActivatableAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.TurnOn)
                .SetDeactivateImmediately(true)
                .Configure();
            #endregion
            #region Hierophant
            var hb = BuffConfigurator.New(FeatName + "AbilityHierophantBuff", Guids.MediumChannelSpiritAbilityHierophantBuff)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Hierophant);
                    c.SpiritLesserPower = new BlueprintFeatureReference();
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.HierophantEnergyFont);
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.HierophantOverflowingGrace);
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.HierophantSupreme);
                })
                .AddComponent<AddSharedSeance>()
                .AddComponent<MediumSpiritComponent>(c => {
                    c.SpiritClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Hierophant);
                    c.SpiritInfluencePenalty = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.MediumInfluenceDebuff);
                    c.MediumInfluence = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource);
                    c.SpiritBonusFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.HierophantSpiritBonus);
                    c.SpiritLesserPower = new BlueprintFeatureReference();
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.HierophantEnergyFont); 
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.HierophantOverflowingGrace); 
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.HierophantSupreme); 
                    c.Stats = new StatType[] { StatType.SkillPerception, StatType.SkillLoreNature, StatType.SkillLoreReligion, StatType.SaveWill };
                    c.Penalties = new StatType[] { StatType.SkillUseMagicDevice, StatType.SkillPersuasion };
                })
                .Configure();
            var h = ActivatableAbilityConfigurator.New(FeatName + "AbilityHierophant", Guids.MediumChannelSpiritAbilityHierophant)
                .SetDisplayName(HierophantName)
                .SetDescription(HierophantDescription)
                .SetIcon(HierophantIconName)
                .SetBuff(hb)
                .SetGroup((ActivatableAbilityGroup)239480)
                .AddActivatableAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.TurnOn)
                .SetDeactivateImmediately(true)
                .Configure();
            #endregion
            #region Marshal
            var mb = BuffConfigurator.New(FeatName + "AbilityMarshalBuff", Guids.MediumChannelSpiritAbilityMarshalBuff)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Marshal);
                    c.SpiritLesserPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalMarshalsOrders);
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalInspiringCallStandard); //TODO
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalDecisiveStrikeFeature);
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalLegendaryMarshal);
                })
                .AddComponent<AddSharedSeance>()
                .AddComponent<MediumSpiritComponent>(c => {
                    c.SpiritClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Marshal);
                    c.SpiritInfluencePenalty = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.MediumInfluenceDebuff);
                    c.MediumInfluence = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource);
                    c.SpiritBonusFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalSpiritBonus);
                    c.SpiritLesserPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalMarshalsOrders); 
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalInspiringCallStandard); //TODO
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalDecisiveStrikeFeature); 
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MarshalLegendaryMarshal); 
                    c.Stats = new StatType[] { StatType.AdditionalAttackBonus }.Concat(StatTypeHelper.Saves).Concat(StatTypeHelper.Skills).ToArray();
                    c.Penalties = new StatType[] { StatType.SkillPerception, StatType.SkillLoreNature, StatType.SkillLoreReligion , StatType.SaveWill };
                    c.Concentration = true;
                })
                .Configure();

            var m = ActivatableAbilityConfigurator.New(FeatName + "AbilityMarshal", Guids.MediumChannelSpiritAbilityMarshal)
                .SetDisplayName(MarshalName)
                .SetDescription(MarshalDescription)
                .SetIcon(MarshalIconName)
                .SetBuff(mb)
                .SetGroup((ActivatableAbilityGroup)239480)
                .AddActivatableAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.TurnOn)
                .SetDeactivateImmediately(true)
                .Configure();
            #endregion
            #region Trickster
            var tb = BuffConfigurator.New(FeatName + "AbilityTricksterBuff", Guids.MediumChannelSpiritAbilityTricksterBuff)
                .AddComponent<ApplySpirit>(c =>
                {
                    c.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Trickster);
                    c.SpiritLesserPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterEdge);
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterSurpriseStrike);
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterTransferMagic);
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterLegendaryTrickster);
                })
                .AddComponent<AddSharedSeance>()
                .AddComponent<MediumSpiritComponent>(c => {
                    c.SpiritClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.Trickster);
                    c.SpiritInfluencePenalty = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.MediumInfluenceDebuff);
                    c.MediumInfluence = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource);
                    c.SpiritBonusFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterSpiritBonus);
                    c.SpiritLesserPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterEdge);
                    c.SpiritIntermediatePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterSurpriseStrike); 
                    c.SpiritGreaterPower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterTransferMagic); 
                    c.SpiritSupremePower = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.TricksterLegendaryTrickster); 
                    c.Penalties = new StatType[] { StatType.AC, StatType.AdditionalCMB, StatType.AdditionalCMD };
                    c.Stats = new StatType[] { StatType.SkillAthletics, StatType.SkillKnowledgeArcana, StatType.SkillKnowledgeWorld, StatType.SkillLoreNature, StatType.SkillLoreReligion, StatType.SkillMobility, StatType.SkillPerception, StatType.SkillPersuasion, StatType.SkillStealth, StatType.SkillThievery, StatType.SkillUseMagicDevice, StatType.SaveReflex, StatType.Initiative };
                })
                .Configure(); 
            
            var t = ActivatableAbilityConfigurator.New(FeatName + "AbilityTrickster", Guids.MediumChannelSpiritAbilityTrickster)
                .SetDisplayName(TricksterName)
                .SetDescription(TricksterDescription)
                .SetIcon(TricksterIconName)
                .SetBuff(tb)
                .SetGroup((ActivatableAbilityGroup)239480)
                .AddActivatableAbilityResourceLogic(requiredResource: BlueprintTool.GetRef<BlueprintAbilityResourceReference>(Guids.MediumInfluenceResource), spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.TurnOn)
                .SetDeactivateImmediately(true)
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
