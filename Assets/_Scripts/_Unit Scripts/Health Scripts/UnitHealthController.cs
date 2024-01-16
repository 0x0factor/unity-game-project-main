using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.Units.UnitFunction;

namespace GameProject.ProjectAssets.Units.UnitHealth.UnitHealthManager
{
    public class UnitHealthController : MonoBehaviour
    {
        [SerializeField] private BasicUnitScriptableObject basicUnitScriptableObject = null;
        private BasicUnit basicUnit;

        public ArmourBar armourBar;
        public HealthBar healthBar;

        [SerializeField] private int currentArmour;
        [SerializeField] private int currentHealth;

        private bool armourDestroyed;

        // Start is called before the first frame update
        void Start()
        {
            basicUnit = gameObject.GetComponent<BasicUnit>();
            armourBar.SetMaxArmour(basicUnitScriptableObject.totalArmour);
            healthBar.SetMaxHealth(basicUnitScriptableObject.totalHealth);

            currentHealth = basicUnitScriptableObject.totalHealth;
            currentArmour = basicUnitScriptableObject.totalArmour;
        }

        public void TakeDamage(int damage)
        {
            //if there is armour
            if (basicUnitScriptableObject.totalArmour > 0)
            {
                //current armour value is above zero
                if (currentArmour > 0)
                {
                    currentArmour -= damage;
                    armourBar.SetArmour(currentArmour);

                    //current armour value is less than or equal to 0
                    if (currentArmour <= 0)
                    {
                        //take remainding armour damage from health value
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
            if (basicUnitScriptableObject.totalArmour == 0 || armourDestroyed == true)
            {
                currentHealth -= damage;
                healthBar.SetHealth(currentHealth);

                //unit is killed
                if (currentHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
            //the unit suffers greater damage than it has health and armour, thus killing it immediately
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        //getter
        public int GetCurrentArmour()
        {
            return currentArmour;
        }

        //getter
        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        //getter
        public int GetMaxArmour()
        {
            return basicUnit.GetTotalArmour();
        }

        //getter
        public int GetMaxHealth()
        {
            return basicUnit.GetTotalHealth();
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
