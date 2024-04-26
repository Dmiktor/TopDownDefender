using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private float speed;
    [SerializeField] private float sprintingModifier;

    private bool isBlocked;
    private bool isSprinting;
    private Vector2 movementDirection;

    private float Speed
    {
        get
        {
            if (isSprinting)
            {
                return speed * sprintingModifier;
            }
            else
            {
                return speed;
            }

        }
        set { speed = value; }
    }

    public bool IsBlocked { get => isBlocked; set => isBlocked = value; }
    public Rigidbody2D PlayerRigidBody => playerRigidBody;

    public void Subscribe(PlayerController playerController)
    {
        playerController.PlayerInputValidator.OnMove += SetMovingVector;
        playerController.PlayerInputValidator.OnSprint += SetSprint;
    }

    public void UnSubscribe(PlayerController playerController)
    {
        playerController.PlayerInputValidator.OnMove -= SetMovingVector;
        playerController.PlayerInputValidator.OnSprint -= SetSprint;
    }

    internal void DoFixedUpdate()
    {
        if (!IsBlocked)
        {
            Move();
        }
    }
    private void Move()
    {
        PlayerRigidBody.velocity = movementDirection * Speed;
    }

    private void SetMovingVector(Vector2 movementDirection)
    {
        this.movementDirection = movementDirection;
    }

    private void SetSprint(bool obj)
    {
        isSprinting = obj;
    }
}
