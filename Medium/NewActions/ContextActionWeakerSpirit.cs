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
    [TypeId("f9a7102d-bd78-4b62-9d8e-582505efd277")]
    public class ContextActionWeakerSpirit : ContextAction
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(ContextActionWeakerSpirit));
        public override string GetCaption()
        {
            return string.Format("Adds weaker spirit channel");
        }

        public override void RunAction()
        {
            UnitEntityData maybeCaster = base.Context.MaybeCaster;
            if (maybeCaster == null)
            {
                LogChannel.Default.Error(this, "ContextActionWeakerSpirit: target is null", Array.Empty<object>());
                return;
            }
            UnitPartMedium unitPartMedium = maybeCaster.Get<UnitPartMedium>();
            Logger.Log($"RunAction ContextActionWeakerSpirit");
            if (unitPartMedium != null)
            {
                return;
                //unitPartMedium.AddWeakerSpiritChannel(base.Context);
            }
        }
    }
}
