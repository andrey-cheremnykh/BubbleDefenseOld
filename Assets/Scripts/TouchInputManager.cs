using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    BuildTowerManager buildTower;
    // Start is called before the first frame update
    void Start()
    {
        buildTower = FindObjectOfType<BuildTowerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {

            }
            else
            {
                buildTower.DisableAllButtons();

            }

        }

    }
}
