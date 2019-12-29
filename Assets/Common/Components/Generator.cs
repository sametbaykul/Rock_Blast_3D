using System.Collections;
using UnityEngine;

using Common.Data;

namespace Common.Components
{
    // --------------------------------------------------
    // Generator.cs
    // --------------------------------------------------

    public class Generator : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Properties")]
        public GameObject nestObject;
        public GameObject initialPositionObject;

        [Header("Data")]
        public GenerationData generationData;

        [Header("Info")]
        public bool isLifeTimerRunning;

        // --------------------------------------------------
        // PRIVATE VARIABLES
        // --------------------------------------------------

        private GeneratedObject[] generatedObjects;

        // --------------------------------------------------
        // FUNDAMENTAL
        // --------------------------------------------------

        void Awake()
        {
            INIT_GENERATION();
        }

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void INIT_GENERATION()
        {
            generatedObjects = new GeneratedObject[generationData.maximumObjectNumber];

            isLifeTimerRunning = false;

            for (int i = 0; i < generationData.maximumObjectNumber; i++)
            {
                GameObject generatedObject = (GameObject)Instantiate(generationData.generatedObject, nestObject.transform);
                GeneratedObject generatedObjectData = new GeneratedObject(generatedObject, i);

                generatedObjectData.ACTIVE = false;
                generatedObjectData.LIFE_DURATION = 0;

                generatedObjects[i] = generatedObjectData;
            }

            generationData.spawnReady = true;
            generationData.activeObjectNumber = 0;
        }

        public void RESTART()
        {
            for (int i = 0; i < generationData.maximumObjectNumber; i++)
            {
                generatedObjects[i].LIFE_DURATION = 0;
                generatedObjects[i].ACTIVE = false;
            }

            generationData.spawnReady = true;
            generationData.activeObjectNumber = 0;
        }

        public void GENERATE()
        {
            GENERATE_AND_TAKE();
        }

        public GeneratedObject GENERATE_AND_TAKE()
        {
            if (!generationData.spawnReady)
            {
                return null;
            }

            if (generationData.activeObjectNumber >= generationData.maximumObjectNumber)
            {
                return null;
            }

            GeneratedObject generatedObject = GetFirstReadyObject();

            if (generatedObject == null)
            {
                return null;
            }

            if (initialPositionObject != null)
            {
                generatedObject.gameObject.transform.position = initialPositionObject.transform.position;
            }

            generatedObject.gameObject.transform.position += generationData.initialPositionOffset;
            generatedObject.ACTIVE = true;

            if (!isLifeTimerRunning)
            {
                StartCoroutine(LifeTimer());
            }

            SET_SPAWN_READY(false);

            return generatedObject;
        }

        public void SET_SPAWN_READY(bool isReady)
        {
            generationData.spawnReady = isReady;

            if (!isReady)
            {
                StartCoroutine(PrepareNextSpawn());
            }
        }

        public int GET_AVAILABLE_OBJECT_NUMBER()
        {
            return generationData.maximumObjectNumber - generationData.activeObjectNumber;
        }

        public int GET_ACTIVE_OBJECT_NUMBER()
        {
            generationData.activeObjectNumber = 0;

            for (int i = 0; i < generationData.maximumObjectNumber; i++)
            {
                if (generatedObjects[i].ACTIVE)
                {
                    generationData.activeObjectNumber++;
                }
            }

            return generationData.activeObjectNumber;
        }

        // --------------------------------------------------
        // ACCESS METHODS
        // --------------------------------------------------

        public GeneratedObject[] GENERATED_OBJECTS
        {
            get
            {
                return generatedObjects;
            }
        }

        // --------------------------------------------------
        // FUNCTIONS
        // --------------------------------------------------

        private GeneratedObject GetFirstReadyObject()
        {
            for (int i = 0; i < generatedObjects.Length; i++)
            {
                if (!generatedObjects[i].ACTIVE)
                {
                    generatedObjects[i].LIFE_DURATION = 0;

                    //lifeDurations[i] = 0;

                    return generatedObjects[i];
                }
            }

            return null;
        }

        // --------------------------------------------------
        // COROUTINES
        // --------------------------------------------------

        private IEnumerator LifeTimer()
        {
            isLifeTimerRunning = true;

            generationData.activeObjectNumber = 1;

            while (generationData.activeObjectNumber > 0)
            {
                generationData.activeObjectNumber = 0;

                for (int i = 0; i < generationData.maximumObjectNumber; i++)
                {
                    if (generatedObjects[i].LIFE_DURATION >= generationData.lifeDuration && !generationData.infiniteLife)
                    {
                        generatedObjects[i].LIFE_DURATION = 0;

                        generatedObjects[i].ACTIVE = false;
                    }
                    else
                    {
                        generatedObjects[i].LIFE_DURATION += generationData.spawnDelay;
                    }

                    if (generatedObjects[i].ACTIVE)
                    {
                        generationData.activeObjectNumber++;
                    }
                }

                yield return new WaitForSeconds(generationData.spawnDelay);
            }

            isLifeTimerRunning = false;

            yield break;
        }

        private IEnumerator PrepareNextSpawn()
        {
            yield return new WaitForSeconds(generationData.spawnDelay);

            generationData.spawnReady = true;

            yield break;
        }
    }

    // --------------------------------------------------
    // GeneratedObject.cs
    // --------------------------------------------------

    [System.Serializable]

    public class GeneratedObject
    {
        public GameObject gameObject;

        private int index;
        private bool isActive;
        private float lifeDuration;

        public GeneratedObject(GameObject _gameObject, int _index)
        {
            gameObject = _gameObject;
            index = _index;
        }

        public int INDEX
        {
            get
            {
                return index;
            }
        }

        public bool ACTIVE
        {
            set
            {
                isActive = value;

                gameObject.SetActive(isActive);
            }
            get
            {
                isActive = gameObject.activeInHierarchy;

                return isActive;
            }
        }

        public float LIFE_DURATION
        {
            set
            {
                lifeDuration = value;
            }
            get
            {
                return lifeDuration;
            }
        }
    }
}
