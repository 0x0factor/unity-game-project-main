
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameProject.ProjectAssets.Units.UnitFunction;
using GameProject.ProjectAssets.Buildings.BuildingFunction;
using GameProject.ProjectAssets.Control.MenuController;

public class UpgradeSystemScript : MonoBehaviour
{
    Camera myCam;
    public LayerMask clickableBuildings;
    public LayerMask clickableUnits;

    private GameObject currentGameObject;
    private int currentLevel;

    private BasicUnit basicUnit;
    private BasicBuilding basicBuilding;

    private ResourceMenuManager resourceMenuManager;
    private GameMenuController gameMenuController;

    // Start is called before the first frame update
    void Start()
    {
        resourceMenuManager = FindObjectOfType<ResourceMenuManager>();
        gameMenuController = FindObjectOfType<GameMenuController>();
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //building layer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableBuildings))
            {
                currentGameObject = hit.collider.gameObject;
                Debug.Log(hit.collider.name);
                if (hit.collider.name != "Player Command Center")
                {
                    if (currentGameObject.name == "Player Generator")
                    {
                        //show the object is selected
                        if (!hit.collider.transform.GetChild(0).gameObject.activeSelf)
                        {
                            currentGameObject.transform.GetChild(0).gameObject.SetActive(true);
                            gameMenuController.EnableUpgradeMenu();
                        }
                    }
                }
            }
            
            //unit layer
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableUnits))
            {
                //show the object is selected
                currentGameObject = hit.collider.gameObject;
                Debug.Log(hit.collider.name);
                if (!hit.collider.transform.GetChild(0).gameObject.activeSelf)
                {
                    currentGameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameMenuController.EnableUpgradeMenu();
                }
            }
            
            //deselect all
            else
            {
                //checks to see if the mouse was clicked over a UI element
                if (!EventSystem.current.IsPointerOverGameObject())
                {

                    if (currentGameObject.transform.GetChild(0).gameObject.activeSelf)
                    {
                        if (currentGameObject != null)
                        {
                            currentGameObject.transform.GetChild(0).gameObject.SetActive(false);
                            currentGameObject = null;
                            gameMenuController.DisableUpgradeMenu();
                        }
                    }


                }
                else
                {
                    Debug.Log("Clicked on the UI");
                }

            }
        }
    }

    //button for upgrading the game object
    private void Upgrade()
    {
        
    }

    //button for selling the game object
    private void Sell()
    {

    }
}

