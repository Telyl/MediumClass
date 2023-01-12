using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;
using MediumClass.Medium.NewUnitParts;
using MediumClass.Utilities;
using MediumClass.Utils;
using Owlcat.Runtime.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewActions
{
    [TypeId("4113da50-006a-40d5-b100-11450fe90159")]
    public class ContextActionApplySpirit : ContextAction
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(ContextActionApplySpirit));
        public override string GetCaption()
        {
            return string.Format("Sets the primary or secondary spirit.");
        }

        public override void RunAction()
        {
            UnitEntityData maybeCaster = base.Context.MaybeCaster;
            if (maybeCaster == null)
            {
                LogChannel.Default.Error(this, "ContextActionApplySpirit: target is null", Array.Empty<object>());
                return;
            }
            UnitPartMedium unitPartMedium = maybeCaster.Get<UnitPartMedium>();
            if (unitPartMedium != null)
            {
                unitPartMedium.PrimarySpirit = Spirit;
                BlueprintBuff buff = BlueprintTool.Get<BlueprintBuff>(Guids.MediumChannelSpiritPrimarySpiritBuff);
                maybeCaster.Buffs.AddBuff(buff, base.Context, null);
            }
        }

        public BlueprintCharacterClassReference Spirit;
 
    }
}
