using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    BuildTowerManager buildTower;
    UpgradeTowerManager upgradeTower;
    // Start is called before the first frame update
    void Start()
    {
        buildTower = FindObjectOfType<BuildTowerManager>();
        upgradeTower = FindObjectOfType<UpgradeTowerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                buildTower.DisableButtons();
                upgradeTower.DisableAllButtons();
                    
            }
            else
            {
                buildTower.DisableButtons();

            }

        }

    }
}
