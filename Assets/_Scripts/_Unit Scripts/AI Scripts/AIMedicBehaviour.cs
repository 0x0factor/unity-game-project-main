using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.Units.UnitFunction.PlayerBehaviour;
using GameProject.ProjectAssets.Units.UnitHealth.UnitHealthManager;
using GameProject.ProjectAssets.Units;
using GameProject.ProjectAssets.Units.UnitFunction;


namespace GameProject.ProjectAssets.Units.AI.MedicRange
{
    public class AIMedicBehaviour : MonoBehaviour
    {
        //list for enemy untis within the attackrange of enemy unit
        [SerializeField] private List<GameObject> aiSupportInRange = new List<GameObject>();

        private BasicUnit basicUnit;

        //array for the total player friendlies on the map
        [SerializeField] private GameObject[] aiFriendlies;

        private float attackRange;
        public LayerMask aiUnits;

        private Transform targetFriendly;
        private float shortestDistanceToFriendly;

        // Start is called before the first frame update
        void Start()
        {
            basicUnit = gameObject.GetComponent<BasicUnit>();
            attackRange = basicUnit.GetAttackRange();
            InvokeRepeating("LocateNearestAIFriendlies", 0f, 0.25f);

        }

        private void LocateNearestAIFriendlies()
        {
            shortestDistanceToFriendly = Mathf.Infinity;
            //player locating a friendly unit within range
            aiFriendlies = GameObject.FindGameObjectsWithTag("AI");
            GameObject nearestAIUnit = null;

            foreach (GameObject friendly in aiFriendlies)
            {
                float distanceToFriendly = Vector3.Distance(transform.position, friendly.transform.position);

                if (distanceToFriendly < shortestDistanceToFriendly)
                {
                    if (friendly != gameObject)
                    {
                        shortestDistanceToFriendly = distanceToFriendly;
                        nearestAIUnit = friendly;
                    }
                }
                //add friendly units in attack range of this game object to the list
                if (distanceToFriendly < attackRange)
                {
                    if (!aiSupportInRange.Contains(friendly))
                    {
                        aiSupportInRange.Add(friendly);
                    }
                }
                else
                {
                    aiSupportInRange.Remove(friendly);
                }
            }
            if (nearestAIUnit != null)
            {
                targetFriendly = nearestAIUnit.transform;
            }
            else
            {
                targetFriendly = null;
            }
        }

        //return the whole array of support
        public List<GameObject> GetAISupportInRangeList()
        {
            return aiSupportInRange;
        }

        public Transform GetTargetFriendly()
        {
            return targetFriendly;
        }
    }
}

