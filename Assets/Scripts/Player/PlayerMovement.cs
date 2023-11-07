using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    private bool isBlocked;
    private bool canDash = true;
    private Vector2 movementDirection;

    public void Subscribe(PlayerController playerController)
    {
        playerController.PlayerInputValidator.OnDash += TryDash;
        playerController.PlayerInputValidator.OnMove += SetMovingVector;
    }

    public void UnSubscribe(PlayerController playerController)
    {
        playerController.PlayerInputValidator.OnDash -= TryDash;
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
        StartCoroutine(StarDashCooldown());
        playerRigidBody.velocity = movementDirection * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        isBlocked = false;
    }
    private IEnumerator StarDashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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
