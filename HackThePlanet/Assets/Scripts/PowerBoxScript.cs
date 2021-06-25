using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBoxScript : MonoBehaviour
{
    [SerializeField]
    int overallNumber;

    int currentNumber;

    [SerializeField]
    int[] switchValues;

    [SerializeField]
    GameObject[] switches;
    
    Animator animator;

    bool resetting = false;

    [HideInInspector]
    public bool puzzleCompleted;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);

            if (hit.collider.gameObject.CompareTag("Switch"))
            {
                animator = hit.collider.gameObject.GetComponent<Animator>();

                if (!animator.GetBool("Activated"))
                {
                    animator.SetBool("Activated", true);
                    
                    // Check what switch has been touched
                    for (int i = 0; i < switches.Length; i++)
                    {
                        if (hit.collider.gameObject.name == switches[i].name)
                        {
                            currentNumber += switchValues[i];
                        }
                    }
                }
            }
        }

        // If no animations are playing and the player's current number is greater than the overall number
        if (!resetting && !AnimationIsPlaying() && currentNumber > overallNumber)
        {
            StartCoroutine(ResetSwitches());
        }

        if (currentNumber == overallNumber)
        {
            puzzleCompleted = true;
        }
    }

    // Check if an animation is playing
    bool AnimationIsPlaying()
    {
        Animator[] switchAnimators = new Animator[switches.Length];

        for (int i = 0; i < switchAnimators.Length; i++)
        {
            switchAnimators[i] = switches[i].GetComponent<Animator>();

            if (switchAnimators[i].GetCurrentAnimatorStateInfo(0).normalizedTime < 1 &&
                switchAnimators[i].GetCurrentAnimatorStateInfo(0).IsName("Switch"))
            {
                return true;
            }
        }

        return false;
    }

    // Return the switches to their original positions
    IEnumerator ResetSwitches()
    {
        resetting = true;
        
        Animator[] switchAnimators = new Animator[switches.Length];

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < switchAnimators.Length; i++)
        {
            switchAnimators[i] = switches[i].GetComponent<Animator>();
            switchAnimators[i].SetBool("Activated", false);
        }

        currentNumber = 0;

        resetting = false;
    }
}
