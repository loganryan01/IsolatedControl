using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineConnection : MonoBehaviour
{
    public GameObject linePrefab;

    private bool hasDrawn = false;

    private void Update()
    {
        if (!hasDrawn)
        {
            DrawConnections();
            hasDrawn = true;
        }
    }

    public void DrawLine(Vector3 a_node1, Vector3 a_node2, RectTransform a_line)
    {
        a_line.transform.position = a_node1;

        float distance = Vector3.Distance(a_node1, a_node2) + 10;

        a_line.transform.position = (a_node1 + a_node2) / 2;
        a_line.rect.Set((a_node1.x + a_node2.x) / 2, (a_node1.y + a_node2.y) / 2, distance, 0.005f);

        Vector3 dir = a_node2 - a_node1;
        Vector2 dirV2 = new Vector2(dir.x, dir.y);
        float angle = Vector2.SignedAngle(dirV2, Vector2.down);

        a_line.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
    }

    public void DrawConnections()
    {
        HackingNode[] allNodes = FindObjectsOfType<HackingNode>();

        if (allNodes.Length == 0)
        {
            return;
        }

        foreach (HackingNode node in allNodes)
        {
            if (node.m_connections.Length != 0)
            {
                foreach (HackingNode connection in node.m_connections)
                {
                    GameObject line = Instantiate(linePrefab, node.gameObject.transform.parent);
                    line.GetComponent<LineDrawer>().DrawLine(line, node.m_position, connection.m_position);

                    //GameObject line = Instantiate(linePrefab, node.gameObject.transform.parent);
                    //DrawLine(node.m_position, connection.m_position, line.GetComponent<RectTransform>());
                }
            }
        }
    }
}
