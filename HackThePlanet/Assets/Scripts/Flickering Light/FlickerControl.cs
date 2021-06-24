using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerControl : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    public float range1 = 0.01f;
    public float range2 = 0.1f;

    public float range3 = 0.01f;
    public float range4 = 0.1f;

    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(range1, range2);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(range3, range4);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
