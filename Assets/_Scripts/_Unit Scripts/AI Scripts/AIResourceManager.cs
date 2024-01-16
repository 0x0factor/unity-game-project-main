using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIResourceManager : MonoBehaviour
{
    public List<GameObject> aiPlasmaGenerators = new List<GameObject>();

    /*essential ai fields*/
    public float totalCash = 0;
    public int totalPower = 0;
    /*essential ai fields*/

    private float delayTime;

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
        if (aiPlasmaGenerators.Count == 0)
        {
            StopCoroutine(UpdateTotalCash());
            repeat = Mathf.Infinity;
            activeBehaviour = false;
        }
        else
        {
            delayTime = 1f / (10f * aiPlasmaGenerators.Count);
        }
    }

    /*invoke repeated procedure which updates the power value.
    this value rarely increase unless increase in plasma generators*/
    private void UpdatePower()
    {
        totalPower = aiPlasmaGenerators.Count * power;
    }

    //coroutine repeating UpdateTotalCash method constantly changes the totalCash value
    private IEnumerator UpdateTotalCash()
    {
        while (activeBehaviour)
        {
            yield return new WaitForSeconds(delayTime);

            totalCash += cash;
        }
    }

    //getter
    public float GetTotalCash()
    {
        return totalCash;
    }

    //getter
    public int GetTotalPower()
    {
        return totalPower;
    }

    //setter
    public void SetTotalCash(float _cashAdjustment, bool _sign)
    {
        //positive
        if (_sign == true)
        {
            totalCash += _cashAdjustment;
        }
        //negative
        else
        {
            totalCash -= _cashAdjustment;
        }
    }

    //setter
    public void SetTotalPower(int _powerAdjustment, bool _sign)
    {
        //positive
        if (_sign == true)
        {
            totalPower += _powerAdjustment;
        }
        //negative
        else
        {
            totalPower -= _powerAdjustment;
        }
    }
}
