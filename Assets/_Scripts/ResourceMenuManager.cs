using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMenuManager : MonoBehaviour
{
    public Text cashText;
    public Text powerText;
    private float delayTime;

    public List<GameObject> plasmaGenerators = new List<GameObject>();

    public float totalCash = 0;
    public int totalPower = 0;

    //plasma generator default increment
    private float cash = 1f;

    private int power = 10;

    private bool activeBehaviour = true;
    //default 0.5 else infinity when no generators in game scene
    private float repeat;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTotalCash());
        repeat = 0.5f;
        //repeats method (acts as an update function or a coroutine, but at a fixed interval
        InvokeRepeating("UpdateDelayTime", 0f, repeat);
        InvokeRepeating("UpdatePower", 0f, 0.5f);
    }

    private void UpdateDelayTime()
    {
        if (plasmaGenerators.Count == 0)
        {
            StopCoroutine(UpdateTotalCash());
            repeat = Mathf.Infinity;
            activeBehaviour = false;
            /*
            delayTime = Mathf.Infinity;
            activeBehaviour = false;
            */
        }
        else
        {
            //Debug.Log("Active generators: " + plasmaGenerators.Count);
            //1 second divided by the total cash per second (10f is the cash per second of 1 generator)
            delayTime = 1f / (100f * plasmaGenerators.Count);
        }
        Debug.Log(plasmaGenerators.Count);
    }

    private void UpdatePower()
    {
        totalPower = plasmaGenerators.Count * power;
        //set UI text
        powerText.text = "Power: " + totalPower;
    }

    //coroutine repeating UpdateTotalCash method which updates UI text
    private IEnumerator UpdateTotalCash()
    {
        while (activeBehaviour)
        {
            yield return new WaitForSeconds(delayTime);

            totalCash += cash;
            cashText.text = "Cash: " + totalCash;

            //Debug.Log(totalCash);
        }
    }
}