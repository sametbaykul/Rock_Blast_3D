using UnityEngine;
using System.Collections;

namespace Common.Components
{
    // --------------------------------------------------
    // FixedLook.cs
    // --------------------------------------------------

    public class FixedLook : MonoBehaviour
    {
        [Header("Properties")]
        public Camera lookAt;

        [Header("Rotation Config")]
        public bool isFixedRotationX;
        public bool isFixedRotationY;
        public bool isFixedRotationZ;

        void Update()
        {
            Vector3 distance = lookAt.transform.position - transform.position;
            float rotationX;
            float rotationY;
            float rotationZ;

            distance.x = distance.z = 0.0f;
            transform.LookAt(lookAt.transform.position - distance);

            if (isFixedRotationX)
            {
                rotationX = 0;
            }
            else
            {
                rotationX = transform.rotation.eulerAngles.x;
            }

            if (isFixedRotationY)
            {
                rotationY = 0;
            }
            else
            {
                rotationY = transform.rotation.eulerAngles.y;
            }

            if (isFixedRotationZ)
            {
                rotationZ = 0;
            }
            else
            {
                rotationZ = transform.rotation.eulerAngles.z;
            }

            transform.Rotate(rotationX, rotationY, rotationZ);
        }
    }
}