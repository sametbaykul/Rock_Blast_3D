using UnityEngine;

namespace BallBlast
{
    // --------------------------------------------------
    // ControlManager.cs
    // --------------------------------------------------

    [CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player Data", order = 1)]

    public class PlayerData : ScriptableObject
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Motion Limitations")]
        [Range(-2f, 2f)]
        public float positionMIN = 0;
        [Range(-2f, 2f)]
        public float positionMAX = 5;

        [Header("Motion Config")]
        [Range(0f, 50f)]
        public float speedModifier = 10;

        [Header("Motion Info")]
        public Vector2 position;
        public Vector2 positionTarget;
        public Vector2 speed;
    }
}
