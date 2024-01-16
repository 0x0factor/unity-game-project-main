using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.Units.UnitFunction.UnitSelection;
using GameProject.ProjectAssets.Units.UnitFunction.PlayerBehaviour;

namespace GameProject.ProjectAssets.Units.UnitFunction
{
    public class BasicUnit : MonoBehaviour
    {
        //scriptable object of the game object
        [SerializeField] private BasicUnitScriptableObject basicUnitScriptableObject;

        //identifier for the type of unit by name
        private string unitTypeName;
        //determinant of whether it has armour or not
        private bool hasArmour;

        //integer values
        private int attackDamage;
        private int totalHealth;
        private int totalArmour;
        private int movementSpeed;

        //float values
        private float fireRate;
        private float attackRange;

        //default values for all
        private int defaultAttackDamage;
        private int defaultTotalHealth;
        private int defaultTotalArmour;
        private float defaultFireRate;
        private float defaultAttackRange;

        private int currentLevel;

        //cannot be manipulated externally -- static
        private static readonly List<float> 
            levelMultipliers = new List<float>() {1.2f, 1.4f, 1.6f, 1.8f, 2.0f};

        //awake is called when the script instance is being loaded (before start or the first frame)
        private void Awake()
        {
            unitTypeName = basicUnitScriptableObject.unitTypeName;
            if (basicUnitScriptableObject.totalArmour == 0)
            {
                hasArmour = false;
            }
            else
            {
                hasArmour = true;
            }
            attackDamage = basicUnitScriptableObject.attackDamage;
            totalHealth = basicUnitScriptableObject.totalHealth;

            totalArmour = basicUnitScriptableObject.totalArmour;

            movementSpeed = basicUnitScriptableObject.movementSpeed;

            fireRate = basicUnitScriptableObject.fireRate;
            attackRange = basicUnitScriptableObject.attackRange;
        }

        // Start is called before the first frame update
        private void Start()
        {
            currentLevel = 0;
            UnitSelections.Instance.unitList.Add(gameObject);

            defaultAttackDamage = attackDamage;
            defaultTotalHealth = totalHealth;
            defaultTotalArmour = totalArmour;
            defaultFireRate = fireRate;
            defaultAttackRange = attackRange;
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

        private void Update()
        {
            //level 1 from default of level 0
            if (currentLevel == 1)
            {
                //casting
                attackDamage *= (int)levelMultipliers[0];
                totalHealth *= (int)levelMultipliers[0];
                totalArmour *= (int)levelMultipliers[0];
                fireRate *= (int)levelMultipliers[0];
                attackRange *= (int)levelMultipliers[0];
            }
            //level 2
            else if (currentLevel == 2)
            {
                attackDamage = defaultAttackDamage * (int)levelMultipliers[1];
                totalHealth = defaultTotalHealth * (int)levelMultipliers[1];
                totalArmour = defaultTotalArmour * (int)levelMultipliers[1];
                fireRate = defaultFireRate * (int)levelMultipliers[1];
                attackRange = defaultAttackRange * (int)levelMultipliers[1];
            }
            //level 3
            else if (currentLevel == 3)
            {
                attackDamage = defaultAttackDamage * (int)levelMultipliers[2];
                totalHealth = defaultTotalHealth * (int)levelMultipliers[2];
                totalArmour = defaultTotalArmour * (int)levelMultipliers[2];
                fireRate = defaultFireRate * (int)levelMultipliers[2];
                attackRange = defaultAttackRange * (int)levelMultipliers[2];
            }
            //level 4
            else if (currentLevel == 4)
            {
                attackDamage = defaultAttackDamage * (int)levelMultipliers[3];
                totalHealth = defaultTotalHealth * (int)levelMultipliers[3];
                totalArmour = defaultTotalArmour * (int)levelMultipliers[3];
                fireRate = defaultFireRate * (int)levelMultipliers[3];
                attackRange = defaultAttackRange * (int)levelMultipliers[3];
            }
            //level 5
            else if (currentLevel == 5)
            {
                attackDamage = defaultAttackDamage * (int)levelMultipliers[4];
                totalHealth = defaultTotalHealth * (int)levelMultipliers[4];
                totalArmour = defaultTotalArmour * (int)levelMultipliers[4];
                fireRate = defaultFireRate * (int)levelMultipliers[4];
                attackRange = defaultAttackRange * (int)levelMultipliers[4];
            }
        }

        private void OnDestroy()
        {
            //error checking
            UnitSelections.Instance.unitList.Remove(gameObject);
        }

        //getter
        public string GetUnitType()
        {
            return unitTypeName;
        }

        //getter
        public bool GetHasArmour()
        {
            return hasArmour;
        }

        //getter
        public int GetAttackDamage()
        {
            return attackDamage;
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

        //getter
        public int GetMovementSpeed()
        {
            return movementSpeed;
        }

        //getter
        public float GetFireRate()
        {
            return fireRate;
        }

        //getter
        public float GetAttackRange()
        {
            return attackRange;
        }
    }
}