using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework;

namespace Emotes
{
    class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            ControlEvents.KeyPressed += this.emote;
        }

        private void emote(object sender, EventArgsKeyPressed e)
        {
            Farmer f = Game1.player;
            if(e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.F1)
            {
                f.doEmote(Character.angryEmote);
            }
            else if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.F2)
            {
                f.doEmote(Character.blushEmote);
            }
            else if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.F3)
            {
                f.doEmote(Character.exclamationEmote);
            }
            else if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.F4)
            {
                f.doEmote(Character.happyEmote);
            }
            else if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.F5)
            {
                f.doEmote(Character.heartEmote);
            }
            else if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.F6)
            {
                f.doEmote(Character.musicNoteEmote);
            }
            else if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.F7)
            {
                f.doEmote(Character.questionMarkEmote);
            }
            else if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.F8)
            {
                f.doEmote(Character.sadEmote);
            }
            else if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.F9)
            {
                f.doEmote(Character.sleepEmote);
            }
            else if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.F10)
            {
                f.doEmote(Character.videoGameEmote);
            }
            else if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.F11)
            {
                f.doEmote(Character.xEmote);
            }
            else if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.F12)
            {
                f.doEmote(Character.pauseEmote);
            }
        }
    }
}
