using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
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
    class TrickstersEdge
    {
        private static readonly string FeatName = "TrickstersEdge";
        private static readonly string DisplayName = "TrickstersEdge.Name";

        private static readonly string AthleticsDisplayName = "TrickstersEdgeAthletics.Name";
        private static readonly string MobilityDisplayName = "TrickstersEdgeMobility.Name";
        private static readonly string ThieveryDisplayName = "TrickstersEdgeThievery.Name";
        private static readonly string StealthDisplayName = "TrickstersEdgeStealth.Name";
        private static readonly string ArcanaDisplayName = "TrickstersEdgeArcana.Name";
        private static readonly string WorldDisplayName = "TrickstersEdgeWorld.Name";
        private static readonly string NatureDisplayName = "TrickstersEdgeNature.Name";
        private static readonly string ReligionDisplayName = "TrickstersEdgeReligion.Name";
        private static readonly string PersuasionDisplayName = "TrickstersEdgePersuasion.Name";
        private static readonly string PerceptionDisplayName = "TrickstersEdgePerception.Name";
        private static readonly string UMDDisplayName = "TrickstersEdgeUMD.Name";

        private static readonly string Description = "TrickstersEdge.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(TrickstersEdge));

        public static ActionList CreateActionList(params GameAction[] actions)
        {
            if (actions == null || actions.Length == 1 && actions[0] == null) actions = Array.Empty<GameAction>();
            return new ActionList() { Actions = actions };
        }

        // TODO: Ranks need to be applied on level up to get the +3 from class skill. How do you add just a base "rank".
        public static void ConfigureEnabled()
        {
            Logger.Log("Configuring Trickster's Edge");

            // I need to make a custom component to add to ranks. Can't do it in ActivatableAbilities. It'll be a buff. Logic gets funky in game code.
            // Can re-use this for the seance boon.
            BlueprintBuff athleticsbuff = BuffConfigurator.New(FeatName + "AthleticsBuff", Guids.TricksterEdgeAthleticsBuff).Configure();
            BuffConfigurator.For(athleticsbuff)
                .SetDisplayName(AthleticsDisplayName)
                .SetDescription(Description)
                .AddClassSkill(skill: StatType.SkillAthletics)
                .AddComponent<AddTrickstersEdge>(c => { c.Skill = StatType.SkillAthletics; })
                .SetIcon(FeatureRefs.SkillFocusPhysique.Reference.Get().Icon)
                .Configure();

            BlueprintBuff mobilitybuff = BuffConfigurator.New(FeatName + "MobilityBuff", Guids.TricksterEdgeMobilityBuff)
                .SetDisplayName(MobilityDisplayName)
                .SetDescription(Description)
                .AddClassSkill(skill: StatType.SkillMobility)
                .AddComponent<AddTrickstersEdge>(c => { c.Skill = StatType.SkillMobility; })
                .SetIcon(FeatureRefs.SkillFocusAcrobatics.Reference.Get().Icon)
                .Configure();

            BlueprintBuff thieverybuff = BuffConfigurator.New(FeatName + "ThieveryBuff", Guids.TricksterEdgeThieveryBuff)
                .SetDisplayName(ThieveryDisplayName)
                .SetDescription(Description)
                .AddClassSkill(skill: StatType.SkillThievery)
                .AddComponent<AddTrickstersEdge>(c => { c.Skill = StatType.SkillThievery; })
                .SetIcon(FeatureRefs.SkillFocusThievery.Reference.Get().Icon)
                .Configure();

            BlueprintBuff stealthbuff = BuffConfigurator.New(FeatName + "StealthBuff", Guids.TricksterEdgeStealthBuff)
                .SetDisplayName(StealthDisplayName)
                .SetDescription(Description)
                .AddClassSkill(skill: StatType.SkillStealth)
                .AddComponent<AddTrickstersEdge>(c => { c.Skill = StatType.SkillStealth; })
                .SetIcon(FeatureRefs.SkillFocusStealth.Reference.Get().Icon)
                .Configure();

            BlueprintBuff religionbuff = BuffConfigurator.New(FeatName + "ReligionBuff", Guids.TricksterEdgeLoreReligionBuff)
                .SetDisplayName(ReligionDisplayName)
                .SetDescription(Description)
                .AddClassSkill(skill: StatType.SkillLoreReligion)
                .AddComponent<AddTrickstersEdge>(c => { c.Skill = StatType.SkillLoreReligion; })
                .SetIcon(FeatureRefs.SkillFocusLoreReligion.Reference.Get().Icon)
                .Configure();

            BlueprintBuff naturebuff = BuffConfigurator.New(FeatName + "NatureBuff", Guids.TricksterEdgeLoreNatureBuff)
                .SetDisplayName(NatureDisplayName)
                .SetDescription(Description)
                .AddClassSkill(skill: StatType.SkillLoreNature)
                .AddComponent<AddTrickstersEdge>(c => { c.Skill = StatType.SkillLoreNature; })
                .SetIcon(FeatureRefs.SkillFocusLoreNature.Reference.Get().Icon)
                .Configure();

            BlueprintBuff arcanabuff = BuffConfigurator.New(FeatName + "ArcanaBuff", Guids.TricksterEdgeKnowledgeArcanaBuff)
                .SetDisplayName(ArcanaDisplayName)
                .SetDescription(Description)
                .AddClassSkill(skill: StatType.SkillKnowledgeArcana)
                .AddComponent<AddTrickstersEdge>(c => { c.Skill = StatType.SkillKnowledgeArcana; })
                .SetIcon(FeatureRefs.SkillFocusKnowledgeArcana.Reference.Get().Icon)
                .Configure();

            BlueprintBuff worldbuff = BuffConfigurator.New(FeatName + "WorldBuff", Guids.TricksterEdgeKnowledgeWorldBuff)
                .SetDisplayName(WorldDisplayName)
                .SetDescription(Description)
                .AddClassSkill(skill: StatType.SkillKnowledgeWorld)
                .AddComponent<AddTrickstersEdge>(c => { c.Skill = StatType.SkillKnowledgeWorld; })
                .SetIcon(FeatureRefs.SkillFocusKnowledgeWorld.Reference.Get().Icon)
                .Configure();

            BlueprintBuff perceptionbuff = BuffConfigurator.New(FeatName + "PerceptionBuff", Guids.TricksterEdgePerceptionBuff)
                .SetDisplayName(PerceptionDisplayName)
                .SetDescription(Description)
                .AddClassSkill(skill: StatType.SkillPerception)
                .AddComponent<AddTrickstersEdge>(c => { c.Skill = StatType.SkillPerception; })
                .SetIcon(FeatureRefs.SkillFocusPerception.Reference.Get().Icon)
                .Configure();


            BlueprintBuff persuasionbuff = BuffConfigurator.New(FeatName + "PersuasionBuff", Guids.TricksterEdgePersuasionBuff)
                .SetDisplayName(PersuasionDisplayName)
                .SetDescription(Description)
                .AddClassSkill(skill: StatType.SkillPersuasion)
                .AddComponent<AddTrickstersEdge>(c => { c.Skill = StatType.SkillPersuasion; })
                .SetIcon(FeatureRefs.SkillFocusDiplomacy.Reference.Get().Icon)
                .Configure();


            BlueprintBuff umdbuff = BuffConfigurator.New(FeatName + "UMDBuff", Guids.TricksterEdgeUseMagicDeviceBuff)
                .SetDisplayName(UMDDisplayName)
                .SetDescription(Description)
                .AddClassSkill(skill: StatType.SkillUseMagicDevice)
                .AddComponent<AddTrickstersEdge>(c => { c.Skill = StatType.SkillUseMagicDevice; })
                .SetIcon(FeatureRefs.SkillFocusUseMagicDevice.Reference.Get().Icon)
                .Configure();

            BlueprintAbilityResource TrickstersEdgeResource = AbilityResourceConfigurator.New(FeatName + "Resource", Guids.TricksterEdgeResource)
                .SetMaxAmount(ResourceAmountBuilder.New(1).Build())
                .Configure();


            BlueprintAbility TrickstersEdgeAbility = AbilityConfigurator.New(FeatName + "Ability", Guids.TricksterEdgeAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon("assets/icons/trickstersedge.png")
                .AddAbilityApplyFact(hasDuration: false, restriction: AbilityApplyFact.FactRestriction.CasterHasNoFact, facts: new() { athleticsbuff, mobilitybuff, thieverybuff, stealthbuff, naturebuff, religionbuff, arcanabuff, worldbuff, perceptionbuff, persuasionbuff, umdbuff })
                .AddAbilityResourceLogic(amount: 1, isSpendResource: true, requiredResource: TrickstersEdgeResource)
                .Configure();
            
            FeatureConfigurator.New(FeatName, Guids.TricksterEdge)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { TrickstersEdgeAbility })
                .Configure();
        }   
    }
}
