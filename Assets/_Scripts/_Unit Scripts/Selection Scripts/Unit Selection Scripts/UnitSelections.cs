using System.Collections.Generic;
using UnityEngine;

namespace GameProject.ProjectAssets.Units.UnitFunction.UnitSelection
{
    public class UnitSelections : MonoBehaviour
    {
        public List<GameObject> unitList = new List<GameObject>();
        public List<GameObject> unitsSelected = new List<GameObject>();

        private static UnitSelections _instance;
        public static UnitSelections Instance { get { return _instance; } }

        public void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public void AddSelection(GameObject unitToAdd)
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }



        public void RemoveSelection(GameObject unitToRemove)
        {
            unitsSelected.Remove(unitToRemove);
            unitToRemove.GetComponent<UnitMovement>().enabled = false;
        }

        public void ClickSelect(GameObject unitToAdd)
        {
            DeselectAll();
            AddSelection(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        }


        /* when holding shift with mouse 1, the selected unit is added to the list of selected units,
        also removed if clicked on again */
        public void ShiftClickSelect(GameObject unitToAdd)
        {
            if (unitsSelected.Contains(unitToAdd))
            {
                unitToAdd.transform.GetChild(0).gameObject.SetActive(false);
                RemoveSelection(unitToAdd);
            }
            else
            {
                AddSelection(unitToAdd);
                unitToAdd.transform.GetChild(0).gameObject.SetActive(true);

            }
        }


        //selection on drag
        public void DragSelect(GameObject unitToAdd)
        {
            if (!unitsSelected.Contains(unitToAdd))
            {
                unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
                AddSelection(unitToAdd);
            }
        }

        //removes all units selected in selected list
        public void DeselectAll()
        {
            foreach (var unit in unitsSelected)
            {
                if (unit != null)
                {
                    unit.transform.GetChild(0).gameObject.SetActive(false);
                    unit.GetComponent<UnitMovement>().enabled = false;
                }
            }
            unitsSelected.Clear();
        }
    }
}