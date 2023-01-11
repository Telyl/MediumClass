using System;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UI.Models.Log;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using MediumClass.Medium.NewUnitParts;
using MediumClass.Utilities;
using MediumClass.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{

	[AllowedOn(typeof(BlueprintUnitFact), false)]
	[AllowMultipleComponents]
	[TypeId("995a5492-43e5-48df-bfbf-9ef6d7eab94b")]
	public class CheckWildArcana : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCastSpell>, IRulebookHandler<RuleCastSpell>, ISubscriber, IInitiatorRulebookSubscriber
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(CheckWildArcana));

		public void OnEventAboutToTrigger(RuleCastSpell evt)
		{
		}

		public void OnEventDidTrigger(RuleCastSpell evt)
		{

		}
	}
}
