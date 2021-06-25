using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject fade;
    public Animator fadeEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeEffect()
    {
        Debug.Log("Fading");

        fade.SetActive(true);
        fadeEffect.enabled = true;
    }
}
