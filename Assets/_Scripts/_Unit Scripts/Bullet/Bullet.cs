using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.Units.UnitHealth.UnitHealthManager;
using GameProject.ProjectAssets.Buildings.BuildingHealth;

namespace GameProject.ProjectAssets.Miscellaneous.Bullet
{
    public class Bullet : MonoBehaviour
    {
        private Transform target;

        public GameObject impactEffect;

        /*global private int that can be accessed by other methods of this class
            this is set from the argument int _attackDamage fetched from the attacking unit this bullet is instantiated from
         */
        private int attackDamage;
        public float speed = 70f;
        private string attackersTag;

        public void Seek(Transform _target)
        {
            target = _target;
        }

        public void SetAttackersDamageAndTag(int _attackDamage, string _attackersTag)
        {
            attackDamage = _attackDamage;
            attackersTag = _attackersTag;
        }

        // Update is called once per frame
        void Update()
        {
            Destroy(gameObject, 5f);
            //buildPosition may have already been destroyed or exited range, so the bullet is still left and needs to be destroyed
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }

        private void OnDestroy()
        {
            if (target != null)
            {
                GameObject effectInst = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(effectInst, 3f);
            }
        }

        void HitTarget()
        {
            GameObject effectInst = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectInst, 2f);
            Destroy(gameObject);
        }

        //bullet collision with what object?
        private void OnTriggerEnter(Collider collider)
        {
            if (target != null)
            {
                //PLAYER -> AI
                //a player unit/building is attacking an AI unit/building
                if (collider.CompareTag("AI") && attackersTag == "Player")
                {
                    //building
                    if (collider.gameObject.layer == 12)
                    {
                        BuildingHealthController buildingHealthController = target.gameObject.
                            GetComponent<BuildingHealthController>();
                        buildingHealthController.TakeDamage(attackDamage);
                    }
                    //unit
                    else if (collider.gameObject.layer == 11 || collider.gameObject.layer == 15)
                    {
                        UnitHealthController unitHealthController = target.gameObject.
                            GetComponent<UnitHealthController>();
                        unitHealthController.TakeDamage(attackDamage);
                    }
                }

                //AI -> PLAYER
                //an AI unit/building is attacking a Player unit/building
                else if(collider.CompareTag("Player") && attackersTag == "AI")
                {
                    //building
                    /*clickable and non-clickable */
                    if (collider.gameObject.layer == 9 || collider.gameObject.layer == 13)
                    {
                        BuildingHealthController buildingHealthController = target.gameObject.
                            GetComponent<BuildingHealthController>();
                        buildingHealthController.TakeDamage(attackDamage);
                    }
                    //unit
                    else if (collider.gameObject.layer == 8 || collider.gameObject.layer == 14)
                    {
                        UnitHealthController unitHealthController = target.gameObject.
                            GetComponent<UnitHealthController>();
                        unitHealthController.TakeDamage(attackDamage);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}