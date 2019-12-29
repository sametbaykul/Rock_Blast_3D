using UnityEngine;

namespace BallBlast
{
    // --------------------------------------------------
    // BouncingData.cs
    // --------------------------------------------------

    [CreateAssetMenu(fileName = "Health Data", menuName = "Data/Health Data", order = 1)]

    public class HealthData : ScriptableObject
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Health Config")]
        [Range(0, 1000f)]
        public int initialHealth = 5;
        public string enemyTag;
    }
}