using UnityEngine;
using System.Collections;

public class Skeleton : MonoBehaviour
{
    public float speed = 1f;                  // Speed at which the skeleton moves
    public int health = 3;                   // Health of the skeleton
    public int damage = 1;                   // Damage dealt to the plant or rose
    public int playerDamage = 1;             // Damage dealt to the player
    public float attackInterval = 1f;       // Time interval between attacks
    public AudioClip attackSound;           // Sound clip for attack
    public AudioClip deathSound;            // Sound clip for death
    public int scoreValue = 50;             // Score value when the skeleton is killed

    public Color damageColor = Color.red;   // Color to flash when damaged
    public float flashDuration = 0.1f;      // Duration of the flash effect

    private Animator animator;
    private Rigidbody2D rb;
    private bool isAttacking = false;
    private bool isDead = false;            // Flag to indicate if the skeleton is dead
    private bool stopMovement = false;      // Flag to stop movement
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;  // SpriteRenderer for changing color
    private Color originalColor;            // To store the original color of the skeleton

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get SpriteRenderer component

        // Store the original color
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        // Trigger the move animation
        if (animator != null)
        {
            animator.SetTrigger("MoveTrigger");
        }
    }

    void FixedUpdate()
    {
        if (!isAttacking && !isDead && !stopMovement)
        {
            // Move the skeleton to the left
            Vector2 position = rb.position;
            position.x -= speed * Time.fixedDeltaTime;
            rb.MovePosition(position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant") || collision.CompareTag("Rose"))  // Check for either "Plant" or "Rose"
        {
            isAttacking = true;
            if (animator != null)
            {
                animator.SetTrigger("AttackTrigger");
            }
            StartCoroutine(AttackPlantOrRose(collision.gameObject));
        }
        else if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(playerDamage);
                StartCoroutine(AttackPlayerAndDie());
            }
        }
    }

    private IEnumerator AttackPlantOrRose(GameObject plantOrRose)
    {
        Plant plantComponent = plantOrRose.GetComponent<Plant>();
        Rose roseComponent = plantOrRose.GetComponent<Rose>();

        // If it's a Plant, attack the plant
        if (plantComponent != null)
        {
            while (plantComponent.health > 0)
            {
                if (audioSource != null && attackSound != null)
                {
                    audioSource.PlayOneShot(attackSound);
                }
                plantComponent.TakeDamage(damage);
                if (isDead)
                {
                    yield break;
                }
                if (animator != null)
                {
                    animator.ResetTrigger("AttackTrigger");
                    animator.SetTrigger("AttackTrigger");
                }
                yield return new WaitForSeconds(attackInterval);
            }
        }
        // If it's a Rose, attack the rose
        else if (roseComponent != null)
        {
            while (roseComponent.health > 0)
            {
                if (audioSource != null && attackSound != null)
                {
                    audioSource.PlayOneShot(attackSound);
                }
                roseComponent.TakeDamage(damage);
                if (isDead)
                {
                    yield break;
                }
                if (animator != null)
                {
                    animator.ResetTrigger("AttackTrigger");
                    animator.SetTrigger("AttackTrigger");
                }
                yield return new WaitForSeconds(attackInterval);
            }
        }

        isAttacking = false;
        if (animator != null && !isDead)
        {
            animator.SetTrigger("MoveTrigger");
        }
    }

    private IEnumerator AttackPlayerAndDie()
    {
        stopMovement = true; // Stop the movement
        audioSource.PlayOneShot(attackSound);

        if (animator != null)
        {
            animator.SetTrigger("AttackTrigger");
        }
        // Wait for 1 second
        yield return new WaitForSeconds(1f);
        // Destroy the skeleton object
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Trigger the flash effect on damage
        StartCoroutine(FlashOnDamage());

        if (health <= 0 && !isDead)
        {
            StartCoroutine(Die());
        }
    }

    // Coroutine to flash the skeleton when it takes damage
    private IEnumerator FlashOnDamage()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = damageColor; // Change to damage color
            yield return new WaitForSeconds(flashDuration); // Wait for the flash duration
            spriteRenderer.color = originalColor; // Revert back to original color
        }
    }

    private IEnumerator Die()
    {
        isDead = true; // Set the flag to indicate the skeleton is dead

        // Play death sound
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Trigger the death animation
        if (animator != null)
        {
            animator.SetTrigger("DeathTrigger");
        }

        // Wait for 0.75 seconds
        yield return new WaitForSeconds(0.75f);

        // Add score if the skeleton was killed by a plant
        ScoreManager.instance.AddScore(scoreValue);

        // Destroy the skeleton object
        Destroy(gameObject);
    }
}
