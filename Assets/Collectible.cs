using UnityEngine;
public class Collectible : MonoBehaviour
{
    [SerializeField] private int value = 10;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.AddScore(value);
            }
            Destroy(gameObject);
        }
    }
}