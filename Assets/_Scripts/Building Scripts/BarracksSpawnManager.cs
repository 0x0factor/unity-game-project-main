using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.Buildings.BuildingFunction.BarrackSelection;
using UnityEngine.UI;
using GameProject.ProjectAssets.Units;

namespace GameProject.ProjectAssets.Control.BarracksMenu
{
    public class BarracksSpawnManager : MonoBehaviour
    {
        //scriptable object for unit data access
        [SerializeField] private BasicUnitScriptableObject tankUnitSO;
        [SerializeField] private BasicUnitScriptableObject infantryUnitSO;
        [SerializeField] private BasicUnitScriptableObject lavSO;
        [SerializeField] private BasicUnitScriptableObject medicSO;
        [SerializeField] private BasicUnitScriptableObject engineerSO;

        private Transform barrackSpawnPoint;

        //tank references
        public GameObject blueprintTank;
        private GameObject instBlueprintTank;
        public GameObject tankUnit;

        //infantry references
        public GameObject blueprintInfantry;
        private GameObject instBlueprintInfantry;
        public GameObject infantryUnit;

        //lav references
        public GameObject blueprintLAV;
        private GameObject instBlueprintLAV;
        public GameObject lavUnit;

        //medic references
        public GameObject blueprintMedic;
        private GameObject instBlueprintMedic;
        public GameObject medicUnit;

        //engineer references
        public GameObject blueprintEngineer;
        private GameObject instBlueprintEngineer;
        public GameObject engineerUnit;

        //costs
        //infantry
        private int infantryBuildCost;
        private int infantryBuyCost;
        //tank
        private int tankBuildCost;
        private int tankBuyCost;
        //LAV
        private int lavBuildCost;
        private int lavBuyCost;
        //medic
        private int medicBuildCost;
        private int medicBuyCost;
        //engineer
        private int engineerBuildCost;
        private int engineerBuyCost;

        //text to be changed
        private Text countdownText;

        private BarrackSelections barrackSelections;
        private CountdownTimer countdownTimer;

        //instance reference of the resource manager
        private ResourceMenuManager resourceMenuManager;

        //build time of the units
        public float infantryCountdown = 10;
        public float tankCountdown = 60;
        public float lavCountdown = 45;
        public float medicCountdown = 20;
        public float engineerCountdown = 50;

        private Barracks barracks;
        private bool activeCountdown = false;

        public void SetActiveCountdownBool(bool isActive)
        {
            activeCountdown = isActive;
        }

        private void Awake()
        {
            //create instance of this object for use in this class
            barrackSelections = FindObjectOfType<BarrackSelections>();
            resourceMenuManager = FindObjectOfType<ResourceMenuManager>();
            //costs
            //tank
            tankBuildCost = tankUnitSO.buildCost;
            tankBuyCost = tankUnitSO.buyCosts;
            //infantry
            infantryBuildCost = infantryUnitSO.buildCost;
            infantryBuyCost = infantryUnitSO.buyCosts;
            //lav
            lavBuildCost = lavSO.buildCost;
            lavBuyCost = lavSO.buyCosts;
            //medic
            medicBuildCost = medicSO.buildCost;
            medicBuyCost = medicSO.buyCosts;
            //engineer
            engineerBuildCost = engineerSO.buildCost;
            engineerBuyCost = engineerSO.buyCosts;
        }

        //on button click outright buy tank
        public void BuyTank()
        {
            for (int i = 0; i < barrackSelections.barrackSelected.Count; i++)
            {
                //check for sufficient funds
                if (resourceMenuManager.totalCash >= (tankBuyCost * 
                    barrackSelections.barrackSelected.Count))
                {
                    barracks = barrackSelections.barrackSelected[i].GetComponent<Barracks>();
                    activeCountdown = barracks.GetCurrentlyBuilding();
                    //is there already a unit building?
                    if (!activeCountdown)
                    {
                        barrackSpawnPoint = barrackSelections.barrackSelected[i].transform.GetChild(3);
                        Instantiate(tankUnit, barrackSpawnPoint.position, barrackSpawnPoint.rotation);

                        barracks = barrackSelections.barrackSelected[i].GetComponent<Barracks>();
                        activeCountdown = barracks.GetCurrentlyBuilding();

                        resourceMenuManager.totalCash -=
                            (barrackSelections.barrackSelected.Count * tankBuyCost);

                        activeCountdown = false;
                    }
                    else
                    {
                        Debug.Log("There is already a unit being built!");
                    }
                }
                else
                {
                    Debug.Log("Insufficient Funds!");
                }
            }
        }

