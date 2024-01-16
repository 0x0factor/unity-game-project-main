using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitialiser : MonoBehaviour
{
    //player assets
    private GameObject playerCommandCenter;
    private GameObject playerPlasmaGenerator;
    private GameObject playerBarracks;

    //AI assets
    private GameObject aiCommandCenter;
    private GameObject aiPlasmaGenerator;
    private GameObject aiBarracks;

    private Transform groundPrefabTransform;
    private Transform playerSpawn;
    private Transform aiSpawn;

    private void InitialisePlayerAssets()
    {
        playerSpawn = groundPrefabTransform;
    }

    private void InitialiseAIAssets()
    {

    }
}
