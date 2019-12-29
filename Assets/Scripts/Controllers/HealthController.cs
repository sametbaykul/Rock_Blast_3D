using UnityEngine;
using UnityEngine.UI;

using Common.Data;

namespace BallBlast
{
    // --------------------------------------------------
    // HealthController.cs
    // --------------------------------------------------

    public class HealthController : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Property")]
        public Text healthLabel;
        public ColorModifier colorModifier;
        public Camera _camera;

        [Header("Config")]
        public string enemyTag;
        public bool setInactiveAfterCollision;
        [Range(0f, 10f)]
        public float tremorPower;

        [Header("Data")]
        public SimpleEvent damageEvent;
        public SimpleEvent killedEvent;

        [Header("Health Info")]
        public float currentHP;

        // --------------------------------------------------
        // FUNDAMENTAL
        // --------------------------------------------------

        void Start()
        {
            UpdateTextLabel();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(enemyTag) && this.enabled)
            {
                if (collider.CompareTag("Bullet"))
                {
                    collider.gameObject.SetActive(false);
                }

                if (damageEvent != null)
                {
                    damageEvent.TRIGGER();
                }

                if (colorModifier != null)
                {
                    colorModifier.MODIFY_COLOR();
                }

                if (collider.GetComponent<Damager>() != null)
                {
                    HP -= collider.GetComponent<Damager>().DAMAGE_POINT;
                }
                else
                {
                    HP--;
                }

                if (HP <= 0)
                {
                    killedEvent.TRIGGER();

                    if (setInactiveAfterCollision)
                    {
                        gameObject.SetActive(false);
                    }  
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_camera != null && collision.collider.name == "Floor" && transform.localScale.x >= 1)
            {
                _camera.GetComponent<StressReceiver>().InduceStress(tremorPower);
            }
        }

        // --------------------------------------------------
        // ACCESS METHODS
        // --------------------------------------------------

        public float HP
        {
            set
            {
                currentHP = value;

                UpdateTextLabel();
            }
            get
            {
                return currentHP;
            }
        }

        // --------------------------------------------------
        // FUNCTIONS
        // --------------------------------------------------

        private void UpdateTextLabel()
        {
            if (healthLabel != null)
            {
                healthLabel.text = currentHP.ToString();
            }
        }
    }
}