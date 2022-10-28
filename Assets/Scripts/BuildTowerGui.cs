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
    BuildButtonState stateBuild = BuildButtonState.DISABLED;
    // Start is called before the first frame update
    void Start()
    {
        buildButtons.SetActive(false);
    }

    public IEnumerator SetBuildbuttons(Vector3 worldPos)
    {
        if (stateBuild == BuildButtonState.TRANSITION) yield break;
        stateBuild = BuildButtonState.TRANSITION;
        Vector3 canvasPos = Camera.main.WorldToScreenPoint(worldPos);
        buildButtons.SetActive(true);
        buildButtons.transform.position = canvasPos;
        buildButtons.GetComponent<Animator>().SetBool("show", true);
        yield return new WaitForSeconds(0.5f);
        stateBuild = BuildButtonState.ACTIVE;
    }
}
