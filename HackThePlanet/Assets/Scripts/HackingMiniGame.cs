using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingMiniGame : MonoBehaviour
{
    private HackingNode[] allNodes;

    private HackingNode playerStartNode;
    private HackingNode playerCurrentNode;
    public GameObject player;

    private HackingNode enemyStartNode;
    public GameObject enemy;

    private HackingNode endNode;

    public bool isGameFinished = false;
    private bool updateNodeHover = true;

    private void Start()
    {
        allNodes = FindObjectsOfType<HackingNode>();

        foreach (HackingNode node in allNodes)
        {
            if (node.m_isStartNode == true)
            {
                playerStartNode = node;
            }

            if (node.m_isEnemyStart == true)
            {
                enemyStartNode = node;
            }

            if (node.m_isEndNode == true)
            {
                endNode = node;
            }
        }

        player.transform.position = playerStartNode.transform.position;
        enemy.transform.position = enemyStartNode.transform.position;

        playerCurrentNode = playerStartNode;

        //UpdateNodeHover();
    }

    public void OnNodeSelect(HackingNode a_selectedNode)
    {
        if (a_selectedNode == playerCurrentNode)
        {
            return;
        }

        foreach (HackingNode connection in playerCurrentNode.m_connections)
        {
            if (a_selectedNode == connection)
            {
                player.transform.position = a_selectedNode.transform.position;
                playerCurrentNode = a_selectedNode;
                //UpdateNodeHover();
            }
        }

        if (a_selectedNode == endNode)
        {
            isGameFinished = true;
        }

        Debug.Log(a_selectedNode);
    }

    private void UpdateNodeHover()
    {
        foreach (HackingNode node in allNodes)
        {
            foreach (HackingNode connection in playerCurrentNode.m_connections)
            {
                if (node == connection)
                {
                    node.GetComponentInChildren<Button>().interactable = true;
                }
                else
                {
                    node.GetComponentInChildren<Button>().interactable = false;
                }
            }
        }
    }
}
