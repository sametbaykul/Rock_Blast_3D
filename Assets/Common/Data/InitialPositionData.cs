using UnityEngine;

namespace Common.Data
{
    // --------------------------------------------------
    // InitialPositionData.cs
    // --------------------------------------------------

    [CreateAssetMenu(fileName = "Initial Position Data", menuName = "Common/Initial Position Data", order = 1)]

    public class InitialPositionData : ScriptableObject
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Initial Position Config")]
        public Vector3 initialPosition = new Vector3(0, 0, 0);
        [Range(0, 10f)]
        public float positionWideX = 0;
        [Range(0, 10f)]
        public float positionWideY = 0;
        [Range(0, 10f)]
        public float positionWideZ = 0;

        [Header("Initial Linear Speed Config")]
        [Range(0f, 100f)]
        public float initialSpeedX = 0;
        [Range(0f, 100f)]
        public float initialSpeedY = 0;
        [Range(0f, 100f)]
        public float initialSpeedZ = 0;

        [Header("Initial Angular Speed Config")]
        [Range(0f, 360f)]
        public float minAngularRotationX = 0;
        [Range(0f, 360f)]
        public float minAngularRotationY = 0;
        [Range(0f, 360f)]
        public float minAngularRotationZ = 0;
        [Range(0f, 360f)]
        public float maxAngularRotationX = 0;
        [Range(0f, 360f)]
        public float maxAngularRotationY = 0;
        [Range(0f, 360f)]
        public float maxAngularRotationZ = 0;
    }
}