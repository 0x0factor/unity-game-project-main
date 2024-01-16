using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTangible : MonoBehaviour
{
    [SerializeField] private GameObject tangiblePrefab;

    public void SpawnActualPrefab(GameObject currentBlueprint)
    {
        Instantiate(tangiblePrefab, transform.position, transform.rotation);
        Destroy(currentBlueprint);
    }
}