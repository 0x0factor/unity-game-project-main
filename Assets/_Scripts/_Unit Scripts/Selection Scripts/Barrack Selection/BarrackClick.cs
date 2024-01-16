using UnityEngine;
using GameProject.ProjectAssets.Buildings.BuildingFunction.BarrackSelection;
using UnityEngine.EventSystems;
using GameProject.ProjectAssets.Control.MenuController;

namespace GameProject.ProjectAssets.Control.BarrackClick
{
    public class BarrackClick : MonoBehaviour
    {
        private Camera myCam;
        public LayerMask clickableBuildings;

        private GameMenuController gameMenuController;

        private void Awake()
        {
            gameMenuController = FindObjectOfType<GameMenuController>();
        }

        void Start()
        {
            myCam = Camera.main;
        }

        void Update()
        {
            BarracksSelect();
        }

        void BarracksSelect()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableBuildings))
                {
                    if (hit.collider.gameObject.name == "Player Barracks")
                    {
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            BarrackSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                            gameMenuController.EnableBarracksMenu();
                        }
                        else
                        {
                            BarrackSelections.Instance.ClickSelect(hit.collider.gameObject);
                            gameMenuController.EnableBarracksMenu();
                        }
                    }
                    //deselect all barracks and disable menu when selecting command center
                    else if (hit.collider.gameObject.name == "Player Command Center")
                    {
                        gameMenuController.DisableBarracksMenu();
                        BarrackSelections.Instance.DeselectAll();
                    }
                }
                else
                {
                    //checks to see if the mouse was clicked over a UI element
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            BarrackSelections.Instance.DeselectAll();
                            gameMenuController.DisableBarracksMenu();
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