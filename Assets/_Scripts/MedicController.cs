
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.Units.UnitFunction.PlayerBehaviour;
using GameProject.ProjectAssets.Units.UnitHealth.UnitHealthManager;
using GameProject.ProjectAssets.Units;
using GameProject.ProjectAssets.Units.UnitFunction;

namespace GameProject.ProjectAssets.Units.UnitFunction.Medic
{
    public class MedicController : MonoBehaviour
    {
        private BasicUnit basicUnit;
        private PlayerUnitBehaviour playerUnitBehaviour;

        private float fireRate;
        private int attackDamage;

        private List<GameObject> playerSupportInRange;

        // Start is called before the first frame update
        void Start()
        {
            basicUnit = gameObject.GetComponent<BasicUnit>();
            playerUnitBehaviour = gameObject.GetComponent<PlayerUnitBehaviour>();
            fireRate = basicUnit.GetFireRate();
            attackDamage = basicUnit.GetAttackDamage();
            InvokeRepeating("DelayedUpdate", 0f, fireRate);
        }

        // Update is called once per frame
        private void DelayedUpdate()
        {
            //instance of the player support in range
            playerSupportInRange = playerUnitBehaviour.GetPlayerSupportInRangeList();
            for (int i = 0; i < playerSupportInRange.Count; i++)
            {
                GameObject targetPlayer = (GameObject)playerSupportInRange[i];
                UnitHealthController unitHealthController = targetPlayer.GetComponent<UnitHealthController>();
                BasicUnit targetBasicUnit = targetPlayer.GetComponent<BasicUnit>();
                if (targetPlayer != null)
                {
                    bool targetHasArmour = targetBasicUnit.GetHasArmour();
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
}
