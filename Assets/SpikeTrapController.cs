using NaughtyAttributes;
using UnityEngine;

public class SpikeTrapController : MonoBehaviour
{
    [Layer]
    [SerializeField] private int damage;
    [SerializeField] private bool active;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent<IDamageable>(out IDamageable target)) 
        {
            target.TakeDamage(damage);
        }
    }
}
