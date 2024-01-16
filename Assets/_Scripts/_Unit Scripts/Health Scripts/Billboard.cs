using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}