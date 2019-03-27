using System.Collections.Generic;
using Frog.Models;
using Frog.Levels;


namespace Frog.Utilities
{
    static class LevelFactory
    {
        public static void LoadLevels(List<Level> LevelList)
        {
            LevelList.Add(new Level1());
            LevelList.Add(new Level2());
            LevelList.Add(new Level3());
        }
    }
}
