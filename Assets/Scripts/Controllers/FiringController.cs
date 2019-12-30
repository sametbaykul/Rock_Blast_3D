using UnityEngine;

using Common.Components;

namespace BallBlast
{
    // --------------------------------------------------
    // FiringController.cs
    // --------------------------------------------------

    public class FiringController : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Properties")]
        public Generator generator;

        [Header("Data")]
        public FiringData firingData;
        public LevelData levelData;
        public JSONData dataJSON;

        [Header("Info")]
        public int multipleBullet;
        public float damagePoint;

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void UPDATE_WITH_JSON()
        {
            if (dataJSON != null && levelData != null)
            {
                if (levelData.level <= dataJSON.gameDataJSON.levels.Length)
                {
                    multipleBullet = (int)(1 + dataJSON.gameDataJSON.bullet_count_increase * (levelData.level - 1));
                    damagePoint = 1 + dataJSON.gameDataJSON.bullet_damage_increase * (levelData.level - 1);
                }
                else
                {
                    multipleBullet = Mathf.Min(4 + (int)((levelData.level - 5) / 10), firingData.maxMultipleBullet);
                    damagePoint = 1 + (int)(levelData.level / 10);
                }
            }
            else
            {
                multipleBullet = firingData.maxMultipleBullet;
                damagePoint = firingData.damagePoint;
            }
        }

        public void FIRE()
        {
            if (!this.enabled)
            {
                return;
            }

            if (!generator.generationData.spawnReady)
            {
                return;
            }

            if (generator.GET_AVAILABLE_OBJECT_NUMBER() < multipleBullet)
            {
                return;
            }

            for (int i = 0; i < multipleBullet; i ++)
            {
                GeneratedObject generatedObject = generator.GENERATE_AND_TAKE();

                if (generatedObject != null)
                {
                    set_initial_bullet_position(generatedObject.gameObject, i);

                    if (generatedObject.gameObject.GetComponent<Damager>() != null)
                    {
                        generatedObject.gameObject.GetComponent<Damager>().DAMAGE_POINT = damagePoint;
                    }

                    generator.SET_SPAWN_READY(true);
                }
            }

            generator.SET_SPAWN_READY(false);

            void set_initial_bullet_position(GameObject generatedObject, int line_index)
            {
                float initial_bullet_position_x = generatedObject.transform.position.x;

                initial_bullet_position_x -= firingData.multipleBulletSpread * (multipleBullet - 1) * 0.5f;
                initial_bullet_position_x += firingData.multipleBulletSpread * line_index;

                generatedObject.transform.position = new Vector3(initial_bullet_position_x, generatedObject.transform.position.y, generatedObject.transform.position.z);
            }
        }

        public void RESTART()
        {
            generator.RESTART();
        }
    }
}
