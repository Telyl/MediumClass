using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Mechanics.Actions;
using MediumClass.Medium.NewUnitParts;
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
    [TypeId("3e00ee2adf5c4e879ca6503591552251")]
    public class ContextActionSpiritInfluence : ContextAction
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(ContextActionSpiritInfluence));
        public override string GetCaption()
        {
            return string.Format("Adds influence penalty when influence gets too low.");
        }

        public override void RunAction()
        {
            UnitEntityData maybeCaster = base.Context.MaybeCaster;
            if (maybeCaster == null)
            {
                LogChannel.Default.Error(this, "ContextActionMediumInfluence: target is null", Array.Empty<object>());
                return;
            }
            UnitPartMedium unitPartMedium = maybeCaster.Get<UnitPartMedium>();
            if(unitPartMedium != null)
            {
                unitPartMedium.HandleInfluencePenalty();
            }
        }
    }
}
