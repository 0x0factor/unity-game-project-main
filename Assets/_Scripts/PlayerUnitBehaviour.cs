using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.Units;

namespace GameProject.ProjectAssets.Units.UnitFunction.PlayerBehaviour
{
    public class PlayerUnitBehaviour : MonoBehaviour
    {
        //list for enemy untis within the attackrange of enemy unit
        [SerializeField] private List<GameObject> playerSupportInRange = new List<GameObject>();

        private BasicUnit basicUnit;

        //array for the total player friendlies on the map
        [SerializeField] private GameObject[] playerFriendlies;
        private string playerTag = "Player";

        private float attackRange;

        // Start is called before the first frame update
        void Start()
        {
            basicUnit = gameObject.GetComponent<BasicUnit>();
            attackRange = basicUnit.GetAttackRange();
            InvokeRepeating("LocateNearestFriendlies", 0f, 0.25f);

            //testing
            InvokeRepeating("TestMethod", 0f, 1f);
        }

        private void LocateNearestFriendlies()
        {
            //player locating a friendly unit within range
            playerFriendlies = GameObject.FindGameObjectsWithTag(playerTag);

            foreach (GameObject friendly in playerFriendlies)
            {
                float distanceToFriendly = Vector3.Distance(transform.position, friendly.transform.position);

                //add friendly units in attack range of this game object to the list
                if (distanceToFriendly < attackRange)
                {
                    if (!playerSupportInRange.Contains(friendly))
                    {
                        playerSupportInRange.Add(friendly);
                    }
                }
                else
                {
                    playerSupportInRange.Remove(friendly);
                }
            }
        }

        //return the whole array of support
        public List<GameObject> GetPlayerSupportInRangeList()
        {
            return playerSupportInRange;
        }

        public void TestMethod()
        {
            //Debug.Log(playerSupportInRange.Count);
        }
    }
}