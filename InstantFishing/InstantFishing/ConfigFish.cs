﻿using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;

namespace InstantFishing
{
    class ConfigFish
    {
        public Dictionary<string, Dictionary<int, FishData>> PossibleFish { get; set; }

        public void populateData()
        {
            ModEntry.INSTANCE.Monitor.Log("Automatically populating fish.json with data from Fish.xnb and Locations.xnb", StardewModdingAPI.LogLevel.Info);
            ModEntry.INSTANCE.Monitor.Log("NOTE: If either of these files are modded, the config will reflect the changes! However, legendary fish and fish in the UndergroundMine are not being pulled from those files due to technical reasons.", StardewModdingAPI.LogLevel.Info);

            Dictionary<int, string> fish = Game1.content.Load<Dictionary<int, string>>("Data\\Fish");
            Dictionary<string, string> locations = Game1.content.Load<Dictionary<string, string>>("Data\\Locations");

            this.PossibleFish = this.PossibleFish ?? new Dictionary<string, Dictionary<int, FishData>>();

            foreach (KeyValuePair<string, string> locationKV in locations)
            {
                string location = locationKV.Key;
                string[] locData = locationKV.Value.Split('/');
                const int offset = 4;

                Dictionary<int, FishData> possibleFish = this.PossibleFish.ContainsKey(location) ? this.PossibleFish[location] : new Dictionary<int, FishData> { };
                this.PossibleFish[location] = possibleFish;

                for (int i = 0; i <= 3; i++)
                {
                    Season s = Season.SPRINGSUMMERFALLWINTER;
                    switch (i)
                    {
                        case 0:
                            s = Season.SPRING;
                            break;
                        case 1:
                            s = Season.SUMMER;
                            break;
                        case 2:
                            s = Season.FALL;
                            break;
                        case 3:
                            s = Season.WINTER;
                            break;
                    }

                    string[] seasonData = locData[offset + i].Split(' ');
                    for (int j = 0; j < seasonData.Length; j += 2)
                    {
                        if (seasonData.Length <= j + 1)
                            break;

                        int id = Convert.ToInt32(seasonData[j]);

                        // From location data
                        WaterType water = Helpers.convertWaterType(Convert.ToInt32(seasonData[j + 1])) ?? WaterType.BOTH;

                        // From fish data
                        FishData f;
                        if (possibleFish.TryGetValue(id, out f))
                        {
                            f.WaterType |= water;
                            f.Season |= s;
                        }
                        else if (fish.ContainsKey(id))
                        {
                            string[] fishInfo = fish[id].Split('/');
                            if (fishInfo[1] == "5") // Junk item
                                continue;

                            string[] times = fishInfo[5].Split(' ');
                            string weather = fishInfo[7].ToLower();
                            int minDepth = Convert.ToInt32(fishInfo[9]);
                            int minLevel = Convert.ToInt32(fishInfo[12]);
                            double chance = Convert.ToDouble(fishInfo[10]);

                            Weather w = Weather.BOTH;
                            switch (weather)
                            {
                                case "sunny":
                                    w = Weather.SUNNY;
                                    break;
                                case "rainy":
                                    w = Weather.RAINY;
                                    break;
                            }

                            f = new FishData(chance, water, s, Convert.ToInt32(times[0]), Convert.ToInt32(times[1]), minDepth, minLevel, w);
                            possibleFish[id] = f;
                        }
                        else
                        {
                            ModEntry.INSTANCE.Monitor.Log("A fish listed in Locations.xnb cannot be found in Fish.xnb! Make sure those files aren't corrupt. ID: " + id, LogLevel.Warn);
                        }
                    }
                }
            }

            // NOW THEN, for the special cases >_>

            // Glacierfish
            this.PossibleFish["Forest"][775] = new FishData(.02, WaterType.RIVER, Season.WINTER, maxTime: 2000, minDepth: 5, minLevel: 6);

            // Crimsonfish
            this.PossibleFish["Beach"][159] = new FishData(.02, WaterType.BOTH, Season.SUMMER, maxTime: 2000, minDepth: 4, minLevel: 5);

            // Legend
            this.PossibleFish["Mountain"][163] = new FishData(.02, WaterType.LAKE, Season.SPRING, maxTime: 2300, minDepth: 5, minLevel: 10, weather: Weather.RAINY);

            // Angler
            this.PossibleFish["Town"][160] = new FishData(.02, WaterType.BOTH, Season.FALL, minDepth: 4, minLevel: 3);

            // Mutant Carp
            this.PossibleFish["Sewer"][682] = new FishData(.02, WaterType.BOTH, Season.SPRINGSUMMERFALLWINTER, minDepth: 5);

            // UndergroundMine
            this.PossibleFish["UndergroundMine"][158] = new FishData(0.02, WaterType.BOTH, Season.SPRINGSUMMERFALLWINTER, mineLevel: 0);
            this.PossibleFish["UndergroundMine"][158] = new FishData(0.02, WaterType.BOTH, Season.SPRINGSUMMERFALLWINTER, mineLevel: 20);
            this.PossibleFish["UndergroundMine"][161] = new FishData(0.015, WaterType.BOTH, Season.SPRINGSUMMERFALLWINTER, mineLevel: 60);
            this.PossibleFish["UndergroundMine"][162] = new FishData(0.01, WaterType.BOTH, Season.SPRINGSUMMERFALLWINTER, mineLevel: 100);
        }

        public class FishData
        {
            //public string Name { get; set; }
            public double Chance { get; set; }
            public int MinCastDistance { get; set; }
            public WaterType WaterType { get; set; }
            public int MinTime { get; set; }
            public int MaxTime { get; set; }
            public Season Season { get; set; }
            public int MinLevel { get; set; }
            public Weather Weather { get; set; }
            public int MineLevel { get; set; }

            public FishData(double chance, WaterType waterType, Season season, int minTime = 600, int maxTime = 2600, int minDepth = 0, int minLevel = 0, Weather weather = Weather.BOTH, int mineLevel = -1)
            {
                this.Chance = chance;
                this.WaterType = waterType;
                this.Season = season;
                this.MinTime = minTime;
                this.MaxTime = maxTime;
                this.MinCastDistance = minDepth;
                this.MinLevel = minLevel;
                this.Weather = weather;
                this.MineLevel = mineLevel;
            }

            public bool meetsCriteria(WaterType waterType, Season season, Weather weather, int time, int depth, int level)
            {
                return (this.WaterType & waterType) > 0 && (this.Season & season) > 0 && (this.Weather & weather) > 0 && this.MinTime <= time && this.MaxTime >= time && depth >= this.MinCastDistance && level >= this.MinLevel;
            }

            public bool meetsCriteria(WaterType waterType, Season season, Weather weather, int time, int depth, int level, int mineLevel)
            {
                return this.meetsCriteria(waterType, season, weather, time, depth, level) && (this.MineLevel == -1 || mineLevel == this.MineLevel);
            }

            public virtual float getWeightedChance(int depth, int level)
            {
                if (this.MinCastDistance >= 5) return (float)this.Chance + level / 50f;
                return (float)(5 - depth) / (5 - this.MinCastDistance) * (float)this.Chance + level / 50f;
            }

            public override string ToString()
            {
                return string.Format("Chance: {1}, Weather: {2}, Season: {3}", Chance.ToString(), Weather.ToString(), Season.ToString());
            }
        }
    }
}
}
