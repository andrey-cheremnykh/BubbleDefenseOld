using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonState
{
    ACTIVE,
    DISABLED,
    TRANSITION
}


public class BuildTowerGUI : MonoBehaviour
{

    [SerializeField] GameObject buildButtons;
    [SerializeField] GameObject upgradeButtonsBasic;
    [SerializeField] GameObject upgradeButtonsAdvanced;
    [SerializeField] GameObject sliderTowerPrefab;

    public ButtonState stateBuild = ButtonState.DISABLED;
    public ButtonState stateUpgrades = ButtonState.DISABLED;

    // Start is called before the first frame update
    void Start()
    {
        buildButtons.SetActive(false);
        upgradeButtonsBasic.SetActive(false);
    }

    public IEnumerator SetBuildButtons(Vector3 worldPos)
    {
        if (stateBuild != ButtonState.DISABLED) yield break;

        stateBuild = ButtonState.TRANSITION;
        buildButtons.SetActive(true);
        Vector3 canvasPos = Camera.main.WorldToScreenPoint(worldPos);
        buildButtons.transform.position = canvasPos;
        buildButtons.GetComponent<Animator>().SetBool("show", true);
        yield return new WaitForSeconds(0.5f);

        stateBuild = ButtonState.ACTIVE;
    }

    public IEnumerator DisableBuildButtons()
    {
        if (stateBuild != ButtonState.ACTIVE) yield break;

        stateBuild = ButtonState.TRANSITION;
        buildButtons.GetComponent<Animator>().SetBool("show", false);

        yield return new WaitForSeconds(0.5f);
        buildButtons.SetActive(false);
        stateBuild = ButtonState.DISABLED;
    }

    public IEnumerator SetUpgradeButtons(Vector3 towerPos, TowerState state)
    {
        if (stateUpgrades != ButtonState.DISABLED) yield break;
        stateUpgrades = ButtonState.TRANSITION;
        Vector3 canvasPos = Camera.main.WorldToScreenPoint(towerPos);
        if (state == TowerState.LEVEL_3)
        {
            upgradeButtonsAdvanced.SetActive(true);
            upgradeButtonsAdvanced.transform.position = canvasPos;
            upgradeButtonsAdvanced.GetComponent<Animator>().SetBool("show", true);
        }
        else
        {
            upgradeButtonsBasic.SetActive(true);
            if (state == TowerState.LEVEL_4A || state == TowerState.LEVEL_4B)
                upgradeButtonsBasic.transform.GetChild(0).gameObject.SetActive(false);
            else
                upgradeButtonsBasic.transform.GetChild(0).gameObject.SetActive(true);
            upgradeButtonsBasic.transform.position = canvasPos;
            upgradeButtonsBasic.GetComponent<Animator>().SetBool("show", true);
        }
        yield return new WaitForSeconds(0.5f);
        stateUpgrades = ButtonState.ACTIVE;
    }
    
    public IEnumerator DisableUpgradeButtons()
    {
        if (stateUpgrades != ButtonState.ACTIVE) yield break;
        if(upgradeButtonsBasic.activeSelf == true)
        {
            upgradeButtonsBasic.GetComponent<Animator>().SetBool("show", false);
        }
        else
        {
            upgradeButtonsAdvanced.GetComponent<Animator>().SetBool("show", false);
        }
        stateUpgrades = ButtonState.TRANSITION;

        yield return new WaitForSeconds(0.5f);
        stateUpgrades = ButtonState.DISABLED;
        upgradeButtonsBasic.SetActive(false);
        upgradeButtonsAdvanced.SetActive(false);
    }


    public void CreateTowerSlider(Transform tower)
    {
        GameObject cloneSlider = Instantiate(sliderTowerPrefab, transform);
        cloneSlider.GetComponent<SliderForTower>().SetTower(tower);
    }

   
}
