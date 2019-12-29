using UnityEngine;

namespace BallBlast
{
    // --------------------------------------------------
    // InputData.cs
    // --------------------------------------------------

    [CreateAssetMenu(fileName = "Input Data", menuName = "Data/Input Data", order = 1)]

    public class InputData : ScriptableObject
    {
        [Header("Mouse Position")]
        public Vector2 mousePosition;
        public Vector2 mousePositionNormalized;

        [Header("Mouse States")]
        private bool isSwipe;

        // --------------------------------------------------
        // ACCESS METHODS
        // --------------------------------------------------

        public Vector2 MOUSE_POSITION
        {
            set
            {
                mousePosition = value;
            }
            get
            {
                return mousePosition;
            }
        }

        public Vector2 MOUSE_POSITION_NORMALIZED
        {
            set
            {
                mousePositionNormalized = value;
            }
            get
            {
                return mousePositionNormalized;
            }
        }

        public bool IS_SWIPE
        {
            set
            {
                isSwipe = value;
            }
            get
            {
                return isSwipe;
            }
        }
    }
}
