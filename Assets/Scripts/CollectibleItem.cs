using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public float speed = 10f;
    public int scoreValue = 10;
    public float lifetime = 10f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move forward locally
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (MiniGameManager.Instance != null)
            {
                MiniGameManager.Instance.AddScore(scoreValue);
            }
            Destroy(gameObject);
        }
    }
}
