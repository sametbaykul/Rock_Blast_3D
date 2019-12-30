using UnityEngine;

using Common.Data;

namespace Common.Components
{
    // --------------------------------------------------
    // ConstantVelocity.cs
    // --------------------------------------------------

    public class ConstantVelocity : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Data")]
        public VelocityData velocityData;

        // --------------------------------------------------
        // FUNDAMENTAL
        // --------------------------------------------------

        void FixedUpdate()
        {
            if (gameObject.activeInHierarchy)
            {
                //transform.position = new Vector3(transform.position.x + velocityData.speedX * Time.fixedDeltaTime, transform.position.y + velocityData.speedY * Time.fixedDeltaTime, transform.position.z + velocityData.speedZ * Time.fixedDeltaTime);

                transform.transform.Translate(new Vector3(velocityData.speedX, velocityData.speedY, velocityData.speedZ) * Time.fixedDeltaTime);

                if (gameObject.GetComponent<Rigidbody>() != null)
                {
                    gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(velocityData.angularX, velocityData.angularY, velocityData.angularZ);
                }
            }
        }
    }
}
