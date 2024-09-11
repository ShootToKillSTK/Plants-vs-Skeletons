using UnityEngine;

public class Shield : MonoBehaviour
{
    public int maxHealth = 10;              // Maximum health of the shield
    public int currentHealth;               // Current health of the shield
    public Sprite fullHealthSprite;         // The shield sprite at full health
    public Sprite lowHealthSprite;          // The shield sprite when health is low
    public int lowHealthThreshold = 3;      // Health value at which the shield will switch to low health sprite
    public AudioClip shieldHitSound;        // Sound to play when the shield is hit

    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer component
    private AudioSource audioSource;        // Reference to the AudioSource component

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        UpdateShieldSprite();  // Set the initial sprite
    }

    // Method to take damage from plants
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateShieldSprite();

        // Play the shield hit sound
        if (audioSource != null && shieldHitSound != null)
        {
            audioSource.PlayOneShot(shieldHitSound);
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);  // Destroy the shield if health is 0
        }
    }

    // Method to update the sprite based on the shield's health
    private void UpdateShieldSprite()
    {
        if (currentHealth <= lowHealthThreshold)
        {
            spriteRenderer.sprite = lowHealthSprite;  // Change to broken shield sprite
        }
        else
        {
            spriteRenderer.sprite = fullHealthSprite;  // Use the full health sprite
        }
    }
}
