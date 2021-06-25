using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerScript : MonoBehaviour
{
    [HideInInspector]
    public int completedWires = 0;

    [HideInInspector]
    public bool puzzleCompleted = false;

    private void Update()
    {
        if (completedWires == 3)
        {
            puzzleCompleted = true;
        }
    }
}
