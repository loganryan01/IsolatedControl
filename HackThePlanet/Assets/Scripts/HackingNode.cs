﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HackingNode : MonoBehaviour
{
    #region Fields

    //[HideInInspector]
    public Vector3 m_position;

    [SerializeField]
    public float m_timeValue = 0;

    [SerializeField]
    public bool m_isStartNode = false;

    [SerializeField]
    public bool m_isEndNode = false;

    [SerializeField]
    public HackingNode[] m_connections;

    [SerializeField]
    public bool m_randomSprite;

    [SerializeField]
    public Sprite[] m_nodeSprites;

    [SerializeField]
    public Sprite m_selectedSprite;

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
        m_position = gameObject.transform.position;
        
        if (m_randomSprite)
        {
            int spriteID = Random.Range(0, m_nodeSprites.Length);
            m_selectedSprite = m_nodeSprites[spriteID];
        }

        gameObject.GetComponent<Image>().sprite = m_selectedSprite;

        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = m_timeValue.ToString();
    }
}