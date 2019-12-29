using UnityEngine;

using Common.Data;

namespace Common.Components
{
    // --------------------------------------------------
    // PositionInitializer.cs
    // --------------------------------------------------

    public class PositionInitializer : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Data")]
        public InitialPositionData initialPositionData;

        // --------------------------------------------------
        // PRIVATE VARIABLES
        // --------------------------------------------------

        Rigidbody rigidBody;

        // --------------------------------------------------
        // FUNDAMENTAL
        // --------------------------------------------------

        private void Awake()
        {
            APPLY();
        }

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void APPLY()
        {
            rigidBody = gameObject.GetComponent<Rigidbody>();

            setInitialPosition();
            setInitialLinearVelocity();
            setInitialAngularVelocity();
        }

        // --------------------------------------------------
        // FUNCTIONS
        // --------------------------------------------------

        void setInitialPosition()
        {
            transform.position = new Vector3(
                initialPositionData.initialPosition.x + Random.Range(-0.5f, 0.5f) * initialPositionData.positionWideX,
                initialPositionData.initialPosition.y + Random.Range(-0.5f, 0.5f) * initialPositionData.positionWideY,
                initialPositionData.initialPosition.z + Random.Range(-0.5f, 0.5f) * initialPositionData.positionWideZ);
        }

        void setInitialLinearVelocity()
        {
            float speedX;
            float speedY;
            float speedZ;

            if (transform.position.x < initialPositionData.initialPosition.x)
            {
                speedX = initialPositionData.initialSpeedX;
            }
            else
            {
                speedX = -initialPositionData.initialSpeedX;
            }

            if (transform.position.y < initialPositionData.initialPosition.y)
            {
                speedY = initialPositionData.initialSpeedY;
            }
            else
            {
                speedY = -initialPositionData.initialSpeedY;
            }

            if (transform.position.z < initialPositionData.initialPosition.z)
            {
                speedZ = initialPositionData.initialSpeedZ;
            }
            else
            {
                speedZ = -initialPositionData.initialSpeedZ;
            }

            rigidBody.velocity = new Vector3(speedX, speedY, speedZ);
        }

        void setInitialAngularVelocity()
        {
            rigidBody.angularVelocity = new Vector3(
                Random.Range(initialPositionData.minAngularRotationX, initialPositionData.maxAngularRotationX) * isPositiveOrNegative(), 
                Random.Range(initialPositionData.minAngularRotationY, initialPositionData.maxAngularRotationY) * isPositiveOrNegative(),
                Random.Range(initialPositionData.minAngularRotationZ, initialPositionData.maxAngularRotationZ) * isPositiveOrNegative());
        }

        // --------------------------------------------------
        // FUNCTIONS
        // --------------------------------------------------

        private int isPositiveOrNegative()
        {
            float random_number = Random.Range(-1, 1);

            if (random_number >= 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
