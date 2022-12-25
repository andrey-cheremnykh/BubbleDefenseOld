using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGuiPanels  : MonoBehaviour
{
    [SerializeField] GameObject LevelsPanel;
    [SerializeField] GameObject UpgradePanel;
    [SerializeField] GameObject SettingsPanel;

    public void ShowLevelsPanel()
    {
        LevelsPanel.SetActive(true);
    }public void HideLevelsPanel()
    {
        LevelsPanel.SetActive(false);
    }public void ShowUpgradePanel()
    {
        UpgradePanel.SetActive(true);
    }public void HideUpgadePanel()
    {
        UpgradePanel.SetActive(false);
    }public void ShowSettingsPanel()
    {
        SettingsPanel.SetActive(true);
    }public void HideSettingsPanel()
    {
        SettingsPanel.SetActive(false);
    }
    
}