        //on button click build tank
        public void BuildTank()
        {
            //for every barracks that is selected (multi-selection)
            for (int i = 0; i < barrackSelections.barrackSelected.Count; i++)
            {
                //check for sufficient funds
                if (resourceMenuManager.totalCash >= tankBuildCost *
                    barrackSelections.barrackSelected.Count)
                {
                    barracks = barrackSelections.barrackSelected[i].GetComponent<Barracks>();
                    activeCountdown = barracks.GetCurrentlyBuilding();
                    //is there already a unit building?
                    if (!activeCountdown)
                    {
                        //transform of spawn point game object at child index (3)
                        barrackSpawnPoint = barrackSelections.barrackSelected[i].transform.GetChild(3);
                        //instance of blueprint tank
                        instBlueprintTank = Instantiate(blueprintTank, barrackSpawnPoint.position,
                            barrackSpawnPoint.rotation);
                        //text of countdown of the instance blueprint tank
                        countdownText = instBlueprintTank.transform.GetChild(1).gameObject.
                            transform.GetChild(0).GetComponent<Text>();
                        //CountdownTimer script of the blueprint tank instanced
                        countdownTimer = instBlueprintTank.GetComponent<CountdownTimer>();
                        //method call GetDurationAndTextObject(arg1, arg2)
                        countdownTimer.SetDurationaAndTextObject(tankCountdown, countdownText);
                        countdownTimer.SetTimerRunning(true);
                        resourceMenuManager.totalCash -=
                            barrackSelections.barrackSelected.Count * tankBuildCost;

                        activeCountdown = true;
                        barracks.SetCurrentlyBuilding(true);
                        barracks.SetCurrentBlueprint(instBlueprintTank);
                    }
                    else
                    {
                        Debug.Log("There is already a unit being built!");
                    }
                }
                else
                {
                    Debug.Log("Insufficient Funds!");
                }
            }
        }

