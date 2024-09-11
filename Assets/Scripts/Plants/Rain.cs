using UnityEngine;

public class Rain : MonoBehaviour
{
    public int rainValue = 25;           // Value of the rain
    public AudioClip rainAudioClip;      // Assign the audio clip in the inspector
    public float speed = 5f;             // Speed of the rain drop

    void Update()
    {
        // Move the rain object downwards
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Destroy the rain object if it moves below the screen
        if (transform.position.y < -5f)  // Adjust this threshold based on your game area
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        // Add rain value to the RainManager
        RainManager.instance.AddRain(rainValue);

        // Play the audio and destroy the rain object
        PlayAudioAndDestroy();
    }

    private void PlayAudioAndDestroy()
    {
        // Create an empty game object to play the audio
        GameObject audioPlayer = new GameObject("RainAudioPlayer");
        AudioSource audioSource = audioPlayer.AddComponent<AudioSource>();

        // Assign the audio clip
        audioSource.clip = rainAudioClip;
        audioSource.Play();

        // Destroy the audio player after the audio has finished playing
        Destroy(audioPlayer, rainAudioClip.length);

        // Destroy the rain object immediately
        Destroy(gameObject);
    }
}
