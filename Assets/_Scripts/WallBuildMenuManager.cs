using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuildMenuManager : MonoBehaviour
{
    private GameObject currentBlueprint;
    private SnapBlueprint snapBlueprint;
    private GameObject bpConInstance;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitBuildMode();
        }
    }

    //setter
    public void SetCurrentBlueprint(GameObject _currentBlueprint)
    {
        currentBlueprint = _currentBlueprint;
    }

    public void Reposition()
    {
        snapBlueprint = currentBlueprint.GetComponent<SnapBlueprint>();
        //sets bpConInstance as null, so that the transform is set to the buildPosition -> free form building
        snapBlueprint.SetTangibleInstance(0);
        Destroy(bpConInstance);
    }

    public void ExitBuildMode()
    {
        Destroy(bpConInstance);
        Destroy(currentBlueprint);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
    }

    //setter
    public void SetbpConInstance(GameObject _bpConInstance)
    {
        bpConInstance = _bpConInstance;
    }
}