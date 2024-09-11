using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    public GameObject projectilePrefab;    // The projectile prefab
    public float shootingInterval = 2f;    // Time interval between shots
    public Transform shootingPoint;        // The point from where the projectile is shot
    public float detectionRange = 20f;     // Range within which the plant detects skeletons or shields
    public int health = 5;                 // Health of the plant
    public LayerMask detectionLayer;       // Layer for both skeleton and shield detection
    public AudioClip shootSound;           // Sound to play when shooting
    public AudioClip hurtSound;            // Sound to play when the plant takes damage
    public AudioClip deathSound;           // Sound to play when the plant dies

    public Color damageColor = Color.red;  // Color to flash when damaged
    public float flashDuration = 0.1f;     // Duration of the flash effect

    private Coroutine shootingCoroutine;
    private AudioSource audioSource;       // AudioSource component for playing sounds
    private SpriteRenderer spriteRenderer; // SpriteRenderer for changing color
    private Color originalColor;           // To store the original color of the plant

    void Start()
    {
        shootingCoroutine = StartCoroutine(ShootAtTargets());
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get SpriteRenderer component
        originalColor = spriteRenderer.color; // Store the original color
    }

    private IEnumerator ShootAtTargets()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootingInterval);

            if (IsTargetInLane())
            {
                Shoot();
            }
        }
    }

    private bool IsTargetInLane()
    {
        RaycastHit2D hit = Physics2D.Raycast(shootingPoint.position, Vector2.right, detectionRange, detectionLayer);
        Debug.DrawRay(shootingPoint.position, Vector2.right * detectionRange, Color.red, 1f);

        if (hit.collider != null)
        {
            // Check if the hit object is either a Skeleton or Shield
            if (hit.collider.CompareTag("Skeleton") || hit.collider.CompareTag("Shield"))
            {
                Debug.Log("Hit: " + hit.collider.name);
                return true;
            }
        }
        return false;
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
        Debug.Log("Shoot");

        // Play shoot sound if assigned
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
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
            StartCoroutine(DieAfterDelay(0.5f)); // Start the coroutine to handle delayed death
        }
    }

    // Coroutine to flash the plant when it takes damage
    private IEnumerator FlashOnDamage()
    {
        // Flash effect
        spriteRenderer.color = damageColor; // Change to damage color
        yield return new WaitForSeconds(flashDuration); // Wait for the flash duration
        spriteRenderer.color = originalColor; // Revert back to original color
    }

    private IEnumerator DieAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Die();
    }

    private void Die()
    {
        // Play death sound if assigned
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Optionally play a death animation or sound here
        Destroy(gameObject);  // Destroy the plant object
    }

    void OnDestroy()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
        }
    }
}
