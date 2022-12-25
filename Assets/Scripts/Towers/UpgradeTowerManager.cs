using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowerManager : MonoBehaviour
{

    Tower selectedTower;
    BuildTowerGUI towerGUI;

    // Start is called before the first frame update
    void Start()
    {
        towerGUI = FindObjectOfType<BuildTowerGUI>();
    }

    public void SelectTower(Tower newSelected)
    {
        if (towerGUI.stateUpgrades != ButtonState.DISABLED) return;
        selectedTower = newSelected;
        StartCoroutine(towerGUI. SetUpgradeButtons(selectedTower));

    }

    public void UpgradeSelectedTower()
    {
        if (selectedTower.state == TowerState.LEVEL_1) StartCoroutine(selectedTower.UpgradeToLevel2());
       else if (selectedTower.state == TowerState.LEVEL_2) StartCoroutine(selectedTower.UpgradeToLevel3());
        DisableAllButtons();
    }

    public void UpgradeSelectedTowerToLevel4A()
    {
        StartCoroutine( selectedTower.UpgradeToLevel4A());
        DisableAllButtons();
    }

    public void UpgradeSelectedTowerToLevel4B()
    {
        StartCoroutine(selectedTower.UpgradeToLevel4B());
        DisableAllButtons();
    }

    public void DestroySelectedTower()
    {
        StartCoroutine(selectedTower.DestroyTower());
        FindObjectOfType<BuildTowerManager>().DecreaseTowerCount();
        DisableAllButtons();
    }

    public void DisableAllButtons()
    {
        StartCoroutine(towerGUI.DisableUpgradeButtons());
    }

    
}
