using System;
using UnityEditor.Overlays;
using UnityEngine;

public class PlayerLookTarget : MonoBehaviour
{
    Vector2 lookPosition;
    internal void Subscribe(PlayerController playerController)
    {
        playerController.PlayerInputValidator.OnMousePosition += SetLookPosition;
    }

    internal void UnSubscribe(PlayerController playerController)
    {
        playerController.PlayerInputValidator.OnMousePosition -= SetLookPosition;
    }

    private void SetLookPosition(Vector2 lookPosition)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookPosition - (Vector2)transform.position);
    }
}
