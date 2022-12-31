using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Medium.NewComponents
{
    [TypeId("996f6104335842e08262f14bd153298e")]
    public class AddResourcelessSpell : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCastSpell>, IRulebookHandler<RuleCastSpell>, ISubscriber, IInitiatorRulebookSubscriber
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(AddResourcelessSpell));
        public void OnEventAboutToTrigger(RuleCastSpell evt)
        {
            this.ability = evt.Context.Ability;
            if(this.ability != null) { this.ability.ExtraSpellSlotCost = -1; }
        }

        public void OnEventDidTrigger(RuleCastSpell evt) { }

        public override void OnTurnOff()
        {
            if(this.ability != null) { this.ability.ExtraSpellSlotCost = 0; }
            base.OnTurnOff();
        }
        private AbilityData ability;
    }
}
