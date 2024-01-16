using UnityEngine;
using GameProject.ProjectAssets.Units.UnitFunction.UnitSelection;

namespace GameProject.ProjectAssets.Control.UnitClick
{
    public class UnitClick : MonoBehaviour
    {
        //reference camera
        private Camera myCam;
        public GameObject groundMarker;

        Vector3 movePoint;

        public LayerMask clickableUnits;
        public LayerMask ground;

        public void Start()
        {
            myCam = Camera.main;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

                if (gameObject != null)
                {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableUnits))
                    {
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                        }
                        else
                        {
                            UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                        }
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            UnitSelections.Instance.DeselectAll();
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
                {
                    movePoint = hit.point;
                    movePoint.y += 2.5f;
                    groundMarker.transform.position = movePoint;
                    groundMarker.SetActive(true);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                groundMarker.SetActive(false);
            }
        }
    }
}