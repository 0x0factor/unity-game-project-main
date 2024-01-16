using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SnapBlueprint : MonoBehaviour
{
    private CCSpawnManager ccSpawnManager;

    //tangible game objects
    public GameObject tangibleConnector;
    public GameObject tangibleWall;

    //blueprint game objects
    public GameObject blueprintConnector;
    public GameObject blueprintWall;

    private GridBuildingInput gridBuildingInput;
    private Vector3 snapPosition;

    GameObject bpConInstance;
    GameObject bpWallInstance;

    private float gridSize;

    private WallBuildMenuManager wallBuildMenuManager;

    private GameObject currentConnector;
    private GameObject lastConnector;

    public float blueprintCountdown;

    // Start is called before the first frame update
    void Start()
    {
        ccSpawnManager = FindObjectOfType<CCSpawnManager>();
        gridBuildingInput = FindObjectOfType<GridBuildingInput>();
        gridSize = gridBuildingInput.GetGridSize();
        wallBuildMenuManager = FindObjectOfType<WallBuildMenuManager>();
        //set this game object as current in menu manager
        wallBuildMenuManager.SetCurrentBlueprint(gameObject);
        currentConnector = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        wallBuildMenuManager.SetbpConInstance(bpConInstance);
    }

    //LateUpdate is called after all Update functions have been called
    private void LateUpdate()
    {
        snapPosition = gridBuildingInput.CalculateSnapPosition();
        //if started building
        if (bpConInstance != null)
        {
            float maxX = bpConInstance.transform.position.x + gridSize;
            float minX = bpConInstance.transform.position.x - gridSize;
            float maxZ = bpConInstance.transform.position.z + gridSize;
            float minZ = bpConInstance.transform.position.z - gridSize;

            //if within the range of the anchor connector
            if ((snapPosition.x == bpConInstance.transform.position.x && 
                (snapPosition.z <= maxZ && snapPosition.z >= minZ)) ||
                (snapPosition.z ==  bpConInstance.transform.position.z && 
                (snapPosition.x <= maxX && snapPosition.x >= minX)))
            {
                //move blueprint which follows pointer
                transform.position = snapPosition;
            }
        }
        //position anywhere and do not anchor
        else
        {
            //move blueprint which follows pointer
            transform.position = snapPosition;
        }
    }

    private void GetInput()
    {
        //if not over any UI element
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                BuildConnector();
            }
        }
    }

    private void BuildConnector()
    {
        //cast is best practice!
        /*blueprint connector is created without this script, 
         * so as to not follow the mouse (it is set in place)*/
        bpConInstance = (GameObject)Instantiate(blueprintConnector, 
            transform.position, Quaternion.identity);
        SnapBlueprint snapBlueprintInstance = bpConInstance.GetComponent<SnapBlueprint>();
        //if last connector exists, place a tangible wall
        if (lastConnector != null)
        {
            Vector3 middle = Vector3.Lerp(currentConnector.transform.position,
            lastConnector.transform.position, 0.5f);
            //tangible wall is placed
            bpWallInstance = (GameObject)Instantiate(blueprintWall, middle, Quaternion.identity);
            bpWallInstance.transform.LookAt(lastConnector.transform);

            //is the wall piece outright?
            if (!ccSpawnManager.GetIsOutright())
            {
                CountdownTimer wallCountdownTimer = bpWallInstance.GetComponent<CountdownTimer>();
                //enable text child
                bpWallInstance.transform.GetChild(1).gameObject.SetActive(true);
                //countdown object text component
                Text countdownText = bpWallInstance.transform.GetChild(1).gameObject.
                        transform.GetChild(0).GetComponent<Text>();
                //passing information to the wallCountdownTimer script via member method
                wallCountdownTimer.SetDurationaAndTextObject(blueprintCountdown, countdownText);
                //activate the timer and begin running
                wallCountdownTimer.SetTimerRunning(true);



                CountdownTimer lastConCountdownTimer = lastConnector.GetComponent<CountdownTimer>();
                //enable text child
                lastConnector.transform.GetChild(1).gameObject.SetActive(true);
                //countdown object text component
                Text lastConCountdownText = lastConnector.transform.GetChild(1).gameObject.
                        transform.GetChild(0).GetComponent<Text>();
                //passing information to the wallCountdownTimer script via member method
                lastConCountdownTimer.SetDurationaAndTextObject(blueprintCountdown, lastConCountdownText);
                //activate the timer and begin running
                lastConCountdownTimer.SetTimerRunning(true);



                /*the last blueprint connector needs to be instantiated before the countdown procedures
                 * because the blueprint isnt there. In the buy condition, the tangible connector is
                 instantiated so the full segment is complete, whereas the blueprint isnt. Here, it now is*/
                GameObject currentBPConInst = (GameObject)Instantiate(blueprintConnector, 
                    bpConInstance.transform.position, Quaternion.identity);
                SnapBlueprint currentBlueprintSnapBlueprint = currentBPConInst.GetComponent<SnapBlueprint>();
                Destroy(currentBlueprintSnapBlueprint);

                CountdownTimer currentConCountdown = currentBPConInst.GetComponent<CountdownTimer>();
                //enable text child
                currentBPConInst.transform.GetChild(1).gameObject.SetActive(true);
                //countdown object text component
                Text currentCountdownText = currentBPConInst.transform.GetChild(1).gameObject.
                        transform.GetChild(0).GetComponent<Text>();
                //passing information to the wallCountdownTimer script via member method
                currentConCountdown.SetDurationaAndTextObject(blueprintCountdown,currentCountdownText);
                //activate the timer and begin running
                currentConCountdown.SetTimerRunning(true);
            }
            //buy
            else
            {
                //make last connector tangible
                Instantiate(tangibleConnector, lastConnector.transform.position, Quaternion.identity);
                Destroy(lastConnector);

                //make wall segment tangible
                Instantiate(tangibleWall, bpWallInstance.transform.position, 
                    bpWallInstance.transform.rotation);
                Destroy(bpWallInstance);

                //make current connector tangible
                Instantiate(tangibleConnector, bpConInstance.transform.position, Quaternion.identity);
            }
        }

        lastConnector = bpConInstance;
        Destroy(snapBlueprintInstance);
    }

    public void SetTangibleInstance(int _value)
    {
        //null sender
        if (_value == 0)
        {
            bpConInstance = null;
        }
    }
}