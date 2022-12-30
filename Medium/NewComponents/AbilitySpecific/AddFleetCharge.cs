using System;
using System.Collections.Generic;
using System.Linq;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Armies.TacticalCombat;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using MediumClass.Utils;
using Owlcat.Runtime.Core.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
	// Token: 0x020022E0 RID: 8928
	[AllowedOn(typeof(BlueprintUnitFact), false)]
	[TypeId("1afcf58dcec74e45947dda5a4d4e821d")]
	public class AddFleetCharge : ContextActionMeleeAttack
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(AddFleetCharge));
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
		public override string GetCaption()
		{
			return "Caster melee attack " + (this.SelectNewTarget ? "(change target)" : "");
		}
		public override void RunAction()
		{
			UnitEntityData maybeCaster = base.Context.MaybeCaster;
			if (maybeCaster == null)
			{
				PFLog.Default.Error("Caster is missing", Array.Empty<object>());
				return;
			}
			WeaponSlot threatHandMelee = maybeCaster.GetThreatHandMelee();
			if (threatHandMelee == null)
			{
				PFLog.Default.Error("Caster can't make melee attack", Array.Empty<object>());
				return;
			}
			UnitEntityData maybeCaster2 = base.Context.MaybeCaster;
			float meters = threatHandMelee.Weapon.AttackRange.Meters;
			bool selectNewTarget = this.SelectNewTarget;
			TargetWrapper target = base.Target;
			UnitEntityData unitEntityData = ContextActionMeleeAttack.SelectTarget(maybeCaster2, meters, selectNewTarget, (target != null) ? target.Unit : null);
			if (unitEntityData != null)
			{
				RuleCalculateAttacksCount ruleCalculateAttacksCount = Rulebook.Trigger<RuleCalculateAttacksCount>(new RuleCalculateAttacksCount(maybeCaster));
				int num = 0;
				List<UnitAttack.AttackInfo> list = UnitAttack.EnumerateAttacks(ruleCalculateAttacksCount).ToTempList<UnitAttack.AttackInfo>();
				List<WeaponSlot> list2 = maybeCaster.Body.AdditionalLimbs.Where((WeaponSlot h) => h.HasWeapon && h.HasItem).ToTempList<WeaponSlot>();
				int num2 = list.Count + list2.Count - 1;
				foreach (UnitAttack.AttackInfo attackInfo in list)
				{
					this.RunAttackRule(maybeCaster, unitEntityData, attackInfo.Hand, attackInfo.AttackBonusPenalty, num, num2);
					num++;
				}
				using (List<WeaponSlot>.Enumerator enumerator2 = list2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						WeaponSlot weaponSlot = enumerator2.Current;
						this.RunAttackRule(maybeCaster, unitEntityData, weaponSlot, 0, num, num2);
						num++;
					}
					return;
				}
			}
		}
	}
}
