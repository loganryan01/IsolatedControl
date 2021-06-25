using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableJackScript : MonoBehaviour
{
    [SerializeField]
    ServerScript serverScript;
    CableScript cableScript;
    
    // Start is called before the first frame update
    void Start()
    {
        cableScript = GetComponentInParent<CableScript>();
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.name == cableScript.cablePort.name)
        {
            cableScript.interactable = false;

            cableScript.moveableCablePart.transform.position = cableScript.cablePort.transform.position;

            serverScript.completedWires++;
        }
    }
}
