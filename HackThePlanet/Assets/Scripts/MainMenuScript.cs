using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject fade;
    public Animator fadeEffect;

    //public Object gameScene; //This is not necessary while line 22 is commented out (Original Line)

    // Update is called once per frame
    void Update()
    {
        if (fadeEffect.enabled)
        {
            if (fadeEffect.GetCurrentAnimatorStateInfo(0).IsName("Fade") &&
            fadeEffect.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                Debug.Log("Playing Animation");
            else
                //SceneManager.LoadScene(gameScene.name); //Edited this out to see if it makes a difference (Original Line)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //This is a new line to test if i can transition to the next scene during build
        }
    }

    public void FadeEffect()
    {
        fade.SetActive(true);
        fadeEffect.enabled = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
