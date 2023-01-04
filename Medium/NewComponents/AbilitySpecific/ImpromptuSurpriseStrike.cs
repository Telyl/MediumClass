using System;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using MediumClass.Utilities;
using MediumClass.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
	// Token: 0x020021F7 RID: 8695
	[AllowedOn(typeof(BlueprintUnitFact), false)]
	[TypeId("a1918204526146b5bb2ad32ab8d95e97")]
	public class ImpromptuSurpriseStrike : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IRulebookHandler<RuleAttackWithWeapon>, ISubscriber, IInitiatorRulebookSubscriber
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(ImpromptuSurpriseStrike));
		private static BlueprintUnitFact _cooldownSurpriseStrike;
		private static BlueprintUnitFact CooldownSurpriseStrike
		{
			get
			{
				_cooldownSurpriseStrike ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.TricksterSurpriseStrikeCooldownBuff);
				return _cooldownSurpriseStrike;
			}
		}

		public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
		{
			if(evt.Target.HasFact(CooldownSurpriseStrike)) { return; }
			evt.ForceFlatFooted = true;
		}

		// Token: 0x0600DFBA RID: 57274 RVA: 0x003965B0 File Offset: 0x003947B0
		public void OnEventDidTrigger(RuleAttackWithWeapon evt)
		{
		}
	}
}