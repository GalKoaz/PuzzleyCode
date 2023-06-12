using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // SerializeField to set the delay before transitioning to the next scene
    [SerializeField] public float delay = 10f;

    private void Start()
    {
        // Start the Behaviour coroutine when the scene starts
        StartCoroutine(Behaviour());
    }

    // Coroutine to handle the transition between scenes
    IEnumerator Behaviour()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Call the FadeOut method from the Fader instance
        Fader.instance.FadeOut();

        // Wait for 0.6 seconds after fading out
        yield return new WaitForSeconds(.6f);

        // Load the next scene in the build order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}