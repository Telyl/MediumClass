using System;
using System.Collections.Generic;
using Kingmaker;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using MediumClass.Utils;
using Owlcat.Runtime.Core.Utils;
using UnityEngine;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents.AbilitySpecific
{
	// Token: 0x02001DA8 RID: 7592
	[TypeId("aa47248c0ff84d7a88bda926ec7bc6ce")]
	public class ContextActionTransferMagic : ContextAction
	{
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(ContextActionTransferMagic));
		// Token: 0x0600C9BE RID: 51646 RVA: 0x003462C0 File Offset: 0x003444C0
		public override string GetCaption()
		{
			return "Remove all specified buffs and apply them to caster";
		}

		// Token: 0x0600C9BF RID: 51647 RVA: 0x003462C8 File Offset: 0x003444C8
		public override void RunAction()
		{
			if (base.Target.Unit == null)
			{
				PFLog.Default.Error("Target unit is missing", Array.Empty<object>());
				return;
			}
			UnitEntityData maybeCaster = base.Context.MaybeCaster;
			if (maybeCaster == null)
			{
				PFLog.Default.Error("Caster is missing", Array.Empty<object>());
				return;
			}
			Logger.Log("Getting buffs!");
			List<Buff> list = base.Target.Unit.Buffs.Enumerable.ToTempList<Buff>();
			foreach (Buff buff in list)
			{
				Logger.Log("Adding buffs to list.");
				base.Target.Unit.Buffs.RemoveFact(buff);
				if (string.IsNullOrEmpty(buff.SourceAreaEffectId))
				{
					Buff buff3 = maybeCaster.Buffs.AddBuff(buff.Blueprint, buff.Context.ParentContext, (!buff.IsPermanent) ? new TimeSpan?(buff.TimeLeft) : null);
					if (buff3 != null && buff.IsPermanent)
					{
						buff3.MakePermanent();
					}
				}
				return;
			}
		}

		// Token: 0x0400866C RID: 34412
		[SerializeField]
		[InfoBox("Target buff should have at least one of the descriptors to be stolen")]
		//public SpellDescriptorWrapper descriptor = (SpellDescriptor)432345564227567616;
		public SpellDescriptorWrapper descriptor = (SpellDescriptor) SpellDescriptor.None;
	}
}
