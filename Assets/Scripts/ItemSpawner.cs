using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform playerTransform;
    public float spawnInterval = 2f;
    public float spawnDistance = 20f;
    
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnItem();
            timer = 0f;
        }
    }

    void SpawnItem()
    {
        if (itemPrefab == null || playerTransform == null) return;

        // Random angle around the player
        float angle = Random.Range(0f, 360f);
        Vector3 spawnDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
        
        // Final spawn position
        Vector3 spawnPosition = playerTransform.position + spawnDirection * spawnDistance;
        spawnPosition.y = Random.Range(1f, 3f); // Random height

        GameObject item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        
        // Aim at player
        item.transform.LookAt(playerTransform.position + Vector3.up); // Look slightly up/center
        
        // Optional: ensure CollectibleItem component exists
        CollectibleItem collectible = item.GetComponent<CollectibleItem>();
        if (collectible != null)
        {
             // Logic already handled in CollectibleItem Update
        }
    }
}
