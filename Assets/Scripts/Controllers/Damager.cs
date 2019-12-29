using UnityEngine;

using Common.Components;

namespace BallBlast
{
    // --------------------------------------------------
    // Damager.cs
    // --------------------------------------------------

    public class Damager : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Config")]
        [Range(-10f, 10f)]
        public float damagePoint = 1;

        // --------------------------------------------------
        // ACCESS
        // --------------------------------------------------

        public float DAMAGE_POINT
        {
            set
            {
                damagePoint = value;
            }
            get
            {
                return damagePoint;
            }
        }
    }
}
