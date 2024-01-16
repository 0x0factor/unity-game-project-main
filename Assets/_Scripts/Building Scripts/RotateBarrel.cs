using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBarrel : MonoBehaviour
{
    public GameObject barrelToRotate;
    private float z;

    // Start is called before the first frame update
    void Start()
    {
        z = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        barrelToRotate.transform.Rotate(new Vector3(0, 0, z));
    }
}
