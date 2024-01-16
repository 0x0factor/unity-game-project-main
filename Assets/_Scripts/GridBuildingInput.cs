using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingInput : MonoBehaviour
{
    //empty game object
    public GameObject buildPosition;

    private float gridSize = 25f;

    public LayerMask ground;

    RaycastHit hit;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            buildPosition.transform.position = hit.point;
        }
    }

    public Vector3 CalculateSnapPosition()
    {
        Vector3 snapPosition;
        snapPosition.x = Mathf.Round(buildPosition.transform.position.x / gridSize) * gridSize;
        snapPosition.y = 0;
        snapPosition.z = Mathf.Round(buildPosition.transform.position.z / gridSize) * gridSize;
        return snapPosition;
    }    

    public float GetGridSize()
    {
        return gridSize;
    }
}
