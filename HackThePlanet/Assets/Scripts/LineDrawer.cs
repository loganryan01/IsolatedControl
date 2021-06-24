using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRend;

    // Start is called before the first frame update
    void Start()
    {
        lineRend.positionCount = 2;
    }

    // Update is called once per frame
    public void DrawLine(GameObject a_line, Vector3 a_node1, Vector3 a_node2)
    {
        lineRend = a_line.GetComponent<LineRenderer>();
        lineRend.SetPosition(0, new Vector3(a_node1.x + 0.02f, a_node1.y, a_node1.z));
        lineRend.SetPosition(1, new Vector3(a_node2.x + 0.02f, a_node2.y, a_node2.z));
    }
}
