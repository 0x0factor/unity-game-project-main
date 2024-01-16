using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISupplyDropScript : MonoBehaviour
{
    /*This script allows the AI to gain advantage on the supply drop, which both stops the player 
     * from gathering more money and resources, but also allows the AI to boost its army to stay
     * competitive against the human player. This is the only way to have the AI pose a challenge
     * to the player and allow the AI to progress without any advanced and complex logic to calculate
    where certain types of buildings should be placed.*/

    private float shortestDistanceToAI;
    [SerializeField] private GameObject[] aiFriendlies;
    private Transform targetFriendly;
    private AIUnitBehaviour aiUnitBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateAIDetection", 0f, 0.25f);
    }

    private void UpdateAIDetection()
    {
        shortestDistanceToAI = Mathf.Infinity;
        //AI travelling towards an AI friendly
        aiFriendlies = GameObject.FindGameObjectsWithTag("AI");
        GameObject nearestAIUnit = null;

        foreach (GameObject friendly in aiFriendlies)
        {
            float distanceToFriendly = Vector3.Distance(transform.position, friendly.transform.position);
            if (distanceToFriendly < shortestDistanceToAI)
            {
                if (friendly != gameObject)
                {
                    shortestDistanceToAI = distanceToFriendly;
                    nearestAIUnit = friendly;
                }
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

    // Update is called once per frame
    void Update()
    {
        //only execute if a targetFriendly exists
        if (targetFriendly != null)
        {
            aiUnitBehaviour = targetFriendly.GetComponent<AIUnitBehaviour>();
            if (aiUnitBehaviour != null)
            {
                aiUnitBehaviour.SetPriorityTarget(this.transform, true);

            }
        }
    }
}
