using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}