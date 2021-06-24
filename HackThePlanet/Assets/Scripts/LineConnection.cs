using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineConnection : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject lineHolder;

    private bool hasDrawn = false;

    private void Update()
    {
        if (!hasDrawn)
        {
            DrawConnections();
            hasDrawn = true;
        }
    }

    public void DrawLine(GameObject a_node1, GameObject a_node2)
    {
        GameObject angleBar = Instantiate(linePrefab, a_node2.transform.position, Quaternion.identity);

        // Calculate angle
        Vector2 diference = a_node1.transform.position - a_node2.transform.position;
        float sign = (a_node1.transform.position.y < a_node2.transform.position.y) ? -1.0f : 1.0f;
        float angle = Vector2.Angle(Vector2.right, diference) * sign;
        angleBar.transform.Rotate(0, 0, angle);

        // Calculate length of bar
        float height = 2;
        float width = Vector2.Distance(a_node2.transform.position, a_node1.transform.position);
        angleBar.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        // Calculate midpoint position
        float newposX = a_node2.transform.position.x + (a_node1.transform.position.x - a_node2.transform.position.x) / 2;
        float newposY = a_node2.transform.position.y + (a_node1.transform.position.y - a_node2.transform.position.y) / 2;
        angleBar.transform.position = new Vector3(newposX, newposY, a_node1.transform.position.z);

        // Set bar parent
        angleBar.transform.SetParent(lineHolder.transform, true);
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
                    DrawLine(node.gameObject, connection.gameObject);
                }
            }
        }
    }
}
