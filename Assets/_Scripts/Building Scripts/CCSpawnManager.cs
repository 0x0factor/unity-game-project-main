using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.ProjectAssets.Buildings;
using GameProject.ProjectAssets.Units;
using GameProject.ProjectAssets.Control.MenuController;

public class CCSpawnManager : MonoBehaviour
{
    //scriptable object of each purchasable item
    public BasicBuildingScriptableObject barrackBuildingSO;
    public BasicBuildingScriptableObject plasmaGeneratorBuildingSO;
    public BasicBuildingScriptableObject updatedWallBuildingSO;
    public BasicBuildingScriptableObject barrierBuildingSO;
    public BasicUnitScriptableObject heavyCannonSO;
    public BasicUnitScriptableObject energyRifleSO;
    public BasicUnitScriptableObject plasmaMachineGunSO;

    //blueprint prefabs of each building
    public GameObject blueprintBarracks;
    public GameObject blueprintPlasmaGenerator;
    public GameObject blueprintConnector;
    public GameObject blueprintBarrier;
    public GameObject blueprintHeavyCannon;
    public GameObject blueprintEnergyRifle;
    public GameObject blueprintMachineGun;

    private bool isOutright;

    private ResourceMenuManager resourceMenuManager;

    //building costs
    private int barrackBuildCost;
    private int barrackBuyCost;

    private int plasmaGenBuildCost;
    private int plasmaGenBuyCost;

    private int wallBuildCost;
    private int wallBuyCost;

    private int barrierBuildCost;
    private int barrierBuyCost;

    private int heavyCannonBuildCost;
    private int heavyCannonBuyCost;

    private int energyRifleBuildCost;
    private int energyRifleBuyCost;

    private int machineGunBuildCost;
    private int machineGunBuyCost;

    //power requirements of the buildings which need it
    private int barrackPowerReq = 10;
    private int plasmaGenPowerReq = 5;
    private int barrierPowerReq = 5;
    private int heavyCannonPowerReq = 20;
    private int energyRiflePowerReq = 20;
    private int plasmaMachineGunPowerReq = 30;
    /*wall doesnt require power*/

    private GameMenuController gameMenuController;

    public bool GetIsOutright()
    {
        return isOutright;
    }

    private void Awake()
    {
        //create instance of this object for use in this class
        resourceMenuManager = FindObjectOfType<ResourceMenuManager>();
        gameMenuController = FindObjectOfType<GameMenuController>();

        barrackBuildCost = barrackBuildingSO.buildCost;
        barrackBuyCost = barrackBuildingSO.buyCost;

        plasmaGenBuildCost = plasmaGeneratorBuildingSO.buildCost;
        plasmaGenBuyCost = plasmaGeneratorBuildingSO.buyCost;

        wallBuildCost = updatedWallBuildingSO.buildCost;
        wallBuyCost = updatedWallBuildingSO.buyCost;

        barrierBuildCost = barrierBuildingSO.buildCost;
        barrierBuyCost = barrierBuildingSO.buyCost;

        heavyCannonBuildCost = heavyCannonSO.buildCost;
        heavyCannonBuyCost = heavyCannonSO.buyCosts;

        energyRifleBuildCost = energyRifleSO.buildCost;
        energyRifleBuyCost = energyRifleSO.buyCosts;

        machineGunBuildCost = plasmaMachineGunSO.buildCost;
        machineGunBuyCost = plasmaMachineGunSO.buyCosts;
    }

