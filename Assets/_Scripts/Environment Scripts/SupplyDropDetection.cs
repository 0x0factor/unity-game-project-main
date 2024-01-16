using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyDropDetection : MonoBehaviour
{
    private ResourceMenuManager resourceMenuManager;
    private AIResourceManager aiResourceManager;

    private float supplyDropValue;
    private int countdown;

    // Start is called before the first frame update
    void Start()
    {
        resourceMenuManager = FindObjectOfType<ResourceMenuManager>();
        aiResourceManager = FindObjectOfType<AIResourceManager>();

        supplyDropValue = 1000f;
        countdown = 5;

        StartCoroutine(ProgressionSupplyDropValue());
    }

    private IEnumerator ProgressionSupplyDropValue()
    {
        while (true)
        {
            yield return new WaitForSeconds(countdown);

            //increase value of the supply drop every 5 minutes by 100%
            supplyDropValue += supplyDropValue * 2;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            resourceMenuManager.totalCash += supplyDropValue;
            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("AI"))
        {
            //increase cash by supplyDropValue
            aiResourceManager.SetTotalCash(supplyDropValue, true);
            Destroy(gameObject);
        }
    }
}
