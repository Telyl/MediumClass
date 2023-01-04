using System;
using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers;
using Kingmaker.Designers.EventConditionActionSystem.Events;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Abilities;
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
	[TypeId("f09ba16fcc8f4c4aa918a5fd683af676")]
	public class AddSharedSeance : UnitFactComponentDelegate, ISubscriber, IInitiatorRulebookSubscriber, IUnitSubscriber
	{
		private static readonly ModLogger Logger = Logging.GetLogger(nameof(AddSharedSeance));
        
        private static BlueprintUnitFact _archmageSeance;
        private static BlueprintUnitFact ArchmageSeance
        {
            get
            {
                _archmageSeance ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.ArchmageSeanceBoon);
                return _archmageSeance;
            }
        }

        private static BlueprintUnitFact _championSeance;
        private static BlueprintUnitFact ChampionSeance
        {
            get
            {
                _championSeance ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.ChampionSeanceBoon);
                return _championSeance;
            }
        }

        private static BlueprintUnitFact _guardianSeance;
        private static BlueprintUnitFact GuardianSeance
        {
            get
            {
                _guardianSeance ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.GuardianSeanceBoon);
                return _guardianSeance;
            }
        }

        private static BlueprintUnitFact _hierophantSeance;
        private static BlueprintUnitFact HierophantSeance
        {
            get
            {
                _hierophantSeance ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.HierophantSeanceBoon);
                return _hierophantSeance;
            }
        }

        private static BlueprintUnitFact _marshalSeance;
        private static BlueprintUnitFact MarshalSeance
        {
            get
            {
                _marshalSeance ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.MarshalSeanceBoon);
                return _marshalSeance;
            }
        }

        private static BlueprintUnitFact _tricksterSeance;
        private static BlueprintUnitFact TricksterSeance
        {
            get
            {
                _tricksterSeance ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.TricksterSeanceBoon);
                return _tricksterSeance;
            }
        }

        public override void OnTurnOn()
        {
            UnitEntityData medium = base.Owner;
            if(medium.Progression.CharacterLevel < 2) { return; }
            foreach (UnitEntityData unitEntityData in GameHelper.GetTargetsAround(base.Owner.Position, FeetExtension.Feet(200), false, true))
            {
                if (unitEntityData != base.Owner && !unitEntityData.IsEnemy(base.Owner))
                {
                    if (medium.Descriptor.HasFact(ArchmageSeance)) { unitEntityData.AddFact(ArchmageSeance); }
                    if (medium.Descriptor.HasFact(ChampionSeance)) { unitEntityData.AddFact(ChampionSeance); }
                    if (medium.Descriptor.HasFact(GuardianSeance)) { unitEntityData.AddFact(GuardianSeance); }
                    if (medium.Descriptor.HasFact(HierophantSeance)) { unitEntityData.AddFact(HierophantSeance); }
                    if (medium.Descriptor.HasFact(MarshalSeance)) { unitEntityData.AddFact(MarshalSeance); }
                    if (medium.Descriptor.HasFact(TricksterSeance)) { unitEntityData.AddFact(TricksterSeance); }
                }
            }
        }

        public override void OnTurnOff()
        {
            if (base.Owner.HasFact(BlueprintTool.Get<BlueprintUnitFact>(Guids.MediumSharedSeance))) {
                return; }
            base.Owner.RemoveFact(ArchmageSeance);
            base.Owner.RemoveFact(ChampionSeance);
            base.Owner.RemoveFact(GuardianSeance);
            base.Owner.RemoveFact(HierophantSeance);
            base.Owner.RemoveFact(MarshalSeance);
            base.Owner.RemoveFact(TricksterSeance);
        }
	}
}
