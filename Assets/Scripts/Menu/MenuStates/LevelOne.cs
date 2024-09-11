using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOne : BaseMenu
{
    public AudioClip backgroundMusic; // Music clip to play in this state
    private AudioSource audioSource;   // AudioSource component for playing the music

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.LevelOne;
    }

    public override void EnterState()
    {
        base.EnterState();

        // Check if the AudioSource component is set up
        if (audioSource == null)
        {
            // Try to find an AudioSource component in the scene
            audioSource = FindObjectOfType<AudioSource>();

            if (audioSource == null)
            {
                // If no AudioSource is found, create a new GameObject with an AudioSource
                GameObject audioObject = new GameObject("BackgroundMusic");
                audioSource = audioObject.AddComponent<AudioSource>();
            }
        }

        // Play the background music
        if (audioSource != null && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true; // Set the music to loop
            audioSource.Play();     // Start playing the music
        }
    }

    public override void ExitState()
    {
        base.ExitState();

        // Optionally, stop the music when exiting the state
        if (audioSource != null)
        {
            audioSource.Stop(); // Stop playing the music
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