        //on button click outright buy infantry
        public void BuyInfantry()
        {
            for (int i = 0; i < barrackSelections.barrackSelected.Count; i++)
            {
                if (resourceMenuManager.totalCash >= (infantryBuyCost *
                    barrackSelections.barrackSelected.Count))
                {
                    barracks = barrackSelections.barrackSelected[i].GetComponent<Barracks>();
                    activeCountdown = barracks.GetCurrentlyBuilding();
                    //is there already a unit building?
                    if (!activeCountdown)
                    {
                        barrackSpawnPoint = barrackSelections.barrackSelected[i].transform.GetChild(3);
                        Instantiate(infantryUnit, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
                        //false because it is outright
                        resourceMenuManager.totalCash -=
                            barrackSelections.barrackSelected.Count * infantryBuyCost;

                        activeCountdown = false;
                    }
                    else
                    {
                        Debug.Log("There is already a unit being built!");
                    }
                }
                else
                {
                    Debug.Log("Insufficient Funds!");
                }
            }
        }

        //on button click train infantry
        public void TrainInfantry()
        {
            //for every barracks that is selected (multi-selection)
            for (int i = 0; i < barrackSelections.barrackSelected.Count; i++)
            {
                if (resourceMenuManager.totalCash >= infantryBuildCost *
                    barrackSelections.barrackSelected.Count)
                {
                    barracks = barrackSelections.barrackSelected[i].GetComponent<Barracks>();
                    activeCountdown = barracks.GetCurrentlyBuilding();
                    //is there already a unit building?
                    if (!activeCountdown)
                    {
                        //transform of spawn point game object at child index (3)
                        barrackSpawnPoint = barrackSelections.barrackSelected[i].transform.GetChild(3);
                        //instance of blueprint infantry
                        instBlueprintInfantry = Instantiate(blueprintInfantry, barrackSpawnPoint.position,
                            barrackSpawnPoint.rotation);
                        //text of countdown of the instance blueprint infantry
                        countdownText = instBlueprintInfantry.transform.GetChild(1).gameObject.
                            transform.GetChild(0).GetComponent<Text>();
                        //CountdownTimer script of the blueprint infantry instanced
                        countdownTimer = instBlueprintInfantry.GetComponent<CountdownTimer>();
                        //method call GetDurationAndTextObject(arg1, arg2)
                        countdownTimer.SetDurationaAndTextObject(infantryCountdown, countdownText);
                        countdownTimer.SetTimerRunning(true);
                        resourceMenuManager.totalCash -=
                            barrackSelections.barrackSelected.Count * infantryBuildCost;



                        activeCountdown = true;
                        barracks.SetCurrentlyBuilding(true);
                        barracks.SetCurrentBlueprint(instBlueprintInfantry);
                    }
                    else
                    {
                        Debug.Log("There is already a unit being built!");
                    }   
                }
                else
                {
                    Debug.Log("Insufficient Funds!");
                }
            }
        }

        //on button click outright buy lav
        public void BuyLAV()
        {
            for (int i = 0; i < barrackSelections.barrackSelected.Count; i++)
            {
                if (resourceMenuManager.totalCash >= (lavBuyCost *
                    barrackSelections.barrackSelected.Count))
                {
                    barracks = barrackSelections.barrackSelected[i].GetComponent<Barracks>();
                    activeCountdown = barracks.GetCurrentlyBuilding();
                    //is there already a unit building?
                    if (!activeCountdown)
                    {
                        barrackSpawnPoint = barrackSelections.barrackSelected[i].transform.GetChild(3);
                        Instantiate(lavUnit, barrackSpawnPoint.position, barrackSpawnPoint.rotation);
                        resourceMenuManager.totalCash -=
                            barrackSelections.barrackSelected.Count * lavBuyCost;

                        activeCountdown = false;
                    }
                    else
                    {
                        Debug.Log("There is already a unit being built!");
                    }
                }
                else
                {
                    Debug.Log("Insufficient Funds!");
                }
            }
        }

        //on button click build lav
        public void BuildLAV()
        {
            //for every barracks that is selected (multi-selection)
            for (int i = 0; i < barrackSelections.barrackSelected.Count; i++)
            {
                if (resourceMenuManager.totalCash >= lavBuildCost *
                    barrackSelections.barrackSelected.Count)
                {
                    barracks = barrackSelections.barrackSelected[i].GetComponent<Barracks>();
                    activeCountdown = barracks.GetCurrentlyBuilding();
                    //is there already a unit building?
                    if (!activeCountdown)
                    {
                        //transform of spawn point game object at child index (3)
                        barrackSpawnPoint = barrackSelections.barrackSelected[i].transform.GetChild(3);
                        //instance of blueprint lav
                        instBlueprintLAV = Instantiate(blueprintLAV, barrackSpawnPoint.position,
                            barrackSpawnPoint.rotation);

                        //text of countdown of the instance blueprint lav
                        countdownText = instBlueprintLAV.transform.GetChild(1).gameObject.
                            transform.GetChild(0).GetComponent<Text>();

                        //CountdownTimer script of the blueprint infantry instanced
                        countdownTimer = instBlueprintLAV.GetComponent<CountdownTimer>();

                        //method call GetDurationAndTextObject(arg1, arg2)
                        countdownTimer.SetDurationaAndTextObject(lavCountdown, countdownText);
                        countdownTimer.SetTimerRunning(true);

                        resourceMenuManager.totalCash -=
                            barrackSelections.barrackSelected.Count * lavBuildCost;

                        activeCountdown = true;
                        barracks.SetCurrentlyBuilding(true);
                        barracks.SetCurrentBlueprint(instBlueprintLAV);
                    }
                    else
                    {
                        Debug.Log("There is already a unit being built!");
                    }
                }
                else
                {
                    Debug.Log("Insufficient Funds!");
                }
            }
        }

        //on button click outright buy medic
        public void BuyMedic()
        {
            for (int i = 0; i < barrackSelections.barrackSelected.Count; i++)
            {
                if (resourceMenuManager.totalCash >= (medicBuyCost *
                    barrackSelections.barrackSelected.Count))
                {
                    barracks = barrackSelections.barrackSelected[i].GetComponent<Barracks>();
                    activeCountdown = barracks.GetCurrentlyBuilding();
                    //is there already a unit building?
                    if (!activeCountdown)
                    {
                        barrackSpawnPoint = barrackSelections.barrackSelected[i].transform.GetChild(3);
                        Instantiate(medicUnit, barrackSpawnPoint.position, barrackSpawnPoint.rotation);

                        resourceMenuManager.totalCash -=
                            barrackSelections.barrackSelected.Count * medicBuyCost;

                        activeCountdown = false;
                    }
                    else
                    {
                        Debug.Log("There is already a unit being built!");
                    }
                }
                else
                {
                    Debug.Log("Insufficient Funds!");
                }
            }
        }

        //on button click train medic
        public void TrainMedic()
        {
            //for every barracks that is selected (multi-selection)
            for (int i = 0; i < barrackSelections.barrackSelected.Count; i++)
            {
                if (resourceMenuManager.totalCash >= medicBuildCost *
                    barrackSelections.barrackSelected.Count)
                {
                    barracks = barrackSelections.barrackSelected[i].GetComponent<Barracks>();
                    activeCountdown = barracks.GetCurrentlyBuilding();
                    //is there already a unit building?
                    if (!activeCountdown)
                    {
                        //transform of spawn point game object at child index (3)
                        barrackSpawnPoint = barrackSelections.barrackSelected[i].transform.GetChild(3);
                        //instance of blueprint medic
                        instBlueprintMedic = Instantiate(blueprintMedic, barrackSpawnPoint.position,
                            barrackSpawnPoint.rotation);

                        //text of countdown of the instance blueprint medic
                        countdownText = instBlueprintMedic.transform.GetChild(1).gameObject.
                            transform.GetChild(0).GetComponent<Text>();

                        //CountdownTimer script of the blueprint medic instanced
                        countdownTimer = instBlueprintMedic.GetComponent<CountdownTimer>();

                        //method call GetDurationAndTextObject(arg1, arg2)
                        countdownTimer.SetDurationaAndTextObject(medicCountdown, countdownText);
                        countdownTimer.SetTimerRunning(true);

                        resourceMenuManager.totalCash -=
                            barrackSelections.barrackSelected.Count * medicBuildCost;

                        activeCountdown = true;
                        barracks.SetCurrentlyBuilding(true);
                        barracks.SetCurrentBlueprint(instBlueprintMedic);
                    }
                    else
                    {
                        Debug.Log("There is already a unit being built!");
                    }
                }
                else
                {
                    Debug.Log("Insufficient Funds!");
                }
            }
        }

        //on button click outright buy engineer
        public void BuyEngineer()
        {
            for (int i = 0; i < barrackSelections.barrackSelected.Count; i++)
            {
                if (resourceMenuManager.totalCash >= (engineerBuyCost *
                    barrackSelections.barrackSelected.Count))
                {
                    barracks = barrackSelections.barrackSelected[i].GetComponent<Barracks>();
                    activeCountdown = barracks.GetCurrentlyBuilding();
                    //is there already a unit building?
                    if (!activeCountdown)
                    {
                        barrackSpawnPoint = barrackSelections.barrackSelected[i].transform.GetChild(3);
                        Instantiate(engineerUnit, barrackSpawnPoint.position, barrackSpawnPoint.rotation);

                        resourceMenuManager.totalCash -=
                            barrackSelections.barrackSelected.Count * engineerBuyCost;

                        activeCountdown = false;
                    }
                    else
                    {
                        Debug.Log("There is already a unit being built!");
                    }
                }
                else
                {
                    Debug.Log("Insufficient Funds!");
                }
            }
        }

        //on button click train engineer
        public void TrainEngineer()
        {
            //for every barracks that is selected (multi-selection)
            for (int i = 0; i < barrackSelections.barrackSelected.Count; i++)
            {
                if (resourceMenuManager.totalCash >= engineerBuildCost *
                    barrackSelections.barrackSelected.Count)
                {
                    barracks = barrackSelections.barrackSelected[i].GetComponent<Barracks>();
                    activeCountdown = barracks.GetCurrentlyBuilding();
                    //is there already a unit building?
                    if (!activeCountdown)
                    {
                        //transform of spawn point game object at child index (3)
                        barrackSpawnPoint = barrackSelections.barrackSelected[i].transform.GetChild(3);
                        //instance of blueprint engineer
                        instBlueprintEngineer = Instantiate(blueprintEngineer, barrackSpawnPoint.position,
                            barrackSpawnPoint.rotation);

                        //text of countdown of the instance blueprint engineer
                        countdownText = instBlueprintEngineer.transform.GetChild(1).gameObject.
                            transform.GetChild(0).GetComponent<Text>();

                        //CountdownTimer script of the blueprint engineer instanced
                        countdownTimer = instBlueprintEngineer.GetComponent<CountdownTimer>();

                        //method call GetDurationAndTextObject(arg1, arg2)
                        countdownTimer.SetDurationaAndTextObject(engineerCountdown, countdownText);
                        countdownTimer.SetTimerRunning(true);

                        resourceMenuManager.totalCash -=
                            barrackSelections.barrackSelected.Count * engineerBuildCost;

                        activeCountdown = true;
                        barracks.SetCurrentlyBuilding(true);
                        barracks.SetCurrentBlueprint(instBlueprintMedic);
                    }
                    else
                    {
                        Debug.Log("There is already a unit being built!");
                    }
                }
                else
                {
                    Debug.Log("Insufficient Funds!");
                }
            }
        }
    }
}