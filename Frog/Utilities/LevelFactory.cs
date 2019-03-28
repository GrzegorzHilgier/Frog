using System.Collections.Generic;
using Frog.Models;
using Frog.Levels;


namespace Frog.Utilities
{
    static class LevelFactory
    {
        public static void LoadLevels(Queue<Level> LevelQueue)
        {
            LevelQueue.Enqueue(new Level1());
            LevelQueue.Enqueue(new Level2());
            LevelQueue.Enqueue(new Level3());
            LevelQueue.Enqueue(new Level4());
        }
    }
}
