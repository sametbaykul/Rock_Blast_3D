using UnityEngine;

using Common.Components;
using Common.Data;

namespace BallBlast
{
    // --------------------------------------------------
    // RockController.cs
    // --------------------------------------------------

    public class RockController : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Properties")]
        public Generator rockGenerator;
        public Generator explosionGenerator;
        public Camera _camera;
        public GameObject floor;

        [Header("Data")]
        public RockData rockData;
        public InitialPositionData rockInitialPosition;
        public LevelData levelData;
        public JSONData dataJSON;

        [Header("Events")]
        public SimpleEvent levelVictory;

        [Header("Info")]
        
        public bool isActive;
        public int currentRockNumberForVictory;
        public int targetRockNumberForVictory;

        // --------------------------------------------------
        // PRIVATE VARIABLES
        // --------------------------------------------------

        private bool isJsonUpdateAvailable;

        // --------------------------------------------------
        // FUNDAMENTAL
        // --------------------------------------------------

        void Start()
        {
            GeneratedObject[] _generatedRocks = rockGenerator.GENERATED_OBJECTS;

            for (int i = 0; i < _generatedRocks.Length; i++)
            {
                _generatedRocks[i].gameObject.GetComponent<HealthController>()._camera = _camera;
            }

            GeneratedObject[] _generatedExplosions = explosionGenerator.GENERATED_OBJECTS;

            for (int i = 0; i < _generatedExplosions.Length; i++)
            {
                ParticleSystem.CollisionModule _particleCollisionModule = _generatedExplosions[i].gameObject.GetComponent<ParticleSystem>().collision;

                _particleCollisionModule.SetPlane(0, floor.transform);
            }
        }

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void START()
        {
            rockGenerator.RESTART();
            explosionGenerator.RESTART();

            currentRockNumberForVictory = 0;
            isJsonUpdateAvailable = (dataJSON != null && levelData != null);

            if (isJsonUpdateAvailable)
            {
                if (levelData.level <= dataJSON.gameDataJSON.levels.Length)
                {
                    targetRockNumberForVictory = dataJSON.gameDataJSON.levels[levelData.level - 1].balls.Length;
                }
                else
                {
                    targetRockNumberForVictory = Mathf.Min(levelData.level, rockData.maxRockNumber);
                }
            }

            if (!isActive)
            {
                if (isJsonUpdateAvailable)
                {
                    if (levelData.level <= dataJSON.gameDataJSON.levels.Length)
                    {
                        Invoke("SPAWN", dataJSON.gameDataJSON.levels[levelData.level - 1].balls[currentRockNumberForVictory].delay);
                    }
                    else
                    {
                        Invoke("SPAWN", 0.5f);
                    }  
                }
                else
                {
                    Invoke("SPAWN", 0.5f);
                }

                isActive = true;
            }
        }

        public void STOP()
        {
            for (int i = 0; i < rockGenerator.generationData.maximumObjectNumber; i++)
            {
                GameObject rock = rockGenerator.GENERATED_OBJECTS[i].gameObject;

                rock.GetComponent<Rigidbody>().ResetInertiaTensor();
                rock.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                rock.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            }

            CancelInvoke();

            isActive = false;
            currentRockNumberForVictory = 0;
        }

        public void CHECK_ALL_ROCKS_KILLED()
        {
            if (currentRockNumberForVictory >= targetRockNumberForVictory)
            {
                if (rockGenerator.GET_ACTIVE_OBJECT_NUMBER() == 0)
                {
                    Time.timeScale = 0.1f;

                    STOP();

                    levelVictory.TRIGGER();
                }
            }
        }

