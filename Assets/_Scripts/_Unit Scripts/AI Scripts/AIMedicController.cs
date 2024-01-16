using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.Units.UnitHealth.UnitHealthManager;
using GameProject.ProjectAssets.Units;
using GameProject.ProjectAssets.Units.UnitFunction;
using GameProject.ProjectAssets.Units.AI.MedicRange;
using UnityEngine.AI;

public class AIMedicController : MonoBehaviour
{
    [SerializeField] private GameObject[] playerFriendlies;
    private List<GameObject> supportInRangeOfMedic;

    public BasicUnitScriptableObject basicUnitScriptableObject;

    private BasicUnit basicUnit;
    private AIMedicBehaviour aiMedicBehaviour;
    GameObject targetFriendly;
    Transform nearestFriendly;

    private float fireRate;
    private int attackDamage;

    private NavMeshAgent navMesh;

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        aiMedicBehaviour = gameObject.GetComponent<AIMedicBehaviour>();
        basicUnit = gameObject.GetComponent<BasicUnit>();

        fireRate = basicUnit.GetFireRate();
        attackDamage = basicUnit.GetAttackDamage();
        InvokeRepeating("DelayedUpdate", 0f, fireRate);
    }

    private void Update()
    {
        nearestFriendly = aiMedicBehaviour.GetTargetFriendly();
        if (nearestFriendly != null)
        {
            if (transform.position != nearestFriendly.transform.position)
            {
                navMesh.SetDestination(nearestFriendly.transform.position);
            }
        }
    }

    // Update is called once per frame
    private void DelayedUpdate()
    {
        //instance of the player support in range
        supportInRangeOfMedic = aiMedicBehaviour.GetAISupportInRangeList();
        for (int i = 0; i < supportInRangeOfMedic.Count; i++)
        {
            if (targetFriendly != null)
            {
                targetFriendly = supportInRangeOfMedic[i];
                UnitHealthController unitHealthController = targetFriendly.GetComponent<UnitHealthController>();
                BasicUnit targetBasicUnit = targetFriendly.GetComponent<BasicUnit>();

                if (unitHealthController.GetCurrentHealth() < targetBasicUnit.GetTotalHealth())
                {
                    unitHealthController.SetAddedCurrentHealth(attackDamage);
                    if (unitHealthController.GetCurrentHealth() > targetBasicUnit.GetTotalHealth())
                    {
                        //take remainder health away
                        int difference = unitHealthController.GetCurrentHealth() - targetBasicUnit.GetTotalHealth();
                        unitHealthController.SetSubtractedCurrentHealth(difference);
                    }
                }
                else if (unitHealthController.GetCurrentArmour() < targetBasicUnit.GetTotalArmour())
                {
                    unitHealthController.SetAddedCurrentArmour(attackDamage);
                    if (unitHealthController.GetCurrentArmour() > targetBasicUnit.GetTotalArmour())
                    {
                        //take remainder armour away
                        int difference = unitHealthController.GetCurrentArmour() - targetBasicUnit.GetTotalArmour();
                        unitHealthController.SetSubtractedCurrentArmour(difference);
                    }
                }
            }
        }
    }
}
