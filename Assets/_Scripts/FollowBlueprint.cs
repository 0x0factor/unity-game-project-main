using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FollowBlueprint : MonoBehaviour
{
    RaycastHit hit;
    private CCSpawnManager ccSpawnManager;

    private SpawnTangible spawnTangible;

    public float blueprintCountdown;

    private void Awake()
    {
        ccSpawnManager = FindObjectOfType<CCSpawnManager>();
        spawnTangible = gameObject.GetComponent<SpawnTangible>();
    }

    void Start()
    {
        //on instantiation, begin following mouse pointer
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 7)))
        {
            transform.position = hit.point;
        }
    }

    void Update()
    {
        BlueprintFollowLogic();
    }

    private void BlueprintFollowLogic()
    {
        //follow mouse pointer every frame
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 7)))
        {
            transform.position = hit.point;
        }

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!ccSpawnManager.GetIsOutright())
                {
                    //instance of CountdownTimer class attached to this object
                    CountdownTimer countdownTimer = gameObject.GetComponent<CountdownTimer>();
                    //enable text child
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    //countdown object text component
                    Text countdownText = transform.GetChild(1).gameObject.
                        transform.GetChild(0).GetComponent<Text>();
                    //passing information to the wallCountdownTimer script via member method
                    countdownTimer.SetDurationaAndTextObject(blueprintCountdown, countdownText);
                    //activate the timer and begin running
                    countdownTimer.SetTimerRunning(true);

                    //remove this script from attached game object
                    var followBlueprint = gameObject.GetComponent<FollowBlueprint>();
                    Destroy(followBlueprint);
                }
                else
                {
                    spawnTangible.SpawnActualPrefab(gameObject);
                    Destroy(gameObject);
                }
            }
        }

        //orient the building rotation
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(0, -1 * 2, 0);
        }
        if (Input.GetKey(KeyCode.C))
        {
            transform.Rotate(0, 1 * 2, 0);
        }
    }

    //for future development
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Enter!");
    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("ExitBuildMode!");
    }
}