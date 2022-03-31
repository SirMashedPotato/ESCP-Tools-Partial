using System;
using Verse;
using RimWorld;
using System.Linq;
using System.Collections.Generic;

namespace ESCP_ShieldDig
{
    class CompProperties_DigShield : CompProperties
    {

        public CompProperties_DigShield()
        {
            this.compClass = typeof(Comp_DigShield);
        }

        public HediffDef plantedHediff;
        public string plantIconPath;
        public string removeIconPath;
    }
}
