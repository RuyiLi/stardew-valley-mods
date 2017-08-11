using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Tools;
using Microsoft.Xna.Framework;

namespace InstantFishing
{
    class ModEntry:Mod
    {
        public override void Entry(IModHelper helper)
        {
            GameEvents.UpdateTick += this.checkForFishing;
        }

        public void checkForFishing(object sender, EventArgs e)
        {
            Farmer f = Game1.player;
            if(f.CurrentTool is FishingRod)
            {
                FishingRod fr = (FishingRod)f.CurrentTool;
                
                if (fr.isNibbling)
                {
                    //int[] fish = { 705, 129, 160, 132, 700, 702, 142, 143, 716, 159, 704, 148, 156, 775, 153, 708, 147, 161, 137, 162, 163, 707, 715, 719, 682, 149, 723, 141, 144, 128, 138, 146, 150, 139, 164, 131, 165, 154, 152, 706, 720, 136, 721, 151, 158, 698, 145, 155, 699, 701, 130, 140, 157, 734 };
                    int[] summer = { 795, 796, 132, 700, 702, 142, 143, 716, 795, 796, 132, 700, 702, 142, 143, 716, 795, 796, 132, 700, 702, 142, 143, 716, 159, 704, 156, 153, 708, 161, 137, 162, 715, 719, 149, 723, 144, 128, 138, 146, 150, 164, 165, 152, 706, 720, 721, 158, 698, 145, 155, 701, 130, 157, 734, 704, 156, 153, 708, 161, 137, 162, 715, 719, 149, 723, 144, 128, 138, 146, 150, 164, 165, 152, 706, 720, 721, 158, 698, 145, 155, 701, 130, 157, 734, 704, 156, 153, 708, 161, 137, 162, 715, 719, 149, 723, 144, 128, 138, 146, 150, 164, 165, 152, 706, 720, 721, 158, 698, 145, 155, 701, 130, 157, 734 };
                    //114                                      159 is crimsonfish, legendary, beach
                    int[] spring = { 795, 796, 129, 132, 700, 702, 142, 143, 716, 148, 156, 153, 708, 147, 161, 137, 162, 795, 796, 129, 132, 700, 702, 142, 143, 716, 148, 156, 153, 708, 147, 161, 137, 162, 795, 796, 129, 132, 700, 702, 142, 143, 716, 148, 156, 153, 708, 147, 161, 137, 162, 163, 715, 719, 723, 164, 131, 165, 152, 706, 720, 136, 721, 158, 145, 157, 734, 715, 719, 723, 164, 131, 165, 152, 706, 720, 136, 721, 158, 145, 157, 734, 715, 719, 723, 164, 131, 165, 152, 706, 720, 136, 721, 158, 145, 157, 734 };
                    //97                                                                                        163 is Legend, mountain lake
                    int[] fall = { 795, 796, 705, 129, 795, 796, 705, 129, 795, 796, 705, 129, 160, 132, 700, 702, 142, 143, 716, 148, 156, 153, 161, 137, 162, 715, 719, 723, 150, 139, 164, 131, 165, 154, 152, 706, 720, 136, 721, 158, 155, 699, 701, 140, 157, 734, 132, 700, 702, 142, 143, 716, 148, 156, 153, 161, 137, 162, 715, 719, 723, 150, 139, 164, 131, 165, 154, 152, 706, 720, 136, 721, 158, 155, 699, 701, 140, 157, 734, 132, 700, 702, 142, 143, 716, 148, 156, 153, 161, 137, 162, 715, 719, 723, 150, 139, 164, 131, 165, 154, 152, 706, 720, 136, 721, 158, 155, 699, 701, 140, 157, 734 };
                    //112                      160 is an angler, legendary, town
                    int[] winter = { 795, 796, 705, 132, 700, 702, 142, 716, 156, 795, 796, 705, 132, 700, 702, 142, 716, 156, 795, 796, 705, 132, 700, 702, 142, 716, 156, 775, 153, 708, 147, 161, 137, 162, 707, 715, 719, 723, 141, 144, 146, 164, 131, 165, 154, 152, 720, 721, 158, 151, 698, 699, 130, 157, 734, 153, 708, 147, 161, 137, 162, 707, 715, 719, 723, 141, 144, 146, 164, 131, 165, 154, 152, 720, 721, 158, 151, 698, 699, 130, 157, 734, 153, 708, 147, 161, 137, 162, 707, 715, 719, 723, 141, 144, 146, 164, 131, 165, 154, 152, 720, 721, 158, 151, 698, 699, 130, 157, 734 };
                    //109                                                775 is glacierfish, legendary, forest
                    int[] sewers = { 682, 142, 142, 142, 142, 142, 142, 142, 142, 142, 142 };
                    //11           Mutant Carp

                    Random rnd = new Random();
                    if (Game1.player.currentLocation.Name == "Sewers")
                    {
                        int toBeChosen = rnd.Next(0, 10);
                        int chosen = sewers[toBeChosen];
                        this.Monitor.Log("" + chosen);
                        fr.pullFishFromWater(chosen, 10, 2, 5, true);
                    }
                    else if (Game1.currentSeason == "summer")
                    {
                        int toBeChosen = rnd.Next(0, 113);
                        int chosen = summer[toBeChosen];
                        this.Monitor.Log("" + chosen);
                        fr.pullFishFromWater(chosen, 10, 2, 5, true);
                    }
                    else if (Game1.currentSeason == "spring")
                    {
                        int toBeChosen = rnd.Next(0, 96);
                        int chosen = spring[toBeChosen];
                        this.Monitor.Log("" + chosen);
                        fr.pullFishFromWater(chosen, 10, 2, 5, true);
                    }
                    else if (Game1.currentSeason == "fall")
                    {
                        int toBeChosen = rnd.Next(0, 111);
                        int chosen = fall[toBeChosen];
                        this.Monitor.Log("" + chosen);
                        fr.pullFishFromWater(chosen, 10, 2, 5, true);
                    }
                    else if (Game1.currentSeason == "winter")
                    {
                        int toBeChosen = rnd.Next(0, 108);
                        int chosen = winter[toBeChosen];
                        this.Monitor.Log("" + chosen);
                        fr.pullFishFromWater(chosen, 10, 2, 5, true);
                    }
                    fr.isNibbling = false;
                }
            }
        }


    }
}
