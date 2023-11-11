using System.Collections;
using UnityEngine;

public class PlayerAbility
{
    [SerializeField] private string abilityName;
    [SerializeField] private Sprite abilityIcon;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float duration;


    public virtual void StartAbilityCasting(PlayerController playerController)
    {

    }

    public virtual void CancelAbilityCosting()
    {

    }
    public virtual void ExitAbility()
    {

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

