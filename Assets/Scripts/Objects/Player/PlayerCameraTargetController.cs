using System;
using UnityEngine;

public class PlayerCameraTargetController : MonoBehaviour
{
    [SerializeField] private float threshold;
    private Transform playerTransform;

    internal void Subscribe(PlayerController playerController)
    {
        playerTransform = playerController.transform;
        playerController.PlayerInputValidator.OnMousePosition += MoveToTargetPosition;
    }


    internal void UnSubscribe(PlayerController playerController)
    {
        playerController.PlayerInputValidator.OnMousePosition -= MoveToTargetPosition;

    }
    private void MoveToTargetPosition(Vector2 mousePosition)
    {
        SetCursorToMousePosition(mousePosition);

    }

    private void SetCursorToMousePosition(Vector2 mousePosition)
    {
        Vector3 targetPosition = playerTransform.position + new Vector3(mousePosition.x, mousePosition.y, 0) / 2f;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -threshold + playerTransform.position.x, threshold + playerTransform.position.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -threshold + playerTransform.position.y, threshold + playerTransform.position.y);

        transform.position = targetPosition;
    }
}
