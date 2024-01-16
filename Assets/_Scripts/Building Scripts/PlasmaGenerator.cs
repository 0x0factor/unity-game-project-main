using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGenerator : MonoBehaviour
{
    private ResourceMenuManager resourceMenuManager;
    private AIResourceManager aIResourceManager;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        //instance of ResourceMenuManager to access the list
        resourceMenuManager = FindObjectOfType<ResourceMenuManager>();

        //instance of AIResourceManager to access AI list
        aIResourceManager = FindObjectOfType<AIResourceManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //AI
        if (gameObject.CompareTag("AI"))
        {
            //add the generator to the list for collection of cash
            aIResourceManager.aiPlasmaGenerators.Add(gameObject);
        }
        //Player
        else if (gameObject.CompareTag("Player"))
        {
            //add the generator to the list for collection of cash
            resourceMenuManager.plasmaGenerators.Add(gameObject);
        }
        //Unspecified
        else
        {
            Debug.LogError("The game object this component is attached to is either untagged, " +
                "or the building is not that of a plasma generator...");
        }
    }

    public void OnDestroy()
    {
        if (gameObject.CompareTag("AI"))
        {
            aIResourceManager.aiPlasmaGenerators.Remove(gameObject);
            //SetTotalPower(_powerAdjustment, _sign) where sign is indicative to a negative or positive value
            aIResourceManager.SetTotalPower(10, false);
        }
        else
        {
            resourceMenuManager.plasmaGenerators.Remove(gameObject);
            resourceMenuManager.totalPower -= 10;
        }
    }
}