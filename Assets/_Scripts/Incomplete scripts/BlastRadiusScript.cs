/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastRadiusScript : MonoBehaviour
{
    private int attackDamage;
    private string attackersTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAttackersDamageAndTag(int _attackDamage, string _attackersTag)
    {
        attackDamage = _attackDamage;
        attackersTag = _attackersTag;
    }

    private void OnParticleCollision(GameObject colliderGameObject)
    {
        if (target != null)
        {
            //PLAYER -> AI
            //a player unit/building is attacking an AI unit/building
            if (colliderGameObject.CompareTag("AI") && attackersTag == "Player")
            {
                //building
                if (colliderGameObject.gameObject.layer == 12)
                {
                    BuildingHealthController buildingHealthController = target.gameObject.
                        GetComponent<BuildingHealthController>();
                    buildingHealthController.TakeDamage(attackDamage);
                }
                //unit
                else if (colliderGameObject.gameObject.layer == 11 || colliderGameObject.gameObject.layer == 14)
                {
                    UnitHealthController unitHealthController = target.gameObject.
                        GetComponent<UnitHealthController>();
                    unitHealthController.TakeDamage(attackDamage);
                }
            }

            //AI -> PLAYER
            //an AI unit/building is attacking a Player unit/building
            else if (colliderGameObject.CompareTag("Player") && attackersTag == "AI")
            {
                //building
                /*clickable and non-clickable 
                if (colliderGameObject.gameObject.layer == 9 || colliderGameObject.gameObject.layer == 13)
                {
                    BuildingHealthController buildingHealthController = target.gameObject.
                        GetComponent<BuildingHealthController>();
                    buildingHealthController.TakeDamage(attackDamage);
                }
                //unit
                else if (colliderGameObject.gameObject.layer == 8 || colliderGameObject.gameObject.layer == 14)
                {
                    UnitHealthController unitHealthController = target.gameObject.
                        GetComponent<UnitHealthController>();
                    unitHealthController.TakeDamage(attackDamage);
                }
            }
        }
    }
}
*/
