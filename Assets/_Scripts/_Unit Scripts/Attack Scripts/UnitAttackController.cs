using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.Miscellaneous.Bullet;

namespace GameProject.ProjectAssets.Units.UnitFunction.AttackController
{
    public class UnitAttackController : MonoBehaviour
    {
        //basicunit reference
        private BasicUnit basicUnit;

        private Transform target;

        [Header("Attributes")]

        private float fireRate;
        private float attackRange;
        private int attackDamage;

        [Header("Unity Setup Fields")]

        public string aiTag = "AI";
        public string playerTag = "Player";

        public Transform objectToRotate;

        public float turnSpeed = 10f;
        private float fireCountdown = 0f;

        public GameObject bulletPrefab;
        public Transform firePoint;

        // Start is called before the first frame update
        void Start()
        {
            //setting the basicUnit field
            basicUnit = gameObject.GetComponent<BasicUnit>();
            //repeats the UpdateTarget method
            InvokeRepeating("UpdateTarget", 0f, 0.1f);

            fireRate = basicUnit.GetFireRate();
            attackRange = basicUnit.GetAttackRange();
            attackDamage = basicUnit.GetAttackDamage();
        }

        //updates less often than the Update function as it may take more computational power
        void UpdateTarget()
        {
            float shortestDistance = Mathf.Infinity;
            //if this game object is a player unit
            if (gameObject.tag == playerTag)
            {
                GameObject[] aiUnits = GameObject.FindGameObjectsWithTag(aiTag);
                GameObject nearestAIUnit = null;

                //loops through the enemy array
                foreach (GameObject enemy in aiUnits)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestAIUnit = enemy;
                    }
                }

                if (nearestAIUnit != null && shortestDistance <= attackRange)
                {
                    target = nearestAIUnit.transform;
                }
                else
                {
                    target = null;
                }
            }
            //if this game object is an AI unit
            else if (gameObject.tag == aiTag)
            {
                GameObject[] playerUnits = GameObject.FindGameObjectsWithTag(playerTag);
                GameObject nearestPlayerUnit = null;

                //loops through the enemy array
                foreach (GameObject enemy in playerUnits)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestPlayerUnit = enemy;
                    }
                }

                if (nearestPlayerUnit != null && shortestDistance <= attackRange)
                {
                    target = nearestPlayerUnit.transform;
                }
                else
                {
                    target = null;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            //no target
            if (target != null)
            {
                Vector3 targetDirection = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
                Vector3 rotation = Quaternion.Lerp(objectToRotate.rotation, lookRotation,
                    Time.deltaTime * turnSpeed).eulerAngles;
                objectToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
            else
            {
                //resets the objectToRotate rotation when there is no any enemies within aggro range
                Quaternion defaultLookRotation = Quaternion.LookRotation(transform.forward);
                Vector3 defaultrotation = Quaternion.Lerp(objectToRotate.rotation, defaultLookRotation,
                    Time.deltaTime * turnSpeed).eulerAngles;
                objectToRotate.rotation = Quaternion.Euler(0f, defaultrotation.y, 0f);
                return;
            }
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }

        //Sets the target in the bullet script
        void Shoot()
        {
            GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletGameObject.GetComponent<Bullet>();

            if (bullet != null)
            {
                //passing information to the bullet script
                bullet.Seek(target);
                bullet.SetAttackersDamageAndTag(attackDamage, gameObject.tag);
            }
        }

        //displays the radius of the aggro range in the game scene for debugging and development
        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}