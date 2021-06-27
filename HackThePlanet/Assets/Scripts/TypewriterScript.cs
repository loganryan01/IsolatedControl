using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TypewriterScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string text;
    public float timeLapse;

    private bool completeText = false;

    public GameObject continueButton;
    //public Object gameScene; //Testing (Original Line)

    void Start()
    {
        StartCoroutine(BuildText());
    }

    private void Update()
    {
        if (completeText)
        {
            continueButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        //SceneManager.LoadScene(gameScene.name); //Testing (Original Line)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator BuildText()
    {
        yield return new WaitForSeconds(3.0f);
        
        for (int i = 0; i < text.Length; i++)
        {
            textComponent.text = string.Concat(textComponent.text, text[i]);
            //Wait a certain amount of time, then continue with the for loop
            yield return new WaitForSeconds(timeLapse);
        }

        completeText = true;
    }
}
