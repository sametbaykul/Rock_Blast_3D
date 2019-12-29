using UnityEngine;

namespace BallBlast
{
    // --------------------------------------------------
    // FiringData.cs
    // --------------------------------------------------

    [CreateAssetMenu(fileName = "Firing Data", menuName = "Data/Firing Data", order = 1)]

    public class FiringData : ScriptableObject
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Multiple Bullet Config")]
        [Range(0, 10)]
        public int maxMultipleBullet = 1;
        [Range(0, 5f)]
        public float multipleBulletSpread = 1;
        [Range(0, 10f)]
        public float damagePoint = 1;
    }
}