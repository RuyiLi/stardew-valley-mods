using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;
using StardewValley.BellsAndWhistles;
using StardewValley.Monsters;

namespace SMAPITrailMod
{
    class TrailMod : Mod
    {
        bool on = true;
        public override void Entry(IModHelper helper)
        {
            PlayerEvents.LoadedGame += this.registerTickEvent;
        }

        public void registerTickEvent(object sender, EventArgs e)
        {
            GameEvents.SecondUpdateTick += this.trail;
            ControlEvents.KeyPressed += this.setTrailState;
        }

        public void setTrailState(object sender, EventArgsKeyPressed e)
        {
            if(e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.T)
                on = !on;
        }

        public void trail(object sender, EventArgs e)
        {
            if (on)
            {
                Farmer f = Game1.player;
                Monster monster = new Duggy(f.position);
                NPC npc = Game1.getCharacterFromName("Pierre");
                npc.showTextAboveHead("Test");
                Game1.currentLocation.characters.Add(monster);
                monster.deathAnimation();
                Game1.currentLocation.characters.Remove(monster);
            }
        }
    }
}
