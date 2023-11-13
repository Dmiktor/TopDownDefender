using System;
using UnityEngine;

public class InRangeTrigger : MonoBehaviour
{
    public event Action<Collider2D> OnTarget;
    public event Action<Collider2D> OnTargetExit;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTarget?.Invoke(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTargetExit?.Invoke(collision);
    }
}
