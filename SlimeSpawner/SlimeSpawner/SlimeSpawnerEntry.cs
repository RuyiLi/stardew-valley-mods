using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Monsters;
using Microsoft.Xna.Framework.Input;

namespace SlimeSpawner
{
    public class SlimeSpawnerEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            PlayerEvents.LoadedGame += this.loadSlimeSpawnerMod;
        }

        public void loadSlimeSpawnerMod(object sender, EventArgs e)
        {
            ControlEvents.MouseChanged += this.checkForSlimeAndSpawn;
        }

        public void checkForSlimeAndSpawn(object sender, EventArgsMouseStateChanged e)
        {
            Farmer f = Game1.player;
            if (f.CurrentItem != null)
            {
                if (f.CurrentItem.parentSheetIndex == 766)
                {
                    if (e.NewState.RightButton == ButtonState.Pressed && e.PriorState.RightButton != ButtonState.Pressed)
                    {
                        f.reduceActiveItemByOne();
                        Monster monster = new GreenSlime(f.position);
                        Game1.currentLocation.characters.Add(monster);
                        monster.jump();
                    }
                }
            }
        }
    }
}