        public void SPAWN()
        {
            GeneratedObject generatedRock = rockGenerator.GENERATE_AND_TAKE();

            if (generatedRock != null)
            {
                GameObject rock = generatedRock.gameObject;

                rock.transform.localScale = new Vector3(rockData.size, rockData.size, rockData.size);
                rock.GetComponent<Rigidbody>().ResetInertiaTensor();
                rock.GetComponent<PositionInitializer>().APPLY();

                if (isJsonUpdateAvailable)
                {
                    if (levelData.level <= dataJSON.gameDataJSON.levels.Length)
                    {
                        rock.GetComponent<HealthController>().HP = dataJSON.gameDataJSON.levels[levelData.level - 1].balls[currentRockNumberForVictory].hp;
                    }
                    else
                    {
                        rock.GetComponent<HealthController>().HP = levelData.level * rockData.initialHP;
                    } 
                }
                else
                {
                    rock.GetComponent<HealthController>().HP = rockData.initialHP;
                }

                currentRockNumberForVictory++;
            }

            if (isJsonUpdateAvailable)
            {
                if (levelData.level <= dataJSON.gameDataJSON.levels.Length)
                {
                    if (currentRockNumberForVictory < dataJSON.gameDataJSON.levels[levelData.level - 1].balls.Length)
                    {
                        Invoke("SPAWN", dataJSON.gameDataJSON.levels[levelData.level - 1].balls[currentRockNumberForVictory].delay);
                    }
                }
                else if (currentRockNumberForVictory < targetRockNumberForVictory)
                {
                    Invoke("SPAWN", rockData.spawnDelay);
                }
            }
            else
            {
                Invoke("SPAWN", rockData.spawnDelay);
            }
        }

        public void SPLIT()
        {
            for (int i = 0; i < rockGenerator.generationData.maximumObjectNumber; i++)
            {
                GeneratedObject generated_rock = rockGenerator.GENERATED_OBJECTS[i];
                GameObject rock = generated_rock.gameObject;

                if (rock.activeInHierarchy)
                {
                    if (rock.GetComponent<HealthController>().HP <= 0)
                    {
                        GeneratedObject _generatedExplosion = explosionGenerator.GENERATE_AND_TAKE();
                        ParticleSystem.MainModule _particleMain;

                        if (_generatedExplosion != null)
                        {
                            _particleMain = _generatedExplosion.gameObject.GetComponent<ParticleSystem>().main;
                            _generatedExplosion.gameObject.transform.position = generated_rock.gameObject.transform.position;
                            _generatedExplosion.gameObject.GetComponent<Renderer>().material.color = rock.GetComponent<MeshRenderer>().material.color;
                        }

                        if (rock.transform.localScale.x > rockData.splitSize)
                        {
                            // Split Big

                            if (_generatedExplosion != null)
                            {
                                _particleMain.maxParticles = 100;
                            }   
                            
                            Split(generated_rock);
                        }
                        else
                        {
                            // Kill small

                            if (_generatedExplosion != null)
                            {
                                _particleMain.maxParticles = 50;
                            }

                            rock.SetActive(false);
                        }
                    }
                }
            }

            void Split(GeneratedObject _generatedRock)
            {
                GameObject _rock = _generatedRock.gameObject;

                for (int j = 0; j < rockData.splitNumber; j++)
                {
                    rockGenerator.SET_SPAWN_READY(true);

                    GeneratedObject generated_split_rock = rockGenerator.GENERATE_AND_TAKE();

                    if (generated_split_rock == null)
                    {
                        return;
                    }

                    GameObject split_rock = generated_split_rock.gameObject;

                    if (split_rock != null)
                    {
                        split_rock.GetComponent<Rigidbody>().ResetInertiaTensor();
                        split_rock.GetComponent<PositionInitializer>().APPLY();

                        split_rock.transform.position = _rock.transform.position;
                        split_rock.transform.localScale = new Vector3(rockData.splitSize, rockData.splitSize, rockData.splitSize);

                        float speedX = rockData.splitSpeed * (1 +  j / rockData.splitNumber) * isEven(j);
                        float speedY = rockData.splitSpeed;

                        split_rock.GetComponent<Rigidbody>().velocity = new Vector3(speedX, speedY, 0);
                        split_rock.GetComponent<MeshRenderer>().material.color = _rock.GetComponent<MeshRenderer>().material.color;

                        if (isJsonUpdateAvailable)
                        {
                            if (levelData.level <= dataJSON.gameDataJSON.levels.Length)
                            {
                                split_rock.GetComponent<HealthController>().HP = dataJSON.gameDataJSON.levels[levelData.level - 1].balls[_generatedRock.INDEX].splits[j];
                            }
                            else
                            {
                                split_rock.GetComponent<HealthController>().HP = levelData.level * rockData.splitHP;
                            }
                        }
                        else
                        {
                            split_rock.GetComponent<HealthController>().HP = rockData.splitHP;
                        }
                    }
                }

                rockGenerator.SET_SPAWN_READY(true);
            }
        }

        // --------------------------------------------------
        // FUNCTIONS
        // --------------------------------------------------

        private int isEven(int number)
        {
            if (number % 2 == 0)
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
