using System.Collections;
using UnityEngine;

public class Rose : MonoBehaviour
{
    public GameObject rainPrefab;           // The Rain prefab to be generated
    public int health = 5;                  // Health of the Rose
    public float rainInterval = 5f;         // Fixed time interval for generating rain
    public float spawnRadius = 0.65f;       // Radius around the Rose to spawn rain
    public AudioClip rainSound;             // Optional sound to play when generating rain
    public AudioClip hurtSound;             // Sound to play when the plant takes damage
    public AudioClip deathSound;            // Sound to play when the plant dies
    public Color damageColor = Color.red;   // Color to flash when damaged
    public float flashDuration = 0.1f;      // Duration of the flash effect

    private AudioSource audioSource;        // AudioSource component for playing sounds
    private SpriteRenderer spriteRenderer;  // SpriteRenderer for changing color
    private Color originalColor;            // To store the original color of the Rose

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // Get AudioSource component
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get SpriteRenderer component

        // Store the original color of the Rose
        originalColor = spriteRenderer.color;

        // Start the coroutine to generate rain at fixed intervals
        StartCoroutine(GenerateRain());
    }

    private IEnumerator GenerateRain()
    {
        while (health > 0)  // Continue generating rain while the Rose is alive
        {
            yield return new WaitForSeconds(rainInterval);

            // Get a random position around the Rose within the spawnRadius
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

            // Instantiate the rain prefab at the random position
            Instantiate(rainPrefab, spawnPosition, Quaternion.identity);

            // Play the rain sound if assigned
            if (audioSource != null && rainSound != null)
            {
                audioSource.PlayOneShot(rainSound);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Play hurt sound if assigned
        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        // Trigger the flash effect on damage
        StartCoroutine(FlashOnDamage());

        if (health <= 0)
        {
            StartCoroutine(DieAfterDelay(0.5f));  // Start the coroutine to handle delayed death
        }
    }

    // Coroutine to handle the flashing effect when the Rose takes damage
    private IEnumerator FlashOnDamage()
    {
        if (spriteRenderer != null)
        {
            // Change to the damage color
            spriteRenderer.color = damageColor;

            // Wait for the flash duration
            yield return new WaitForSeconds(flashDuration);

            // Revert back to the original color
            spriteRenderer.color = originalColor;
        }
    }

    private IEnumerator DieAfterDelay(float delay)
    {
        // Flash effect before dying
        yield return FlashOnDamage();

        // Wait for the delay duration
        yield return new WaitForSeconds(delay);

        // Call the Die method
        Die();
    }

    private void Die()
    {
        // Play death sound if assigned
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Optionally play a death animation or additional effects here
        Destroy(gameObject);  // Destroy the plant when it dies
    }
}
