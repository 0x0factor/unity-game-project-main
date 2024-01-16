using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameProject.ProjectAssets.Units;
using GameProject.ProjectAssets.Units.UnitHealth.UnitHealthManager;

public class AIUnitBehaviour : MonoBehaviour
{
    //Scriptable Object
    [SerializeField] private BasicUnitScriptableObject basicUnitScriptableObject = null;

    //player unit
    private float shortestDistanceToEnemy;
    //ai friendly
    private float shortestDistanceToFriendly;

    //unit tags
    private string playerTag = "Player";
    private string aiTag = "AI";

    private Transform targetPlayer;
    private Transform targetFriendly;

    NavMeshAgent aiNavMesh;

    //stopping distance set in the prefabs
    private float defaultStoppingDistance;

    //serialized arrays to show the contents on runtime
    [SerializeField] private GameObject[] playerTargets;
    [SerializeField] private GameObject[] aiFriendlies;

    private UnitHealthController unithealthController;

    [SerializeField] private GameObject aiCommandCenter;

    private int currentArmour;
    private int currentHealth;

    //list for friendly units within the attackrange
    [SerializeField] private List<GameObject> aiSupportInRange = new List<GameObject>();

    private Transform ccTransform;

    private Transform targetPriority = null;
    private bool isPriorityTargetFriendly;

    private float attackRange;

    private void Awake()
    {
        aiNavMesh = gameObject.GetComponent<NavMeshAgent>();
        unithealthController = gameObject.GetComponent<UnitHealthController>();
        //prefab set stopping distance
        defaultStoppingDistance = aiNavMesh.stoppingDistance;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateAILogic", 0f, 0.25f);
        InvokeRepeating("LocateNearestEnemy", 0f, 0.25f);
        InvokeRepeating("LocateNearestAI", 0f, 0.25f);

        ccTransform = aiCommandCenter.transform;
        attackRange = basicUnitScriptableObject.attackRange;
    }

    //setter
    public void SetPriorityTarget(Transform _targetPriority, bool _isFriendly)
    {
        targetPriority = _targetPriority;
        isPriorityTargetFriendly = _isFriendly;
    }

    private void LocateNearestEnemy()
    {
        shortestDistanceToEnemy = Mathf.Infinity;
        //AI travelling towards a player
        playerTargets = GameObject.FindGameObjectsWithTag(playerTag);
        GameObject nearestPlayerUnit = null;

        foreach (GameObject enemy in playerTargets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistanceToEnemy)
            {
                shortestDistanceToEnemy = distanceToEnemy;
                nearestPlayerUnit = enemy;
            }
        }
        if (nearestPlayerUnit != null)
        {
            //closest enemy buildPosition
            targetPlayer = nearestPlayerUnit.transform;
        }
        else
        {
            targetPlayer = null;
        }
    }

    private void LocateNearestAI()
    {
        shortestDistanceToFriendly = Mathf.Infinity;
        //AI travelling towards an AI friendly
        aiFriendlies = GameObject.FindGameObjectsWithTag(aiTag);
        GameObject nearestAIUnit = null;

        foreach (GameObject friendly in aiFriendlies)
        {
            //difference between the parameter values
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

    private void UpdateAILogic()
    {
        //if needed, manually feed a target priority into this script, which
        //ignores the default behaviour of finding a target dependent on its health
        if (targetPriority == null)
        {
            //Debug.Log("UpdateAILogic Update!");
            currentHealth = unithealthController.GetCurrentHealth();
            currentArmour = unithealthController.GetCurrentArmour();

            //LAYER 1 - check health
            //if has armour, seek player unit
            if (currentArmour > 0)
            {
                if (targetFriendly != null)
                {
                    SeekPlayerTarget();
                }
            }

            //if health is greater than 50%
            if ((currentHealth >= basicUnitScriptableObject.totalHealth / 2) && (currentArmour <= 0))
            {
                if (targetFriendly != null)
                {
                    SeekFriendlyUnit();
                }
            }

            //if health is less than 50%
            if (currentHealth < (basicUnitScriptableObject.totalHealth / 2))
            {
                if (aiCommandCenter != null)
                {
                    Retreat();
                }
            }
        }
        else
        {
            aiNavMesh.SetDestination(targetPriority.position);
            if (isPriorityTargetFriendly)
            {
                aiNavMesh.stoppingDistance = 1;
            }
            else
            {
                aiNavMesh.stoppingDistance = defaultStoppingDistance;
            }
        }
    }

    private void SeekPlayerTarget()
    {
        if (targetPlayer != null)
        {
            aiNavMesh.SetDestination(targetPlayer.position);
            aiNavMesh.stoppingDistance = basicUnitScriptableObject.attackRange;
        }
    }

    /*intended for grouping of units to help with more strategic ai gameplay. infantry units are intended to group to
     * stronger units such as the tank and the LAV unit. the ai will always follow this rule as infantry units on their
     * own are very weak*/
    private void SeekFriendlyUnit()
    {
        //if the positions are not the same, indicating the unit has not reached it, and never will
        if (gameObject.transform.position != targetFriendly.position)
        {
            aiNavMesh.SetDestination(targetFriendly.position);
            aiNavMesh.stoppingDistance = defaultStoppingDistance * 2;
        }
    }

    private void Retreat()
    {
        aiNavMesh.SetDestination(ccTransform.position);
        aiNavMesh.stoppingDistance = basicUnitScriptableObject.attackRange;
    }
}