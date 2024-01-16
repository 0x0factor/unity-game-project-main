using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.AI;


public class AIBarracksSpawner : MonoBehaviour
{
    private AICommandCenter aiCommandCenter;

    public Transform barrackSpawnPoint;
    private float infantryDelayTime = 20;
    private float medicDelayTime = 40;
    private float lavDelayTime = 90;
    private float tankDelayTime = 120;

    public GameObject aiInfantry;
    public GameObject aiMedic;
    public GameObject aiLav;
    public GameObject aiTank;

    //default to 10
    private int wavePool = 10;

    //ai command center values
    private int progressionValue;
    private float gameStage;

    // Start is called before the first frame update
    void Start()
    {
        aiCommandCenter = FindObjectOfType<AICommandCenter>();
        progressionValue = aiCommandCenter.GetProgressionValue();
        gameStage = aiCommandCenter.GetGameStage();

        StartCoroutine(SpawnAIInfantry());
        StartCoroutine(SpawnAILav());
        StartCoroutine(SpawnAITank());
        StartCoroutine(ProgressionSpawnDelay());
        StartCoroutine(MassSpawn());
        StartCoroutine(SpawnAIMedic());
    }

    private void Update()
    {
        //update the command center values
        progressionValue = aiCommandCenter.GetProgressionValue();
        gameStage = aiCommandCenter.GetGameStage();
    }

    //game progression
    private IEnumerator ProgressionSpawnDelay()
    {
        while (true)
        {
            //3 minutes
            yield return new WaitForSeconds(progressionValue / 5);

            lavDelayTime /= 1.25f;
            tankDelayTime /= 1.25f;
        }
    }

    //public callable procedure
    public void PoolSpawn(GameObject gameObject, int count, Transform priorityTarget, bool isFriendly)
    {
        //count is quantity
        for (int i = 0; i <= count; i++)
        {
            //spawn parameter value gameObject
            GameObject clone = Instantiate(gameObject, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
            AIUnitBehaviour aiUnitBehaviour = clone.GetComponent<AIUnitBehaviour>();
            aiUnitBehaviour.SetPriorityTarget(priorityTarget, isFriendly);
        }
    }

    //purpose of giving the ai more behaviour, particularly less predictable behaviour
    //also gives ai an upper hand
    private IEnumerator MassSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(progressionValue);
            //every progressive cycle, increment gameStage by 1. This is every
            //value in the var progressionvalue

            if (gameStage <= 2)
            {
                for (int i = 0; i < wavePool; i++)
                {
                    Instantiate(aiInfantry, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
                }
            }
            else if (gameStage > 4 && gameStage <= 6)
            {
                for (int i = 0; i < wavePool; i++)
                {
                    Instantiate(aiLav, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
                }
            }
            else if (gameStage > 6 && gameStage <= 8)
            {
                for (int i = 0; i < wavePool; i++)
                {
                    Instantiate(aiTank, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
                }
            }
            else if (gameStage > 8 && gameStage <= 10)
            {
                for (int i = 0; i < wavePool; i++)
                {
                    Instantiate(aiTank, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
                    Instantiate(aiLav, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
                }
                for (int i = 0; i < wavePool * 3; i++)
                {
                    Instantiate(aiInfantry, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
                }
            }
            else if (gameStage > 10)
            {
                for (int i = 0; i < wavePool; i++)
                {
                    Instantiate(aiTank);
                }
            }
        }
    }

    //spawn infantry
    private IEnumerator SpawnAIInfantry()
    {
        while (true)
        {
            yield return new WaitForSeconds(infantryDelayTime);


            Instantiate(aiInfantry, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
        }
    }

    //spawn medic
    private IEnumerator SpawnAIMedic()
    {
        while (true)
        {
            yield return new WaitForSeconds(medicDelayTime);

            Instantiate(aiMedic, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
        }
    }

    //spawn lav
    private IEnumerator SpawnAILav()
    {
        while (true)
        {
            yield return new WaitForSeconds(lavDelayTime);


            Instantiate(aiLav, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
        }
    }

    //spawn tank
    private IEnumerator SpawnAITank()
    {
        while (true)
        {
            yield return new WaitForSeconds(tankDelayTime);

            Instantiate(aiTank, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
        }
    }
}
