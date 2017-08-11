using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.TerrainFeatures;

namespace AutoWaterSMAPI
{
    public class AutoWaterEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            PlayerEvents.LoadedGame += this.waterAllFields;
            TimeEvents.DayOfMonthChanged += this.waterAllFields;
        }

        private void waterAllFields(object sender, EventArgs e)
        {
            GameLocation farm = Game1.getFarm();
            int watered = 0;
            foreach (var tf in farm.terrainFeatures)
            {
                TerrainFeature feature = tf.Value;
                if (feature is HoeDirt)
                {
                    this.Monitor.Log("test");
                    watered++;
                    StardewValley.Tools.WateringCan wc = Game1.player.items.OfType<StardewValley.Tools.WateringCan>().FirstOrDefault();
                    feature.performToolAction(wc, 0, tf.Key);
                }
            }
            this.Monitor.Log("Watered " + watered + " tiles");
        }
    }
}
