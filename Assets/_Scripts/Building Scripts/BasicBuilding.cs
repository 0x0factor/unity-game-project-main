using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.ProjectAssets.Buildings.BuildingFunction
{
    public class BasicBuilding : MonoBehaviour
    {
        [SerializeField] private BasicBuildingScriptableObject 
            basicBuildingScriptableObject = null;
        private int buildCost;
        private int buyCost;
        private int totalHealth;
        private int totalArmour;

        private int currentLevel;

        //cannot be manipulated externally -- static
        private static readonly List<float>
            levelMultipliers = new List<float>() { 1.2f, 1.4f, 1.6f, 1.8f, 2.0f };

        private int defaultTotalHealth;
        private int defaultTotalArmour;

        //exlusively for the plasma building
        private int defaultPlasmaCash;

        private void Awake()
        {
            buildCost = basicBuildingScriptableObject.buildCost;
            buyCost = basicBuildingScriptableObject.buyCost;
            totalHealth = basicBuildingScriptableObject.totalHealth;
            totalArmour = basicBuildingScriptableObject.totalArmour;
        }

        // Start is called before the first frame update
        public void Start()
        {
            currentLevel = 0;
            defaultTotalHealth = totalHealth;
            defaultTotalArmour = totalArmour;

            /*
            if (gameObject.name == "Player Generator")
            {
                PlasmaGenerator plasmaGenerator = GetComponent<PlasmaGenerator>();
                defaultPlasmaCash = 
            }
            */
        }

        //getter
        public int GetCurrentLevel()
        {
            return currentLevel;
        }

        //setter
        public void SetCurrentLevel(int _setLevel)
        {
            currentLevel = _setLevel;
        }

        //getter
        public int GetBuildCost()
        {
            return buildCost;
        }

        //getter
        public int GetBuyCost()
        {
            return buyCost;
        }

        //getter
        public int GetTotalHealth()
        {
            return totalHealth;
        }

        //getter
        public int GetTotalArmour()
        {
            return totalArmour;
        }
    }
}