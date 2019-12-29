using UnityEngine;

namespace BallBlast
{
    // --------------------------------------------------
    // RockData.cs
    // --------------------------------------------------

    [CreateAssetMenu(fileName = "Rock Data", menuName = "Data/Rock Data", order = 1)]

    public class RockData : ScriptableObject
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("General Config")]
        [Range(0, 10)]
        public int maxRockNumber = 6;
        [Range(0, 10)]
        public int splitNumber = 2;
        [Range(0, 10)]
        public int spawnDelay = 5;
        [Range(0f, 10f)]
        public float splitSpeed = 5;

        [Header("HP Config")]
        [Range(0, 1000)]
        public float initialHP = 100;
        [Range(0, 1000)]
        public float splitHP = 50;

        [Header("Size Config")]
        [Range(0f, 10f)]
        public float size = 1;
        [Range(0f, 10f)]
        public float splitSize = 0.5f;
    }
}