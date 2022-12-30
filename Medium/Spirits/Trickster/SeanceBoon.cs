using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Trickster
{
    class SeanceBoon
    {
        private static readonly string FeatName = "TricksterSeanceBoon";
        private static readonly string DisplayName = "TricksterSeanceBoon.Name";

        private static readonly string AthleticsDisplayName = "TricksterSeanceBoonAthletics.Name";
        private static readonly string MobilityDisplayName = "TricksterSeanceBoonMobility.Name";
        private static readonly string ThieveryDisplayName = "TricksterSeanceBoonThievery.Name";
        private static readonly string StealthDisplayName = "TricksterSeanceBoonStealth.Name";
        private static readonly string ArcanaDisplayName = "TricksterSeanceBoonArcana.Name";
        private static readonly string WorldDisplayName = "TricksterSeanceBoonWorld.Name";
        private static readonly string NatureDisplayName = "TricksterSeanceBoonNature.Name";
        private static readonly string ReligionDisplayName = "TricksterSeanceBoonReligion.Name";
        private static readonly string PersuasionDisplayName = "TricksterSeanceBoonPersuasion.Name";
        private static readonly string PerceptionDisplayName = "TricksterSeanceBoonPerception.Name";
        private static readonly string UMDDisplayName = "TricksterSeanceBoonUMD.Name";

        private static readonly string Description = "TricksterSeanceBoon.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SeanceBoon));

        // TODO: Ranks need to be applied on level up to get the +3 from class skill. How do you add just a base "rank".
        public static void ConfigureEnabled()
        {
            Logger.Log("Configuring Trickster's Seance Boon");

            // I need to make a custom component to add to ranks. Can't do it in ActivatableAbilities. It'll be a buff. Logic gets funky in game code.
            // Can re-use this for the seance boon.
            BlueprintBuff athleticsbuff = BuffConfigurator.New(FeatName + "AthleticsBuff", Guids.TricksterSeanceBoonAthleticsBuff).Configure();
            BuffConfigurator.For(athleticsbuff)
                .SetDisplayName(AthleticsDisplayName)
                .AddClassSkill(skill: StatType.SkillAthletics)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(athleticsbuff))
                .AddComponent<AddTricksterSeance>(c =>
                {
                    c.Skill = StatType.SkillAthletics;
                })
                .SetIcon(FeatureRefs.SkillFocusPhysique.Reference.Get().Icon)
                .Configure();

            BlueprintBuff mobilitybuff = BuffConfigurator.New(FeatName + "MobilityBuff", Guids.TricksterSeanceBoonMobilityBuff).Configure();
            BuffConfigurator.For(mobilitybuff)
                .SetDisplayName(MobilityDisplayName)
                .AddClassSkill(skill: StatType.SkillMobility)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(mobilitybuff))
                .AddComponent<AddTricksterSeance>(c =>
                {
                    c.Skill = StatType.SkillMobility;
                })
                .SetIcon(FeatureRefs.SkillFocusAcrobatics.Reference.Get().Icon)
                .Configure();

            BlueprintBuff thieverybuff = BuffConfigurator.New(FeatName + "ThieveryBuff", Guids.TricksterSeanceBoonThieveryBuff).Configure();
            BuffConfigurator.For(thieverybuff)
                .SetDisplayName(ThieveryDisplayName)
                .AddClassSkill(skill: StatType.SkillThievery)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(thieverybuff))
                .AddComponent<AddTricksterSeance>(c =>
                {
                    c.Skill = StatType.SkillThievery;
                })
                .SetIcon(FeatureRefs.SkillFocusThievery.Reference.Get().Icon)
                .Configure();

            BlueprintBuff stealthbuff = BuffConfigurator.New(FeatName + "StealthBuff", Guids.TricksterSeanceBoonStealthBuff).Configure();
            BuffConfigurator.For(stealthbuff)
                .SetDisplayName(StealthDisplayName)
                .AddClassSkill(skill: StatType.SkillStealth)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(stealthbuff))
                .AddComponent<AddTricksterSeance>(c =>
                {
                    c.Skill = StatType.SkillStealth;
                })
                .SetIcon(FeatureRefs.SkillFocusStealth.Reference.Get().Icon)
                .Configure();

            BlueprintBuff religionbuff = BuffConfigurator.New(FeatName + "ReligionBuff", Guids.TricksterSeanceBoonLoreReligionBuff).Configure();
            BuffConfigurator.For(religionbuff)
                .SetDisplayName(ReligionDisplayName)
                .AddClassSkill(skill: StatType.SkillLoreReligion)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(religionbuff))
                .AddComponent<AddTricksterSeance>(c =>
                {
                    c.Skill = StatType.SkillLoreReligion;
                })
                .SetIcon(FeatureRefs.SkillFocusLoreReligion.Reference.Get().Icon)
                .Configure();

            BlueprintBuff naturebuff = BuffConfigurator.New(FeatName + "NatureBuff", Guids.TricksterSeanceBoonLoreNatureBuff).Configure();
            BuffConfigurator.For(naturebuff)
                .SetDisplayName(NatureDisplayName)
                .AddClassSkill(skill: StatType.SkillLoreNature)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(naturebuff))
                .AddComponent<AddTricksterSeance>(c =>
                {
                    c.Skill = StatType.SkillLoreNature;
                })
                .SetIcon(FeatureRefs.SkillFocusLoreNature.Reference.Get().Icon)
                .Configure();

            BlueprintBuff arcanabuff = BuffConfigurator.New(FeatName + "ArcanaBuff", Guids.TricksterSeanceBoonKnowledgeArcanaBuff).Configure();
            BuffConfigurator.For(arcanabuff)
                .SetDisplayName(ArcanaDisplayName)
                .AddClassSkill(skill: StatType.SkillKnowledgeArcana)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(arcanabuff))
                .AddComponent<AddTricksterSeance>(c =>
                {
                    c.Skill = StatType.SkillKnowledgeArcana;
                })
                .SetIcon(FeatureRefs.SkillFocusKnowledgeArcana.Reference.Get().Icon)
                .Configure();

            BlueprintBuff worldbuff = BuffConfigurator.New(FeatName + "WorldBuff", Guids.TricksterSeanceBoonKnowledgeWorldBuff).Configure();
            BuffConfigurator.For(worldbuff)
                .SetDisplayName(WorldDisplayName)
                .AddClassSkill(skill: StatType.SkillKnowledgeWorld)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(worldbuff))
                .AddComponent<AddTricksterSeance>(c =>
                {
                    c.Skill = StatType.SkillKnowledgeWorld;
                })
                .SetIcon(FeatureRefs.SkillFocusKnowledgeWorld.Reference.Get().Icon)
                .Configure();

            BlueprintBuff perceptionbuff = BuffConfigurator.New(FeatName + "PerceptionBuff", Guids.TricksterSeanceBoonPerceptionBuff).Configure();
            BuffConfigurator.For(perceptionbuff)
                .SetDisplayName(PerceptionDisplayName)
                .AddClassSkill(skill: StatType.SkillPerception)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(perceptionbuff))
                .AddComponent<AddTricksterSeance>(c =>
                {
                    c.Skill = StatType.SkillPerception;
                })
                .SetIcon(FeatureRefs.SkillFocusPerception.Reference.Get().Icon)
                .Configure();


            BlueprintBuff persuasionbuff = BuffConfigurator.New(FeatName + "PersuasionBuff", Guids.TricksterSeanceBoonPersuasionBuff).Configure();
            BuffConfigurator.For(persuasionbuff)
                .SetDisplayName(PersuasionDisplayName)
                .AddClassSkill(skill: StatType.SkillPersuasion)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(persuasionbuff))
                .AddComponent<AddTricksterSeance>(c =>
                {
                    c.Skill = StatType.SkillPersuasion;
                })
                .SetIcon(FeatureRefs.SkillFocusDiplomacy.Reference.Get().Icon)
                .Configure();


            BlueprintBuff umdbuff = BuffConfigurator.New(FeatName + "UMDBuff", Guids.TricksterSeanceBoonUseMagicDeviceBuff).Configure();
            BuffConfigurator.For(umdbuff)
                .SetDisplayName(UMDDisplayName)
                .AddClassSkill(skill: StatType.SkillUseMagicDevice)
                .AddRestTrigger(action: ActionsBuilder.New().RemoveBuff(umdbuff))
                .AddComponent<AddTricksterSeance>(c =>
                {
                    c.Skill = StatType.SkillUseMagicDevice;
                })
                .SetIcon(FeatureRefs.SkillFocusUseMagicDevice.Reference.Get().Icon)
                .Configure();

            BlueprintAbilityResource TrickstersEdgeResource = AbilityResourceConfigurator.New(FeatName + "Resource", Guids.TricksterSeanceBoonResource)
                .SetMaxAmount(ResourceAmountBuilder.New(1).Build())
                .Configure();


            BlueprintAbility Ability = AbilityConfigurator.New(FeatName + "Ability", Guids.TricksterSeanceBoonAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(FeatureRefs.FastLearner.Reference.Get().Icon)
                .AddAbilityApplyFact(hasDuration: false, restriction: AbilityApplyFact.FactRestriction.CasterHasNoFact, facts: new() { athleticsbuff, mobilitybuff, thieverybuff, stealthbuff, naturebuff, religionbuff, arcanabuff, worldbuff, perceptionbuff, persuasionbuff, umdbuff })
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: TrickstersEdgeResource)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.TricksterSeanceBoon)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(FeatureRefs.SneakAttack.Reference.Get().Icon)
                .AddFacts(new() { Ability })
                .AddAbilityResources(amount: 1, restoreAmount: true, resource: TrickstersEdgeResource)
                .Configure();
        }
    }
}
