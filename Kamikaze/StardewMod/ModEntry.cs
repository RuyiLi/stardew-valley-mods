using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework;

namespace StardewMod
{
    public class ModEntry : Mod
    {
        ModConfig config;
        public override void Entry(IModHelper helper)
        {
            ControlEvents.KeyPressed += this.ReceiveKeyPress;
            config = helper.ReadConfig<ModConfig>();
        }

        private void ReceiveKeyPress(object sender, EventArgsKeyPressed e)
        {
            Farmer f = Game1.player;
            if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.LeftShift)
            {
                f.currentLocation.explode(f.getTileLocation(), config.radius, f);
            }

        }
    }

    class ModConfig
    {
        public int radius { get; set; } = 5;
    }
}
