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

        void Update()
        {
            transform.position = new Vector3(transform.position.x + velocityData.speedX * Time.deltaTime, transform.position.y + velocityData.speedY * Time.deltaTime, transform.position.z + velocityData.speedZ * Time.deltaTime);
            
            if (gameObject.GetComponent<Rigidbody>() != null)
            {
                gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(velocityData.angularX, velocityData.angularY, velocityData.angularZ);
            }
        }
    }
}
