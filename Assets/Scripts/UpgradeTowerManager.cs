using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowerManager : MonoBehaviour
{
    Tower selectedTower;
    BuildTowerGui towerGui;TowerState state;
    // Start is called before the first frame update
    void Start()
    {
        towerGui = FindObjectOfType<BuildTowerGui>();  
    }
    

    public void SelectTower(Tower newSelected)
    {
        if (towerGui.stateUpgrade == BuildButtonState.TRANSITION) return;
        selectedTower = newSelected;
        StartCoroutine(towerGui.SetUpgrateButtons(selectedTower.transform.position));
    }
    public void UpgradeSelectedTower()
    {
        StartCoroutine(selectedTower.DestroyTower());
    }
    public void DisableButtons()
    {
        StartCoroutine(towerGui.DisableUpgradeButtons());


    }
}
