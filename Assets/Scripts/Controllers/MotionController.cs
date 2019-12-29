using UnityEngine;
using System.Collections.Generic;

namespace BallBlast
{
    // --------------------------------------------------
    // LevelController.cs
    // --------------------------------------------------

    public class MotionController : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Properties")]
        public List<Wheel> wheels = new List<Wheel>();

        [Header("Data")]
        public InputData inputData;
        public PlayerData playerData;

        // --------------------------------------------------
        // PROVATE VARIABLES
        // --------------------------------------------------

        private float posX;
        private float speedX;
        private bool isActive = true;

        // --------------------------------------------------
        // FUNDAMENTAL
        // --------------------------------------------------

        void FixedUpdate()
        {
            if (isActive)
            {
                update_speed();
                update_position();
                update_player_data();
                update_wheels();
            }

            void update_speed()
            {
                speedX = (playerData.positionTarget.x - playerData.position.x) * playerData.speedModifier;
            }
            void update_position()
            {
                posX = transform.position.x + speedX * Time.fixedDeltaTime;

                if (posX < playerData.positionMIN)
                {
                    speedX = 0;
                    posX = playerData.positionMIN;
                }
                else if (posX > playerData.positionMAX)
                {
                    speedX = 0;
                    posX = playerData.positionMAX;
                }

                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
            }    
            void update_player_data()
            {
                playerData.position.x = posX;
                playerData.speed.x = speedX;
            }
            void update_wheels()
            {
                for (int i = 0; i < wheels.Count; i++)
                {
                    wheels[i].UPDATE_ROTATION();
                }
            }
        }

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void UPDATE_MOTION_TARGET()
        {
            if (isActive)
            {
                playerData.positionTarget.x = playerData.positionMIN + (playerData.positionMAX - playerData.positionMIN) * inputData.MOUSE_POSITION_NORMALIZED.x;
            }
            else
            {
                playerData.positionTarget.x = 0;
            }
        }

        // --------------------------------------------------
        // ACCESS METHODS
        // --------------------------------------------------

        public void SET_ACTIVE(bool active)
        {
            isActive = active;
        }
    }
}
