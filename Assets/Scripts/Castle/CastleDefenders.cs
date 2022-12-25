using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDefenders : MonoBehaviour
{
    [SerializeField] GameObject archerPrefab;
    int archerCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void SpawnArchers()
    {
        archerCount = PlayerPrefs.GetInt("archers");
        for (int i = 0; i < archerCount; i++)
        {
            GameObject clone = Instantiate(archerPrefab, transform.GetChild((i)));
            clone.transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
