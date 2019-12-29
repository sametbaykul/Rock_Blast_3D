using UnityEngine;

using Common.Components;

namespace BallBlast
{
    // --------------------------------------------------
    // Recoil.cs
    // --------------------------------------------------

    public class Recoil : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Config")]
        [Range(0f, 10f)]
        public float amplitude = 1;
        [Range(0f, 1f)]
        public float restoringDelay = 1;
        [Range(0f, 1f)]
        public float recoilDelay = 1;
        public Vector3 directionVector;

        // --------------------------------------------------
        // PRIVATE VARIABLES
        // --------------------------------------------------

        private bool isReadyToMove;

        // --------------------------------------------------
        // FUNDAMENTALS
        // --------------------------------------------------

        private void Start()
        {
            isReadyToMove = true;
        }

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void RECOIL()
        {
            if (isReadyToMove)
            {
                isReadyToMove = false;

                transform.position += directionVector * amplitude;

                Invoke("restore", restoringDelay);
                Invoke("prepareNextRecoil", restoringDelay + recoilDelay);
            }
        }

        // --------------------------------------------------
        // FUNCTIONS
        // --------------------------------------------------

        private void restore()
        {
            transform.position -= directionVector * amplitude;
        }

        private void prepareNextRecoil()
        {
            isReadyToMove = true;
        }
    }
}
