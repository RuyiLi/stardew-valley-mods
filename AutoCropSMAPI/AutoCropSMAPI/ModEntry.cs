using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework;
using StardewValley.TerrainFeatures;

namespace AutoCropSMAPI
{
    class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            ControlEvents.KeyPressed += this.ReceiveKeyPress;
        }

        private void ReceiveKeyPress(object sender, EventArgsKeyPressed e)
        {
            this.Monitor.Log($"Player pressed {e.KeyPressed}.");
            if(e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.G)
            {
                GameLocation farm = Game1.player.currentLocation;
                Crop c;
                foreach(var tf in farm.terrainFeatures)
                {
                    TerrainFeature feature = tf.Value;
                    if (feature is HoeDirt && (feature as HoeDirt).crop != null)
                    {
                        c = (feature as HoeDirt).crop;
                        c.growCompletely();
                    }
                }
            }
        }
    }
}
