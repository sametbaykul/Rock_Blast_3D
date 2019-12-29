using UnityEngine;
using System.Collections.Generic;

namespace BallBlast
{
    // --------------------------------------------------
    // JSONData.cs
    // --------------------------------------------------

    [CreateAssetMenu(fileName = "JSON Data", menuName = "Data/JSON Data", order = 1)]

    public class JSONData : ScriptableObject
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Game Data JSON Info")]
        public GameDataJSON gameDataJSON;
    }

    [System.Serializable]
    public class GameDataJSON
    {
        public float gravity;
        public int bullet_count_increase;
        public int bullet_damage_increase;
        public LevelDataJSON[] levels;
    }

    [System.Serializable]
    public class LevelDataJSON
    {
        public BallDataJSON[] balls;
    }

    [System.Serializable]
    public class BallDataJSON
    {
        public int hp;
        public int[] splits;
        public float delay;
    }
}
