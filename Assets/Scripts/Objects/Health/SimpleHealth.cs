using System;
using UnityEngine;

public class SimpleHealth : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHitPoints;
    [SerializeField] private UiBar uiBar;
    protected int currentHitPoints;

    public event Action OnDeath;

    public virtual void Init()
    {
        currentHitPoints = maxHitPoints;
        UpdateHealthBar();
    }
    public virtual void TakeDamage(int damageAmount)
    {
        currentHitPoints = Mathf.Max(0, currentHitPoints - damageAmount);
       
        UpdateHealthBar();
        
        if (currentHitPoints == 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();
    }

    protected virtual void UpdateHealthBar()
    {
        uiBar.UpdateBar(currentHitPoints, maxHitPoints);
    }
}
