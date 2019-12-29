using UnityEngine;

using Common.Components;

namespace BallBlast
{
    // --------------------------------------------------
    // Wheel.cs
    // --------------------------------------------------

    public class Wheel : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Config")]
        [Range(-100f, 100f)]
        public float speedModifier;
        public Vector3 axleVector = new Vector3(1, 0, 0);

        [Header("Data")]
        public PlayerData playerMotion;

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void UPDATE_ROTATION()
        {
            transform.Rotate(axleVector, playerMotion.speed.x * speedModifier * Time.deltaTime * 100);
        }
    }
}
