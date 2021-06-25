using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HackingNode : MonoBehaviour
{
    #region Fields

    [HideInInspector]
    public Vector3 m_position;

    [Header("Node Settings")]

    [SerializeField]
    public float m_timeValue = 0;

    [SerializeField]
    public bool m_isStartNode = false;

    [SerializeField]
    public bool m_isEndNode = false;

    [SerializeField]
    public bool m_isEnemyStart = false;

    [SerializeField]
    public HackingNode[] m_connections;

    [Header("Sprite Settings")]

    [SerializeField]
    public bool m_randomSprite;

    [SerializeField]
    public Sprite[] m_nodeSprites;

    [SerializeField]
    public Sprite m_selectedSprite;

    [SerializeField]
    public Sprite[] m_specialSprites;

    #endregion

    #region Constructors

    public HackingNode()
    {
        
    }

    public HackingNode(Vector3 a_position, float a_timeValue, bool a_isStartNode, bool a_isEndNode)
    {
        m_position = a_position;
        m_timeValue = a_timeValue;
        m_isStartNode = a_isStartNode;
        m_isEndNode = a_isEndNode;
    }

    public HackingNode(Vector3 a_position, float a_timeValue, bool a_isStartNode, bool a_isEndNode, HackingNode[] a_connections)
    {
        m_position = a_position;
        m_timeValue = a_timeValue;
        m_isStartNode = a_isStartNode;
        m_isEndNode = a_isEndNode;
        m_connections = a_connections;
    }

    #endregion

    public void Start()
    {
        // Get node position
        m_position = gameObject.transform.position;

        // Set time text
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = m_timeValue.ToString();

        // Set sprite
        if (m_randomSprite && !m_isStartNode && !m_isEndNode)
        {
            int spriteID = Random.Range(0, m_nodeSprites.Length);
            m_selectedSprite = m_nodeSprites[spriteID];
        }
        else if (m_isStartNode || m_isEnemyStart)
        {
            m_selectedSprite = m_specialSprites[0];
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "2";
        }
        else if (m_isEndNode)
        {
            m_selectedSprite = m_specialSprites[1];
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "1";
        }

        gameObject.GetComponent<Image>().sprite = m_selectedSprite;
    }
}