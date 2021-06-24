using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void Update()
    {
        //m_position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }
}