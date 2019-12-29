using UnityEngine;

using Common.Components;

namespace BallBlast
{
    // --------------------------------------------------
    // ColorModifier.cs
    // --------------------------------------------------

    public class ColorModifier : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Config")]
        [Range(0f, 1f)]
        public float aggressiveness = 0.001f;
        [Range(0f, 100f)]
        public float randomPeriod = 5;

        // --------------------------------------------------
        // PRIVATE VARIABLES
        // --------------------------------------------------

        private int directionR;
        private int directionG;
        private int directionB;

        private float R;
        private float G;
        private float B;

        // --------------------------------------------------
        // FUNDAMENTALS
        // --------------------------------------------------

        private void Start()
        {
            R = gameObject.GetComponent<MeshRenderer>().material.color.r;
            G = gameObject.GetComponent<MeshRenderer>().material.color.g;
            B = gameObject.GetComponent<MeshRenderer>().material.color.b;

            findColorDirection();
            Invoke("findColorDirection", randomPeriod);
        }

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void MODIFY_COLOR()
        {
            UpdateColors();
            CheckColorLimits();

            gameObject.GetComponent<MeshRenderer>().material.color = new Color(R, G, B);
        }

        // --------------------------------------------------
        // FUNCTIONS
        // --------------------------------------------------

        private void findColorDirection()
        {
            directionR = randomPositive();
            directionG = randomPositive();
            directionB = randomPositive();
        }

        private int randomPositive()
        {
            if (Random.Range(-1, 1) >= 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        void UpdateColors()
        {
            R += directionR * aggressiveness;
            G += directionG * aggressiveness;
            B += directionB * aggressiveness;
        }

        void CheckColorLimits()
        {
            if (R <= 0)
            {
                R = 0.005f;
                directionR *= -1;
            }
            else if (R >= 1)
            {
                R = 0.995f;
                directionR *= -1;
            }

            if (G <= 0)
            {
                G = 0.005f;
                directionG *= -1;
            }
            else if (G >= 1)
            {
                G = 0.995f;
                directionG *= -1;
            }

            if (B <= 0)
            {
                B = 0.005f;
                directionB *= -1;
            }
            else if (B >= 1)
            {
                B = 0.995f;
                directionB *= -1;
            }
        }
    }
}
