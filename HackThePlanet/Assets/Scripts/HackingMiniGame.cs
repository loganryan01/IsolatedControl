using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HackingMiniGame : MonoBehaviour
{
    private HackingNode[] allNodes;

    private HackingNode playerStartNode;
    private HackingNode playerCurrentNode;
    public GameObject player;

    private HackingNode enemyStartNode;
    private HackingNode enemyCurrentNode;
    public GameObject enemy;

    private List<HackingNode> enemyVisited = new List<HackingNode>();

    private HackingNode endNode;

    public bool isGameFinished = false;
    public bool hasEnemyWon = false;
    private bool moveEnemy = true;
    public bool hasGameStarted = false;

    public GameObject startScreen;
    public GameObject endScreen;

    private void Start()
    {
        startScreen.SetActive(true);
        endScreen.SetActive(false);

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
        enemyCurrentNode = enemyStartNode;
        enemyVisited.Add(enemyCurrentNode);
    }

    private void Update()
    {
        if (hasGameStarted)
        {
            startScreen.SetActive(false);
            MoveEnemy();
        }

        if (isGameFinished)
        {
            StartCoroutine(EndGameDelay());
        }
    }

    public void StartGame()
    {
        hasGameStarted = true;
    }

    public void OnNodeSelect(HackingNode a_selectedNode)
    {
        if (a_selectedNode == playerCurrentNode)
        {
            StartCoroutine(InvalidNode(playerCurrentNode));
            return;
        }

        bool notConnectedNode = false;

        foreach (HackingNode connection in playerCurrentNode.m_connections)
        {
            if (a_selectedNode == connection)
            {
                StartCoroutine(TurnWaitTimePlayer(a_selectedNode));
                notConnectedNode = false;
                break;
            }

            notConnectedNode = true;
        }

        if (notConnectedNode)
        {
            StartCoroutine(InvalidNode(a_selectedNode));
        }

        if (a_selectedNode == endNode)
        {
            isGameFinished = true;
        }
    }

    IEnumerator InvalidNode(HackingNode a_node)
    {
        GameObject invalidImage = a_node.transform.Find("InvalidImage").gameObject;
        invalidImage.SetActive(true);

        // Wait for given amount of time
        yield return new WaitForSeconds(0.8f);

        invalidImage.SetActive(false);
    }

    private void MoveEnemy()
    {
        if (enemyCurrentNode != endNode)
        {
            float smallestTime = 10;
            HackingNode node = new HackingNode();

            foreach (HackingNode connection in enemyCurrentNode.m_connections)
            {
                if (connection.m_timeValue < smallestTime && !enemyVisited.Contains(connection) || connection == endNode)
                {
                    smallestTime = connection.m_timeValue;

                    if (connection == endNode)
                    {
                        node = endNode;
                        smallestTime = endNode.m_timeValue;
                        break;
                    }
                    node = connection;
                }
            }

            StartCoroutine(TurnWaitTimeEnemy(node));
        }
        else
        {
            isGameFinished = true;
            hasEnemyWon = true;
            StartCoroutine(EndGameDelay());
        }
    }

    IEnumerator TurnWaitTimePlayer(HackingNode a_node)
    {
        yield return new WaitForSeconds(a_node.m_timeValue + 0.2f);

        player.transform.position = a_node.transform.position;
        playerCurrentNode = a_node;
    }

    IEnumerator TurnWaitTimeEnemy(HackingNode a_node)
    {
        yield return new WaitForSecondsRealtime(a_node.m_timeValue * 2);

        enemyVisited.Add(a_node);
        enemy.transform.position = a_node.transform.position;
        enemyCurrentNode = a_node;
    }

    IEnumerator EndGameDelay()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        FinishGameUI();
    }

    private void FinishGameUI()
    {
        endScreen.SetActive(true);

        Canvas canvas = endScreen.GetComponentInChildren<Canvas>();
        TextMeshProUGUI gameState = canvas.GetComponentInChildren<TextMeshProUGUI>();
        Image background = canvas.transform.Find("Background").GetComponent<Image>();

        if (hasEnemyWon)
        {
            gameState.text = "You Failed!";
            background.color = Color.red;
        }
        else if (!hasEnemyWon)
        {
            gameState.text = "You Succeeded!";
            background.color = Color.green;
        }
    }

    // Used to convert hex values to decimal values and normalise them 
    private float HexToFloatNormalised(string a_hex)
    {
        int dec = System.Convert.ToInt32(a_hex, 16);
        return dec / 255f;
    }

    // Using the given hex string it returns it's color
    private Color GetColourFromString(string a_hexString)
    {
        float red = HexToFloatNormalised(a_hexString.Substring(0, 2));
        float green = HexToFloatNormalised(a_hexString.Substring(2, 2));
        float blue = HexToFloatNormalised(a_hexString.Substring(4, 2));
        float alpha = 1f;

        if (a_hexString.Length >= 8)
        {
            alpha = HexToFloatNormalised(a_hexString.Substring(6, 2));
        }

        return new Color(red, green, blue, alpha);
    }
}
