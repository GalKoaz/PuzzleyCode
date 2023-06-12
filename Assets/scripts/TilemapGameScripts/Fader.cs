using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fader is a MonoBehaviour class that manages screen fading
public class Fader : MonoBehaviour
{
    // Singleton instance of the Fader class
    public static Fader instance;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Assign the singleton instance
        instance = this;
    }

    // Reference to the Animator component
    public Animator animator;

    // FadeOut method triggers the "FadeOut" animation
    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    // FadeIn method starts the FadeInCo coroutine
    public void FadeIn()
    {
        StartCoroutine(FadeInCo());
    }

    // FadeInCo coroutine manages the FadeIn animation process
    IEnumerator FadeInCo()
    {
        // Set the "FadeIn" animator parameter to true
        animator.SetBool("FadeIn", true);

        // Wait for 0.6 seconds before continuing
        yield return new WaitForSeconds(.6f);

        // Set the "FadeIn" animator parameter back to false
        animator.SetBool("FadeIn", false);
    }
}