using UnityEngine;
using System.Collections;

public class SkeletonManager : MonoBehaviour
{
    [System.Serializable]
    public class SkeletonType
    {
        public GameObject skeletonPrefab; // The skeleton prefab
        public float spawnProbability;    // Probability of spawning this type of skeleton
    }

    public SkeletonType[] skeletonTypes; // Array of different skeleton types
    public Transform[] spawnPoints;      // Array of spawn points for each lane
    public float initialSpawnInterval = 10f;  // Initial time interval between spawns
    public float finalSpawnInterval = 0.5f;    // Final time interval between spawns
    public float spawnAccelerationDuration = 120f;  // Duration over which the spawn interval decreases

    private float currentSpawnInterval;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnSkeletons());
        StartCoroutine(AccelerateSpawnRate());
    }

    private IEnumerator SpawnSkeletons()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnInterval);

            // Choose a random spawn point (lane)
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Choose a random skeleton type based on their probabilities
            GameObject skeletonToSpawn = ChooseSkeletonType();

            if (skeletonToSpawn != null)
            {
                Instantiate(skeletonToSpawn, spawnPoint.position, Quaternion.identity);
            }
        }
    }

    private GameObject ChooseSkeletonType()
    {
        float totalProbability = 0f;

        // Calculate total probability of all skeleton types
        foreach (var skeleton in skeletonTypes)
        {
            totalProbability += skeleton.spawnProbability;
        }

        // Generate a random value between 0 and the total probability
        float randomValue = Random.Range(0f, totalProbability);

        // Determine which skeleton type to spawn based on the random value
        float cumulativeProbability = 0f;
        foreach (var skeleton in skeletonTypes)
        {
            cumulativeProbability += skeleton.spawnProbability;
            if (randomValue <= cumulativeProbability)
            {
                return skeleton.skeletonPrefab;
            }
        }

        return null;
    }

    private IEnumerator AccelerateSpawnRate()
    {
        float elapsedTime = 0f;

        while (elapsedTime < spawnAccelerationDuration)
        {
            elapsedTime += Time.deltaTime;
            currentSpawnInterval = Mathf.Lerp(initialSpawnInterval, finalSpawnInterval, elapsedTime / spawnAccelerationDuration);
            yield return null;
        }

        // Ensure the spawn interval reaches the final value
        currentSpawnInterval = finalSpawnInterval;
    }
}
