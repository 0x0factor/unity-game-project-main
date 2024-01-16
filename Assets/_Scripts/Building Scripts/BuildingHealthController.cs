using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.Buildings;
using GameProject.ProjectAssets.Units.UnitHealth;
using GameProject.ProjectAssets.Buildings.BuildingFunction;

namespace GameProject.ProjectAssets.Buildings.BuildingHealth
{
    public class BuildingHealthController : MonoBehaviour
    {
        [SerializeField] private BasicBuildingScriptableObject basicBuildingScriptableObject = null;
        private BasicBuilding basicBuilding;

        public ArmourBar armourBar;
        public HealthBar healthBar;

        private int currentArmour;
        private int currentHealth;

        bool armourDestroyed;

        // Start is called before the first frame update
        void Start()
        {
            basicBuilding = GetComponent<BasicBuilding>();

            //set the maximum arour of the building from the scriptable object script
            armourBar.SetMaxArmour(basicBuildingScriptableObject.totalArmour);
            healthBar.SetMaxHealth(basicBuildingScriptableObject.totalHealth);

            currentHealth = basicBuildingScriptableObject.totalHealth;
            currentArmour = basicBuildingScriptableObject.totalArmour;
        }

        public void TakeDamage(int damage)
        {
            //if there is armour
            if (basicBuildingScriptableObject.totalArmour > 0)
            {
                //current armour value is above zero
                if (currentArmour > 0)
                {
                    currentArmour -= damage;
                    armourBar.SetArmour(currentArmour);

                    //current armour value is greater than or equal to
                    if (currentArmour <= 0)
                    {
                        //take remainder health damage
                        currentHealth += currentArmour;
                        healthBar.SetHealth(currentHealth);
                    }

                }
                //current armour value is 0 or negative
                else if (currentArmour <= 0)
                {
                    armourDestroyed = true;
                }
            }
            //there is no armour or current armour has been destroyed (set to zero or negative)
            if (basicBuildingScriptableObject.totalArmour == 0 || armourDestroyed == true)
            {
                currentHealth -= damage;
                healthBar.SetHealth(currentHealth);

                //building is destroyed
                if (currentHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        public int GetCurrentArmour()
        {
            return currentArmour;
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        
        //getter
        public int GetMaxArmour()
        {
            return basicBuilding.GetTotalArmour();
        }

        //getter
        public int GetMaxHealth()
        {
            return basicBuilding.GetTotalHealth();
        }

        //setter
        public void SetAddedCurrentArmour(int _armour)
        {
            currentArmour += _armour;
            armourBar.SetArmour(currentArmour);
        }

        //setter
        public void SetAddedCurrentHealth(int _health)
        {
            currentHealth += _health;
            healthBar.SetHealth(currentHealth);
        }

        //setter
        public void SetSubtractedCurrentArmour(int _armour)
        {
            currentArmour -= _armour;
            armourBar.SetArmour(currentArmour);
        }

        //setter
        public void SetSubtractedCurrentHealth(int _health)
        {
            currentHealth -= _health;
            healthBar.SetHealth(currentHealth);
        }
        
    }
}