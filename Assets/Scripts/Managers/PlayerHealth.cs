using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5; // Set the maximum health
    public int currentHealth;
    public Text healthText; // Reference to the UI Text element for health

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            // Save the score before loading the game over scene
            ScoreManager.instance.SaveScore();
            // Start a coroutine to delay loading the scene
            StartCoroutine(LoadGameOverScene());
        }
    }

    private IEnumerator LoadGameOverScene()
    {
        // Wait for 1 second before loading the "Game Over" scene
        yield return new WaitForSeconds(1f);

        // Load the "Game Over" scene
        SceneManager.LoadScene("Game Over");
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }
}
