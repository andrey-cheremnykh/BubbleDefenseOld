using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum BuildButtonState
{
    ACTIVE,
    DISABLED,
    TRANSITION
}
public class BuildTowerGui : MonoBehaviour
{
    [SerializeField] GameObject buildButtons;
    [SerializeField] GameObject upgradeButtons;
    [SerializeField] GameObject SliderTowerPrefab;
    public BuildButtonState stateBuild = BuildButtonState.DISABLED;
    public BuildButtonState stateUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        buildButtons.SetActive(false);
        upgradeButtons.SetActive(false);
    }

    public IEnumerator SetBuildbuttons(Vector3 worldPos)
    {
        if (stateBuild == BuildButtonState.DISABLED) yield break;
        stateBuild = BuildButtonState.TRANSITION;
        Vector3 canvasPos = Camera.main.WorldToScreenPoint(worldPos);
        buildButtons.SetActive(true);
        buildButtons.GetComponent<Animator>().SetBool("show", true);
        yield return new WaitForSeconds(0.5f);
    }
    public IEnumerator SetUpgrateButtons(Vector3 towerPos)
    {
        if (stateUpgrade != BuildButtonState.DISABLED) yield break;
        stateUpgrade = BuildButtonState.TRANSITION;
        Vector3 canvasPos = Camera.main.WorldToScreenPoint(towerPos);
        upgradeButtons.SetActive(true);
        upgradeButtons.transform.position = canvasPos;
        stateUpgrade = BuildButtonState.ACTIVE;
        yield return new WaitForSeconds(0.5f);
        stateUpgrade = BuildButtonState.ACTIVE;
    }
    public IEnumerator DisableBuildButtons()
    {
        if (stateBuild != BuildButtonState.ACTIVE) yield break;
        stateBuild = BuildButtonState.TRANSITION;
        buildButtons.GetComponent<Animator>().SetBool("show",false);
        yield return new WaitForSeconds(0.5f);
        buildButtons.SetActive(false);
        stateBuild = BuildButtonState.DISABLED;
    }public IEnumerator DisableUpgradeButtons()
    {
        if (stateUpgrade != BuildButtonState.ACTIVE) yield break;
        stateUpgrade = BuildButtonState.TRANSITION;
        upgradeButtons.GetComponent<Animator>().SetBool("show",false);
        yield return new WaitForSeconds(0.5f);
        upgradeButtons.SetActive(false);
        stateUpgrade = BuildButtonState.DISABLED;
    }
    public void CreateTowerSlider(Transform tower)
    {
        GameObject cloneSlider = Instantiate(SliderTowerPrefab, transform);
        cloneSlider.GetComponent<SliderForTower>().SetTower(tower);
    }
}
