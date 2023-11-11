using System;

public interface IDamageable
{
    public void TakeDamage(int damageAmount);
    public event Action OnDeath;
}
