using SplineMesh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableScript : MonoBehaviour
{
    [SerializeField]
    float mouseZPosition = 10.0f;

    public GameObject moveableCablePart;
    public GameObject cablePort;
    
    [SerializeField]
    Spline spline;

    [HideInInspector]
    public bool interactable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spline.nodes[1].Position = moveableCablePart.transform.position;
        spline.nodes[1].Position -= new Vector3(0, 0, 1);
        spline.nodes[1].Direction = moveableCablePart.transform.position;
        spline.nodes[1].Direction -= new Vector3(0, 0, 1);
        spline.nodes[0].Direction = moveableCablePart.transform.position;
        spline.nodes[0].Direction -= new Vector3(0, 0, 1);

        var v3 = Input.mousePosition;
        v3.z = mouseZPosition;
        v3 = Camera.main.ScreenToWorldPoint(v3);

        if (Input.GetMouseButton(0) && interactable)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);

            if (hit.collider.gameObject.name == moveableCablePart.name)
            {
                moveableCablePart.transform.position = v3;
            }
        }
    }
}
