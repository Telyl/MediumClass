using System;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
	// Token: 0x020022E0 RID: 8928
	[AllowedOn(typeof(BlueprintUnitFact), false)]
	[TypeId("34c60249c85644e0a584c8aa78f004af")]
	public class AddSuddenAttack : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAttacksCount>, IRulebookHandler<RuleCalculateAttacksCount>, ISubscriber, IInitiatorRulebookSubscriber
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(AddSuddenAttack));
		private static BlueprintUnitFact _flurryofblows;
		private static BlueprintUnitFact FlurryOfBlows
		{
			get
			{
				_flurryofblows ??= BlueprintTool.Get<BlueprintUnitFact>(FeatureRefs.FlurryOfBlows.ToString());
				return _flurryofblows;
			}
		}
		private static BlueprintUnitFact _flurryofblows11;
		private static BlueprintUnitFact FlurryOfBlowsLevel11
		{
			get
			{
				_flurryofblows11 ??= BlueprintTool.Get<BlueprintUnitFact>(FeatureRefs.FlurryOfBlowsLevel11.ToString());
				return _flurryofblows11;
			}
		}
		// Token: 0x0600E348 RID: 58184 RVA: 0x003A1A42 File Offset: 0x0039FC42
		public void OnEventAboutToTrigger(RuleCalculateAttacksCount evt)
		{
			if (evt.Reason.Caster.HasFact(FlurryOfBlows)) { return; }
			else if (evt.Reason.Caster.HasFact(FlurryOfBlowsLevel11)) { return; }
			if(!evt.Initiator.State.IsCharging)
			{
				evt.AddExtraAttacks(this.Number, this.Haste, this.Penalized, null);
			}
		}

		// Token: 0x0600E349 RID: 58185 RVA: 0x003A1A5D File Offset: 0x0039FC5D
		public void OnEventDidTrigger(RuleCalculateAttacksCount evt)
		{
		}

		public int Number = 1;
		public bool Haste =  true;
		public bool Penalized = false;
	}
}
