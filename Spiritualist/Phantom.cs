using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using MediumClass.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Utilities;
using static Kingmaker.Blueprints.BlueprintUnit;

namespace MediumClass.Spiritualist
{
    class Phantom
    {
        static public BlueprintCharacterClass phantom_class;

        public static void ConfigureEnabled()
        {
            /*createAnger();
            createDespair();
            createHatred();
            createFear();
            createZeal();
            createKindness();*/
        }

        public void CreatePhantom(string name, string display_name, string descripton, UnityEngine.Sprite icon,
                                  BlueprintArchetype archetype,
                                  BlueprintFeature feature1, BlueprintFeature feature7, BlueprintFeature feature12, BlueprintFeature feature17,
                                  StatType[] skills, int str_value, int dex_value,
                                  BlueprintAbility[] spell_like_abilities,
                                  BlueprintFeature exciter_feature1 = null, BlueprintFeature exciter_feature2 = null,
                                  BlueprintAbility[] emotion_conduit_spells = null)
        {
            var armor_2 = BlueprintTool.Get<BlueprintUnitFact>("45a52ce762f637f4c80cc741c91f58b7");
            var ghost_fx = BlueprintTool.Get<BlueprintBuff>("20f79fea035330b479fc899fa201d232");

            UnitBody body = new UnitBody();
            body.EmptyHandWeapon = BlueprintTool.Get<BlueprintItemWeapon>("767e6932882a99c4b8ca95c88d823137");
            body.PrimaryHand = null;
            body.SecondaryHand = null;
            body.m_AdditionalLimbs= new BlueprintItemWeaponReference[0];
            body.m_AdditionalSecondaryLimbs = new BlueprintItemWeaponReference[0];
            body.DisableHands = false;

            UnitConfigurator.New("PhantomUnit", Guids.PhantomUnit)
                .SetAlignment(Kingmaker.Enums.Alignment.TrueNeutral)
                .SetStrength(10)
                .SetDexterity(10)
                .SetConstitution(13)
                .SetIntelligence(7)
                .SetWisdom(10)
                .SetCharisma(13)
                .SetSpeed(Kingmaker.Utility.FeetExtension.Feet(30))
                .AddFacts(new() { armor_2, ghost_fx })
                .SetBody(body)
                .AddComponent<AddClassLevels>(a =>
                {
                    a.DoNotApplyAutomatically = true;
                    a.Levels = 0;
                    a.m_Archetypes = new BlueprintArchetypeReference[] { archetype.ToReference<BlueprintArchetypeReference>() };
                    a.CharacterClass = phantom_class;
                    a.Skills = new StatType[] { StatType.SkillAthletics, StatType.SkillMobility, StatType.SkillStealth, StatType.SkillPersuasion, StatType.SkillKnowledgeArcana }.RemoveFromArray(skills[0]).RemoveFromArray(skills[1]);
                    a.Selections = new SelectionEntry[0];
                    a.m_MemorizeSpells = new BlueprintAbilityReference[0];
                    a.m_SelectSpells = new BlueprintAbilityReference[0];
                })
                .Configure();
        }

        /*static void createPhantomClass()
        {
            var druid_class = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var fighter_class = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("48ac8db94d5de7645906c7d0ad3bcfbd");
            var animal_class = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("4cd1757a0eea7694ba5c933729a53920");

            phantom_class = Helpers.Create<BlueprintCharacterClass>();
            phantom_class.name = "PhantomClass";
            library.AddAsset(phantom_class, "");


            phantom_class.LocalizedName = Helpers.CreateString("Phantom.Name", "Phantom");
            phantom_class.LocalizedDescription = Helpers.CreateString("Phantom.Description",
                "A phantom was once a sentient, living creature that experienced great turmoil in life or during death. The power of its emotional trauma ripped it from the flow of spirits rushing toward the Astral Plane and the fates beyond, pulling it through the Ethereal Plane and toward the Negative Energy Plane. During the decent to nothingness and undeath, the spirit was able to break free, and made its way back to the Material Plane to find shelter within the consciousness of a powerful psychic spellcaster. That fusion created a spiritualist.\n"
                + "Phantoms may retain some of their memories from life, but not many. Some phantoms wish to unburden themselves of their emotional shackles, while others just wish to continue existing while avoiding the corruption of undeath. Others still wish nothing more than to inflict their torment upon the living—taking their revenge on life for the horrors they faced during and after death.\n"
                + "Phantoms are powerful beings, but they are far more emotional than they are rational. Phantoms are still shackled by the emotions that created them, and spiritualists must maintain strong control over their phantoms to keep the phantom’s often-violent emotions in check."
                );
            phantom_class.m_Icon = druid_class.Icon;
            phantom_class.SkillPoints = druid_class.SkillPoints - 1;
            phantom_class.HitDie = DiceType.D10;
            phantom_class.BaseAttackBonus = fighter_class.BaseAttackBonus;
            phantom_class.FortitudeSave = druid_class.ReflexSave;
            phantom_class.ReflexSave = druid_class.ReflexSave;
            phantom_class.WillSave = druid_class.ReflexSave;
            phantom_class.Spellbook = null;
            phantom_class.ClassSkills = new StatType[] { StatType.SkillPersuasion, StatType.SkillPerception, StatType.SkillLoreReligion, StatType.SkillStealth };
            phantom_class.IsDivineCaster = false;
            phantom_class.IsArcaneCaster = false;
            phantom_class.StartingGold = fighter_class.StartingGold;
            phantom_class.PrimaryColor = fighter_class.PrimaryColor;
            phantom_class.SecondaryColor = fighter_class.SecondaryColor;
            phantom_class.RecommendedAttributes = new StatType[0];
            phantom_class.NotRecommendedAttributes = new StatType[0];
            phantom_class.EquipmentEntities = animal_class.EquipmentEntities;
            phantom_class.MaleEquipmentEntities = animal_class.MaleEquipmentEntities;
            phantom_class.FemaleEquipmentEntities = animal_class.FemaleEquipmentEntities;
            phantom_class.ComponentsArray = new BlueprintComponent[] { Helpers.PrerequisiteClassLevel(phantom_class, 1) };
            phantom_class.StartingItems = animal_class.StartingItems;
            createPhantomProgression();
            phantom_class.Progression = phantom_progression;

            phantom_class.Archetypes = new BlueprintArchetype[] { };
            Helpers.RegisterClass(phantom_class);
        }*/

    }
}
