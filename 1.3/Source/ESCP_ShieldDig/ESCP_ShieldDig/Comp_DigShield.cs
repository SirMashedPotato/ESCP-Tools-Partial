using System;
using Verse;
using RimWorld;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace ESCP_ShieldDig
{
    class Comp_DigShield : ThingComp
    {

        public CompProperties_DigShield Props
        {
            get
            {
                return (CompProperties_DigShield)this.props;
            }
        }

        public override IEnumerable<Gizmo> CompGetWornGizmosExtra()
        {
            Pawn p = this.parent as Pawn;
            Hediff h = p.health.hediffSet.GetFirstHediffOfDef(Props.plantedHediff);
            if (h == null)
            {
                yield return new Command_Action
                {
                    defaultLabel = "ESCP_Shields_ImplantShield".Translate(this.parent.Label),
                    defaultDesc = "ESCP_Shields_ImplantShield_Tooltip".Translate(),
                    icon = ContentFinder<Texture2D>.Get(Props.plantIconPath, true),
                    action = delegate ()
                    {
                        p.health.AddHediff(Props.plantedHediff);
                    }
                };
            }
            else
            {
                yield return new Command_Action
                {
                    defaultLabel = "ESCP_Shields_RemoveShield".Translate(this.parent.Label),
                    defaultDesc = "ESCP_Shields_RemoveShield_Tooltip".Translate(),
                    icon = ContentFinder<Texture2D>.Get(Props.plantIconPath, true),
                    action = delegate ()
                    {

                        p.health.RemoveHediff(h);
                    }
                };
            }
        }

        public override void Notify_Equipped(Pawn pawn)
        {
            base.Notify_Equipped(pawn);
        }

        public override void Notify_Unequipped(Pawn pawn)
        {
            Hediff h = pawn.health.hediffSet.GetFirstHediffOfDef(Props.plantedHediff);
            if (h != null)
            {
                pawn.health.RemoveHediff(h);
            }
            base.Notify_Unequipped(pawn);
        }
    }
}
