using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    private bool currentlyBuilding;

    private GameObject currentBlueprint;
    private CountdownTimer countdownTimer;

    // Start is called before the first frame update
    private void Start()
    {
        currentlyBuilding = false;
    }

    //setter
    public void SetCurrentlyBuilding(bool _currentlyBuilding)
    {
        currentlyBuilding = _currentlyBuilding;
    }

    //getter
    public bool GetCurrentlyBuilding()
    {
        return currentlyBuilding;
    }

    public void SetCurrentBlueprint(GameObject _currentBlueprint)
    {
        currentBlueprint = _currentBlueprint;
    }

    private void Update()
    {
        if (currentBlueprint != null)
        {
            countdownTimer = currentBlueprint.GetComponent<CountdownTimer>();
            countdownTimer.SetCurrentBarracks(gameObject);
        }
    }
}
