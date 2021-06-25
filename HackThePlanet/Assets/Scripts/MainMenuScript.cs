using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject fade;
    public Animator fadeEffect;

    public Object gameScene;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeEffect.enabled)
        {
            if (fadeEffect.GetCurrentAnimatorStateInfo(0).IsName("Fade") &&
            fadeEffect.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                Debug.Log("Playing Animation");
            else
                SceneManager.LoadScene(gameScene.name);
        }
    }

    public void FadeEffect()
    {
        Debug.Log("Fading");

        fade.SetActive(true);
        fadeEffect.enabled = true;
    }
}
