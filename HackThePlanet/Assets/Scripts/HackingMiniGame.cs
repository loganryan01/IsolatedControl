using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public AudioSource beepSound;

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
            if (beepSound.isPlaying == false)
            {
                beepSound.Play();
            }

            startScreen.SetActive(false);
            MoveEnemy();
        }

        if (isGameFinished)
        {
            beepSound.Stop();
            FinishGameUI();
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
            return;
        }

        if (a_selectedNode == endNode)
        {
            StartCoroutine(EndGameDelay());
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
            hasGameStarted = false;
            hasEnemyWon = true;
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
        hasGameStarted = false;
        isGameFinished = true;

        yield return new WaitForSecondsRealtime(1.0f);
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

    public void ExitMiniGame()
    {
        //SceneManager.LoadScene("MainGame"); //Working on a work around
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}