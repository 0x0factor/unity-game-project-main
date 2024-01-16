using UnityEngine;
using GameProject.ProjectAssets.Buildings.BuildingFunction.BarrackSelection;
using UnityEngine.EventSystems;
using GameProject.ProjectAssets.Control.MenuController;

namespace GameProject.ProjectAssets.Control.BarrackClick
{
    public class CommandCenterClick : MonoBehaviour
    {
        private Camera myCam;
        public LayerMask clickableBuildings;

        private GameMenuController gameMenuController;

        private bool isCommandCenterSelected;
        public GameObject playerCommandCenter;

        private void Awake()
        {
            gameMenuController = FindObjectOfType<GameMenuController>();
        }

        void Start()
        {
            myCam = Camera.main;
            isCommandCenterSelected = false;
        }

        void Update()
        {
            CCSelect();
        }

        void CCSelect()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableBuildings))
                {
                    if (hit.collider.gameObject.name == "Player Command Center")
                    {
                        if (!isCommandCenterSelected)
                        {
                            playerCommandCenter.transform.GetChild(0).gameObject.SetActive(true);
                            gameMenuController.DisableBarracksMenu();
                            gameMenuController.EnableCommandCenterMenu();
                            isCommandCenterSelected = true;
                        }
                    }
                    //deselect command center and disable menu when selecting barracks
                    else if (hit.collider.gameObject.name == "Player Barracks")
                    {
                        gameMenuController.DisableCommandCenterMenu();
                        playerCommandCenter.transform.GetChild(0).gameObject.SetActive(false);
                        isCommandCenterSelected = false;
                    }
                }
                else
                {
                    //checks to see if the mouse was not clicked over a UI element
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (isCommandCenterSelected)
                            {
                                gameMenuController.DisableCommandCenterMenu();
                                playerCommandCenter.transform.GetChild(0).gameObject.SetActive(false);
                                isCommandCenterSelected = false;
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
    }
}