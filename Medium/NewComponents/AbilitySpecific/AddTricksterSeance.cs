using System;
using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.EventConditionActionSystem.Events;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.Utility;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
	// Token: 0x020022E0 RID: 8928
	[AllowedOn(typeof(BlueprintUnitFact), false)]
	[TypeId("a190c88a901a43ca8753ddc0391388cd")]
	public class AddTricksterSeance : UnitFactComponentDelegate, ISubscriber, IInitiatorRulebookSubscriber, IUnitSubscriber
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(AddTricksterSeance));

		public override void OnTurnOn()
		{
			if (!Skill.IsSkill()) { return; }
			ModifiableValue stat = base.Owner.Stats.GetStat(Skill);
			int OriginalValue = stat.BaseValue;
			int BuffValue = 1;
			if (OriginalValue == 0)
			{
				BuffValue += 3; // Class Skill Bonus, because why not.
			}

			stat.AddModifier((int)BuffValue, base.Runtime, ModifierDescriptor.BaseStatBonus);
		}

		public override void OnTurnOff()
		{
			ModifiableValue stat = base.Owner.Stats.GetStat(Skill);
			if (stat == null)
			{
				return;
			}
			stat.RemoveModifiersFrom(base.Runtime);
		}

		public StatType Skill;
	}
}
