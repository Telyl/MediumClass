using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using MediumClass.Medium.NewComponents.AbilitySpecific;
using MediumClass.Utilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.Spirits.Trickster
{
    class SurpriseStrike
    {
        private static readonly string FeatName = "TricksterSurpriseStrike";
        private static readonly string DisplayName = "SurpriseStrike.Name";
        private static readonly string Description = "SurpriseStrike.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(SurpriseStrike));

        public static object Value { get; private set; }

        public static void ConfigureEnabled()
        {
            var ImpromptuSurpriseStrikeCooldown = BuffConfigurator.New(FeatName + "ImpromptuCooldown", Guids.TricksterSurpriseStrikeCooldownBuff)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.RemoveOnRest)
                .SetStacking(StackingType.Replace)
                .SetRanks(0)
                .Configure();

            var ImpromptuSurpriseStrike = FeatureConfigurator.New(FeatName + "Impromptu", Guids.TricksterSurpriseStrikeImpromptu)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddComponent<ImpromptuSurpriseStrike>()
                .AddInitiatorAttackWithWeaponTrigger(action: ActionsBuilder.New().ApplyBuffPermanent(ImpromptuSurpriseStrikeCooldown), onlyHit: true)
                .Configure();

            var SurpriseStrike = FeatureConfigurator.New(FeatName, Guids.TricksterSurpriseStrike)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(FeatureRefs.SneakAttack.Reference.Get().Icon)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .SetRanks(10)
                .AddFacts(new() { ImpromptuSurpriseStrike})
                .AddComponent<AddSurpriseStrike>(c =>
                {
                    c.Value = new ContextDiceValue()
                    {
                        DiceType = DiceType.D6,
                        DiceCountValue = ContextValues.Rank(AbilityRankType.Default),
                        BonusValue = 0
                    };
                    c.DamageType = new DamageTypeDescription()
                    {
                        Type = DamageType.Physical,
                        Common =
                        {
                            Precision = true,
                        },
                        Physical =
                        {
                            Form = PhysicalDamageForm.Slashing & PhysicalDamageForm.Piercing & PhysicalDamageForm.Bludgeoning
                        }
                    };
                })
                .Configure();
            FeatureConfigurator.For(SurpriseStrike)
                .AddContextRankConfig(new ContextRankConfig
                {
                    m_Type = AbilityRankType.Default,
                    m_BaseValueType = ContextRankBaseValueType.FeatureRank,
                    m_Feature = SurpriseStrike.ToReference<BlueprintFeatureReference>(),
                    m_Stat = StatType.Unknown,
                    m_Buff = null,
                    m_Progression = ContextRankProgression.AsIs,
                    m_StartLevel = 0,
                    m_StepLevel = 0,
                    m_UseMin = false,
                    m_Min = 0,
                    m_UseMax = false,
                    m_Max = 20,
                    m_ExceptClasses = false,
                    Archetype = null
                })
                .Configure();
        }
        
    }
}
