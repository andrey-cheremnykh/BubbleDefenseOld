using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    [SerializeField] GameObject[] cannonPrefabs;
    MeshFilter renderer; 
    [SerializeField] Mesh[] levelMesh;
    [SerializeField] float[] heights;
    int level = 0;
    // Start is called before the first frame update
    public void SetTop(int level)
    {
        level--;
        this.level = level;
        renderer = GetComponent<MeshFilter>();
        renderer.mesh = levelMesh[level];
        transform.localPosition = new Vector3(0, heights[level], 0);
    }

    public void ShootEnemy(Transform enemy)
    {
        Vector3 startPos = transform.position + Vector3.up;
        GameObject cannon = Instantiate(cannonPrefabs[level], startPos, Quaternion.identity);
        cannon.GetComponent<Cannonball>().LaunchCannonball(enemy.position); 
    }
}
