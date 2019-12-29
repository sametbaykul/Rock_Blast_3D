using UnityEngine;
using Common.Data;

namespace BallBlast
{
    // --------------------------------------------------
    // InputController.cs
    // --------------------------------------------------

    public class InputController : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Data")]
        public InputData inputData;
        public SimpleEvent inputEvent;

        // --------------------------------------------------
        // FUNDAMENTAL
        // --------------------------------------------------

        void Update()
        {
            UpdateSwipe();

            if (inputData.IS_SWIPE)
            {
                UpdateMousePosition();
                UpdateNormalizedMousePosition();

                inputEvent.TRIGGER();
            }
        }

        // --------------------------------------------------
        // FUNCTIONS
        // --------------------------------------------------

        void UpdateSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                inputData.IS_SWIPE = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                inputData.IS_SWIPE = false;
            }
        }

        void UpdateMousePosition()
        {
            inputData.MOUSE_POSITION = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        void UpdateNormalizedMousePosition()
        {
            float mouse_ratio_x = Input.mousePosition.x / Screen.width;
            float mouse_ratio_y = Input.mousePosition.y / Screen.height;

            bound_normalized_values();

            inputData.MOUSE_POSITION_NORMALIZED = new Vector2(mouse_ratio_x, mouse_ratio_y);

            void bound_normalized_values()
            {
                if (mouse_ratio_x < 0)
                {
                    mouse_ratio_x = 0;
                }
                else if (mouse_ratio_x > 1)
                {
                    mouse_ratio_x = 1;
                }

                if (mouse_ratio_y < 0)
                {
                    mouse_ratio_y = 0;
                }
                else if (mouse_ratio_y > 1)
                {
                    mouse_ratio_y = 1;
                }
            }
        }
    }
}
