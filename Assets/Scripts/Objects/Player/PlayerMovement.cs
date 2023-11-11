using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private Image staminaCooldown;
    [SerializeField] private Ease staminaCooldownEase;

    private bool isBlocked;
    private bool canDash = true;
    private Vector2 movementDirection;

    public void Subscribe(PlayerController playerController)
    {
        playerController.PlayerInputValidator.OnMove += SetMovingVector;
    }

    public void UnSubscribe(PlayerController playerController)
    {
        playerController.PlayerInputValidator.OnMove -= SetMovingVector;
    }

    private void TryDash()
    {
        if (canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        isBlocked = true;
        canDash = false;
        StarDashCooldown();
        playerRigidBody.velocity = movementDirection * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        isBlocked = false;
    }
    private void StarDashCooldown()
    {
        canDash = false;
        staminaCooldown.fillAmount = 0f;
        staminaCooldown.gameObject.SetActive(true);
        staminaCooldown.DOFillAmount(1, dashCooldown).SetEase(staminaCooldownEase).OnComplete(() =>
        {
            staminaCooldown.gameObject.SetActive(false);
            canDash = true;
        });
    }

    private void SetMovingVector(Vector2 movementDirection)
    {
        this.movementDirection = movementDirection;
    }

    internal void DoFixedUpdate()
    {
        if (!isBlocked)
        {
            Move();
        }
    }

    private void Move()
    {
        playerRigidBody.velocity = movementDirection * speed;
    }
}
