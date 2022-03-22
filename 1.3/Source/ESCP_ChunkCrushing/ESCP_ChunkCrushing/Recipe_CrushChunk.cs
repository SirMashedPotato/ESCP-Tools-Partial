using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
using System.Reflection;

namespace ESCP_ChunkCrushing
{


    [StaticConstructorOnStartup]
    public static class Main
    {
        public static List<ESCP_ChunkCrushing.CrushableOutputDef> defs = new List<CrushableOutputDef> { };

        static Main()
        {
            defs = DefDatabase<ESCP_ChunkCrushing.CrushableOutputDef>.AllDefsListForReading;
        }
    }

    public class Recipe_CrushChunk : RecipeWorker
    {
        public override void ConsumeIngredient(Thing ingredient, RecipeDef recipe, Map map)
        {
            if (Main.defs.NullOrEmpty())
            {
                Log.Error("No ESCP_ChunkCrushing.CrushableOutputDef found!");
            }
            else
            {
                //getting the def
                ESCP_ChunkCrushing.CrushableOutputDef output = Main.defs.RandomElementByWeightWithFallback(x => x.chance * 100, Main.defs[0]);
                /* Spawning items
                 * done this way if count is larger than stack size
                 */

                int num = output.outputRange.RandomInRange;
                int limit = output.outputThingDef.stackLimit;

                while (num != 0)
                {
                    Thing thing = ThingMaker.MakeThing(output.outputThingDef);
                    if (num >= limit)
                    {
                        thing.stackCount = limit;
                        num -= limit;
                    }
                    else
                    {
                        thing.stackCount = num;
                        num = 0;
                    }
                    GenPlace.TryPlaceThing(thing, ingredient.Position, map, ThingPlaceMode.Near, null, null, default(Rot4));
                }

            }

            //do the vanilla shite
            base.ConsumeIngredient(ingredient, recipe, map);
        }
    }
}
