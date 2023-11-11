using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInputValidator playerInputValidator;
    [SerializeField] private PlayerCameraTargetController playerCameraTargetController;
    [SerializeField] private PlayerAbilityController playerAbilityController;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerHealth playerHealth;


    public PlayerInputValidator PlayerInputValidator => playerInputValidator;

    internal void Init()
    {
        playerHealth.Init();
        playerAbilityController.Subscribe(this);
        playerMovement.Subscribe(this);
        PlayerInputValidator.Init(playerCamera);
        playerCameraTargetController.Subscribe(this);
    }
    internal void UnSubscribe()
    {
        playerCameraTargetController.UnSubscribe(this);
        playerAbilityController.UnSubscribe(this);
        playerMovement.UnSubscribe(this);
    }

    internal void DoFixedUpdate()
    {
        playerMovement.DoFixedUpdate();
    }

    internal void DoUpdate()
    {
        playerInputValidator.DoUpdate();
    }
}
