using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RainPrefab
{
    public GameObject prefab;    // The rain prefab
    public float weight = 1f;    // The weight for this prefab (higher values = more likely to spawn)
}

public class RainManager : MonoBehaviour
{
    public static RainManager instance;

    public List<RainPrefab> rainPrefabs;  // List of rain prefabs with weights
    public float minSpawnInterval = 0.5f;   // Minimum interval between rain spawns
    public float maxSpawnInterval = 3f;   // Maximum interval between rain spawns

    public Text rainText;
    private int rainPoints = 0;

    private Coroutine spawnCoroutine;    // Coroutine reference for spawning rain

    void Awake()
    {
        // Ensure there is only one instance of RainManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateRainText();
        spawnCoroutine = StartCoroutine(SpawnRainRoutine());
    }

    IEnumerator SpawnRainRoutine()
    {
        while (true)
        {
            // Wait for a random interval between min and max spawn interval
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);

            // Choose a random prefab based on weight
            GameObject selectedRainPrefab = GetRandomWeightedPrefab();

            // Spawn at a random horizontal position
            float xPos = Random.Range(-9f, 9f); // Adjust range according to your game area
            Vector3 spawnPosition = new Vector3(xPos, 143f, 0); // Adjust y position if needed
            Instantiate(selectedRainPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Method to select a rain prefab based on weight
    private GameObject GetRandomWeightedPrefab()
    {
        float totalWeight = 0f;
        foreach (RainPrefab rainPrefab in rainPrefabs)
        {
            totalWeight += rainPrefab.weight;
        }

        float randomWeight = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        foreach (RainPrefab rainPrefab in rainPrefabs)
        {
            currentWeight += rainPrefab.weight;
            if (randomWeight <= currentWeight)
            {
                return rainPrefab.prefab;
            }
        }

        // Fallback, should never happen
        return rainPrefabs[0].prefab;
    }

    private void UpdateRainText()
    {
        rainText.text = "" + rainPoints;
    }

    public void AddRain(int amount)
    {
        rainPoints += amount;
        UpdateRainText();
    }

    public bool SpendRain(int amount)
    {
        if (rainPoints >= amount)
        {
            rainPoints -= amount;
            UpdateRainText();
            return true;
        }
        return false;
    }

    void OnDestroy()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }
}
