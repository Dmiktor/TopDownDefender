using System.Collections;
using UnityEngine;

public class PlayerAbility:ScriptableObject
{
    [SerializeField] protected string abilityName;
    [SerializeField] protected Sprite abilityIcon;
    [SerializeField] protected PlayerController player;
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected float duration;

    protected AbilityState currentAbilityState;

    protected Coroutine castingCoroutine;
    protected Coroutine coolDownCoroutine;

    public virtual void Init(PlayerController playerController)
    {
        player = playerController;
    }
    public virtual void StartAbilityCasting()
    {

    }

    public virtual void CancelAbilityCasting()
    {

    }
    public virtual void ExitAbility()
    {
        if (castingCoroutine != null)
        {
            player.StopCoroutine(castingCoroutine);
        }
        if (coolDownCoroutine != null)
        {
            player.StopCoroutine(coolDownCoroutine);
        }
    }

    public virtual IEnumerator AbilityCastings()
    {
        yield return null;

    }

    public virtual IEnumerator AbilityCooldown()
    {
        yield return null;
    }
}

public enum AbilityState
{
    ReadyToUse,
    InUse,
    InCooldown,
}

