using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.ProjectAssets.Control.MenuController
{
    public class GameMenuController : MonoBehaviour
    {
        public GameObject barracksMenu;
        public GameObject commandCenterMenu;
        public GameObject wallBuilderMenu;
        public GameObject upgradeMenu;

        public void EnableBarracksMenu()
        {
            if (!barracksMenu.activeSelf)
            {
                barracksMenu.SetActive(true);
            }
            else
            {
                return;
            }
        }
        public void DisableBarracksMenu()
        {
            if (barracksMenu.activeSelf)
            {
                barracksMenu.SetActive(false);
            }
            else
            {
                return;
            }
        }

        public void EnableCommandCenterMenu()
        {
            if (!commandCenterMenu.activeSelf)
            {
                commandCenterMenu.SetActive(true);
            }
            else
            {
                return;
            }
        }
        public void DisableCommandCenterMenu()
        {
            if (commandCenterMenu.activeSelf)
            {
                commandCenterMenu.SetActive(false);
            }
            else
            {
                return;
            }
        }

        public void EnableWallBuilderMenu()
        {
            if (!wallBuilderMenu.activeSelf)
            {
                wallBuilderMenu.SetActive(true);
            }
            else
            {
                return;
            }
        }

        public void DisableWallBuilderMenu()
        {
            if (wallBuilderMenu.activeSelf)
            {
                wallBuilderMenu.SetActive(false);
            }
            else
            {
                return;
            }
        }

        public void EnableUpgradeMenu()
        {
            if (!upgradeMenu.activeSelf)
            {
                upgradeMenu.SetActive(true);
            }
            else
            {
                return;
            }
        }

        public void DisableUpgradeMenu()
        {
            if (upgradeMenu.activeSelf)
            {
                upgradeMenu.SetActive(false);
            }
            else
            {
                return;
            }
        }
    }
}