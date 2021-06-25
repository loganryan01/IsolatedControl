using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public bool toServers = false;
    public bool toDeskFromServers = false;
    public bool toPowerBox = false;
    public bool toDeskFromPowerBox = false;

    bool audioIsPlaying = false;
    Animator animator;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toServers)
        {
            animator.SetBool("GoToServers", true);
        }

        if (toDeskFromServers)
        {
            animator.SetBool("GoToDeskFromServers", true);
        }

        if (toPowerBox)
        {
            animator.SetBool("GoToPowerBox", true);
        }

        if (toDeskFromPowerBox)
        {
            animator.SetBool("GoToDeskFromPowerBox", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ToServers") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && !audioIsPlaying ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("FromServerToDesk") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && !audioIsPlaying ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("ToPowerBox") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && !audioIsPlaying ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("FromPowerBoxToDesk") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && !audioIsPlaying)
        {
            audioIsPlaying = true;
            audioSource.Play();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("ToServers") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && audioIsPlaying ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("FromServerToDesk") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && audioIsPlaying ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("ToPowerBox") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && audioIsPlaying ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("FromPowerBoxToDesk") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && audioIsPlaying)
        {
            audioIsPlaying = false;
            audioSource.Stop();
        }
    }
}
