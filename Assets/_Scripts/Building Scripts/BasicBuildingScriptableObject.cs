using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.ProjectAssets.Buildings
{
    [CreateAssetMenu(fileName = "New Building", menuName = "Scriptable Building")]
    public class BasicBuildingScriptableObject : ScriptableObject
    {
        public string buildingTypeName;

        public string description;

        [Header("Attributes")]
        //costs
        public int buildCost;
        public int buyCost;

        public int totalHealth;
        public int totalArmour;
    }
}