using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorCube : MonoBehaviour
{
    Waypoint waypoint;
    int gridScale;
    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        gridScale = waypoint.GetGridScale();
    }

    // Update is called once per frame
    void Update()
    {
        SnapToGrid();
        LabelCube();
    }

    void SnapToGrid()
    {
        
        float snapX = Mathf.RoundToInt(transform.position.x / gridScale) * gridScale;
        float snapZ = Mathf.RoundToInt(transform.position.z / gridScale) * gridScale;
        transform.position = new Vector3(snapX, 0, snapZ);
    }

    void LabelCube()
    {
        TextMesh text = GetComponentInChildren<TextMesh>();
        float snapX = Mathf.RoundToInt(transform.position.x / gridScale);
        float snapZ = Mathf.RoundToInt(transform.position.z / gridScale);
        if(text != null)text.text = "(" + snapX + " , " + snapZ + ")";
        gameObject.name = "Cube (" + snapX + " , " + snapZ + ")";
    }
}
