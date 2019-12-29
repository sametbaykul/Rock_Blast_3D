using UnityEngine;

using Common.Components;

namespace Common.Data
{
    // --------------------------------------------------
    // VelocityData.cs
    // --------------------------------------------------

    [CreateAssetMenu(fileName = "Velocity Data", menuName = "Common/VelocityData Data", order = 1)]

    public class VelocityData : ScriptableObject
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Linear Velocity Config")]
        [Range(-100, 100)]
        public float speedX = 0;
        [Range(-100, 100)]
        public float speedY = 0;
        [Range(-100, 100)]
        public float speedZ = 0;

        [Header("Angular Velocity Config")]
        [Range(-100, 100)]
        public float angularX = 0;
        [Range(-100, 100)]
        public float angularY = 0;
        [Range(-100, 100)]
        public float angularZ = 0;
    }
}