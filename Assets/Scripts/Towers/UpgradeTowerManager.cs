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
        StartCoroutine(towerGUI.SetUpgradeButtons(selectedTower.transform.position, selectedTower.state));

    }

    public void UpgradeSelectedTower()
    {
        selectedTower.UpgradeTower();
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
        DisableAllButtons();
    }

    public void DisableAllButtons()
    {
        StartCoroutine(towerGUI.DisableUpgradeButtons());
    }

    
}
