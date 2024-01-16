using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.Buildings.BuildingHealth;

namespace GameProject.ProjectAssets.AI
{
    public class AICommandCenter : MonoBehaviour
    {
        //stage of the game
        private int progressionValue;
        private float gameStage;

        private BuildingHealthController buildingHealthController;
        private AIBarracksSpawner aiBarracksSpawner;

        //health related
        private int maxArmour;
        private int maxHealth;
        private int currentArmour;
        private int currentHealth;

        Transform barrackLocation;

        //prefabs of buildings to be spawned

        public GameObject aiBarracksPrefab;
        public GameObject aiWall;
        public GameObject aiPlasmaGenPrefab;

        private List<int> children = new List<int> { 0, 1, 2, 3, 4 };

        private IEnumerator AIProgression()
        {
            while (true)
            {
                yield return new WaitForSeconds(progressionValue);
                //increase the game stage value to represent the game progressing to a harder stage
                //increment by 1, with a max of 10
                gameStage++;
                
            }
        }

        //getter
        public int GetProgressionValue()
        {
            return progressionValue;
        }

        //getter
        public float GetGameStage()
        {
            return gameStage;
        }

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            ProgressionSpawnBarrack();
        }

        private void ProgressionSpawnBarrack()
        {
            Transform buildingEmpty = transform.GetChild(4);
            //random value between 0 and the number of spawn points
            int rand = Random.Range(0, children.Count);
            children.Remove(rand);
            barrackLocation = buildingEmpty.GetChild(rand);
            GameObject firstBarrack = Instantiate(aiBarracksPrefab, barrackLocation.position, barrackLocation.rotation);
            aiBarracksSpawner = firstBarrack.GetComponent<AIBarracksSpawner>();
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(AIProgression());
            //6 minutes
            progressionValue = 360;
            gameStage = 0;

            buildingHealthController = gameObject.GetComponent<BuildingHealthController>();
            maxArmour = buildingHealthController.GetMaxArmour();
            maxHealth = buildingHealthController.GetMaxHealth();
            currentArmour = maxArmour;
            currentHealth = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            //check current health every frame
            currentArmour = buildingHealthController.GetCurrentArmour();
            currentHealth = buildingHealthController.GetCurrentHealth();
            if (currentHealth < (maxHealth / 2))
            {
                if (gameStage <= 2)
                {
                    aiBarracksSpawner.PoolSpawn(aiBarracksSpawner.aiInfantry, 10, null, false);
                }
                else if (gameStage > 2 && gameStage <= 4)
                {
                    aiBarracksSpawner.PoolSpawn(aiBarracksSpawner.aiLav, 10, null, false);
                    aiBarracksSpawner.PoolSpawn(aiBarracksSpawner.aiInfantry, 5, null, false);
                }
                else if (gameStage > 4 && gameStage <= 6)
                {
                    aiBarracksSpawner.PoolSpawn(aiBarracksSpawner.aiTank, 5, null, false);
                    aiBarracksSpawner.PoolSpawn(aiBarracksSpawner.aiLav, 5, null, false);
                    aiBarracksSpawner.PoolSpawn(aiBarracksSpawner.aiInfantry, 10, null, false);
                }
            }
        }
    }
}