using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.ProjectAssets.Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
    public class BasicUnitScriptableObject : ScriptableObject
    {
        public string unitTypeName;

        public bool hasTurret;

        public string description;

        [Header("Attributes")]
        //costs
        public int buildCost;
        public int buyCosts;

        public int attackDamage;
        public int totalHealth;
        public int totalArmour;
        public int movementSpeed;

        public float fireRate;
        public float attackRange;
    }
}