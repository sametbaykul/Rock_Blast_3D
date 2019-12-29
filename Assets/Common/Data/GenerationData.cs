using UnityEngine;

using Common.Components;

namespace Common.Data
{
    // --------------------------------------------------
    // GeneratorData.cs
    // --------------------------------------------------

    [CreateAssetMenu(fileName = "Generation Data", menuName = "Common/Generation Data", order = 1)]

    public class GenerationData : ScriptableObject
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Properties")]
        public GameObject generatedObject;

        [Header("Spawn Config")]
        [Range(0, 1000)]
        public int maximumObjectNumber = 100;
        [Range(0f, 10f)]
        public float spawnDelay = 0.5f;
        [Range(0f, 10f)]
        public float lifeDuration = 10;
        public bool infiniteLife = false;

        [Header("Starting Config")]
        public Vector3 initialPositionOffset = new Vector3(0, 0, 0);

        [Header("Firing Info")]
        public int activeObjectNumber;
        public bool spawnReady;
    }
}