    //method for the button build barracks
    public void BuildBarracks()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= barrackBuildCost && 
            resourceMenuManager.totalPower >= barrackPowerReq)
        {
            //deduct costs
            resourceMenuManager.totalCash -= barrackBuildCost;
            //deduct power cost
            resourceMenuManager.totalPower -= barrackPowerReq;
            isOutright = false;
            Instantiate(blueprintBarracks);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button buy barracks
    public void BuyBarracks()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= barrackBuyCost && 
            resourceMenuManager.totalPower >= barrackPowerReq)
        {
            //deduct costs
            resourceMenuManager.totalCash -= barrackBuyCost;
            //deduct power cost
            resourceMenuManager.totalPower -= barrackPowerReq;
            isOutright = true;
            Instantiate(blueprintBarracks);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button build plasma generator
    public void BuildPlasmaGenerator()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= plasmaGenBuildCost && 
            resourceMenuManager.totalPower >= plasmaGenPowerReq)
        {
            //deduct costs
            resourceMenuManager.totalCash -= plasmaGenBuildCost;
            //deduct power cost
            resourceMenuManager.totalPower -= plasmaGenPowerReq;
            isOutright = false;
            Instantiate(blueprintPlasmaGenerator);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button buy plasma generator
    public void BuyPlasmaGenerator()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= plasmaGenBuyCost)
        {
            //deduct costs
            resourceMenuManager.totalCash -= plasmaGenBuyCost;
            //deduct power cost
            resourceMenuManager.totalPower -= plasmaGenPowerReq;
            isOutright = true;
            Instantiate(blueprintPlasmaGenerator);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button build wall
    public void BuildWall ()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= wallBuildCost)
        {
            gameMenuController.EnableWallBuilderMenu();
            //deduct costs
            resourceMenuManager.totalCash -= wallBuildCost;
            isOutright = false;
            Instantiate(blueprintConnector);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button buy wall
    public void BuyWall()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= wallBuyCost)
        {
            gameMenuController.EnableWallBuilderMenu();
            //deduct costs
            resourceMenuManager.totalCash -= wallBuyCost;
            isOutright = true;
            Instantiate(blueprintConnector);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button build barrier
    public void BuildBarrier()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= barrierBuildCost &&
            resourceMenuManager.totalPower >= barrierPowerReq)
        {
            //deduct costs
            resourceMenuManager.totalCash -= barrierBuildCost;
            //deduct power cost
            resourceMenuManager.totalPower -= barrierPowerReq;
            isOutright = false;
            Instantiate(blueprintBarrier);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button buy barrier
    public void BuyBarrier()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= barrierBuyCost &&
            resourceMenuManager.totalPower >= barrierPowerReq)
        {
            //deduct costs
            resourceMenuManager.totalCash -= barrierBuyCost;
            //deduct power cost
            resourceMenuManager.totalPower -= barrierPowerReq;
            isOutright = true;
            Instantiate(blueprintBarrier);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button build heavy cannon
    public void BuildHeavyCannon()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= heavyCannonBuildCost &&
            resourceMenuManager.totalPower >= heavyCannonPowerReq)
        {
            //deduct costs
            resourceMenuManager.totalCash -= heavyCannonBuildCost;
            //deduct power cost
            resourceMenuManager.totalPower -= heavyCannonPowerReq;
            isOutright = false;
            Instantiate(blueprintHeavyCannon);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button buy heavy cannon
    public void BuyHeavyCannon()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= heavyCannonBuyCost &&
            resourceMenuManager.totalPower >= heavyCannonPowerReq)
        {
            //deduct costs
            resourceMenuManager.totalCash -= heavyCannonBuyCost;
            //deduct power cost
            resourceMenuManager.totalPower -= heavyCannonPowerReq;
            isOutright = true;
            Instantiate(blueprintHeavyCannon);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button build energy rifle
    public void BuildEnergyRifle()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= energyRifleBuildCost &&
            resourceMenuManager.totalPower >= energyRiflePowerReq)
        {
            //deduct costs
            resourceMenuManager.totalCash -= energyRifleBuildCost;
            //deduct power cost
            resourceMenuManager.totalPower -= energyRiflePowerReq;
            isOutright = false;
            Instantiate(blueprintEnergyRifle);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button buy energy rifle
    public void BuyEnergyRifle()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= energyRifleBuyCost &&
            resourceMenuManager.totalPower >= energyRiflePowerReq)
        {
            //deduct costs
            resourceMenuManager.totalCash -= energyRifleBuyCost;
            //deduct power cost
            resourceMenuManager.totalPower -= energyRiflePowerReq;
            isOutright = true;
            Instantiate(blueprintEnergyRifle);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button build plasma machine gun
    public void BuildMachineGun()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= machineGunBuildCost && 
            resourceMenuManager.totalPower >= plasmaMachineGunPowerReq)
        {
            //deduct costs
            resourceMenuManager.totalCash -= machineGunBuildCost;
            //deduct power cost
            resourceMenuManager.totalPower -= plasmaMachineGunPowerReq;
            isOutright = false;
            Instantiate(blueprintMachineGun);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }

    //method for the button buy plasma machine gun
    public void BuyMachineGun()
    {
        //check for sufficient funds
        if (resourceMenuManager.totalCash >= machineGunBuyCost && 
            resourceMenuManager.totalPower >= plasmaMachineGunPowerReq)
        {
            //deduct costs
            resourceMenuManager.totalCash -= machineGunBuyCost;
            //deduct power cost
            resourceMenuManager.totalPower -= plasmaMachineGunPowerReq;
            isOutright = true;
            Instantiate(blueprintMachineGun);
        }
        else
        {
            Debug.Log("Insufficient Funds!");
        }
    }
}