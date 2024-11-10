using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.LevelGenerator
{
    public static class CurrentScene
    {
        public static LevelSettings Level { get; private set; }
        public static event Action OnLevelChange = delegate { };


        public static void UpdateLevel(LevelSettings level)
        {
            Level = level;
            OnLevelChange.Invoke();
        }
    }
}