using System.Collections;
using UnityEngine;

public class PlayerHealth : SimpleHealth
{
    [SerializeField] private int healthRegeneratingRate;
    [SerializeField] private int healthRegeneratingTime;

    private Coroutine healthRegenCoroutine;
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        ResetCoroutine();
        if (currentHitPoints > 0)
        {
            healthRegenCoroutine = StartCoroutine(HealthRegeneratingCoroutine());
        }
    }

    protected override void Die()
    {
        ResetCoroutine();
        base.Die();
    }

    private void ResetCoroutine()
    {
        if (healthRegenCoroutine != null)
        {
            StopCoroutine(healthRegenCoroutine);
            healthRegenCoroutine = null;
        }
    }

    private IEnumerator HealthRegeneratingCoroutine()
    {
        while (currentHitPoints < maxHitPoints)
        {
            yield return new WaitForSeconds(healthRegeneratingTime);
            currentHitPoints = Mathf.Min(currentHitPoints + healthRegeneratingRate, maxHitPoints);
            base.UpdateHealthBar();
        }
        ResetCoroutine();
    }
}
