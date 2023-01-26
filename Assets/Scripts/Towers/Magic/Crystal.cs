using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] MeshFilter mesh;
    [SerializeField]Mesh[] meshes;
    [SerializeField]float[] yPos;
    float startY = 6;
    // Start is called before the first frame update
    void SetCrystalLevel(int level)
    {
        mesh = GetComponent<MeshFilter>();
        level--;
        mesh.mesh = meshes[level];
    }

    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = 200;
        transform.Rotate(0,rotationSpeed*Time.deltaTime, 0);
        float move = Mathf.Sin(Time.time * 5);
        transform.localPosition = new Vector3(0, startY + move, 0);
    }
}
