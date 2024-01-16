using System.Collections.Generic;
using UnityEngine;

namespace GameProject.ProjectAssets.Buildings.BuildingFunction.BarrackSelection
{
    public class BarrackSelections : MonoBehaviour
    {
        public List<GameObject> barrackList = new List<GameObject>();
        public List<GameObject> barrackSelected = new List<GameObject>();

        private static BarrackSelections _instance;
        public static BarrackSelections Instance { get { return _instance; } }

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

        public void AddSelection(GameObject barrackToAdd)
        {
            barrackSelected.Add(barrackToAdd);
        }

        public void RemoveSelection(GameObject barrackToRemove)
        {
            barrackSelected.Remove(barrackToRemove);
        }

        public void ClickSelect(GameObject barrackToAdd)
        {
            DeselectAll();
            AddSelection(barrackToAdd);
            barrackToAdd.transform.GetChild(0).gameObject.SetActive(true);
        }


        /*when holding shift with mouse 1, the selected unit is added to the list of selected units,
        also removed if clicked on again */
        public void ShiftClickSelect(GameObject barrackToAdd)
        {
            if (barrackSelected.Contains(barrackToAdd))
            {
                barrackToAdd.transform.GetChild(0).gameObject.SetActive(false);
                RemoveSelection(barrackToAdd);
            }
            else
            {
                AddSelection(barrackToAdd);
                barrackToAdd.transform.GetChild(0).gameObject.SetActive(true);

            }
        }

        //removes all units selected in selected list
        public void DeselectAll()
        {
            foreach (var barrack in barrackSelected)
            {
                if (barrack != null)
                {
                    barrack.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            barrackSelected.Clear();
        }
    }
}