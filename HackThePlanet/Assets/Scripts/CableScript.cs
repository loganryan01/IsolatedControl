using SplineMesh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableScript : MonoBehaviour
{
    [SerializeField]
    float mouseZPosition = 10.0f;

    

    public GameObject fixedCableJack;
    public GameObject moveableCablePart;
    public GameObject cablePort;
    
    [SerializeField]
    Spline spline;

    [HideInInspector]
    public bool interactable = true;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPosition = new Vector3(fixedCableJack.transform.position.x /** 10*/, fixedCableJack.transform.position.y /** 10*/, fixedCableJack.transform.position.z /** 10*/);
        startPosition -= new Vector3(0, 0, 1);

        spline.nodes[0].Position = startPosition;
        spline.nodes[0].Direction = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movePosition = new Vector3(moveableCablePart.transform.position.x /** 10*/, moveableCablePart.transform.position.y /** 10*/, moveableCablePart.transform.position.z /** 10*/);
        movePosition -= new Vector3(0, 0, 1);

        spline.nodes[1].Position = movePosition;
        spline.nodes[1].Direction = movePosition;

        var v3 = Input.mousePosition;
        v3.z = mouseZPosition;
        v3 = Camera.main.ScreenToWorldPoint(v3);

        if (Input.GetMouseButton(0) && interactable)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool rayHit = Physics.Raycast(ray, out hit);

            if (rayHit && hit.collider.gameObject.name == moveableCablePart.name)
            {
                moveableCablePart.transform.position = v3;

                
            }
        }
    }
}
