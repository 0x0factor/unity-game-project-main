using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyDropScript : MonoBehaviour
{
    public Transform ground;
    public GameObject supplyDrop;
    private int progressionValue;

    private GameObject instanceSupplyDrop;

    // Start is called before the first frame update
    void Start()
    {
        //6 minutes
        progressionValue = 360;
        StartCoroutine(UpdateSupplyDrop());
    }

    private IEnumerator UpdateSupplyDrop()
    {
        while (true)
        {
            //every 5 minutes
            yield return new WaitForSeconds(progressionValue);

            if (instanceSupplyDrop == null)
            {
                instanceSupplyDrop = Instantiate(supplyDrop, ground.transform.position, Quaternion.identity);
            }
        }
    }

    //getter
    public GameObject GetSupplyDropInstance()
    {
        return instanceSupplyDrop;
    }
}